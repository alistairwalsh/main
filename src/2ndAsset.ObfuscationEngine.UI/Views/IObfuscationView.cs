/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IObfuscationView : IDocumentView
	{
		#region Properties/Indexers/Events

		IAvalancheSettingsView AvalancheSettings
		{
			get;
		}

		IAdapterSettingsView DestinationAdapterSettings
		{
			get;
		}

		IDictionarySettingsView DictionarySettings
		{
			get;
		}

		IExecutionView Execution
		{
			get;
		}

		IMetadataSettingsView MetadataSettings
		{
			get;
		}

		IAdapterSettingsView SourceAdapterSettings
		{
			get;
		}

		int ConfigurationVersion
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		bool TryGetDatabaseConnection(ref Type connectionType, ref string connectionString);

		#endregion
	}
}