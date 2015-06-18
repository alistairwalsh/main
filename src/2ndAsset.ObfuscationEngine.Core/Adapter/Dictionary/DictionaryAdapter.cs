/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	public abstract class DictionaryAdapter : Adapter, IDictionaryAdapter
	{
		#region Constructors/Destructors

		protected DictionaryAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected abstract object CoreGetAlternativeValueFromId(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId);

		protected abstract void CoreInitializePreloadCache(DictionaryConfiguration dictionaryConfiguration, IDictionary<string, IDictionary<long, object>> substitutionCacheRoot);

		public object GetAlternativeValueFromId(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId)
		{
			return this.CoreGetAlternativeValueFromId(dictionaryConfiguration, metaColumn, surrogateId);
		}

		public void InitializePreloadCache(DictionaryConfiguration dictionaryConfiguration, IDictionary<string, IDictionary<long, object>> substitutionCacheRoot)
		{
			this.CoreInitializePreloadCache(dictionaryConfiguration, substitutionCacheRoot);
		}

		#endregion
	}
}