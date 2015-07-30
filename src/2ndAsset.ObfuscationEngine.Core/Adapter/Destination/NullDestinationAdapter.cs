/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	[AdapterSpecificConfiguration(SpecificConfigurationAqtn = "", UserControlAqtn = "")]
	public class NullDestinationAdapter : DestinationAdapter<AdapterSpecificConfiguration>, INullAdapter
	{
		#region Constructors/Destructors

		public NullDestinationAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override void CoreInitialize()
		{
		}

		protected override void CorePushData(TableConfiguration tableConfiguration, IEnumerable<IDictionary<string, object>> sourceDataEnumerable)
		{
			if ((object)tableConfiguration == null)
				throw new ArgumentNullException("tableConfiguration");

			if ((object)sourceDataEnumerable == null)
				throw new ArgumentNullException("sourceDataEnumerable");

			foreach (IDictionary<string, object> sourceDataItem in sourceDataEnumerable)
			{
				// do nothing
			}
		}

		protected override void CoreTerminate()
		{
		}

		#endregion
	}
}