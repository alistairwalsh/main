﻿/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

using TextMetal.Middleware.Common;
using TextMetal.Middleware.Common.Utilities;
using TextMetal.Middleware.Data;
using TextMetal.Middleware.Data.UoW;
using TextMetal.Middleware.Solder.Serialization;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Strategy;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public sealed class OxymoronEngine : IOxymoronEngine
	{
		#region Constructors/Destructors

		public OxymoronEngine(TableConfiguration tableConfiguration)
		{
			if ((object)tableConfiguration == null)
				throw new ArgumentNullException("tableConfiguration");

			this.tableConfiguration = tableConfiguration;

			EnsureValidConfigurationOnce(this.TableConfiguration);
			this.InitializePreloadCache();
		}

		#endregion

		#region Fields/Constants

		private readonly IDictionary<int, ColumnConfiguration> columnCache = new Dictionary<int, ColumnConfiguration>();
		private readonly IHashingStrategy hashingStrategy = DefaultHashingStrategy.Instance;
		private readonly IDictionary<string, IDictionary<long, object>> substitutionCacheRoot = new Dictionary<string, IDictionary<long, object>>();
		private readonly TableConfiguration tableConfiguration;

		#endregion

		#region Properties/Indexers/Events

		private IDictionary<int, ColumnConfiguration> ColumnCache
		{
			get
			{
				return this.columnCache;
			}
		}

		private IHashingStrategy HashingStrategy
		{
			get
			{
				return this.hashingStrategy;
			}
		}

		private IDictionary<string, IDictionary<long, object>> SubstitutionCacheRoot
		{
			get
			{
				return this.substitutionCacheRoot;
			}
		}

		private TableConfiguration TableConfiguration
		{
			get
			{
				return this.tableConfiguration;
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
				throw new ConfigurationException(string.Format("Obfuscation configuration validation failed:\r\n{0}", string.Join("\r\n", messages.Select(m => m.Description).ToArray())));
		}

		public static TConfiguration FromJson<TConfiguration>(string jsonData)
			where TConfiguration : class, IConfigurationObject, new()
		{
			TConfiguration configuration;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(jsonData))
				configuration = null;
			else
				configuration = JsonSerializationStrategy.Instance.GetObjectFromString<TConfiguration>(jsonData);

			return configuration;
		}

		public static TConfiguration FromJsonFile<TConfiguration>(string jsonFilePath)
			where TConfiguration : class, IConfigurationObject, new()
		{
			TConfiguration configuration;

			configuration = JsonSerializationStrategy.Instance.GetObjectFromFile<TConfiguration>(jsonFilePath);

			return configuration;
		}

		public static string ToJson<TConfiguration>(TConfiguration configuration)
			where TConfiguration : class, IConfigurationObject, new()
		{
			string jsonData;

			if ((object)configuration == null)
				jsonData = null;
			else
				jsonData = JsonSerializationStrategy.Instance.SetObjectToString<TConfiguration>(configuration);

			return jsonData;
		}

		public static void ToJsonFile<TConfiguration>(TConfiguration configuration, string jsonFilePath)
			where TConfiguration : class, IConfigurationObject, new()
		{
			JsonSerializationStrategy.Instance.SetObjectToFile<TConfiguration>(jsonFilePath, configuration);
		}

		private object _GetObfuscatedValue(int columnIndex, string columnName, Type columnType, object columnValue)
		{
			MetaColumn metaColumn;
			ColumnConfiguration columnConfiguration;
			DictionaryConfiguration dictionaryConfiguration;
			long valueHash, signHash, valueHashBucketSize, signHashBucketSize;
			int? extentValue;
			const bool COLUMN_CACHE_ENABLED = true;

			if ((object)columnName == null)
				throw new ArgumentNullException("columnName");

			if ((object)columnType == null)
				throw new ArgumentNullException("columnType");

			// KILLS performance - not sure why
			//EnsureValidConfigurationOnce(this.TableConfiguration);
			//columnConfiguration = this.TableConfiguration.ColumnConfigurations.SingleOrDefault(c => c.ColumnName.SafeToString().Trim().ToLower() == columnName.SafeToString().Trim().ToLower());

			if (!COLUMN_CACHE_ENABLED || !this.ColumnCache.TryGetValue(columnIndex, out columnConfiguration))
			{
				columnConfiguration = this.TableConfiguration.ColumnConfigurations.SingleOrDefault(c => c.ColumnName.SafeToString().Trim().ToLower() == columnName.SafeToString().Trim().ToLower());
				//columnConfiguration = this.TableConfiguration.ColumnConfigurations.SingleOrDefault(c => c.ColumnName == columnName);

				if (COLUMN_CACHE_ENABLED)
					this.ColumnCache.Add(columnIndex, columnConfiguration);
			}

			if ((object)columnConfiguration == null)
				return columnValue; // do nothing when no matching column configuration

			if (columnConfiguration.ObfuscationStrategy == ObfuscationStrategy.None)
				return columnValue;

			if (columnConfiguration.DictionaryReference.SafeToString().Trim().ToLower() == string.Empty)
				dictionaryConfiguration = new DictionaryConfiguration();
			else
				dictionaryConfiguration = this.TableConfiguration.Parent.DictionaryConfigurations.SingleOrDefault(d => d.DictionaryId.SafeToString().Trim().ToLower() == columnConfiguration.DictionaryReference.SafeToString().Trim().ToLower());

			if ((object)dictionaryConfiguration == null)
				throw new ConfigurationException(string.Format("Unknown dictionary reference '{0}' specified for column '{1}'.", columnConfiguration.DictionaryReference, columnName));

			signHashBucketSize = long.MaxValue;
			signHash = this.HashingStrategy.GetHash(this.TableConfiguration.Parent.HashConfiguration.Multiplier,
				signHashBucketSize,
				this.TableConfiguration.Parent.HashConfiguration.Seed,
				columnValue.SafeToString()) ?? int.MinValue;

			if (signHash == int.MinValue)
				throw new InvalidOperationException(string.Format("Obfuscation mixin failed to calculate a valid sign hash for input '{0}' specified for column '{1}'.", columnValue.SafeToString(null, "<null>"), columnName));

			extentValue = columnConfiguration.ExtentValue;

			switch (columnConfiguration.ObfuscationStrategy)
			{
				case ObfuscationStrategy.Substitution:
					valueHashBucketSize = dictionaryConfiguration.RecordCount ?? int.MaxValue;
					break;
				case ObfuscationStrategy.Variance:
					valueHashBucketSize = extentValue ?? int.MaxValue;
					break;
				default:
					valueHashBucketSize = int.MaxValue;
					break;
			}

			valueHash = this.HashingStrategy.GetHash(this.TableConfiguration.Parent.HashConfiguration.Multiplier ?? 0L,
				valueHashBucketSize,
				this.TableConfiguration.Parent.HashConfiguration.Seed ?? 0L,
				columnValue.SafeToString()) ?? int.MinValue;

			if (valueHash == int.MinValue)
				throw new InvalidOperationException(string.Format("Obfuscation mixin failed to calculate a valid value hash for input '{0}' specified for column '{1}'.", columnValue.SafeToString(null, "<null>"), columnName));

			metaColumn = new MetaColumn()
						{
							ColumnIndex = columnIndex,
							ColumnName = columnName,
							ColumnIsNullable = columnConfiguration.IsColumnNullable.GetValueOrDefault(),
							ColumnType = columnType,
							MetaTableIndex = 0,
							TagContext = null
						};

			switch (columnConfiguration.ObfuscationStrategy)
			{
				case ObfuscationStrategy.Substitution:
					return new SubstitutionObfuscationStrategy(dictionaryConfiguration, this.SubstitutionCacheRoot).GetObfuscatedValue(signHash, valueHash, extentValue, metaColumn, columnValue);
				case ObfuscationStrategy.Shuffling:
					return new ShufflingObfuscationStrategy().GetObfuscatedValue(signHash, valueHash, extentValue, metaColumn, columnValue);
				case ObfuscationStrategy.Variance:
					return new VarianceObfuscationStrategy().GetObfuscatedValue(signHash, valueHash, extentValue, metaColumn, columnValue);
				case ObfuscationStrategy.Ciphering:
					return new CipheringObfuscationStrategy().GetObfuscatedValue(signHash, valueHash, extentValue, metaColumn, columnValue);
				case ObfuscationStrategy.Defaulting:
					return new DefaultingObfuscationStrategy().GetObfuscatedValue(signHash, valueHash, extentValue, metaColumn, columnValue);
				case ObfuscationStrategy.Masking:
					return new MaskingObfuscationStrategy().GetObfuscatedValue(signHash, valueHash, extentValue, metaColumn, columnValue);
				case ObfuscationStrategy.Script:
					return new ScriptObfuscationStrategy().GetObfuscatedValue(signHash, valueHash, extentValue, metaColumn, columnValue);
				case ObfuscationStrategy.None:
					return new NoneObfuscationStrategy().GetObfuscatedValue(signHash, valueHash, extentValue, metaColumn, columnValue);
				default:
					throw new ConfigurationException(string.Format("Unknown obfuscation strategy '{0}' specified for column '{1}'.", columnConfiguration.ObfuscationStrategy, columnName));
			}
		}

		public void Dispose()
		{
			this.SubstitutionCacheRoot.Clear();
			this.ColumnCache.Clear();
		}

		public object GetObfuscatedValue(int columnIndex, string columnName, Type columnType, object columnValue)
		{
			object value;

			if ((object)columnValue == DBNull.Value)
				columnValue = null;

			value = this._GetObfuscatedValue(columnIndex, columnName, columnType, columnValue);
			//Console.WriteLine("[{0}]: '{1}' => '{2}'", columnName, columnValue, value);
			return value;
		}

		private void InitializePreloadCache()
		{
			IEnumerable<IRecord> records;
			IDictionary<long, object> dictionaryCache;

			foreach (DictionaryConfiguration dictionaryConfiguration in this.TableConfiguration.Parent.DictionaryConfigurations)
			{
				if (dictionaryConfiguration.PreloadEnabled)
				{
					using (IUnitOfWork unitOfWork = dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.GetUnitOfWork())
					{
						records = unitOfWork.ExecuteRecords(dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType ?? CommandType.Text, dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText, new IDbDataParameter[] { }, null);

						if ((object)records == null)
							throw new InvalidOperationException(string.Format("Records were invalid."));

						dictionaryCache = new Dictionary<long, object>();

						foreach (IRecord record in records)
						{
							long id = record[record.Keys.ToArray()[0]].ChangeType<long>();
							object value = record[record.Keys.ToArray()[1]].ChangeType<string>();
							dictionaryCache.Add(id, value);
						}

						this.SubstitutionCacheRoot.Add(dictionaryConfiguration.DictionaryId, dictionaryCache);
					}
				}
			}
		}

		#endregion
	}
}