/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
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

		private IEnumerable<MetaColumn> upstreamMetadata;

		#endregion

		#region Properties/Indexers/Events

		public IEnumerable<MetaColumn> UpstreamMetadata
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