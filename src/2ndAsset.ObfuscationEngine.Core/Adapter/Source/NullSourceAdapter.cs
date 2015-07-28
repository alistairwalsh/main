/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Source
{
	[AdapterSpecificConfiguration(SpecificConfigurationAqtn = "", UserControlAqtn = "")]
	public class NullSourceAdapter : SourceAdapter<AdapterSpecificConfiguration>, INullAdapter
	{
		#region Constructors/Destructors

		public NullSourceAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override void CoreInitialize()
		{
		}

		protected override IEnumerable<IDictionary<string, object>> CorePullData(TableConfiguration tableConfiguration)
		{
			return new IDictionary<string, object>[] { };
		}

		protected override void CoreTerminate()
		{
		}

		#endregion
	}
}