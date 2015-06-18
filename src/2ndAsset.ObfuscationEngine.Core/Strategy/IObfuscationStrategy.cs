/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public interface IObfuscationStrategy<TConfigurationContext>
		where TConfigurationContext : class, IConfigurationObject, new()
	{
		#region Methods/Operators

		object GetObfuscatedValue(TConfigurationContext configurationContext, HashResult hashResult, IMetaColumn metaColumn, object columnValue);

		#endregion
	}
}