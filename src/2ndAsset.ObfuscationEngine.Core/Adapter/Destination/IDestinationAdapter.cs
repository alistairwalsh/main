/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	public interface IDestinationAdapter : IAdapter
	{
		#region Properties/Indexers/Events

		IEnumerable<IMetaColumn> UpstreamMetadata
		{
			set;
		}

		#endregion

		#region Methods/Operators

		void PushData(TableConfiguration tableConfiguration, IEnumerable<IDictionary<string, object>> sourceDataEnumerable);

		#endregion
	}
}