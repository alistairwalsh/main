/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	public interface IDictionaryAdapter : IAdapter
	{
		#region Methods/Operators

		object GetAlternativeValueFromId(DictionaryConfiguration dictionaryConfiguration, object id);

		#endregion
	}
}