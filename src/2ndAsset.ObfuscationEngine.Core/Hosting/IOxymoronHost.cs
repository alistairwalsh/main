/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Hosting
{
	public interface IOxymoronHost : IDisposable
	{
		#region Methods/Operators

		object GetValueForIdViaDictionaryResolution(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId);

		#endregion
	}
}