/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Source
{
	public interface ISourceAdapter : IAdapter
	{
		#region Properties/Indexers/Events

		IEnumerable<MetaColumn> UpstreamMetadata
		{
			get;
		}

		#endregion

		#region Methods/Operators

		IEnumerable<IDictionary<string, object>> PullData(TableConfiguration tableConfiguration);

		#endregion
	}
}