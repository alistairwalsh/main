/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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

		long GetBoundedHash(long? size, object value);

		object GetObfuscatedValue(IMetaColumn metaColumn, object columnValue);

		IEnumerable<IDictionary<string, object>> GetObfuscatedValues(IEnumerable<IDictionary<string, object>> records);

		#endregion
	}
}