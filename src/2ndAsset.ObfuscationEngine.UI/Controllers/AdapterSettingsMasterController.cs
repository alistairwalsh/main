/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;

using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Source;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public sealed class AdapterSettingsMasterController : MasterController<IAdapterSettingsFullView>
	{
		#region Constructors/Destructors

		public AdapterSettingsMasterController()
		{
		}

		#endregion

		#region Methods/Operators

		public override void InitializeView(IAdapterSettingsFullView view)
		{
			IList<IListItem<Type>> typeListItems;

			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);

			this.View.ViewText = string.Format("{0} Studio", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(NullDictionaryAdapter), "Null/Nop (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(DelimitedTextSourceAdapter), "Delimited Text File (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(AdoNetSourceAdapter), "ADO.NET DB Provider (dictionary)"));

			this.View.AdapterSettingsPartialView.AdapterTypes = typeListItems;
			this.View.AdapterSettingsPartialView.SelectedAdapterType = null;

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(OleDbConnection), "OleDbConnection (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(OdbcConnection), "OdbcConnection (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlConnection), "SqlConnection (dictionary)"));

			this.View.StatusText = "Ready";
		}

		public void ProceedNow()
		{
			this.View.CloseView(false);
		}

		[DispatchActionUri(Uri = Constants.URI_ADAPTER_UPDATE_EVENT)]
		public void UpdateAdapter(IPartialView partialView, object context)
		{
			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)partialView == this.View.AdapterSettingsPartialView)
			{
				// do nothing
			}
			else
				throw new InvalidOperationException(string.Format(partialView.GetType().FullName));

			this.View.RefreshView();
		}

		#endregion
	}
}