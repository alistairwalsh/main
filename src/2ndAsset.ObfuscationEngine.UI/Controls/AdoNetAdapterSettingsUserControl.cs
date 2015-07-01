/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	public partial class AdoNetAdapterSettingsUserControl : _AdoNetAdapterSettingsUserControl, IAdoNetAdapterSettingsView
	{
		#region Constructors/Destructors

		public AdoNetAdapterSettingsUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		IEnumerable<IListItem<CommandType?>> IAdoNetAdapterSettingsView.CommandTypes
		{
			set
			{
				this.ddlPreExecuteCommandType.CoreBindSelectionItems(value, true, this.ddlType_SelectedIndexChanged);
				this.ddlExecuteCommandType.CoreBindSelectionItems(value, true, this.ddlType_SelectedIndexChanged);
				this.ddlPostExecuteCommandType.CoreBindSelectionItems(value, true, this.ddlType_SelectedIndexChanged);
			}
		}

		string IAdoNetAdapterSettingsView.ConnectionString
		{
			get
			{
				return this.txtBxConnectionString.CoreGetValue();
			}
			set
			{
				this.txtBxConnectionString.CoreSetValue(value);
			}
		}

		Type IAdoNetAdapterSettingsView.ConnectionType
		{
			get
			{
				return this.ddlConnectionType.CoreGetSelectedValue<Type>();
			}
			set
			{
				this.ddlConnectionType.CoreSetSelectedValue<Type>(value, true);
			}
		}

		IEnumerable<IListItem<Type>> IAdoNetAdapterSettingsView.ConnectionTypes
		{
			set
			{
				this.ddlConnectionType.CoreBindSelectionItems(value, true, this.ddlType_SelectedIndexChanged);
			}
		}

		string IAdoNetAdapterSettingsView.ExecuteCommandText
		{
			get
			{
				return this.txtBxExecuteCommandText.CoreGetValue();
			}
			set
			{
				this.txtBxExecuteCommandText.CoreSetValue(value);
			}
		}

		CommandType? IAdoNetAdapterSettingsView.ExecuteCommandType
		{
			get
			{
				return this.ddlExecuteCommandType.CoreGetSelectedValue<CommandType?>();
			}
			set
			{
				this.ddlExecuteCommandType.CoreSetSelectedValue<CommandType?>(value, true);
			}
		}

		bool ISpecificAdapterSettingsView.IsActiveSettings
		{
			get
			{
				return this.Visible;
			}
			set
			{
				this.Visible = value;
			}
		}

		bool IAdoNetAdapterSettingsView.IsCommandTypeReadOnly
		{
			get
			{
				return !this.ddlExecuteCommandType.Enabled;
			}
			set
			{
				this.ddlExecuteCommandType.Enabled = !value;
			}
		}

		bool IAdoNetAdapterSettingsView.IsConnectionTypeReadOnly
		{
			get
			{
				return !this.ddlConnectionType.Enabled;
			}
			set
			{
				this.ddlConnectionType.Enabled = !value;
			}
		}

		string IAdoNetAdapterSettingsView.PostExecuteCommandText
		{
			get
			{
				return this.txtBxPostExecuteCommandText.CoreGetValue();
			}
			set
			{
				this.txtBxPostExecuteCommandText.CoreSetValue(value);
			}
		}

		CommandType? IAdoNetAdapterSettingsView.PostExecuteCommandType
		{
			get
			{
				return this.ddlPostExecuteCommandType.CoreGetSelectedValue<CommandType?>();
			}
			set
			{
				this.ddlPostExecuteCommandType.CoreSetSelectedValue<CommandType?>(value, true);
			}
		}

		string IAdoNetAdapterSettingsView.PreExecuteCommandText
		{
			get
			{
				return this.txtBxPreExecuteCommandText.CoreGetValue();
			}
			set
			{
				this.txtBxPreExecuteCommandText.CoreSetValue(value);
			}
		}

		CommandType? IAdoNetAdapterSettingsView.PreExecuteCommandType
		{
			get
			{
				return this.ddlPreExecuteCommandType.CoreGetSelectedValue<CommandType?>();
			}
			set
			{
				this.ddlPreExecuteCommandType.CoreSetSelectedValue<CommandType?>(value, true);
			}
		}

		#endregion

		#region Methods/Operators

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			this.FullView.DispatchControllerAction(this, new Uri("action://obfuscation/adapter-settings/ado-net/browse-database-connection"), null);
			this.CoreRefreshControlState();
		}

		private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		#endregion
	}

	public class _AdoNetAdapterSettingsUserControl : _2ndAssetUserControl<IAdoNetAdapterSettingsView>
	{
		#region Constructors/Destructors

		public _AdoNetAdapterSettingsUserControl()
		{
		}

		#endregion
	}
}