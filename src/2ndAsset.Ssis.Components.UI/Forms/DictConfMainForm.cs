using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using TextMetal.Middleware.Common.Utilities;

using _2ndAsset.Common.WinForms.Forms;

using Message = TextMetal.Middleware.Common.Message;

namespace _2ndAsset.Ssis.Components.UI.Forms
{
	public partial class DictConfMainForm : _2ndAssetForm
	{
		#region Constructors/Destructors

		public DictConfMainForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Fields/Constants

		private string editValue;
		private object resultValue;

		#endregion

		#region Properties/Indexers/Events

		public string EditValue
		{
			get
			{
				return this.editValue;
			}
			set
			{
				this.editValue = value;
			}
		}

		#endregion

		#region Methods/Operators

		private void AddItem()
		{
			ListViewItem lvi;
			DictionaryMetadataWrapper item;

			item = new DictionaryMetadataWrapper();
			lvi = new ListViewItem(new string[] { item.DictionaryId, item.RecordCount.ToString(), item.CommandText });
			lvi.Tag = item;

			this.lvMain.Items.Add(lvi);
		}

		private void ApplyModelToView()
		{
			IEnumerable<DictionaryMetadataWrapper> dictionaryMetadataWrappers;
			ListViewItem lvItem;

			dictionaryMetadataWrappers = DictionaryMetadataWrapper.FromJson(this.EditValue);

			this.lvMain.Items.Clear();
			if ((object)dictionaryMetadataWrappers != null)
			{
				foreach (var dictionaryMetadataWrapper in dictionaryMetadataWrappers)
				{
					lvItem = new ListViewItem(new string[] { dictionaryMetadataWrapper.DictionaryId, dictionaryMetadataWrapper.RecordCount.ToString(), dictionaryMetadataWrapper.CommandText });
					lvItem.Tag = dictionaryMetadataWrapper;
					this.lvMain.Items.Add(lvItem);
				}
			}

			this.pgRoot.SelectedObject = null;
			this.pgRoot.Refresh();
		}

		private void ApplyViewToModel()
		{
			List<DictionaryMetadataWrapper> dictionaryMetadataWrappers;

			if (this.CoreIsDirty)
				this.CoreIsDirty = false;

			dictionaryMetadataWrappers = new List<DictionaryMetadataWrapper>();

			foreach (ListViewItem lvItem in this.lvMain.Items)
				dictionaryMetadataWrappers.Add((DictionaryMetadataWrapper)lvItem.Tag);

			this.EditValue = DictionaryMetadataWrapper.ToJson(dictionaryMetadataWrappers);
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			try
			{
				this.AddItem();
			}
			catch (Exception ex)
			{
				MessageBox.Show("A fatal error occured:" + Environment.NewLine + (object)ex == null ? ReflectionFascade.Instance.GetErrors(ex, 0) : "<unknown>" + Environment.NewLine + "The application will now terminate.", this.CoreText, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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

		private void btnClear_Click(object sender, EventArgs e)
		{
			try
			{
				this.ClearItems();
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

		private void btnRemove_Click(object sender, EventArgs e)
		{
			try
			{
				this.RemoveItem();
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

		private void ClearItems()
		{
			this.pgRoot.SelectedObject = null;
			this.lvMain.Items.Clear();
		}

		private void CloseCheckDirty(out bool cancel)
		{
			cancel = false;

			if (this.CoreIsDirty)
			{
				if (MessageBox.Show(this, string.Format("Do you want close without saving the dictionary configuration?"), this.CoreText, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
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

		private void EditItem()
		{
			if (this.lvMain.SelectedItems.Count != 1)
				this.pgRoot.SelectedObject = null;
			else
				this.pgRoot.SelectedObject = (DictionaryMetadataWrapper)this.lvMain.SelectedItems[0].Tag;
		}

		private void ItemUpdated()
		{
			DictionaryMetadataWrapper dictionaryMetadataWrapper;
			this.CoreIsDirty = true;

			if (this.lvMain.SelectedItems.Count == 1)
			{
				dictionaryMetadataWrapper = (DictionaryMetadataWrapper)this.lvMain.SelectedItems[0].Tag;

				if ((object)dictionaryMetadataWrapper != null)
				{
					this.lvMain.SelectedItems[0].SubItems[0].Text = dictionaryMetadataWrapper.DictionaryId;
					this.lvMain.SelectedItems[0].SubItems[1].Text = dictionaryMetadataWrapper.RecordCount.ToString();
					this.lvMain.SelectedItems[0].SubItems[2].Text = dictionaryMetadataWrapper.CommandText;
				}
			}
		}

		private void lvMain_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				this.EditItem();
			}
			catch (Exception ex)
			{
				MessageBox.Show("A fatal error occured:" + Environment.NewLine + (object)ex == null ? ReflectionFascade.Instance.GetErrors(ex, 0) : "<unknown>" + Environment.NewLine + "The application will now terminate.", this.CoreText, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Okay()
		{
			IEnumerable<Message> messages;

			messages = new Message[] { };

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
			try
			{
				this.ItemUpdated();
			}
			catch (Exception ex)
			{
				MessageBox.Show("A fatal error occured:" + Environment.NewLine + (object)ex == null ? ReflectionFascade.Instance.GetErrors(ex, 0) : "<unknown>" + Environment.NewLine + "The application will now terminate.", this.CoreText, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void RefreshControlState()
		{
			this.CoreIsDirty = false;
		}

		private void RemoveItem()
		{
			int index;

			if (this.lvMain.SelectedItems.Count == 1)
			{
				index = this.lvMain.SelectedIndices[0];
				this.lvMain.SelectedItems[0].Remove();

				if (0 <= (index - 1) && (index - 1) < this.lvMain.Items.Count)
					this.lvMain.Items[index - 1].Selected = true;
			}
		}

		#endregion
	}
}