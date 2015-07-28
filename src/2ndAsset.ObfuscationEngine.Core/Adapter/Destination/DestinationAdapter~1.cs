/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	public abstract class DestinationAdapter<TAdapterSpecificConfiguration> : Adapter<TAdapterSpecificConfiguration>, IDestinationAdapter
		where TAdapterSpecificConfiguration : AdapterSpecificConfiguration, new()
	{
		#region Constructors/Destructors

		protected DestinationAdapter()
		{
		}

		#endregion

		#region Fields/Constants

		private IEnumerable<IMetaColumn> upstreamMetadata;

		#endregion

		#region Properties/Indexers/Events

		public IEnumerable<IMetaColumn> UpstreamMetadata
		{
			set
			{
				this.upstreamMetadata = value;
			}
			protected get
			{
				return this.upstreamMetadata;
			}
		}

		#endregion

		#region Methods/Operators

		protected abstract void CorePushData(TableConfiguration tableConfiguration, IEnumerable<IDictionary<string, object>> sourceDataEnumerable);

		public void PushData(TableConfiguration tableConfiguration, IEnumerable<IDictionary<string, object>> sourceDataEnumerable)
		{
			this.CorePushData(tableConfiguration, sourceDataEnumerable);
		}

		#endregion
	}
}