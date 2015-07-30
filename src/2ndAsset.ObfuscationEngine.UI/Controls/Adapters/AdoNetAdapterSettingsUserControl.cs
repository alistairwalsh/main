/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.ObfuscationEngine.UI.Controllers.Adapters;
using _2ndAsset.ObfuscationEngine.UI.Views.Adapters;

namespace _2ndAsset.ObfuscationEngine.UI.Controls.Adapters
{
	public partial class AdoNetAdapterSettingsUserControl : _AdoNetAdapterSettingsUserControl, IAdoNetAdapterSettingsPartialView
	{
		#region Constructors/Destructors

		public AdoNetAdapterSettingsUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		IEnumerable<IListItem<CommandType?>> IAdoNetAdapterSettingsPartialView.CommandTypes
		{
			set
			{
				this.ddlPreExecuteCommandType.CoreBindSelectionItems(value, true, this.ddlType_SelectedIndexChanged);
				this.ddlExecuteCommandType.CoreBindSelectionItems(value, true, this.ddlType_SelectedIndexChanged);
				this.ddlPostExecuteCommandType.CoreBindSelectionItems(value, true, this.ddlType_SelectedIndexChanged);
			}
		}

		string IAdoNetAdapterSettingsPartialView.ConnectionString
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

		Type IAdoNetAdapterSettingsPartialView.ConnectionType
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

		IEnumerable<IListItem<Type>> IAdoNetAdapterSettingsPartialView.ConnectionTypes
		{
			set
			{
				this.ddlConnectionType.CoreBindSelectionItems(value, true, this.ddlType_SelectedIndexChanged);
			}
		}

		string IAdoNetAdapterSettingsPartialView.ExecuteCommandText
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

		CommandType? IAdoNetAdapterSettingsPartialView.ExecuteCommandType
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

		bool IAdoNetAdapterSettingsPartialView.IsCommandTypeReadOnly
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

		bool IAdoNetAdapterSettingsPartialView.IsConnectionTypeReadOnly
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

		string IAdoNetAdapterSettingsPartialView.PostExecuteCommandText
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

		CommandType? IAdoNetAdapterSettingsPartialView.PostExecuteCommandType
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

		string IAdoNetAdapterSettingsPartialView.PreExecuteCommandText
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

		CommandType? IAdoNetAdapterSettingsPartialView.PreExecuteCommandType
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
			this.Controller.BrowseDatabaseConnection();
			this.CoreRefreshControlState();
		}

		private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		#endregion
	}

	public class _AdoNetAdapterSettingsUserControl : AdapterSpecificConfigurationUserControl<IAdoNetAdapterSettingsPartialView, AdoNetAdapterSettingsSlaveController>
	{
		#region Constructors/Destructors

		public _AdoNetAdapterSettingsUserControl()
		{
		}

		#endregion
	}
}