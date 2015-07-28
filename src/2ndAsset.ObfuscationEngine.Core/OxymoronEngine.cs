/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Solder.Framework;
using Solder.Framework.Serialization;
using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Hosting;
using _2ndAsset.ObfuscationEngine.Core.Strategy;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public sealed class OxymoronEngine : IOxymoronEngine
	{
		#region Constructors/Destructors

		public OxymoronEngine(IOxymoronHost oxymoronHost, ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)oxymoronHost == null)
				throw new ArgumentNullException("oxymoronHost");

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			this.oxymoronHost = oxymoronHost;
			this.obfuscationConfiguration = obfuscationConfiguration;

			EnsureValidConfigurationOnce(this.ObfuscationConfiguration);
		}

		#endregion

		#region Fields/Constants

		private readonly IDictionary<int, ColumnConfiguration> columnCache = new Dictionary<int, ColumnConfiguration>();
		private readonly ObfuscationConfiguration obfuscationConfiguration;
		private readonly IDictionary<string, IObfuscationStrategy> obfuscationStrategyCache = new Dictionary<string, IObfuscationStrategy>();
		private readonly IOxymoronHost oxymoronHost;
		private readonly IDictionary<string, IDictionary<long, object>> substitutionCacheRoot = new Dictionary<string, IDictionary<long, object>>();
		private bool disposed;

		#endregion

		#region Properties/Indexers/Events

		private IDictionary<int, ColumnConfiguration> ColumnCache
		{
			get
			{
				return this.columnCache;
			}
		}

		public ObfuscationConfiguration ObfuscationConfiguration
		{
			get
			{
				return this.obfuscationConfiguration;
			}
		}

		private IDictionary<string, IObfuscationStrategy> ObfuscationStrategyCache
		{
			get
			{
				return this.obfuscationStrategyCache;
			}
		}

		public IOxymoronHost OxymoronHost
		{
			get
			{
				return this.oxymoronHost;
			}
		}

		public IDictionary<string, IDictionary<long, object>> SubstitutionCacheRoot
		{
			get
			{
				return this.substitutionCacheRoot;
			}
		}

		public bool Disposed
		{
			get
			{
				return this.disposed;
			}
			private set
			{
				this.disposed = value;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// NOTE: Calling this more than once kills performance.
		/// </summary>
		/// <param name="configurationObject"> </param>
		private static void EnsureValidConfigurationOnce(ConfigurationObject configurationObject)
		{
			IEnumerable<Message> messages;

			if ((object)configurationObject == null)
				throw new ArgumentNullException("configurationObject");

			messages = configurationObject.Validate();

			if (messages.Any())
				throw new InvalidOperationException(string.Format("Obfuscation configuration validation failed:\r\n{0}", string.Join("\r\n", messages.Select(m => m.Description).ToArray())));
		}

		public static TConfiguration FromJsonFile<TConfiguration>(string jsonFilePath)
			where TConfiguration : class, IConfigurationObject, new()
		{
			TConfiguration configuration;

			configuration = JsonSerializationStrategy.Instance.GetObjectFromFile<TConfiguration>(jsonFilePath);

			return configuration;
		}

		public static void ToJsonFile<TConfiguration>(TConfiguration configuration, string jsonFilePath)
			where TConfiguration : class, IConfigurationObject, new()
		{
			JsonSerializationStrategy.Instance.SetObjectToFile<TConfiguration>(jsonFilePath, configuration);
		}

		private object _GetObfuscatedValue(IMetaColumn metaColumn, object columnValue)
		{
			IObfuscationStrategy obfuscationStrategy;
			ColumnConfiguration columnConfiguration;
			object obfuscatedValue;

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			// KILLS performance - not sure why
			//EnsureValidConfigurationOnce(this.TableConfiguration);
			//columnConfiguration = this.TableConfiguration.ColumnConfigurations.SingleOrDefault(c => c.ColumnName.SafeToString().Trim().ToLower() == columnName.SafeToString().Trim().ToLower());

			if (!this.ColumnCache.TryGetValue(metaColumn.ColumnIndex, out columnConfiguration))
			{
				columnConfiguration = this.ObfuscationConfiguration.TableConfiguration.ColumnConfigurations.SingleOrDefault(c => c.ColumnName.SafeToString().Trim().ToLower() == metaColumn.ColumnName.SafeToString().Trim().ToLower());
				//columnConfiguration = this.TableConfiguration.ColumnConfigurations.SingleOrDefault(c => c.ColumnName == columnName);
				this.ColumnCache.Add(metaColumn.ColumnIndex, columnConfiguration);
			}

			if ((object)columnConfiguration == null)
				return columnValue; // do nothing when no matching column configuration

			if (!this.ObfuscationStrategyCache.TryGetValue(columnConfiguration.ObfuscationStrategyAqtn, out obfuscationStrategy))
			{
				obfuscationStrategy = columnConfiguration.GetObfuscationStrategyInstance();

				if ((object)obfuscationStrategy == null)
					throw new InvalidOperationException(string.Format("Unknown obfuscation strategy '{0}' specified for column '{1}'.", columnConfiguration.ObfuscationStrategyAqtn, metaColumn.ColumnName));

				this.ObfuscationStrategyCache.Add(columnConfiguration.ObfuscationStrategyAqtn, obfuscationStrategy);
			}

			obfuscatedValue = obfuscationStrategy.GetObfuscatedValue(this, columnConfiguration, metaColumn, columnValue);

			return obfuscatedValue;
		}

		private void CoreDispose(bool disposing)
		{
			if (disposing)
			{
				this.SubstitutionCacheRoot.Clear();
				this.ColumnCache.Clear();
			}
		}

		public void Dispose()
		{
			if (this.Disposed)
				return;

			try
			{
				this.CoreDispose(true);
			}
			finally
			{
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		public long GetBoundedHash(long? size, object value)
		{
			long? hash;

			hash = this.GetHash(this.ObfuscationConfiguration.HashConfiguration.Multiplier,
				size,
				this.ObfuscationConfiguration.HashConfiguration.Seed,
				value.SafeToString());

			if ((object)hash == null)
				throw new InvalidOperationException(string.Format("Oxymoron engine failed to calculate a valid hash for input '{0}'.", value.SafeToString(null, "<null>")));

			return hash.GetValueOrDefault();
		}

		private long? GetHash(long? multiplier, long? size, long? seed, object value)
		{
			const long DEFAULT_HASH = -1L;
			long hashCode;
			byte[] buffer;
			Type valueType;
			string _value;

			if ((object)multiplier == null)
				return null;

			if ((object)size == null)
				return null;

			if ((object)seed == null)
				return null;

			if (size == 0L)
				return null; // prevent DIV0

			if ((object)value == null)
				return null;

			valueType = value.GetType();

			if (valueType != typeof(String))
				return null;

			_value = (String)value;

			if (DataTypeFascade.Instance.IsWhiteSpace(_value))
				return DEFAULT_HASH;

			_value = _value.Trim();

			buffer = Encoding.UTF8.GetBytes(_value);

			hashCode = (long)seed;
			for (int index = 0; index < buffer.Length; index++)
				hashCode = ((long)multiplier * hashCode + buffer[index]) % uint.MaxValue;

			if (hashCode > int.MaxValue)
				hashCode = hashCode - uint.MaxValue;

			if (hashCode < 0)
				hashCode = hashCode + int.MaxValue;

			hashCode = (hashCode % (long)size);

			return (int)hashCode;
		}

		public object GetObfuscatedValue(IMetaColumn metaColumn, object columnValue)
		{
			object value;

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)columnValue == DBNull.Value)
				columnValue = null;

			value = this._GetObfuscatedValue(metaColumn, columnValue);

			return value;
		}

		public IEnumerable<IDictionary<string, object>> GetObfuscatedValues(IEnumerable<IDictionary<string, object>> records)
		{
			int columnIndex;
			string columnName;
			Type columnType;
			object columnValue, obfusscatedValue;
			bool? columnIsNullable = null;

			IDictionary<string, object> obfuscatedRecord;
			IMetaColumn metaColumn;

			if ((object)records == null)
				throw new ArgumentNullException("records");

			foreach (IDictionary<string, object> record in records)
			{
				obfuscatedRecord = new Dictionary<string, object>();

				columnIndex = 0;
				foreach (KeyValuePair<string, object> field in record)
				{
					columnName = field.Key;
					columnValue = record[field.Key];
					columnType = (columnValue ?? new object()).GetType();

					metaColumn = new MetaColumn()
								{
									ColumnIndex = columnIndex,
									ColumnName = columnName,
									ColumnType = columnType,
									ColumnIsNullable = columnIsNullable,
									TableIndex = 0,
									TagContext = null
								};

					obfusscatedValue = this.GetObfuscatedValue(metaColumn, columnValue);
					obfuscatedRecord.Add(columnName, obfusscatedValue);
					columnIndex++;
				}

				yield return obfuscatedRecord;
			}
		}

		#endregion
	}
}