/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary;
using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Hosting.Tool
{
	public interface IToolHost : IOxymoronHost
	{
		#region Properties/Indexers/Events

		IDictionary<DictionaryConfiguration, IDictionaryAdapter> DictionaryConfigurationToAdapterMappings
		{
			get;
		}

		IDictionary<string, IDictionary<long, object>> SubstitutionCacheRoot
		{
			get;
		}

		#endregion

		#region Methods/Operators

		void Host(string sourceFilePath);

		#endregion
	}
}