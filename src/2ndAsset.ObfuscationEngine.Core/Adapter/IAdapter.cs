/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using Solder.Framework;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter
{
	public interface IAdapter : IDisposable
	{
		#region Methods/Operators

		Type GetAdapterSpecificConfigurationType();

		void Initialize(AdapterConfiguration adapterConfiguration);

		void Terminate();

		IEnumerable<Message> ValidateAdapterSpecificConfiguration(AdapterConfiguration adapterConfiguration, string adapterContext);

		#endregion
	}
}