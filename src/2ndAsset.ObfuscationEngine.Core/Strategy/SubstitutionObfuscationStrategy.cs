/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;

using TextMetal.Middleware.Common.Utilities;
using TextMetal.Middleware.Data.UoW;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Hosting.Tool;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public sealed class SubstitutionObfuscationStrategy : ObfuscationStrategy<DictionaryConfiguration>
	{
		#region Constructors/Destructors

		public SubstitutionObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static object GetSubstitution(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, long surrogateId, object value)
		{
			Type valueType;
			string _value;
			const bool SUBSTITUTION_CACHE_ENABLED = true;

			IDictionary<long, object> dictionaryCache;

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

			if (!SUBSTITUTION_CACHE_ENABLED || !ToolHost.Current.SubstitutionCacheRoot.TryGetValue(dictionaryConfiguration.DictionaryId, out dictionaryCache))
			{
				dictionaryCache = new Dictionary<long, object>();

				if (SUBSTITUTION_CACHE_ENABLED)
					ToolHost.Current.SubstitutionCacheRoot.Add(dictionaryConfiguration.DictionaryId, dictionaryCache);
			}

			if (!SUBSTITUTION_CACHE_ENABLED || !dictionaryCache.TryGetValue(surrogateId, out value))
			{
				if (dictionaryConfiguration.PreloadEnabled)
					throw new InvalidOperationException(string.Format("PreloadEnabled state fail."));

				value = ToolHost.Current.DictionaryConfigurationToAdapterMappings[dictionaryConfiguration].GetAlternativeValueFromId(dictionaryConfiguration, metaColumn, surrogateId);

				if (SUBSTITUTION_CACHE_ENABLED)
					dictionaryCache.Add(surrogateId, value);
			}

			return value;
		}

		protected override object CoreGetObfuscatedValue(DictionaryConfiguration configurationContext, HashResult hashResult, IMetaColumn metaColumn, object columnValue)
		{
			object value;
			long surrogateId;

			if ((object)configurationContext == null)
				throw new ArgumentNullException("configurationContext");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			surrogateId = hashResult.ValueHash;

			value = GetSubstitution(configurationContext, metaColumn, surrogateId, columnValue);

			return value;
		}

		#endregion
	}
}