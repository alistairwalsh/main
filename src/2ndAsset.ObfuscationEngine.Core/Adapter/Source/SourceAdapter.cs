/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Source
{
	public abstract class SourceAdapter : Adapter, ISourceAdapter
	{
		#region Constructors/Destructors

		protected SourceAdapter()
		{
		}

		#endregion

		#region Fields/Constants

		private IEnumerable<MetaColumn> upstreamMetadata;

		#endregion

		#region Properties/Indexers/Events

		public IEnumerable<MetaColumn> UpstreamMetadata
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

		protected abstract IEnumerable<IDictionary<string, object>> CorePullData(TableConfiguration configuration);

		public IEnumerable<IDictionary<string, object>> PullData(TableConfiguration configuration)
		{
			return this.CorePullData(configuration);
		}

		#endregion
	}
}