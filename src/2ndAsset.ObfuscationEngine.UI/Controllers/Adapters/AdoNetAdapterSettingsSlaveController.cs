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
			typeListItems.Add(new ListItem<Type>(typeof(OleDbConnection), "OleDbConnection"));
			typeListItems.Add(new ListItem<Type>(typeof(OdbcConnection), "OdbcConnection"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlConnection), "SqlConnection"));

			this.View.ConnectionTypes = typeListItems;
			this.View.ConnectionType = null;

			commandTypeListItems = new List<IListItem<CommandType?>>();
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.Text, "Text"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.StoredProcedure, "Stored Procedure"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.TableDirect, "Table Direct"));

			this.View.CommandTypes = commandTypeListItems;
			this.View.ExecuteCommandType = null;
		}

		#endregion
	}
}