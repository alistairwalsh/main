/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using TextMetal.Middleware.Common;
using TextMetal.Middleware.Common.Utilities;
using TextMetal.Middleware.Data;
using TextMetal.Middleware.Data.UoW;
using TextMetal.Middleware.Solder.Serialization;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public sealed class ObfuscationMixIn : IObfuscationMixIn
	{
		#region Constructors/Destructors

		public ObfuscationMixIn(TableConfiguration tableConfiguration)
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
				configuration = new JsonSerializationStrategy().GetObjectFromString<TConfiguration>(jsonData);

			return configuration;
		}

		public static TConfiguration FromJsonFile<TConfiguration>(string jsonFilePath)
			where TConfiguration : class, IConfigurationObject, new()
		{
			TConfiguration configuration;

			configuration = new JsonSerializationStrategy().GetObjectFromFile<TConfiguration>(jsonFilePath);

			return configuration;
		}

		private static object GetCipher(string sharedSecret, object value)
		{
			const string INIT_VECTOR = "0123456701234567";
			const int KEY_SIZE = 256;

			byte[] initVectorBytes;
			byte[] plainTextBytes;
			ICryptoTransform encryptor;
			byte[] keyBytes;
			byte[] cipherTextBytes;
			Type valueType;
			string _value;

			if ((object)sharedSecret == null)
				throw new ArgumentNullException("sharedSecret");

			if ((object)value == null)
				return null;

			valueType = value.GetType();

			if (valueType != typeof(String))
				return null;

			_value = (String)value;

			if (DataTypeFascade.Instance.IsWhiteSpace(_value))
				return _value;

			_value = _value.Trim();

			initVectorBytes = Encoding.UTF8.GetBytes(INIT_VECTOR);
			plainTextBytes = Encoding.UTF8.GetBytes(_value);

#if !CLR_35
			using (
#endif
				PasswordDeriveBytes password = new PasswordDeriveBytes(sharedSecret, null)
#if CLR_35
			;
#endif
#if !CLR_35
				)
#endif
			{
				// support 3.5 and above
				keyBytes = password.GetBytes(KEY_SIZE / 8);
			}

			using (RijndaelManaged symmetricKey = new RijndaelManaged())
			{
				symmetricKey.Mode = CipherMode.CBC;
				encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
					{
						cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
						cryptoStream.FlushFinalBlock();
						cipherTextBytes = memoryStream.ToArray();
					}
				}
			}

			return Encoding.UTF8.GetString(cipherTextBytes);
		}

		private static object GetDefault(bool isNullable, Type valueType)
		{
			if ((object)valueType == null)
				throw new ArgumentNullException("valueType");

			if (valueType == typeof(String))
				return isNullable ? null : string.Empty;

			if (isNullable)
				valueType = ReflectionFascade.Instance.MakeNullableType(valueType);

			return DataTypeFascade.Instance.DefaultValue(valueType);
		}

		private static long? GetHash(long? hashMultiplier, long? hashBucketSize, long? hashSeed, object value)
		{
			const long DEFAULT_HASH = -1L;
			long hashCode;
			byte[] buffer;
			Type valueType;
			string _value;

			if ((object)hashMultiplier == null)
				return null;

			if ((object)hashBucketSize == null)
				return null;

			if ((object)hashSeed == null)
				return null;

			if (hashBucketSize == 0L)
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

			hashCode = (long)hashSeed;
			for (int index = 0; index < buffer.Length; index++)
				hashCode = ((long)hashMultiplier * hashCode + buffer[index]) % uint.MaxValue;

			if (hashCode > int.MaxValue)
				hashCode = hashCode - uint.MaxValue;

			if (hashCode < 0)
				hashCode = hashCode + int.MaxValue;

			hashCode = (hashCode % (long)hashBucketSize);

			return (int)hashCode;
		}

		private static object GetMask(double maskFactor, object value)
		{
			StringBuilder sb;
			Type valueType;
			string _value;

			if ((int)(maskFactor * 100) > 100)
				throw new ArgumentOutOfRangeException("maskFactor");

			if ((int)(maskFactor * 100) == 000)
				throw new ArgumentOutOfRangeException("maskFactor");

			if ((int)(maskFactor * 100) < -100)
				throw new ArgumentOutOfRangeException("maskFactor");

			if ((object)value == null)
				return null;

			valueType = value.GetType();

			if (valueType != typeof(String))
				return null;

			_value = (String)value;

			if (DataTypeFascade.Instance.IsWhiteSpace(_value))
				return _value;

			_value = _value.Trim();

			sb = new StringBuilder(_value);

			if (Math.Sign(maskFactor) == 1)
			{
				for (int index = 0; index < (int)Math.Round((double)_value.Length * maskFactor); index++)
					sb[index] = '*';
			}
			else if (Math.Sign(maskFactor) == -1)
			{
				for (int index = _value.Length - (int)Math.Round((double)_value.Length * Math.Abs(maskFactor)); index < _value.Length; index++)
					sb[index] = '*';
			}
			else
				throw new InvalidOperationException("maskFactor");

			return sb.ToString();
		}

		private static object GetShuffle(long randomSeed, object value)
		{
			Random random;
			Type valueType;
			string _value;

			if ((object)value == null)
				return null;

			valueType = value.GetType();

			if (valueType != typeof(String))
				return null;

			_value = (String)value;

			if (DataTypeFascade.Instance.IsWhiteSpace(_value))
				return _value;

			_value = _value.Trim();

			random = new Random((int)randomSeed);
			var fidelityMap = ImplNormalize(ref _value);
			_value = new string(_value.ToCharArray().OrderBy(s => random.Next(int.MaxValue)).ToArray());
			ImplDenormalize(fidelityMap, ref _value);

			return _value;
		}

		private static object GetSubstitution(IDictionary<string, IDictionary<long, object>> substitutionCacheRoot,
			DictionaryConfiguration dictionaryConfiguration,
			long selectId, object value)
		{
			Type valueType;
			string _value;
			const bool SUBSTITUTION_CACHE_ENABLED = true;

			IDictionary<long, object> dictionaryCache;

			if ((object)substitutionCacheRoot == null)
				throw new ArgumentNullException("substitutionCacheRoot");

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)value == null)
				return null;

			valueType = value.GetType();

			if (valueType != typeof(String))
				return null;

			_value = (String)value;

			if (DataTypeFascade.Instance.IsWhiteSpace(_value))
				return _value;

			_value = _value.Trim();

			if ((dictionaryConfiguration.RecordCount ?? 0L) <= 0L)
				return null;

			if (!SUBSTITUTION_CACHE_ENABLED || !substitutionCacheRoot.TryGetValue(dictionaryConfiguration.DictionaryId, out dictionaryCache))
			{
				dictionaryCache = new Dictionary<long, object>();

				if (SUBSTITUTION_CACHE_ENABLED)
					substitutionCacheRoot.Add(dictionaryConfiguration.DictionaryId, dictionaryCache);
			}

			if (!SUBSTITUTION_CACHE_ENABLED || !dictionaryCache.TryGetValue(selectId, out value))
			{
				if (dictionaryConfiguration.PreloadEnabled)
					throw new InvalidOperationException(string.Format("PreloadEnabled state fail."));

				using (IUnitOfWork unitOfWork = dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.GetUnitOfWork())
				{
					IDbDataParameter dbDataParameterKey;

					dbDataParameterKey = unitOfWork.CreateParameter(ParameterDirection.Input, DbType.Object, 0, 0, 0, false, "@ID", selectId);

					value = unitOfWork.ExecuteScalar<string>(dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType ?? CommandType.Text, dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText, new IDbDataParameter[] { dbDataParameterKey });
				}

				if (SUBSTITUTION_CACHE_ENABLED)
					dictionaryCache.Add(selectId, value);
			}

			return value;
		}

		private static object GetVariance(double varianceFactor, object value)
		{
			Type valueType;
			Type openNullableType;
			object originalValue;

			if ((object)value == null)
				return null;

			originalValue = value;
			valueType = value.GetType();
			openNullableType = typeof(Nullable<>);

			if (valueType.IsGenericType &&
				!valueType.IsGenericTypeDefinition &&
				valueType.GetGenericTypeDefinition().Equals(openNullableType))
				valueType = valueType.GetGenericArguments()[0];

			if (valueType == typeof(Boolean))
				value = (Math.Sign(varianceFactor) >= 0) ? true : false;
			else if (valueType == typeof(SByte))
				value = (SByte)value + (SByte)(varianceFactor * (Double)(SByte)value);
			else if (valueType == typeof(Int16))
				value = (Int16)value + (Int16)(varianceFactor * (Double)(Int16)value);
			else if (valueType == typeof(Int32))
				value = (Int32)value + (Int32)(varianceFactor * (Double)(Int32)value);
			else if (valueType == typeof(Int64))
				value = (Int64)value + (Int64)(varianceFactor * (Double)(Int64)value);
			else if (valueType == typeof(Byte))
				value = (Byte)value + (Byte)(varianceFactor * (Double)(Byte)value);
			else if (valueType == typeof(UInt16))
				value = (UInt16)value + (UInt16)(varianceFactor * (Double)(UInt16)value);
			else if (valueType == typeof(Int32))
				value = (UInt32)value + (UInt32)(varianceFactor * (Double)(UInt32)value);
			else if (valueType == typeof(UInt64))
				value = (UInt64)value + (UInt64)(varianceFactor * (Double)(UInt64)value);
			else if (valueType == typeof(Decimal))
				value = (Decimal)value + ((Decimal)varianceFactor * (Decimal)value);
			else if (valueType == typeof(Single))
				value = (Single)value + (Single)(varianceFactor * (Double)(Single)value);
			else if (valueType == typeof(Double))
				value = (Double)value + (Double)(varianceFactor * (Double)value);
			else if (valueType == typeof(Char))
				value = (Char)value + (Char)(varianceFactor * (Char)value);
			else if (valueType == typeof(DateTime))
				value = ((DateTime)value).AddDays((Double)(varianceFactor * 365.25));
			else if (valueType == typeof(DateTimeOffset))
				value = ((DateTimeOffset)value).AddDays((Double)(varianceFactor * 365.25));
			else if (valueType == typeof(TimeSpan))
				value = ((TimeSpan)value).Add(TimeSpan.FromDays((Double)(varianceFactor * 365.25)));
			else // unsupported type
				value = "#VALUE";

			// roll a recursive doubler call until a new value is achieved
			//if (DataType.ObjectsEqualValueSemantics(originalValue, value))
			//return GetVariance(varianceFactor * 2.0, value);

			return value;
		}

		private static void ImplDenormalize(Dictionary<int, char> fidelityMap, ref string value)
		{
			StringBuilder sb;
			char ch;
			int offset = 0;

			if ((object)fidelityMap == null)
				throw new ArgumentNullException("fidelityMap");

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(value))
				return;

			sb = new StringBuilder(value);

			for (int index = 0; index < value.Length; index++)
			{
				if (fidelityMap.TryGetValue(index, out ch))
				{
					sb.Insert(index, ch);
					offset++;
				}
			}

			value = sb.ToString();
		}

		private static Dictionary<int, char> ImplNormalize(ref string value)
		{
			StringBuilder sb;
			Dictionary<int, char> fidelityMap;

			fidelityMap = new Dictionary<int, char>();

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(value))
			{
				value = value;
				return fidelityMap;
			}

			sb = new StringBuilder();

			// 212-555-1234 => 2125551212 => 1945687302 => 194-568-7302
			for (int index = 0; index < value.Length; index++)
			{
				if (char.IsLetterOrDigit(value[index]))
					sb.Append(value[index]);
				else
					fidelityMap.Add(index, value[index]);
			}

			value = sb.ToString();
			return fidelityMap;
		}

		public static string ToJson<TConfiguration>(TConfiguration configuration)
			where TConfiguration : class, IConfigurationObject, new()
		{
			string jsonData;

			if ((object)configuration == null)
				jsonData = null;
			else
				jsonData = new JsonSerializationStrategy().SetObjectToString<TConfiguration>(configuration);

			return jsonData;
		}

		public static void ToJsonFile<TConfiguration>(TConfiguration configuration, string jsonFilePath)
			where TConfiguration : class, IConfigurationObject, new()
		{
			new JsonSerializationStrategy().SetObjectToFile<TConfiguration>(jsonFilePath, configuration);
		}

		private object _GetObfuscatedValue(int columnIndex, string columnName, Type columnType, object columnValue)
		{
			ColumnConfiguration columnConfiguration;
			DictionaryConfiguration dictionaryConfiguration;
			long valueHash, signHash, valueHashBucketSize, signHashBucketSize;
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
			signHash = GetHash(this.TableConfiguration.Parent.HashConfiguration.Multiplier,
				signHashBucketSize,
				this.TableConfiguration.Parent.HashConfiguration.Seed,
				columnValue.SafeToString()) ?? int.MinValue;

			if (signHash == int.MinValue)
				throw new InvalidOperationException(string.Format("Obfuscation mixin failed to calculate a valid sign hash for input '{0}' specified for column '{1}'.", columnValue.SafeToString(null, "<null>"), columnName));

			switch (columnConfiguration.ObfuscationStrategy)
			{
				case ObfuscationStrategy.Substitution:
					valueHashBucketSize = dictionaryConfiguration.RecordCount ?? int.MaxValue;
					break;
				case ObfuscationStrategy.Variance:
					valueHashBucketSize = columnConfiguration.ExtentValue ?? int.MaxValue;
					break;
				default:
					valueHashBucketSize = int.MaxValue;
					break;
			}

			valueHash = GetHash(this.TableConfiguration.Parent.HashConfiguration.Multiplier ?? 0L,
				valueHashBucketSize,
				this.TableConfiguration.Parent.HashConfiguration.Seed ?? 0L,
				columnValue.SafeToString()) ?? int.MinValue;

			if (valueHash == int.MinValue)
				throw new InvalidOperationException(string.Format("Obfuscation mixin failed to calculate a valid value hash for input '{0}' specified for column '{1}'.", columnValue.SafeToString(null, "<null>"), columnName));

			switch (columnConfiguration.ObfuscationStrategy)
			{
				case ObfuscationStrategy.Substitution:
					var selectId =
						valueHash;
					return GetSubstitution(this.SubstitutionCacheRoot, dictionaryConfiguration, selectId, columnValue);
				case ObfuscationStrategy.Shuffling:
					var randomSeed =
						valueHash;
					return GetShuffle(randomSeed, columnValue);
				case ObfuscationStrategy.Variance:
					var varianceFactor =
						((((valueHash <= 0 ? 1 : valueHash)) * ((signHash % 2 == 0 ? 1.0 : -1.0))) / 100.0);
					return GetVariance(varianceFactor, columnValue);
				case ObfuscationStrategy.Ciphering:
					var sharedSecret =
						((valueHash <= 0 ? 1 : valueHash) * (signHash % 2 == 0 ? -1 : 1)).ToString("X");
					return GetCipher(sharedSecret, columnValue);
				case ObfuscationStrategy.Defaulting:
					var isColumnNullable =
						(bool)columnConfiguration.IsColumnNullable;
					return GetDefault(isColumnNullable, columnType);
				case ObfuscationStrategy.Masking:
					var maskingFactor = ((int)columnConfiguration.ExtentValue / 100.0);
					return GetMask(maskingFactor, columnValue);
				case ObfuscationStrategy.None:
					return columnValue;
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