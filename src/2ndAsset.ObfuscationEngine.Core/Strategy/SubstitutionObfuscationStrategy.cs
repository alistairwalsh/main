/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Hosting;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public sealed class SubstitutionObfuscationStrategy : ObfuscationStrategy<DictionaryConfiguration>
	{
		#region Constructors/Destructors

		public SubstitutionObfuscationStrategy(IOxymoronHost oxymoronHost, IOxymoronEngine oxymoronEngine)
		{
			if ((object)oxymoronHost == null)
				throw new ArgumentNullException("oxymoronHost");

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			this.oxymoronHost = oxymoronHost;
			this.oxymoronEngine = oxymoronEngine;
		}

		#endregion

		#region Fields/Constants

		private readonly IOxymoronEngine oxymoronEngine;
		private readonly IOxymoronHost oxymoronHost;

		#endregion

		#region Properties/Indexers/Events

		public IOxymoronEngine OxymoronEngine
		{
			get
			{
				return this.oxymoronEngine;
			}
		}

		private IOxymoronHost OxymoronHost
		{
			get
			{
				return this.oxymoronHost;
			}
		}

		#endregion

		#region Methods/Operators

		private static object GetSubstitution(IOxymoronHost oxymoronHost, IOxymoronEngine oxymoronEngine, DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, long surrogateId, object value)
		{
			Type valueType;
			string _value;
			const bool SUBSTITUTION_CACHE_ENABLED = true;

			IDictionary<long, object> dictionaryCache;

			if ((object)oxymoronHost == null)
				throw new ArgumentNullException("oxymoronHost");

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

				value = oxymoronHost.GetValueForIdViaDictionaryResolution(dictionaryConfiguration, metaColumn, surrogateId);

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

			value = GetSubstitution(this.OxymoronHost, this.OxymoronEngine, configurationContext, metaColumn, surrogateId, columnValue);

			return value;
		}

		#endregion
	}
}