/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.UI.Controllers;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	public partial class MetadataSettingsUserControl : _MetadataSettingsUserControl, IMetadataSettingsPartialView
	{
		#region Constructors/Destructors

		public MetadataSettingsUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		public IEnumerable<IMetaColumnSpecListView> MetaColumnSpecListViews
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

		public IMetaColumnSpecListView SelectedMetaColumnSpecListView
		{
			get
			{
				if (this.lvMetaColumnSpecs.SelectedItems.Count != 1)
					return null;

				return this.lvMetaColumnSpecs.SelectedItems[0] as IMetaColumnSpecListView;
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

		IMetaColumnSpecListView IMetadataSettingsPartialView.AddMetaColumnSpecView(string columnName, string obfuscationStrategyAqtn)
		{
			MetaColumnListViewItem lviMetaColumnSpec;

			lviMetaColumnSpec = new MetaColumnListViewItem(new string[] { columnName.SafeToString(), obfuscationStrategyAqtn.SafeToString() });
			lviMetaColumnSpec.Tag = new MetaColumnSpec()
									{
										ColumnName = columnName.SafeToString(),
										ObfuscationStrategyAqtn = obfuscationStrategyAqtn
									};

			this.lvMetaColumnSpecs.Items.Add(lviMetaColumnSpec);

			this.CoreRefreshControlState();

			return lviMetaColumnSpec;
		}

		private void btnAddMetaColumnSpec_Click(object sender, EventArgs e)
		{
			this._.AddMetaColumnSpecView(string.Format("Column_{0:N}", Guid.NewGuid()), string.Empty);
			this.CoreRefreshControlState();
		}

		private void btnClearMetaColumnSpecs_Click(object sender, EventArgs e)
		{
			this._.ClearMetaColumnSpecViews();
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
			this.Controller.RefreshUpstream();
			this.CoreRefreshControlState();
		}

		private void btnRemoveMetaColumnSpec_Click(object sender, EventArgs e)
		{
			this._.RemoveMetaColumnSpecView(this._.SelectedMetaColumnSpecListView);
		}

		void IMetadataSettingsPartialView.ClearMetaColumnSpecViews()
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
				this.lvMetaColumnSpecs.SelectedItems[0].SubItems[2].Text = metaColumnSpec.ObfuscationStrategyAqtn.SafeToString();
				this.lvMetaColumnSpecs.SelectedItems[0].SubItems[3].Text = metaColumnSpec.DictionaryRef.SafeToString();
				this.lvMetaColumnSpecs.SelectedItems[0].SubItems[4].Text = metaColumnSpec.ExtentValue.SafeToString();
			}
		}

		private void lvMetaColumnSpecs_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.CoreRefreshControlState();
		}

		bool IMetadataSettingsPartialView.RemoveMetaColumnSpecView(IMetaColumnSpecListView metaColumnSpecListView)
		{
			MetaColumnListViewItem lviMetaColumn;

			lviMetaColumn = metaColumnSpecListView as MetaColumnListViewItem;

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

		private sealed class MetaColumnListViewItem : ListViewItem, IMetaColumnSpecListView
		{
			#region Constructors/Destructors

			public MetaColumnListViewItem(IEnumerable<string> values)
				: base((object)values != null ? values.ToArray() : null)
			{
			}

			#endregion

			#region Properties/Indexers/Events

			string IMetaColumnSpecListView.ColumnName
			{
				get
				{
					return this.SubItems[0].Text;
				}
			}

			bool? IMetaColumnSpecListView.IsColumnNullable
			{
				get
				{
					bool? value;

					if (DataTypeFascade.Instance.TryParse<bool?>(this.SubItems[1].Text, out value))
						return value;

					return null;
				}
			}

			string IMetaColumnSpecListView.ObfuscationStrategyAqtn
			{
				get
				{
					string value;

					if (DataTypeFascade.Instance.TryParse<string>(this.SubItems[2].Text, out value))
						return value;

					return null;
				}
			}

			IBaseView IBaseView.ParentView
			{
				get
				{
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
			private string obfuscationStrategyAqtn;

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

			public string ObfuscationStrategyAqtn
			{
				get
				{
					return this.obfuscationStrategyAqtn;
				}
				set
				{
					this.obfuscationStrategyAqtn = value;
				}
			}

			#endregion
		}

		#endregion
	}

	public class _MetadataSettingsUserControl : BasePartialViewUserControl<IMetadataSettingsPartialView, MetadataSettingsSlaveController>
	{
		#region Constructors/Destructors

		public _MetadataSettingsUserControl()
		{
		}

		#endregion
	}
}