/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Source
{
	public class NullSourceAdapter : SourceAdapter, INullAdapter
	{
		#region Constructors/Destructors

		public NullSourceAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override void CoreInitialize(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration.SourceAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration"));
		}

		protected override IEnumerable<IDictionary<string, object>> CorePullData(TableConfiguration tableConfiguration)
		{
			//int max = new Random().Next(0, 999999);

			//for (int i = 0; i < max; i++)
			//	yield return new Dictionary<string, object>();
			return new IDictionary<string, object>[] { };
		}

		protected override void CoreTerminate()
		{
		}

		#endregion
	}
}