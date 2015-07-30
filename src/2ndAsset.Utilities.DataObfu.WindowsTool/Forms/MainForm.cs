/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Linq;
using System.Windows.Forms;

using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Forms
{
	public partial class MainForm : _MainForm, IMainView
	{
		#region Constructors/Destructors

		public MainForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

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

		protected override void CoreDocumentFormClosed(BaseForm form)
		{
			ToolStripMenuItem tsmiWindow;

			if ((object)form == null)
				throw new ArgumentNullException("form");

			base.CoreDocumentFormClosed(form);

			tsmiWindow = this.tsmiDocumentWindows.DropDownItems.OfType<ToolStripMenuItem>().SingleOrDefault(tsmi => (object)tsmi.Tag == form);

			if ((object)tsmiWindow == null)
				throw new InvalidOperationException(string.Format("ToolStripMenuItem was null."));

			tsmiWindow.Tag = null;
			tsmiWindow.Click -= this.tsmiWindow_Click;
			this.tsmiDocumentWindows.DropDownItems.Remove(tsmiWindow);
			tsmiWindow.Dispose();
		}

		protected override void CoreDocumentFormLoaded(BaseForm form)
		{
			ToolStripMenuItem tsmiWindow;

			if ((object)form == null)
				throw new ArgumentNullException("form");

			base.CoreDocumentFormLoaded(form);

			tsmiWindow = new ToolStripMenuItem();
			tsmiWindow.Text = form.Text;
			tsmiWindow.Tag = form;
			tsmiWindow.Click += this.tsmiWindow_Click;
			this.tsmiDocumentWindows.DropDownItems.Add(tsmiWindow);
		}

		protected override void CoreDocumentFormTextChanged(BaseForm form)
		{
			ToolStripMenuItem tsmiWindow;

			if ((object)form == null)
				throw new ArgumentNullException("form");

			base.CoreDocumentFormTextChanged(form);

			tsmiWindow = this.tsmiDocumentWindows.DropDownItems.OfType<ToolStripMenuItem>().SingleOrDefault(tsmi => (object)tsmi.Tag == form);

			if ((object)tsmiWindow == null)
				throw new InvalidOperationException();

			tsmiWindow.Text = form.Text;
		}

		protected override void CoreQuit(out bool cancel)
		{
			base.CoreQuit(out cancel);

			if (cancel)
				return;

			cancel = this.Controller.CloseAllDocuments("quit");
		}

		protected override void CoreRefreshControlState()
		{
			base.CoreRefreshControlState();

			this.tsmiNew.Enabled = true;
			this.tsmiOpen.Enabled = true;
			this.tsmiCloseAll.Enabled = this.HasAnyDocuments;
			this.tsmiSaveAll.Enabled = this.HasAnyDocuments;
			this.tsmiDocumentWindows.Enabled = this.HasAnyDocuments;
			this.tsmiExit.Enabled = true;
		}

		protected override void CoreSetup()
		{
			base.CoreSetup();

			// UI URIs
			this.UriToControlTypes.Add(MainController.AboutBoxViewUri, typeof(AboutForm));
			this.UriToControlTypes.Add(MainController.DocumentViewUri, typeof(ExecutableObfuscationDocumentForm));
		}

		protected override void CoreShown()
		{
			base.CoreShown();

			this.CoreRefreshControlState();
		}

		private void tsmiAbout_Click(object sender, EventArgs e)
		{
			this.Controller.AboutBox();
		}

		private void tsmiCloseAll_Click(object sender, EventArgs e)
		{
			this.Controller.CloseAllDocuments("close all documents");
		}

		private void tsmiExit_Click(object sender, EventArgs e)
		{
			this.Controller.QuitNow();
		}

		private void tsmiNew_Click(object sender, EventArgs e)
		{
			this.Controller.NewDocument();
		}

		private void tsmiOpen_Click(object sender, EventArgs e)
		{
			this.Controller.OpenDocument();
		}

		private void tsmiSaveAll_Click(object sender, EventArgs e)
		{
			this.Controller.SaveAllDocuments();
		}

		private void tsmiTopics_Click(object sender, EventArgs e)
		{
			this.Controller.HelpTopics();
		}

		private void tsmiWindow_Click(object sender, EventArgs e)
		{
			ExecutableObfuscationDocumentForm executableObfuscationDocumentForm;
			ToolStripMenuItem tsmiWindow;

			tsmiWindow = (ToolStripMenuItem)sender;
			executableObfuscationDocumentForm = (ExecutableObfuscationDocumentForm)tsmiWindow.Tag;

			executableObfuscationDocumentForm.BringToFront();
		}

		#endregion
	}

	public class _MainForm : BaseMultiDocumentForm<IMainView, MainController>
	{
		#region Constructors/Destructors

		public _MainForm()
		{
		}

		#endregion
	}
}