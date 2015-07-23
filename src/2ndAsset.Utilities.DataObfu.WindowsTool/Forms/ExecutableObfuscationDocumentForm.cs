/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.UI.Forms;
using _2ndAsset.ObfuscationEngine.UI.Views;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Forms
{
	public partial class ExecutableObfuscationDocumentForm : _ExecutableObfuscationDocumentForm, IExecutableObfuscationDocumentView, IExecutionPartialView
	{
		#region Constructors/Destructors

		public ExecutableObfuscationDocumentForm()
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

		public IExecutionPartialView ExecutionPartialView
		{
			get
			{
				return this;
			}
		}

		IObfuscationPartialView IObfuscationDocumentView.ObfuscationPartialView
		{
			get
			{
				return this.obfuscationUc;
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

		double? IExecutionPartialView.DurationSeconds
		{
			set
			{
				// do nothing
			}
		}

		bool? IExecutionPartialView.IsComplete
		{
			set
			{
				if (value.GetValueOrDefault())
					this.FullView.StatusText = "Execution completed.";
			}
		}

		long? IExecutionPartialView.RecordCount
		{
			set
			{
				if ((object)value == null)
					this.FullView.StatusText = "Execution starting...";
				else
					this.FullView.StatusText = string.Format("Executing: {0}...", value);
			}
		}

		#endregion

		#region Methods/Operators

		void IPartialView._()
		{
			throw new NotImplementedException();
		}

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
			this.UriToControlTypes.Add(ExecutableObfuscationDocumentMasterController.AdapterSettingsViewUri, typeof(AdapterSettingsForm));
		}

		bool IObfuscationDocumentView.TryGetDatabaseConnection(ref Type connectionType, ref string connectionString)
		{
			return DataConnectionConfiguration.TryGetDatabaseConnection(ref connectionType, ref connectionString);
		}

		private void tsmiClose_Click(object sender, EventArgs e)
		{
			this.Controller.CloseDocument();
		}

		private void tsmiExecute_Click(object sender, EventArgs e)
		{
			//this.tabMain.SelectedTab = this.tpExecution;
			this.Controller.ExecuteObfuscation(this, null);
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

	public class _ExecutableObfuscationDocumentForm : BaseDocumentForm<IExecutableObfuscationDocumentView, ExecutableObfuscationDocumentMasterController>
	{
	}
}