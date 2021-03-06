﻿/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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

		IFullView IPartialView.FullView
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
					this._.StatusText = "Execution completed.";
			}
		}

		long? IExecutionPartialView.RecordCount
		{
			set
			{
				if ((object)value == null)
					this._.StatusText = "Execution starting...";
				else
					this._.StatusText = string.Format("Executing: {0}...", value);
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

	public class _ExecutableObfuscationDocumentForm : BaseDocumentViewForm<IExecutableObfuscationDocumentView, ExecutableObfuscationDocumentMasterController>
	{
	}
}