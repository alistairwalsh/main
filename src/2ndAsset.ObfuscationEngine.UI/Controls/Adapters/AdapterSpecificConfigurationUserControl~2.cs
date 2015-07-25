/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.ObfuscationEngine.UI.Controllers.Adapters;
using _2ndAsset.ObfuscationEngine.UI.Views.Adapters;

namespace _2ndAsset.ObfuscationEngine.UI.Controls.Adapters
{
	public class AdapterSpecificConfigurationUserControl<TAdapterSpecificConfigurationPartialView, TAdapterSpecificConfigurationSlaveController> : BasePartialViewUserControl<TAdapterSpecificConfigurationPartialView, TAdapterSpecificConfigurationSlaveController>, IAdapterSpecificSettingsPartialView
		where TAdapterSpecificConfigurationPartialView : class, IAdapterSpecificSettingsPartialView
		where TAdapterSpecificConfigurationSlaveController : AdapterSpecificSettingsSlaveController<TAdapterSpecificConfigurationPartialView>, new()
	{
		#region Constructors/Destructors

		public AdapterSpecificConfigurationUserControl()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		bool IAdapterSpecificSettingsPartialView.IsActiveSettings
		{
			get
			{
				return this.Visible;
			}
			set
			{
				this.Visible = value;
			}
		}

		#endregion
	}
}