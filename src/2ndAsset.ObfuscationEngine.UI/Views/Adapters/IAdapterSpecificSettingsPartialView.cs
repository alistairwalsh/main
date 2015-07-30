/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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