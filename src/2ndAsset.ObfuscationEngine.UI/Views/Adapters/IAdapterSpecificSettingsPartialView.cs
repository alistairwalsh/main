/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Views.Adapters
{
	public interface IAdapterSpecificSettingsPartialView : IPartialView
	{
		#region Properties/Indexers/Events

		bool IsActiveSettings
		{
			get;
			set;
		}

		#endregion
	}
}