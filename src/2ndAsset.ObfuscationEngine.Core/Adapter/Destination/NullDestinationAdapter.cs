/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	public sealed class NullDestinationAdapter : DestinationAdapter, INullAdapter
	{
		#region Constructors/Destructors

		public NullDestinationAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override void CoreInitialize(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration"));
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