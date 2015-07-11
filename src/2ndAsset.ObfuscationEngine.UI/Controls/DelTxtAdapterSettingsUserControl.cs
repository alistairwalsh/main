/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	public partial class DelTxtAdapterSettingsUserControl : _DelTxtAdapterSettingsUserControl, IDelTextAdapterSettingsPartialView
	{
		#region Constructors/Destructors

		public DelTxtAdapterSettingsUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		IEnumerable<IHeaderSpecListView> IDelTextAdapterSettingsPartialView.HeaderSpecViews
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

		string IDelTextAdapterSettingsPartialView.FieldDelimiter
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

		bool ISpecificAdapterSettingsPartialView.IsActiveSettings
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

		bool IDelTextAdapterSettingsPartialView.IsFirstRowHeaders
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

		string IDelTextAdapterSettingsPartialView.QuoteValue
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

		string IDelTextAdapterSettingsPartialView.RecordDelimiter
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

		IHeaderSpecListView IDelTextAdapterSettingsPartialView.SelectedHeaderSpecListView
		{
			get
			{
				if (this.lvFieldSpecs.SelectedItems.Count != 1)
					return null;

				return this.lvFieldSpecs.SelectedItems[0] as IHeaderSpecListView;
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

		string IDelTextAdapterSettingsPartialView.TextFilePath
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

		IHeaderSpecListView IDelTextAdapterSettingsPartialView.AddHeaderSpecView(string headerName, FieldType? fieldType)
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
			this.PartialView.RemoveHeaderSpecView(this.PartialView.SelectedHeaderSpecListView);
		}

		void IDelTextAdapterSettingsPartialView.ClearHeaderSpecViews()
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

		bool IDelTextAdapterSettingsPartialView.RemoveHeaderSpecView(IHeaderSpecListView headerSpecListView)
		{
			HeaderSpecListViewItem lviHeaderSpec;

			lviHeaderSpec = headerSpecListView as HeaderSpecListViewItem;

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

		private sealed class HeaderSpecListViewItem : ListViewItem, IHeaderSpecListView
		{
			#region Constructors/Destructors

			public HeaderSpecListViewItem(IEnumerable<string> values)
				: base((object)values != null ? values.ToArray() : null)
			{
			}

			#endregion

			#region Properties/Indexers/Events

			FieldType? IHeaderSpecListView.FieldType
			{
				get
				{
					FieldType? value;

					if (DataTypeFascade.Instance.TryParse<FieldType?>(this.SubItems[1].Text, out value))
						return value;

					return null;
				}
			}

			string IHeaderSpecListView.HeaderName
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

	public class _DelTxtAdapterSettingsUserControl : BaseUserControl<IDelTextAdapterSettingsPartialView>
	{
		#region Constructors/Destructors

		public _DelTxtAdapterSettingsUserControl()
		{
		}

		#endregion
	}
}