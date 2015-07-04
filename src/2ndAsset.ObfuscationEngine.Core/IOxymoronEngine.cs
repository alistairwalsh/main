/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Hosting;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public interface IOxymoronEngine : IDisposable
	{
		#region Properties/Indexers/Events

		ObfuscationConfiguration ObfuscationConfiguration
		{
			get;
		}

		IOxymoronHost OxymoronHost
		{
			get;
		}

		IDictionary<string, IDictionary<long, object>> SubstitutionCacheRoot
		{
			get;
		}

		#endregion

		#region Methods/Operators

		object GetObfuscatedValue(IMetaColumn metaColumn, object columnValue);

		IEnumerable<IDictionary<string, object>> GetObfuscatedValues(IEnumerable<IDictionary<string, object>> records);

		#endregion
	}
}