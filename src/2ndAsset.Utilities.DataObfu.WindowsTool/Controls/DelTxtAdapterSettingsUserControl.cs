/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

using TextMetal.Middleware.Common.Utilities;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controls
{
	public partial class DelTxtAdapterSettingsUserControl : _DelTxtAdapterSettingsUserControl, IDelTextAdapterSettingsView
	{
		#region Constructors/Destructors

		public DelTxtAdapterSettingsUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		IEnumerable<IHeaderSpecView> IDelTextAdapterSettingsView.HeaderSpecViews
		{
			get
			{
				if ((object)this.lvFieldSpecs.Items != null)
				{
					foreach (HeaderSpecListViewItem lviHeaderSpec in this.lvFieldSpecs.Items.Cast<HeaderSpecListViewItem>())
						yield return lviHeaderSpec;
				}
			}
		}

		string IDelTextAdapterSettingsView.FieldDelimiter
		{
			get
			{
				return this.txtBxFieldDelimiter.CoreGetValue<string>();
			}
			set
			{
				this.txtBxFieldDelimiter.CoreSetValue<string>(value, null);
			}
		}

		bool ISpecificAdapterSettingsView.IsActiveSettings
		{
			get
			{
				return this.Visible;
			}
			set
			{
				this.Visible = value;
			}
		}

		bool IDelTextAdapterSettingsView.IsFirstRowHeaders
		{
			get
			{
				return this.chkBxFirstRowIsHeaders.CoreGetValue() ?? false;
			}
			set
			{
				this.chkBxFirstRowIsHeaders.CoreSetValue(value);
			}
		}

		string IDelTextAdapterSettingsView.QuoteValue
		{
			get
			{
				return this.txtBxQuoteValue.CoreGetValue<string>();
			}
			set
			{
				this.txtBxQuoteValue.CoreSetValue<string>(value, null);
			}
		}

		string IDelTextAdapterSettingsView.RecordDelimiter
		{
			get
			{
				return this.txtBxRecordDelimiter.CoreGetValue<string>();
			}
			set
			{
				this.txtBxRecordDelimiter.CoreSetValue<string>(value, null);
			}
		}

		IHeaderSpecView IDelTextAdapterSettingsView.SelectedHeaderSpecView
		{
			get
			{
				if (this.lvFieldSpecs.SelectedItems.Count != 1)
					return null;

				return this.lvFieldSpecs.SelectedItems[0] as IHeaderSpecView;
			}
			set
			{
				HeaderSpecListViewItem lviHeaderSpec;

				lviHeaderSpec = value as HeaderSpecListViewItem;

				this.lvFieldSpecs.SelectedItems.Clear();

				if ((object)lviHeaderSpec != null)
					lviHeaderSpec.Selected = true;
			}
		}

		string IDelTextAdapterSettingsView.TextFilePath
		{
			get
			{
				return this.txtBxTextFilePath.CoreGetValue<string>();
			}
			set
			{
				this.txtBxTextFilePath.CoreSetValue<string>(value, null);
			}
		}

		#endregion

		#region Methods/Operators

		IHeaderSpecView IDelTextAdapterSettingsView.AddHeaderSpecView(string headerName, FieldType? fieldType)
		{
			HeaderSpecListViewItem lviHeaderSpec;

			lviHeaderSpec = new HeaderSpecListViewItem(new string[] { headerName.SafeToString(), fieldType.SafeToString() });
			lviHeaderSpec.Tag = new HeaderSpec()
								{
									HeaderName = headerName.SafeToString(),
									FieldType = fieldType ?? FieldType.String,
									FieldNumberFormatSpec = new NumberFormatInfo()
								};

			this.lvFieldSpecs.Items.Add(lviHeaderSpec);

			this.CoreRefreshControlState();

			return lviHeaderSpec;
		}

		private void btnAddHeaderSpec_Click(object sender, EventArgs e)
		{
			this.PartialView.AddHeaderSpecView(string.Format("Field_{0:N}", Guid.NewGuid()), FieldType.String);
			this.CoreRefreshControlState();
		}

		private void btnBrowse_Click(object sender, EventArgs e)
		{
			this.FullView.DispatchControllerAction(this, new Uri("action://obfuscation/adapter-settings/delimited-text/browse-file-system-location"), null);
			this.CoreRefreshControlState();
		}

		private void btnClearHeaderSpecs_Click(object sender, EventArgs e)
		{
			this.PartialView.ClearHeaderSpecViews();
			this.CoreRefreshControlState();
		}

		private void btnMoveDnHeaderSpec_Click(object sender, EventArgs e)
		{
			this.CoreRefreshControlState();
		}

		private void btnMoveUpHeaderSpec_Click(object sender, EventArgs e)
		{
			this.CoreRefreshControlState();
		}

		private void btnRefreshFieldSpecs_Click(object sender, EventArgs e)
		{
			this.FullView.DispatchControllerAction(this, new Uri("action://obfuscation/adapter-settings/delimited-text/refresh-field-specs"), null);
			this.CoreRefreshControlState();
		}

		private void btnRemoveHeaderSpec_Click(object sender, EventArgs e)
		{
			this.PartialView.RemoveHeaderSpecView(this.PartialView.SelectedHeaderSpecView);
		}

		void IDelTextAdapterSettingsView.ClearHeaderSpecViews()
		{
			this.lvFieldSpecs.Items.Clear();
			this.CoreRefreshControlState();
		}

		protected override void CoreRefreshControlState()
		{
			bool hasSelection;

			base.CoreRefreshControlState();

			hasSelection = this.lvFieldSpecs.SelectedItems.Count == 1;

			this.btnAddHeaderSpec.Enabled = true;
			this.btnRemoveHeaderSpec.Enabled = hasSelection;
			this.btnClearHeaderSpecs.Enabled = true;
			this.btnMoveUpHeaderSpec.Enabled = hasSelection;
			this.btnMoveDnHeaderSpec.Enabled = hasSelection;
		}

		private void lvFieldSpecs_DoubleClick(object sender, EventArgs e)
		{
			HeaderSpecListViewItem lviHeaderSpec;
			HeaderSpec headerSpec;

			if (this.lvFieldSpecs.SelectedItems.Count != 1)
				return;

			lviHeaderSpec = this.lvFieldSpecs.SelectedItems[0] as HeaderSpecListViewItem;

			if ((object)lviHeaderSpec == null)
				return;

			headerSpec = (HeaderSpec)lviHeaderSpec.Tag;

			using (PropertyForm frmProperty = new PropertyForm(headerSpec))
			{
				//frmProperty.PropertyUpdate += new EventHandler(this.f_PropertyUpdate);
				frmProperty.ShowDialog(this.ParentForm);
				//frmProperty.PropertyUpdate -= new EventHandler(this.f_PropertyUpdate);

				this.lvFieldSpecs.SelectedItems[0].SubItems[0].Text = headerSpec.HeaderName.SafeToString();
				this.lvFieldSpecs.SelectedItems[0].SubItems[1].Text = headerSpec.FieldType.SafeToString();
			}
		}

		private void lvFieldSpecs_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.CoreRefreshControlState();
		}

		bool IDelTextAdapterSettingsView.RemoveHeaderSpecView(IHeaderSpecView headerSpecView)
		{
			HeaderSpecListViewItem lviHeaderSpec;

			lviHeaderSpec = headerSpecView as HeaderSpecListViewItem;

			if ((object)lviHeaderSpec == null)
				return false;

			if (!this.lvFieldSpecs.Items.Contains(lviHeaderSpec))
				return false;

			this.lvFieldSpecs.Items.Remove(lviHeaderSpec);

			this.CoreRefreshControlState();
			return true;
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private sealed class HeaderSpecListViewItem : ListViewItem, IHeaderSpecView
		{
			#region Constructors/Destructors

			public HeaderSpecListViewItem(IEnumerable<string> values)
				: base((object)values != null ? values.ToArray() : null)
			{
			}

			#endregion

			#region Properties/Indexers/Events

			FieldType? IHeaderSpecView.FieldType
			{
				get
				{
					FieldType? value;

					if (DataTypeFascade.Instance.TryParse<FieldType?>(this.SubItems[1].Text, out value))
						return value;

					return null;
				}
			}

			string IHeaderSpecView.HeaderName
			{
				get
				{
					return this.SubItems[0].Text;
				}
			}

			#endregion
		}

		#endregion
	}

	public class _DelTxtAdapterSettingsUserControl : _2ndAssetUserControl<IDelTextAdapterSettingsView>
	{
		#region Constructors/Destructors

		public _DelTxtAdapterSettingsUserControl()
		{
		}

		#endregion
	}
}