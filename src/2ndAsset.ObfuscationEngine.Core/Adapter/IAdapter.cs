﻿/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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