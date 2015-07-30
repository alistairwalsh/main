/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	public abstract class DictionaryAdapter<TAdapterSpecificConfiguration> : Adapter<TAdapterSpecificConfiguration>, IDictionaryAdapter
		where TAdapterSpecificConfiguration : AdapterSpecificConfiguration, new()
	{
		#region Constructors/Destructors

		protected DictionaryAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected abstract object CoreGetAlternativeValueFromId(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId);

		protected abstract void CorePreloadCache(DictionaryConfiguration dictionaryConfiguration, IDictionary<string, IDictionary<long, object>> substitutionCacheRoot);

		public object GetAlternativeValueFromId(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId)
		{
			return this.CoreGetAlternativeValueFromId(dictionaryConfiguration, metaColumn, surrogateId);
		}

		public void InitializePreloadCache(DictionaryConfiguration dictionaryConfiguration, IDictionary<string, IDictionary<long, object>> substitutionCacheRoot)
		{
			this.CorePreloadCache(dictionaryConfiguration, substitutionCacheRoot);
		}

		#endregion
	}
}