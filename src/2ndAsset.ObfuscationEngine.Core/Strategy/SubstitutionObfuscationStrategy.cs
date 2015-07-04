/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Strategies;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	/// <summary>
	/// Returns an alternate value using a hashed lookup into a dictionary.
	/// DATA TYPE: string
	/// </summary>
	public sealed class SubstitutionObfuscationStrategy : ObfuscationStrategy<SubstitutionObfuscationStrategyConfiguration>
	{
		#region Constructors/Destructors

		public SubstitutionObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static object GetSubstitution(IOxymoronEngine oxymoronEngine, DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, long surrogateId, object value)
		{
			Type valueType;
			string _value;
			const bool SUBSTITUTION_CACHE_ENABLED = true;

			IDictionary<long, object> dictionaryCache;

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

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

			if (!SUBSTITUTION_CACHE_ENABLED || !oxymoronEngine.SubstitutionCacheRoot.TryGetValue(dictionaryConfiguration.DictionaryId, out dictionaryCache))
			{
				dictionaryCache = new Dictionary<long, object>();

				if (SUBSTITUTION_CACHE_ENABLED)
					oxymoronEngine.SubstitutionCacheRoot.Add(dictionaryConfiguration.DictionaryId, dictionaryCache);
			}

			if (!SUBSTITUTION_CACHE_ENABLED || !dictionaryCache.TryGetValue(surrogateId, out value))
			{
				if (dictionaryConfiguration.PreloadEnabled)
					throw new InvalidOperationException(string.Format("Cache miss when is preload enabled for dictionary '{0}'; current cache slot item count: {1}.", dictionaryConfiguration.DictionaryId, dictionaryCache.Count));

				value = oxymoronEngine.OxymoronHost.GetValueForIdViaDictionaryResolution(dictionaryConfiguration, metaColumn, surrogateId);

				if (SUBSTITUTION_CACHE_ENABLED)
					dictionaryCache.Add(surrogateId, value);
			}

			return value;
		}

		protected override object CoreGetObfuscatedValue(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, SubstitutionObfuscationStrategyConfiguration> contextualConfiguration, HashResult hashResult, IMetaColumn metaColumn, object columnValue)
		{
			DictionaryConfiguration dictionaryConfiguration;
			object value;
			long surrogateId;

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			if ((object)contextualConfiguration == null)
				throw new ArgumentNullException("contextualConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			dictionaryConfiguration = this.GetDictionaryConfiguration(oxymoronEngine, contextualConfiguration);
			surrogateId = hashResult.ValueHash;

			value = GetSubstitution(oxymoronEngine, dictionaryConfiguration, metaColumn, surrogateId, columnValue);

			return value;
		}

		protected override long CoreGetValueHashBucketSize(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, SubstitutionObfuscationStrategyConfiguration> contextualConfiguration)
		{
			DictionaryConfiguration dictionaryConfiguration;
			long value;

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			if ((object)contextualConfiguration == null)
				throw new ArgumentNullException("contextualConfiguration");

			dictionaryConfiguration = this.GetDictionaryConfiguration(oxymoronEngine, contextualConfiguration);
			value = dictionaryConfiguration.RecordCount ?? base.CoreGetValueHashBucketSize(oxymoronEngine, contextualConfiguration);

			return value;
		}

		private DictionaryConfiguration GetDictionaryConfiguration(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, SubstitutionObfuscationStrategyConfiguration> contextualConfiguration)
		{
			DictionaryConfiguration dictionaryConfiguration;

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			if ((object)contextualConfiguration == null)
				throw new ArgumentNullException("contextualConfiguration");

			if (contextualConfiguration.Item2.DictionaryReference.SafeToString().Trim().ToLower() == string.Empty)
				dictionaryConfiguration = new DictionaryConfiguration();
			else
				dictionaryConfiguration = oxymoronEngine.ObfuscationConfiguration.DictionaryConfigurations.SingleOrDefault(d => d.DictionaryId.SafeToString().Trim().ToLower() == contextualConfiguration.Item2.DictionaryReference.SafeToString().Trim().ToLower());

			if ((object)dictionaryConfiguration == null)
				throw new InvalidOperationException(string.Format("Unknown dictionary reference '{0}' specified for column '{1}'.", contextualConfiguration.Item2.DictionaryReference, contextualConfiguration.Item1.ColumnName));

			return dictionaryConfiguration;
		}

		#endregion
	}
}