/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.ComponentModel;
using System.Windows.Forms;

using _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities;

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

		private void UpdateFormText()
		{
			this.Text = string.Format("{0}{1}", this.CoreText.SafeToString(), this.CoreIsDirty ? this.CoreIsDirtyIndicator.SafeToString() : string.Empty);
		}

		#endregion
	}
}