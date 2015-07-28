/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	public interface IDictionaryAdapter : IAdapter
	{
		#region Methods/Operators

		object GetAlternativeValueFromId(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId);

		void InitializePreloadCache(DictionaryConfiguration dictionaryConfiguration, IDictionary<string, IDictionary<long, object>> substitutionCacheRoot);

		#endregion
	}
}