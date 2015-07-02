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

using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Source;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public sealed class AdapterSettingsController : BaseController<IAdapterSettingsView2>
	{
		#region Constructors/Destructors

		public AdapterSettingsController()
		{
		}

		#endregion

		#region Methods/Operators

		public override void InitializeView(IAdapterSettingsView2 view)
		{
			IList<IListItem<Type>> typeListItems;
			IList<IListItem<CommandType?>> commandTypeListItems;

			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);

			this.View.ViewText = string.Format("{0} Studio", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(NullDictionaryAdapter), "Null/Nop (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(DelimitedTextSourceAdapter), "Delimited Text File (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(AdoNetSourceAdapter), "ADO.NET DB Provider (dictionary)"));

			this.View.DictionaryAdapterSettings.AdapterTypes = typeListItems;
			this.View.DictionaryAdapterSettings.SelectedAdapterType = null;
			this.View.DictionaryAdapterSettings.DelTextAdapterSettingsView.IsActiveSettings = false;
			this.View.DictionaryAdapterSettings.AdoNetAdapterSettingsView.IsActiveSettings = false;

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(OleDbConnection), "OleDbConnection (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(OdbcConnection), "OdbcConnection (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlConnection), "SqlConnection (dictionary)"));

			this.View.DictionaryAdapterSettings.AdoNetAdapterSettingsView.ConnectionTypes = typeListItems;
			this.View.DictionaryAdapterSettings.AdoNetAdapterSettingsView.ConnectionType = null;

			commandTypeListItems = new List<IListItem<CommandType?>>();
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.Text, "Text (dictionary)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.StoredProcedure, "Stored Procedure (dictionary)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.TableDirect, "Table Direct (dictionary)"));

			this.View.DictionaryAdapterSettings.AdoNetAdapterSettingsView.CommandTypes = commandTypeListItems;
			this.View.DictionaryAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandType = null;

			this.View.StatusText = "Ready";
		}

		public void ProceedNow()
		{
			this.View.CloseView(false);
		}

		[DispatchActionUri(Uri = "action://obfuscation/adapter/update")]
		public void UpdateAdapter(IPartialView partialView, object context)
		{
			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)partialView == this.View.DictionaryAdapterSettings)
			{
				this.View.DictionaryAdapterSettings.DelTextAdapterSettingsView.IsActiveSettings = false;
				this.View.DictionaryAdapterSettings.AdoNetAdapterSettingsView.IsActiveSettings = false;
				this.View.DictionaryAdapterSettings.AdoNetAdapterSettingsView.IsConnectionTypeReadOnly = false;
				this.View.DictionaryAdapterSettings.AdoNetAdapterSettingsView.IsCommandTypeReadOnly = false;

				if ((object)this.View.DictionaryAdapterSettings.SelectedAdapterType == null)
				{
					// do nothing
				}
				else if (this.View.DictionaryAdapterSettings.SelectedAdapterType == typeof(NullDictionaryAdapter))
				{
					// do nothing
				}
				else if (this.View.DictionaryAdapterSettings.SelectedAdapterType == typeof(DelimitedTextSourceAdapter))
					this.View.DictionaryAdapterSettings.DelTextAdapterSettingsView.IsActiveSettings = true;
				else if (this.View.DictionaryAdapterSettings.SelectedAdapterType == typeof(AdoNetSourceAdapter))
					this.View.DictionaryAdapterSettings.AdoNetAdapterSettingsView.IsActiveSettings = true;
				else
					throw new InvalidOperationException(string.Format("Unrecognized dictionary adapter UI-view AQTN '{0}'.", partialView.GetType().FullName));
			}
			else
				throw new InvalidOperationException(string.Format(partialView.GetType().FullName));

			this.View.RefreshView();
		}

		#endregion
	}
}