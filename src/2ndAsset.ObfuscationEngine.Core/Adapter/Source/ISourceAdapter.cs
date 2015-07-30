/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Source
{
	public interface ISourceAdapter : IAdapter
	{
		#region Properties/Indexers/Events

		IEnumerable<IMetaColumn> UpstreamMetadata
		{
			get;
		}

		#endregion

		#region Methods/Operators

		IEnumerable<IDictionary<string, object>> PullData(TableConfiguration tableConfiguration);

		#endregion
	}
}