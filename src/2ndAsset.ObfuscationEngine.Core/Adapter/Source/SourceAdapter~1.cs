/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Source
{
	public abstract class SourceAdapter<TAdapterSpecificConfiguration> : Adapter<TAdapterSpecificConfiguration>, ISourceAdapter
		where TAdapterSpecificConfiguration : AdapterSpecificConfiguration, new()
	{
		#region Constructors/Destructors

		protected SourceAdapter()
		{
		}

		#endregion

		#region Fields/Constants

		private IEnumerable<IMetaColumn> upstreamMetadata;

		#endregion

		#region Properties/Indexers/Events

		public IEnumerable<IMetaColumn> UpstreamMetadata
		{
			get
			{
				return this.upstreamMetadata;
			}
			protected set
			{
				this.upstreamMetadata = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected abstract IEnumerable<IDictionary<string, object>> CorePullData(TableConfiguration tableConfiguration);

		public IEnumerable<IDictionary<string, object>> PullData(TableConfiguration tableConfiguration)
		{
			if ((object)tableConfiguration == null)
				throw new ArgumentNullException("tableConfiguration");

			return this.CorePullData(tableConfiguration);
		}

		#endregion
	}
}