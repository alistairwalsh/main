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
using System.Linq;

using Solder.Framework;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Destination;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Source;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Hosting.Tool;
using _2ndAsset.ObfuscationEngine.UI.Controllers;
using _2ndAsset.ObfuscationEngine.UI.Views;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers
{
	public sealed class ExecutableObfuscationDocumentController : ObfuscationDocumentController<IExecutableObfuscationDocumentView>
	{
		#region Constructors/Destructors

		public ExecutableObfuscationDocumentController()
		{
		}

		#endregion

		#region Methods/Operators

		[DispatchActionUri(Uri = "action://obfuscation/execute")]
		public void ExecuteObfuscation(IPartialView partialView, object context)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;

			this.View.ExecutionPartialView.RecordCount = null;
			this.View.ExecutionPartialView.IsComplete = null;
			this.View.ExecutionPartialView.DurationSeconds = null;

			obfuscationConfiguration = this.ApplyViewToDocument();

			messages = obfuscationConfiguration.Validate();

			if ((object)messages != null && messages.Any())
			{
				this.View.ExecutionPartialView.IsComplete = true;
				this.View.ShowMessages(messages, "Configuration validation on execution", false);

				return;
			}

			using (IToolHost toolHost = new ToolHost())
			{
				toolHost.Host(obfuscationConfiguration, (count, complete, duration) =>
														{
															this.View.ExecutionPartialView.RecordCount = count;
															this.View.ExecutionPartialView.IsComplete = complete;
															this.View.ExecutionPartialView.DurationSeconds = duration;
														});
			}

			this.View.ShowAlert("Done.");
		}

		protected override void InitializeDictionaryAdapterView(IDictionarySpecListView view)
		{
			IList<IListItem<Type>> typeListItems;
			IList<IListItem<CommandType?>> commandTypeListItems;

			if ((object)view == null)
				throw new ArgumentNullException("view");

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(DelimitedTextDictionaryAdapter), "Delimited Text File (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(AdoNetDictionaryAdapter), "ADO.NET DB Provider (dictionary)"));

			view.DictionaryAdapterSettingsPartialView.AdapterTypes = typeListItems;
			view.DictionaryAdapterSettingsPartialView.SelectedAdapterType = null;
			view.DictionaryAdapterSettingsPartialView.DelTextAdapterSettingsPartialView.IsActiveSettings = false;
			view.DictionaryAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsActiveSettings = false;

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(OleDbConnection), "OleDbConnection (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(OdbcConnection), "OdbcConnection (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlConnection), "SqlConnection (dictionary)"));

			view.DictionaryAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.ConnectionTypes = typeListItems;
			view.DictionaryAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.ConnectionType = null;

			commandTypeListItems = new List<IListItem<CommandType?>>();
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.Text, "Text (source)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.StoredProcedure, "Stored Procedure (dictionary)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.TableDirect, "Table Direct (dictionary)"));

			view.DictionaryAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.CommandTypes = commandTypeListItems;
			view.DictionaryAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.ExecuteCommandType = null;
		}

		public override void InitializeView(IExecutableObfuscationDocumentView view)
		{
			IList<IListItem<Type>> typeListItems;
			IList<IListItem<CommandType?>> commandTypeListItems;

			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(NullSourceAdapter), "Null/Nop (source)"));
			typeListItems.Add(new ListItem<Type>(typeof(DelimitedTextSourceAdapter), "Delimited Text File (source)"));
			typeListItems.Add(new ListItem<Type>(typeof(AdoNetSourceAdapter), "ADO.NET DB Provider (source)"));

			this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdapterTypes = typeListItems;
			this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType = null;
			this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.DelTextAdapterSettingsPartialView.IsActiveSettings = false;
			this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsActiveSettings = false;

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(OleDbConnection), "OleDbConnection (source)"));
			typeListItems.Add(new ListItem<Type>(typeof(OdbcConnection), "OdbcConnection (source)"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlConnection), "SqlConnection (source)"));

			this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.ConnectionTypes = typeListItems;
			this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.ConnectionType = null;

			commandTypeListItems = new List<IListItem<CommandType?>>();
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.Text, "Text (source)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.StoredProcedure, "Stored Procedure (source)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.TableDirect, "Table Direct (source)"));

			this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.CommandTypes = commandTypeListItems;
			this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.ExecuteCommandType = null;

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(NullDestinationAdapter), "Null/Nop (destination)"));
			typeListItems.Add(new ListItem<Type>(typeof(DelimitedTextDestinationAdapter), "Delimited Text File (destination)"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlBulkCopyAdoNetDestinationAdapter), "SQL Server Bulk Provider (destination)"));

			this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdapterTypes = typeListItems;
			this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType = null;
			this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.DelTextAdapterSettingsPartialView.IsActiveSettings = false;
			this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsActiveSettings = false;

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(OleDbConnection), "OleDbConnection (destination)"));
			typeListItems.Add(new ListItem<Type>(typeof(OdbcConnection), "OdbcConnection (destination)"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlConnection), "SqlConnection (destination)"));

			this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.ConnectionTypes = typeListItems;
			this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.ConnectionType = null;

			commandTypeListItems = new List<IListItem<CommandType?>>();
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.Text, "Text (destination)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.StoredProcedure, "Stored Procedure (destination)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.TableDirect, "Table Direct (destination)"));

			this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.CommandTypes = commandTypeListItems;
			this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.ExecuteCommandType = null;

			this.View.StatusText = "Ready";
		}

		#endregion
	}
}