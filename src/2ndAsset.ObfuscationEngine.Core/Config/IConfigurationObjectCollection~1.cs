/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public interface IConfigurationObjectCollection<TConfigurationObject> : IConfigurationObjectCollection
		where TConfigurationObject : IConfigurationObject
	{
	}
}