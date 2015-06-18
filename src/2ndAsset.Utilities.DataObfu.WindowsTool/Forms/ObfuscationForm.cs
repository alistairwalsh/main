/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Windows.Forms;

using Microsoft.Data.ConnectionUI;

using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Forms
{
	public partial class ObfuscationForm : _ObfuscationForm, IObfuscationView
	{
		#region Constructors/Destructors

		public ObfuscationForm()
		{
			this.InitializeComponent();
			this.CoreIsDirtyIndicator = '*';
		}

		#endregion

		#region Fields/Constants

		private int configurationVersion;
		private string documentFilePath;

		#endregion

		#region Properties/Indexers/Events

		IAvalancheSettingsView IObfuscationView.AvalancheSettings
		{
			get
			{
				return this.avalancheSettingsUc;
			}
		}

		IAdapterSettingsView IObfuscationView.DestinationAdapterSettings
		{
			get
			{
				return this.destinationAdapterSettingsUc;
			}
		}

		IDictionarySettingsView IObfuscationView.DictionarySettings
		{
			get
			{
				return this.dictionarySettingsUc;
			}
		}

		IExecutionView IObfuscationView.Execution
		{
			get
			{
				return this.executionUc;
			}
		}

		IMetadataSettingsView IObfuscationView.MetadataSettings
		{
			get
			{
				return this.metadataSettingsUc;
			}
		}

		IAdapterSettingsView IObfuscationView.SourceAdapterSettings
		{
			get
			{
				return this.sourceAdapterSettingsUc;
			}
		}

		int IObfuscationView.ConfigurationVersion
		{
			get
			{
				return this.configurationVersion;
			}
			set
			{
				this.configurationVersion = value;
			}
		}

		protected override string CoreStatus
		{
			get
			{
				return this.tsslMain.Text;
			}
			set
			{
				this.tsslMain.Text = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreQuit(out bool cancel)
		{
			base.CoreQuit(out cancel);

			// TODO check isDirty

			if (cancel)
				return;
		}

		protected override void CoreSetup()
		{
			base.CoreSetup();

			// UI URIs
			this.UriToControlTypes.Add(ObfuscationController.AdapterSettingsViewUri, typeof(AdapterSettingsForm));
		}

		bool IObfuscationView.TryGetDatabaseConnection(ref Type connectionType, ref string connectionString)
		{
			DialogResult dialogResult;
			Type _connectionType = connectionType;

			using (DataConnectionDialog dataConnectionDialog = new DataConnectionDialog())
			{
				DataConnectionConfiguration dataConnectionConfiguration;

				dataConnectionConfiguration = new DataConnectionConfiguration(null);
				dataConnectionConfiguration.LoadConfiguration(dataConnectionDialog);
				//dataConnectionDialog.ConnectionString = connectionString ?? string.Empty;

				/*var useThisOne = dataConnectionDialog.DataSources.Where(ds => (object)ds.DefaultProvider != null && ds.DefaultProvider.TargetConnectionType == _connectionType).Select(ds => new { DataSource = ds, DataProvider = ds.DefaultProvider }).SingleOrDefault();

				if ((object)useThisOne != null)
				{
					dataConnectionDialog.SelectedDataProvider = useThisOne.DataProvider;
					dataConnectionDialog.SelectedDataSource = useThisOne.DataSource;
					dataConnectionDialog.ConnectionString = connectionString ?? string.Empty;
				}*/

				dialogResult = DataConnectionDialog.Show(dataConnectionDialog);

				if (dialogResult == DialogResult.OK)
				{
					connectionString = dataConnectionDialog.ConnectionString;

					if ((object)dataConnectionDialog.SelectedDataSource != null &&
						(object)dataConnectionDialog.SelectedDataSource.DefaultProvider != null)
						connectionType = dataConnectionDialog.SelectedDataSource.DefaultProvider.TargetConnectionType;
				}
			}

			return dialogResult == DialogResult.OK;
		}

		private void tsmiClose_Click(object sender, EventArgs e)
		{
			this.Controller.CloseDocument();
		}

		private void tsmiExecute_Click(object sender, EventArgs e)
		{
			this.tabMain.SelectedTab = this.tpExecution;
			this.Controller.ExecuteObfuscation(this.executionUc, null);
		}

		private void tsmiSave_Click(object sender, EventArgs e)
		{
			this.Controller.SaveDocument(false);
		}

		private void tsmiSaveAs_Click(object sender, EventArgs e)
		{
			this.Controller.SaveDocument(true);
		}

		#endregion
	}

	public class _ObfuscationForm : _2ndAssetDocumentForm<IObfuscationView, ObfuscationController>
	{
	}
}