/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;

using _2ndAsset.Common.WinForms;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;
using _2ndAsset.ObfuscationEngine.UI.Views;
using _2ndAsset.ObfuscationEngine.UI.Views.Adapters;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers.Adapters
{
	public sealed class AdoNetAdapterSettingsSlaveController : AdapterSpecificSettingsSlaveController<IAdoNetAdapterSettingsPartialView>
	{
		#region Constructors/Destructors

		public AdoNetAdapterSettingsSlaveController()
		{
		}

		#endregion

		#region Methods/Operators

		private static void _ApplyDocumentToViewAdoNet(AdoNetAdapterConfiguration adoNetAdapterConfiguration, IAdoNetAdapterSettingsPartialView adoNetAdapterSettingsPartialView)
		{
			if ((object)adoNetAdapterConfiguration == null)
				throw new ArgumentNullException("adoNetAdapterConfiguration");

			if ((object)adoNetAdapterSettingsPartialView == null)
				throw new ArgumentNullException("adoNetAdapterSettingsPartialView");

			adoNetAdapterSettingsPartialView.ConnectionString = adoNetAdapterConfiguration.ConnectionString;
			adoNetAdapterSettingsPartialView.ConnectionType = adoNetAdapterConfiguration.GetConnectionType();

			adoNetAdapterSettingsPartialView.PreExecuteCommandText = adoNetAdapterConfiguration.PreExecuteCommandText;
			adoNetAdapterSettingsPartialView.PreExecuteCommandType = adoNetAdapterConfiguration.PreExecuteCommandType;

			adoNetAdapterSettingsPartialView.ExecuteCommandText = adoNetAdapterConfiguration.ExecuteCommandText;
			adoNetAdapterSettingsPartialView.ExecuteCommandType = adoNetAdapterConfiguration.ExecuteCommandType;

			adoNetAdapterSettingsPartialView.PostExecuteCommandText = adoNetAdapterConfiguration.PostExecuteCommandText;
			adoNetAdapterSettingsPartialView.PostExecuteCommandType = adoNetAdapterConfiguration.PostExecuteCommandType;
		}

		private static void _ApplyViewToDocumentAdoNet(IAdoNetAdapterSettingsPartialView adoNetAdapterSettingsPartialView, AdoNetAdapterConfiguration adoNetAdapterConfiguration)
		{
			if ((object)adoNetAdapterSettingsPartialView == null)
				throw new ArgumentNullException("adoNetAdapterSettingsPartialView");

			if ((object)adoNetAdapterConfiguration == null)
				throw new ArgumentNullException("adoNetAdapterConfiguration");

			if ((object)adoNetAdapterSettingsPartialView.ConnectionType != null)
				adoNetAdapterConfiguration.ConnectionAqtn = adoNetAdapterSettingsPartialView.ConnectionType.AssemblyQualifiedName;

			adoNetAdapterConfiguration.ConnectionString = adoNetAdapterSettingsPartialView.ConnectionString;

			adoNetAdapterConfiguration.PreExecuteCommandText = adoNetAdapterSettingsPartialView.PreExecuteCommandText;
			adoNetAdapterConfiguration.PreExecuteCommandType = adoNetAdapterSettingsPartialView.PreExecuteCommandType;

			adoNetAdapterConfiguration.ExecuteCommandText = adoNetAdapterSettingsPartialView.ExecuteCommandText;
			adoNetAdapterConfiguration.ExecuteCommandType = adoNetAdapterSettingsPartialView.ExecuteCommandType;

			adoNetAdapterConfiguration.PostExecuteCommandText = adoNetAdapterSettingsPartialView.PostExecuteCommandText;
			adoNetAdapterConfiguration.PostExecuteCommandType = adoNetAdapterSettingsPartialView.PostExecuteCommandType;
		}

		private void ApplyDocumentToViewAdoNetDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//if ((object)obfuscationConfiguration.DestinationAdapterConfiguration != null &&
			//	(object)obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration != null)
			//	_ApplyDocumentToViewAdoNet(obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration, this.View.ObfuscationPartialView.DestinationAdapterSettings.AdoNetAdapterSettingsView);
		}

		private void ApplyDocumentToViewAdoNetDictionary(DictionaryConfiguration dictionaryConfiguration, IAdapterSettingsPartialView adapterSettingsPartialView)
		{
			//if ((object)dictionaryConfiguration == null)
			//	throw new ArgumentNullException("dictionaryConfiguration");

			//if ((object)adapterSettingsView == null)
			//	throw new ArgumentNullException("adapterSettingsView");

			//if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration != null &&
			//	(object)dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration != null)
			//	_ApplyDocumentToViewAdoNet(dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration, adapterSettingsView.AdoNetAdapterSettingsView);
		}

		private void ApplyDocumentToViewAdoNetSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//if ((object)obfuscationConfiguration.SourceAdapterConfiguration != null &&
			//	(object)obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration != null)
			//	_ApplyDocumentToViewAdoNet(obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration, this.View.ObfuscationPartialView.SourceAdapterSettings.AdoNetAdapterSettingsView);
		}

		private void ApplyViewToDocumentAdoNetDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration = new AdoNetAdapterConfiguration();

			//_ApplyViewToDocumentAdoNet(this.View.ObfuscationPartialView.DestinationAdapterSettings.AdoNetAdapterSettingsView, obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration);
		}

		private void ApplyViewToDocumentAdoNetSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration = new AdoNetAdapterConfiguration();

			//_ApplyViewToDocumentAdoNet(this.View.ObfuscationPartialView.SourceAdapterSettings.AdoNetAdapterSettingsView, obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration);
		}

		public void BrowseDatabaseConnection()
		{
			Type connectionType;
			string connectionString;

			connectionType = this.View.ConnectionType;
			connectionString = this.View.ConnectionString;

			if (!((IObfuscationDocumentView)this.View.FullView).TryGetDatabaseConnection(ref connectionType, ref connectionString))
				return;

			this.View.ConnectionType = connectionType;
			this.View.ConnectionString = connectionString;
		}

		public override void InitializeView(IAdoNetAdapterSettingsPartialView view)
		{
			IList<IListItem<Type>> typeListItems;
			IList<IListItem<CommandType?>> commandTypeListItems;

			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);

			this.View.IsActiveSettings = false;

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(OleDbConnection), "OleDbConnection (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(OdbcConnection), "OdbcConnection (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlConnection), "SqlConnection (dictionary)"));

			this.View.ConnectionTypes = typeListItems;
			this.View.ConnectionType = null;

			commandTypeListItems = new List<IListItem<CommandType?>>();
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.Text, "Text (dictionary)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.StoredProcedure, "Stored Procedure (dictionary)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.TableDirect, "Table Direct (dictionary)"));

			this.View.CommandTypes = commandTypeListItems;
			this.View.ExecuteCommandType = null;
		}

		#endregion
	}
}