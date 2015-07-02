/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.ObfuscationEngine.UI.Forms;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	public partial class DictionarySettingsUserControl : _DictionarySettingsUserControl, IDictionarySettingsView
	{
		#region Constructors/Destructors

		public DictionarySettingsUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		public IEnumerable<IDictionarySpecView> DictionarySpecViews
		{
			get
			{
				if ((object)this.lvDictionarySpecs.Items != null)
				{
					foreach (DictionaryListViewItem lviDictionarySpec in this.lvDictionarySpecs.Items.Cast<DictionaryListViewItem>())
						yield return lviDictionarySpec;
				}
			}
		}

		public IDictionarySpecView SelectedDictionarySpecView
		{
			get
			{
				if (this.lvDictionarySpecs.SelectedItems.Count != 1)
					return null;

				return this.lvDictionarySpecs.SelectedItems[0] as IDictionarySpecView;
			}
			set
			{
				DictionaryListViewItem lviDictionarySpec;

				lviDictionarySpec = value as DictionaryListViewItem;

				this.lvDictionarySpecs.SelectedItems.Clear();

				if ((object)lviDictionarySpec != null)
					lviDictionarySpec.Selected = true;
			}
		}

		#endregion

		#region Methods/Operators

		IDictionarySpecView IDictionarySettingsView.AddDictionarySpecView(string dictionaryId, bool preloadEnabled, long? recordCount, IAdapterSettingsView adapterSettingsView)
		{
			DictionaryListViewItem lviDictionarySpec;
			AdapterSettingsForm adapterSettingsForm;

			adapterSettingsForm = new AdapterSettingsForm();

			lviDictionarySpec = new DictionaryListViewItem(new string[] { dictionaryId.SafeToString(), preloadEnabled.SafeToString(), recordCount.SafeToString(), string.Empty }, adapterSettingsForm);
			lviDictionarySpec.Tag = new DictionarySpec()
									{
										DictionaryId = dictionaryId.SafeToString(),
										RecordCount = recordCount,
										PreloadEnabled = preloadEnabled
									};

			this.lvDictionarySpecs.Items.Add(lviDictionarySpec);

			this.CoreRefreshControlState();

			return lviDictionarySpec;
		}

		private void btnAddDictionarySpec_Click(object sender, EventArgs e)
		{
			this.PartialView.AddDictionarySpecView(string.Format("Dictionary_{0:N}", Guid.NewGuid()), false, null, null);
			this.CoreRefreshControlState();
		}

		private void btnClearDictionarySpecs_Click(object sender, EventArgs e)
		{
			this.PartialView.ClearDictionarySpecViews();
			this.CoreRefreshControlState();
		}

		private void btnMoveDnDictionarySpec_Click(object sender, EventArgs e)
		{
			this.CoreRefreshControlState();
		}

		private void btnMoveUpDictionarySpec_Click(object sender, EventArgs e)
		{
			this.CoreRefreshControlState();
		}

		private void btnRemoveDictionarySpec_Click(object sender, EventArgs e)
		{
			this.PartialView.RemoveDictionarySpecView(this.PartialView.SelectedDictionarySpecView);
		}

		void IDictionarySettingsView.ClearDictionarySpecViews()
		{
			this.lvDictionarySpecs.Items.OfType<DictionaryListViewItem>().ToList().ForEach(lvi => lvi.DisposeEditor());
			this.lvDictionarySpecs.Items.Clear();
			this.CoreRefreshControlState();
		}

		protected override void CoreRefreshControlState()
		{
			bool hasSelection;

			base.CoreRefreshControlState();

			hasSelection = this.lvDictionarySpecs.SelectedItems.Count == 1;

			this.btnAddDictionarySpec.Enabled = true;
			this.btnRemoveDictionarySpec.Enabled = hasSelection;
			this.btnClearDictionarySpecs.Enabled = true;
			this.btnMoveUpDictionarySpec.Enabled = hasSelection;
			this.btnMoveDnDictionarySpec.Enabled = hasSelection;
		}

		private void lvDictionarySpecs_DoubleClick(object sender, EventArgs e)
		{
			DictionaryListViewItem lviDictionarySpec;
			DictionarySpec dictionarySpec;

			if (this.lvDictionarySpecs.SelectedItems.Count != 1)
				return;

			lviDictionarySpec = this.lvDictionarySpecs.SelectedItems[0] as DictionaryListViewItem;

			if ((object)lviDictionarySpec == null)
				return;

			dictionarySpec = (DictionarySpec)lviDictionarySpec.Tag;

			using (PropertyForm frmProperty = new PropertyForm(dictionarySpec))
			{
				//frmProperty.PropertyUpdate += new EventHandler(this.f_PropertyUpdate);
				frmProperty.ShowDialog(this.ParentForm);
				//frmProperty.PropertyUpdate -= new EventHandler(this.f_PropertyUpdate);

				this.lvDictionarySpecs.SelectedItems[0].SubItems[0].Text = dictionarySpec.DictionaryId.SafeToString();
				this.lvDictionarySpecs.SelectedItems[0].SubItems[1].Text = dictionarySpec.PreloadEnabled.SafeToString();
				this.lvDictionarySpecs.SelectedItems[0].SubItems[2].Text = dictionarySpec.RecordCount.SafeToString();
				//this.lvDictionarySpecs.SelectedItems[0].SubItems[3].Text = null;
			}
		}

		private void lvDictionarySpecs_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.CoreRefreshControlState();
		}

		bool IDictionarySettingsView.RemoveDictionarySpecView(IDictionarySpecView headerSpecView)
		{
			DictionaryListViewItem lviDictionary;

			lviDictionary = headerSpecView as DictionaryListViewItem;

			if ((object)lviDictionary == null)
				return false;

			if (!this.lvDictionarySpecs.Items.Contains(lviDictionary))
				return false;

			this.lvDictionarySpecs.Items.Remove(lviDictionary);

			lviDictionary.DisposeEditor();

			this.CoreRefreshControlState();
			return true;
		}

		private void tsmiAdapterSettings_Click(object sender, EventArgs e)
		{
			DictionaryListViewItem lviDictionarySpec;
			DictionarySpec dictionarySpec;

			if (this.lvDictionarySpecs.SelectedItems.Count != 1)
				return;

			lviDictionarySpec = this.lvDictionarySpecs.SelectedItems[0] as DictionaryListViewItem;

			if ((object)lviDictionarySpec == null)
				return;

			dictionarySpec = (DictionarySpec)lviDictionarySpec.Tag;

			lviDictionarySpec.ShowEditorModal();
			// TODO
			//lviDictionarySpec.SubItems[3].Text = (object)adapterAqtn == null ? "(Right-click to change)" : string.Format("{0}", adapterAqtn);
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private sealed class DictionaryListViewItem : ListViewItem, IDictionarySpecView
		{
			#region Constructors/Destructors

			public DictionaryListViewItem(IEnumerable<string> values, AdapterSettingsForm adapterSettingsForm)
				: base((object)values != null ? values.ToArray() : null)
			{
				if ((object)adapterSettingsForm == null)
					throw new ArgumentNullException("adapterSettingsForm");

				this.adapterSettingsForm = adapterSettingsForm;
			}

			#endregion

			#region Fields/Constants

			private readonly AdapterSettingsForm adapterSettingsForm;

			#endregion

			#region Properties/Indexers/Events

			string IDictionarySpecView.AdapterType
			{
				get
				{
					return this.SubItems[3].Text;
				}
			}

			IAdapterSettingsView IDictionarySpecView.DictionaryAdapterSettings
			{
				get
				{
					return ((IAdapterSettingsView2)this.adapterSettingsForm).DictionaryAdapterSettings;
				}
			}

			string IDictionarySpecView.DictionaryId
			{
				get
				{
					return this.SubItems[0].Text;
				}
			}

			bool IDictionarySpecView.PreloadEnabled
			{
				get
				{
					bool value;

					if (DataTypeFascade.Instance.TryParse<bool>(this.SubItems[1].Text, out value))
						return value;

					return false;
				}
			}

			long? IDictionarySpecView.RecordCount
			{
				get
				{
					long? value;

					if (DataTypeFascade.Instance.TryParse<long?>(this.SubItems[2].Text, out value))
						return value;

					return null;
				}
			}

			#endregion

			#region Methods/Operators

			public void DisposeEditor()
			{
				this.adapterSettingsForm.Dispose();
			}

			public void ShowEditorModal()
			{
				Form form;
				form = this.ListView.CoreGetParentForm();
				this.adapterSettingsForm.ShowDialog(form);
			}

			#endregion
		}

		private sealed class DictionarySpec
		{
			#region Constructors/Destructors

			public DictionarySpec()
			{
			}

			#endregion

			#region Fields/Constants

			private string dictionaryId;
			private bool? preloadEnabled;
			private long? recordCount;

			#endregion

			#region Properties/Indexers/Events

			public string DictionaryId
			{
				get
				{
					return this.dictionaryId;
				}
				set
				{
					this.dictionaryId = value;
				}
			}

			public bool? PreloadEnabled
			{
				get
				{
					return this.preloadEnabled;
				}
				set
				{
					this.preloadEnabled = value;
				}
			}

			public long? RecordCount
			{
				get
				{
					return this.recordCount;
				}
				set
				{
					this.recordCount = value;
				}
			}

			#endregion
		}

		#endregion
	}

	public class _DictionarySettingsUserControl : _2ndAssetUserControl<IDictionarySettingsView>
	{
		#region Constructors/Destructors

		public _DictionarySettingsUserControl()
		{
		}

		#endregion
	}
}