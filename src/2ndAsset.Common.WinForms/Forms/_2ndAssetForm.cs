/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

using TextMetal.Middleware.Common;
using TextMetal.Middleware.Common.Utilities;

using Message = TextMetal.Middleware.Common.Message;

namespace _2ndAsset.Common.WinForms.Forms
{
	public partial class _2ndAssetForm : Form
	{
		#region Constructors/Destructors

		public _2ndAssetForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Fields/Constants

		private bool coreHasShown;
		private bool coreIsDirty;
		private char? coreIsDirtyIndicator;
		private string coreText;

		#endregion

		#region Properties/Indexers/Events

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected _2ndAssetForm CoreOwnerForm
		{
			get
			{
				return (_2ndAssetForm)this.Owner;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected _2ndAssetForm CoreParentForm
		{
			get
			{
				return (_2ndAssetForm)this.ParentForm;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected bool CoreHasShow
		{
			get
			{
				return this.coreHasShown;
			}
			private set
			{
				if (this.coreHasShown || !value)
					throw new InvalidOperationException(string.Format("CoreHasShow property cannot be set more than once and cannot be set to false at anytime."));

				this.coreHasShown = true;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CoreIsDirty
		{
			get
			{
				return this.coreIsDirty;
			}
			set
			{
				this.coreIsDirty = value;

				this.UpdateFormText();
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public char? CoreIsDirtyIndicator
		{
			get
			{
				return this.coreIsDirtyIndicator;
			}
			set
			{
				this.coreIsDirtyIndicator = value;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected virtual string CoreStatus
		{
			get
			{
				return null;
			}
			set
			{
				// do nothing
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string CoreText
		{
			get
			{
				return this.coreText;
			}
			set
			{
				this.coreText = value;

				this.UpdateFormText();
			}
		}

		#endregion

		#region Methods/Operators

		private static MessageBoxIcon MapFromSeverity(Severity severity)
		{
			switch (severity)
			{
				case Severity.None:
					return MessageBoxIcon.None;
				case Severity.Information:
					return MessageBoxIcon.Information;
				case Severity.Warning:
					return MessageBoxIcon.Warning;
				case Severity.Error:
					return MessageBoxIcon.Error;
				case Severity.Hit:
					return MessageBoxIcon.Hand;
				case Severity.Debug:
					return MessageBoxIcon.Exclamation;
				default:
					return MessageBoxIcon.Stop;
			}
		}

		private void AssertExecutionContext()
		{
			//if ((object)ExecutableApplicationFascade.Current == null)
			//throw new InvalidOperationException(string.Format("No executable application context exists on the current thread and application domain."));
		}

		protected virtual void CoreQuit(out bool cancel)
		{
			cancel = false;
			// do nothing
		}

		protected virtual void CoreRefreshControlState()
		{
			// do nothing
		}

		public void CoreSetToolTipText(Control control, string caption)
		{
			this.ttMain.SetToolTip(control, caption);
		}

		protected virtual void CoreSetup()
		{
			// do nothing
		}

		protected virtual void CoreShown()
		{
			// do nothing
		}

		protected virtual void CoreTeardown()
		{
			// do nothing
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			this.CoreTeardown();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			bool cancel;

			base.OnClosing(e);

			if (e.Cancel)
				return;

			this.CoreQuit(out cancel);

			e.Cancel = cancel;
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.CoreSetup();
			this.CoreRefreshControlState();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.AssertExecutionContext();
			this.CoreIsDirty = false;
			this.CoreShown();
			this.CoreHasShow = true;
			this.CoreRefreshControlState();
		}

		protected bool ShowAlert(string text, Severity severity = Severity.None)
		{
			DialogResult dialogResult;

			dialogResult = MessageBox.Show(this, text, ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product, MessageBoxButtons.OK, MapFromSeverity(severity));

			return dialogResult == DialogResult.OK;
		}

		protected bool? ShowAttempt(string text, bool ignorable, Severity severity = Severity.None)
		{
			DialogResult dialogResult;

			dialogResult = MessageBox.Show(this, text, ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product, ignorable ? MessageBoxButtons.AbortRetryIgnore : MessageBoxButtons.RetryCancel, MapFromSeverity(severity));

			if (ignorable)
				return dialogResult == DialogResult.Abort ? null : (bool?)(dialogResult == DialogResult.Retry);
			else
				return dialogResult == DialogResult.Retry;
		}

		protected bool ShowConfirm(string text, Severity severity = Severity.None)
		{
			DialogResult dialogResult;

			dialogResult = MessageBox.Show(this, text, ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product, MessageBoxButtons.OKCancel, MapFromSeverity(severity));

			return dialogResult == DialogResult.OK;
		}

		protected bool? ShowMessages(IEnumerable<Message> messages, string text, bool cancelable)
		{
			DialogResult dialogResult;

			using (MessageForm messageForm = new MessageForm())
			{
				messageForm.CoreText = string.Format("Message List - {0} Studio", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);
				messageForm.Message = text;
				messageForm.Messages = messages;
				messageForm.IsCancelAllowed = cancelable;
				dialogResult = messageForm.ShowDialog(this);
			}

			return dialogResult == DialogResult.OK;
		}

		protected bool TryGetOpenFilePath(out string filePath)
		{
			DialogResult dialogResult;

			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Title = string.Format("Open - {0} Studio", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);
				openFileDialog.FileName = filePath = null;
				dialogResult = openFileDialog.ShowDialog(this);

				if (dialogResult != DialogResult.OK ||
					DataTypeFascade.Instance.IsNullOrWhiteSpace(openFileDialog.FileName))
					return false;

				filePath = Path.GetFullPath(openFileDialog.FileName);
				return true;
			}
		}

		protected bool TryGetSaveFilePath(out string filePath)
		{
			DialogResult dialogResult;

			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Title = string.Format("Save As - {0} Studio", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);
				saveFileDialog.FileName = filePath = null;
				dialogResult = saveFileDialog.ShowDialog(this);

				if (dialogResult != DialogResult.OK ||
					DataTypeFascade.Instance.IsNullOrWhiteSpace(saveFileDialog.FileName))
					return false;

				filePath = Path.GetFullPath(saveFileDialog.FileName);
				return true;
			}
		}

		private void UpdateFormText()
		{
			this.Text = string.Format("{0}{1}", this.CoreText.SafeToString(), this.CoreIsDirty ? this.CoreIsDirtyIndicator.SafeToString() : string.Empty);
		}

		#endregion
	}
}