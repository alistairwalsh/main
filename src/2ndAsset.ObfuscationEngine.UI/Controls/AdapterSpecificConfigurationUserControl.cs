/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	public class AdapterSpecificConfigurationUserControl<TAdapterSpecificConfigurationPartialView> : BaseUserControl<TAdapterSpecificConfigurationPartialView>
		where TAdapterSpecificConfigurationPartialView : class, IAdapterSpecificSettingsPartialView
	{
		#region Constructors/Destructors

		public AdapterSpecificConfigurationUserControl()
		{
		}

		#endregion
	}
}