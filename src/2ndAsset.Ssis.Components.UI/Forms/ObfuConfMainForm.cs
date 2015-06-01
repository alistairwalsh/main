using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using TextMetal.Middleware.Common.Utilities;

using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.ObfuscationEngine.Core.Config;

using Message = TextMetal.Middleware.Common.Message;

namespace _2ndAsset.Ssis.Components.UI.Forms
{
	public partial class ObfuConfMainForm : _2ndAssetForm
	{
		#region Constructors/Destructors

		public ObfuConfMainForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Fields/Constants

		private ObfuscationConfiguration obfuscationConfiguration;

		#endregion

		#region Properties/Indexers/Events

		public ObfuscationConfiguration ObfuscationConfiguration
		{
			get
			{
				return this.obfuscationConfiguration;
			}
			set
			{
				this.obfuscationConfiguration = value;
			}
		}

		#endregion

		#region Methods/Operators

		private void ApplyModelToView()
		{
			this.pgRoot.SelectedObject = this.ObfuscationConfiguration;
		}

		private void ApplyViewToModel()
		{
			if (this.CoreIsDirty)
				this.CoreIsDirty = false;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			try
			{
				this.Cancel();
			}
			catch (Exception ex)
			{
				MessageBox.Show("A fatal error occured:" + Environment.NewLine + (object)ex == null ? ReflectionFascade.Instance.GetErrors(ex, 0) : "<unknown>" + Environment.NewLine + "The application will now terminate.", this.CoreText, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			try
			{
			}
			catch (Exception ex)
			{
				MessageBox.Show("A fatal error occured:" + Environment.NewLine + (object)ex == null ? ReflectionFascade.Instance.GetErrors(ex, 0) : "<unknown>" + Environment.NewLine + "The application will now terminate.", this.CoreText, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			try
			{
			}
			catch (Exception ex)
			{
				MessageBox.Show("A fatal error occured:" + Environment.NewLine + (object)ex == null ? ReflectionFascade.Instance.GetErrors(ex, 0) : "<unknown>" + Environment.NewLine + "The application will now terminate.", this.CoreText, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				this.Okay();
			}
			catch (Exception ex)
			{
				MessageBox.Show("A fatal error occured:" + Environment.NewLine + (object)ex == null ? ReflectionFascade.Instance.GetErrors(ex, 0) : "<unknown>" + Environment.NewLine + "The application will now terminate.", this.CoreText, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnUpdateMetadata_Click(object sender, EventArgs e)
		{
			try
			{
				this.UpdateMetadata();
			}
			catch (Exception ex)
			{
				MessageBox.Show("A fatal error occured:" + Environment.NewLine + (object)ex == null ? ReflectionFascade.Instance.GetErrors(ex, 0) : "<unknown>" + Environment.NewLine + "The application will now terminate.", this.CoreText, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Cancel()
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close(); // direct
		}

		private void CloseCheckDirty(out bool cancel)
		{
			cancel = false;

			if (this.CoreIsDirty)
			{
				if (MessageBox.Show(this, string.Format("Do you want close without saving the obfuscation configuration?"), this.CoreText, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
					cancel = true;
			}
		}

		protected override void CoreQuit(out bool cancel)
		{
			base.CoreQuit(out cancel);

			if (cancel)
				return;

			this.CloseCheckDirty(out cancel);
		}

		protected override void CoreSetup()
		{
			base.CoreSetup();

			this.CoreText = string.Format("{0}", Constants.COMPONENT_NAME);

			this.ApplyModelToView();
			this.RefreshControlState();
		}

		protected override void CoreTeardown()
		{
			base.CoreTeardown();
		}

		private void Okay()
		{
			IEnumerable<Message> messages;

			messages = this.ObfuscationConfiguration.Validate();

			if (messages.Any())
			{
				using (MessageForm messageForm = new MessageForm()
												{
													Text = string.Format("{0} - Message(s)", this.Text),
													Message = string.Empty,
													Messages = messages.ToArray()
												})
					messageForm.ShowDialog(this);

				return;
			}

			this.ApplyViewToModel();

			this.DialogResult = DialogResult.OK;
			this.Close(); // direct
		}

		private void pgRoot_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			// TODO does not handle collections
			this.CoreIsDirty = true;
		}

		private void RefreshControlState()
		{
			this.CoreIsDirty = false;
		}

		private void UpdateMetadata()
		{
			if (MessageBox.Show(this, string.Format("Do you want to update the component with the current upstream metadata?"), this.CoreText, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				return;

			this.pgRoot.Refresh();
		}

		#endregion
	}
}