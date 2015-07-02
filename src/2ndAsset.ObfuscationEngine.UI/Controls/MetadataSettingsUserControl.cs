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
using _2ndAsset.ObfuscationEngine.Core;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	public partial class MetadataSettingsUserControl : _MetadataSettingsUserControl, IMetadataSettingsView
	{
		#region Constructors/Destructors

		public MetadataSettingsUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		public IEnumerable<IMetaColumnSpecView> MetaColumnSpecViews
		{
			get
			{
				if ((object)this.lvMetaColumnSpecs.Items != null)
				{
					foreach (MetaColumnListViewItem lviMetaColumnSpec in this.lvMetaColumnSpecs.Items.Cast<MetaColumnListViewItem>())
						yield return lviMetaColumnSpec;
				}
			}
		}

		public IMetaColumnSpecView SelectedMetaColumnSpecView
		{
			get
			{
				if (this.lvMetaColumnSpecs.SelectedItems.Count != 1)
					return null;

				return this.lvMetaColumnSpecs.SelectedItems[0] as IMetaColumnSpecView;
			}
			set
			{
				MetaColumnListViewItem lviMetaColumnSpec;

				lviMetaColumnSpec = value as MetaColumnListViewItem;

				this.lvMetaColumnSpecs.SelectedItems.Clear();

				if ((object)lviMetaColumnSpec != null)
					lviMetaColumnSpec.Selected = true;
			}
		}

		#endregion

		#region Methods/Operators

		IMetaColumnSpecView IMetadataSettingsView.AddMetaColumnSpecView(string columnName, bool? isColumnNullable, ObfuscationStrategy? obfuscationStrategy, string dictionaryRef, int? extentValue)
		{
			MetaColumnListViewItem lviMetaColumnSpec;

			lviMetaColumnSpec = new MetaColumnListViewItem(new string[] { columnName.SafeToString(), isColumnNullable.SafeToString(), obfuscationStrategy.SafeToString(), dictionaryRef.SafeToString(), extentValue.SafeToString() });
			lviMetaColumnSpec.Tag = new MetaColumnSpec()
									{
										ColumnName = columnName.SafeToString(),
										ObfuscationStrategy = obfuscationStrategy ?? ObfuscationStrategy.None,
										DictionaryRef = dictionaryRef.SafeToString(),
										ExtentValue = extentValue,
										IsColumnNullable = isColumnNullable
									};

			this.lvMetaColumnSpecs.Items.Add(lviMetaColumnSpec);

			this.CoreRefreshControlState();

			return lviMetaColumnSpec;
		}

		private void btnAddMetaColumnSpec_Click(object sender, EventArgs e)
		{
			this.PartialView.AddMetaColumnSpecView(string.Format("Column_{0:N}", Guid.NewGuid()), true, ObfuscationStrategy.None, string.Empty, null);
			this.CoreRefreshControlState();
		}

		private void btnClearMetaColumnSpecs_Click(object sender, EventArgs e)
		{
			this.PartialView.ClearMetaColumnSpecViews();
			this.CoreRefreshControlState();
		}

		private void btnMoveDnMetaColumnSpec_Click(object sender, EventArgs e)
		{
			this.CoreRefreshControlState();
		}

		private void btnMoveUpMetaColumnSpec_Click(object sender, EventArgs e)
		{
			this.CoreRefreshControlState();
		}

		private void btnRefreshMetaColumnSpecs_Click(object sender, EventArgs e)
		{
			this.FullView.DispatchControllerAction(this, new Uri("action://obfuscation/metadata-settings/refresh-meta-column-specs"), null);
			this.CoreRefreshControlState();
		}

		private void btnRemoveMetaColumnSpec_Click(object sender, EventArgs e)
		{
			this.PartialView.RemoveMetaColumnSpecView(this.PartialView.SelectedMetaColumnSpecView);
		}

		void IMetadataSettingsView.ClearMetaColumnSpecViews()
		{
			this.lvMetaColumnSpecs.Items.Clear();
			this.CoreRefreshControlState();
		}

		protected override void CoreRefreshControlState()
		{
			bool hasSelection;

			base.CoreRefreshControlState();

			hasSelection = this.lvMetaColumnSpecs.SelectedItems.Count == 1;

			this.btnAddMetaColumnSpec.Enabled = true;
			this.btnRemoveMetaColumnSpec.Enabled = hasSelection;
			this.btnClearMetaColumnSpecs.Enabled = true;
			this.btnMoveUpMetaColumnSpec.Enabled = hasSelection;
			this.btnMoveDnMetaColumnSpec.Enabled = hasSelection;
		}

		private void lvMetaColumnSpecs_DoubleClick(object sender, EventArgs e)
		{
			MetaColumnListViewItem lviMetaColumnSpec;
			MetaColumnSpec metaColumnSpec;

			if (this.lvMetaColumnSpecs.SelectedItems.Count != 1)
				return;

			lviMetaColumnSpec = this.lvMetaColumnSpecs.SelectedItems[0] as MetaColumnListViewItem;

			if ((object)lviMetaColumnSpec == null)
				return;

			metaColumnSpec = (MetaColumnSpec)lviMetaColumnSpec.Tag;

			using (PropertyForm frmProperty = new PropertyForm(metaColumnSpec))
			{
				//frmProperty.PropertyUpdate += new EventHandler(this.f_PropertyUpdate);
				frmProperty.ShowDialog(this.ParentForm);
				//frmProperty.PropertyUpdate -= new EventHandler(this.f_PropertyUpdate);

				this.lvMetaColumnSpecs.SelectedItems[0].SubItems[0].Text = metaColumnSpec.ColumnName.SafeToString();
				this.lvMetaColumnSpecs.SelectedItems[0].SubItems[1].Text = metaColumnSpec.IsColumnNullable.SafeToString();
				this.lvMetaColumnSpecs.SelectedItems[0].SubItems[2].Text = metaColumnSpec.ObfuscationStrategy.ToString();
				this.lvMetaColumnSpecs.SelectedItems[0].SubItems[3].Text = metaColumnSpec.DictionaryRef.SafeToString();
				this.lvMetaColumnSpecs.SelectedItems[0].SubItems[4].Text = metaColumnSpec.ExtentValue.SafeToString();
			}
		}

		private void lvMetaColumnSpecs_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.CoreRefreshControlState();
		}

		bool IMetadataSettingsView.RemoveMetaColumnSpecView(IMetaColumnSpecView headerSpecView)
		{
			MetaColumnListViewItem lviMetaColumn;

			lviMetaColumn = headerSpecView as MetaColumnListViewItem;

			if ((object)lviMetaColumn == null)
				return false;

			if (!this.lvMetaColumnSpecs.Items.Contains(lviMetaColumn))
				return false;

			this.lvMetaColumnSpecs.Items.Remove(lviMetaColumn);

			this.CoreRefreshControlState();
			return true;
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private sealed class MetaColumnListViewItem : ListViewItem, IMetaColumnSpecView
		{
			#region Constructors/Destructors

			public MetaColumnListViewItem(IEnumerable<string> values)
				: base((object)values != null ? values.ToArray() : null)
			{
			}

			#endregion

			#region Properties/Indexers/Events

			string IMetaColumnSpecView.ColumnName
			{
				get
				{
					return this.SubItems[0].Text;
				}
			}

			string IMetaColumnSpecView.DictionaryRef
			{
				get
				{
					return this.SubItems[3].Text;
				}
			}

			int? IMetaColumnSpecView.ExtentValue
			{
				get
				{
					int? value;

					if (DataTypeFascade.Instance.TryParse<int?>(this.SubItems[4].Text, out value))
						return value;

					return null;
				}
			}

			bool? IMetaColumnSpecView.IsColumnNullable
			{
				get
				{
					bool? value;

					if (DataTypeFascade.Instance.TryParse<bool?>(this.SubItems[1].Text, out value))
						return value;

					return null;
				}
			}

			ObfuscationStrategy? IMetaColumnSpecView.ObfuscationStrategy
			{
				get
				{
					ObfuscationStrategy? value;

					if (DataTypeFascade.Instance.TryParse<ObfuscationStrategy?>(this.SubItems[2].Text, out value))
						return value;

					return null;
				}
			}

			#endregion
		}

		private sealed class MetaColumnSpec
		{
			#region Constructors/Destructors

			public MetaColumnSpec()
			{
			}

			#endregion

			#region Fields/Constants

			private string columnName;
			private string dictionaryRef;
			private int? extentValue;
			private bool? isColumnNullable;
			private ObfuscationStrategy obfuscationStrategy;

			#endregion

			#region Properties/Indexers/Events

			public string ColumnName
			{
				get
				{
					return this.columnName;
				}
				set
				{
					this.columnName = value;
				}
			}

			public string DictionaryRef
			{
				get
				{
					return this.dictionaryRef;
				}
				set
				{
					this.dictionaryRef = value;
				}
			}

			public int? ExtentValue
			{
				get
				{
					return this.extentValue;
				}
				set
				{
					this.extentValue = value;
				}
			}

			public bool? IsColumnNullable
			{
				get
				{
					return this.isColumnNullable;
				}
				set
				{
					this.isColumnNullable = value;
				}
			}

			public ObfuscationStrategy ObfuscationStrategy
			{
				get
				{
					return this.obfuscationStrategy;
				}
				set
				{
					this.obfuscationStrategy = value;
				}
			}

			#endregion
		}

		#endregion
	}

	public class _MetadataSettingsUserControl : _2ndAssetUserControl<IMetadataSettingsView>
	{
		#region Constructors/Destructors

		public _MetadataSettingsUserControl()
		{
		}

		#endregion
	}
}