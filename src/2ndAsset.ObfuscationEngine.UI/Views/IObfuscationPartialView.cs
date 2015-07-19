/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Presentation;

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