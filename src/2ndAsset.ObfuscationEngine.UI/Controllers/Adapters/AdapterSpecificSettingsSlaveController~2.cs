/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;
using _2ndAsset.ObfuscationEngine.UI.Views.Adapters;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers.Adapters
{
	public abstract class AdapterSpecificSettingsSlaveController<TView, TAdapterSpecificConfiguration> : AdapterSpecificSettingsSlaveController<TView>
		where TView : class, IAdapterSpecificSettingsPartialView
		where TAdapterSpecificConfiguration : AdapterSpecificConfiguration, new()
	{
		#region Constructors/Destructors

		public AdapterSpecificSettingsSlaveController()
		{
		}

		#endregion
	}
}