/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	public interface IDestinationAdapter : IAdapter
	{
		#region Properties/Indexers/Events

		IEnumerable<MetaColumn> UpstreamMetadata
		{
			set;
		}

		#endregion

		#region Methods/Operators

		void PushData(TableConfiguration tableConfiguration, IEnumerable<IDictionary<string, object>> sourceDataEnumerable);

		#endregion
	}
}