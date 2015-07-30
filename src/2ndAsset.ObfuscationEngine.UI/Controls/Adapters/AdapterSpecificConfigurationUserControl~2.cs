/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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