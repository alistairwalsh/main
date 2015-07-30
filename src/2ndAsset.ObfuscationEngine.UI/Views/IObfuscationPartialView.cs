/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IObfuscationPartialView : IPartialView
	{
		#region Properties/Indexers/Events

		IAvalancheSettingsPartialView AvalancheSettingsPartialView
		{
			get;
		}

		IAdapterSettingsPartialView DestinationAdapterSettingsPartialView
		{
			get;
		}

		IDictionarySettingsPartialView DictionarySettingsPartialView
		{
			get;
		}

		IMetadataSettingsPartialView MetadataSettingsPartialView
		{
			get;
		}

		IAdapterSettingsPartialView SourceAdapterSettingsPartialView
		{
			get;
		}

		string ConfigurationVersion
		{
			get;
			set;
		}

		#endregion
	}
}