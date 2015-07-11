/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	public partial class AdapterSettingsUserControl : _AdapterSettingsUserControl, IAdapterSettingsPartialView
	{
		#region Constructors/Destructors

		public AdapterSettingsUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		IAdoNetAdapterSettingsPartialView IAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView
		{
			get
			{
				return this.adoNetAdapterSettingsUc;
			}
		}

		IDelTextAdapterSettingsPartialView IAdapterSettingsPartialView.DelTextAdapterSettingsPartialView
		{
			get
			{
				return this.delTxtAdapterSettingsUc;
			}
		}

		IEnumerable<IListItem<Type>> IAdapterSettingsPartialView.AdapterTypes
		{
			set
			{
				this.ddlType.CoreBindSelectionItems(value, true, this.ddlType_SelectedIndexChanged);
			}
		}

		Type IAdapterSettingsPartialView.SelectedAdapterType
		{
			get
			{
				return this.ddlType.CoreGetSelectedValue<Type>();
			}
			set
			{
				this.ddlType.CoreSetSelectedValue<Type>(value, true);
			}
		}

		#endregion

		#region Methods/Operators

		private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.FullView.DispatchControllerAction(this, new Uri("action://obfuscation/adapter/update"), null);
			this.CoreRefreshControlState();
		}

		private void pnlAdapterConfigHost_Paint(object sender, PaintEventArgs e)
		{
			if ((object)this.PartialView.SelectedAdapterType == null)
			{
				using (Brush brush = new SolidBrush(Color.Red))
				{
					using (FontFamily fontFamily = new FontFamily(GenericFontFamilies.Monospace))
					{
						using (Font font = new Font(fontFamily, 18, FontStyle.Bold))
						{
							using (StringFormat stringFormat = new StringFormat())
							{
								stringFormat.Alignment = StringAlignment.Center;
								stringFormat.LineAlignment = StringAlignment.Center;

								e.Graphics.DrawString("Select an adapter type to proceed.", font, brush, new PointF(this.pnlAdapterConfigHost.Width / 2.0f, this.pnlAdapterConfigHost.Height / 2.0f), stringFormat);
							}
						}
					}
				}
			}
		}

		#endregion
	}

	public class _AdapterSettingsUserControl : BaseUserControl<IAdapterSettingsPartialView>
	{
		#region Constructors/Destructors

		public _AdapterSettingsUserControl()
		{
		}

		#endregion
	}
}