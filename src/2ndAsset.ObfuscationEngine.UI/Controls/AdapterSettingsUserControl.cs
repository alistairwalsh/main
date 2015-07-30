/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

using Solder.Framework.Injection;
using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.ObfuscationEngine.Core.Adapter;
using _2ndAsset.ObfuscationEngine.UI.Controllers;
using _2ndAsset.ObfuscationEngine.UI.Views;
using _2ndAsset.ObfuscationEngine.UI.Views.Adapters;

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

		#region Fields/Constants

		private readonly IDependencyManager dependencyManager = new DependencyManager();
		private AdapterDirection adapterDirection;
		private BasePartialViewUserControl currentAdapterSpecificConfigurationUc;
		private string paintText;

		#endregion

		#region Properties/Indexers/Events

		IAdapterSpecificSettingsPartialView IAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView
		{
			get
			{
				return (IAdapterSpecificSettingsPartialView)this.CurrentAdapterSpecificConfigurationUc;
			}
		}

		private IDependencyManager DependencyManager
		{
			get
			{
				return this.dependencyManager;
			}
		}

		public AdapterDirection AdapterDirection
		{
			get
			{
				return this.adapterDirection;
			}
			set
			{
				this.adapterDirection = value;
			}
		}

		IEnumerable<IListItem<Type>> IAdapterSettingsPartialView.AdapterTypes
		{
			set
			{
				this.ddlType.CoreBindSelectionItems(value, true, this.ddlType_SelectedIndexChanged);
			}
		}

		private BasePartialViewUserControl CurrentAdapterSpecificConfigurationUc
		{
			get
			{
				return this.currentAdapterSpecificConfigurationUc;
			}
			set
			{
				this.currentAdapterSpecificConfigurationUc = value;
			}
		}

		private string PaintText
		{
			get
			{
				return this.paintText;
			}
			set
			{
				this.paintText = value;
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

		protected override void CoreRefreshControlState()
		{
			base.CoreRefreshControlState();
			this.Invalidate();
		}

		protected override void CoreSetup()
		{
			base.CoreSetup();
		}

		private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.ShowAdapterSpecificConfigurationUserControl(this._.SelectedAdapterType);

			this.Controller.ActivateAdapter();
			this.PaintText = this.ddlType.CoreGetSelectedText(true);
			this.CoreRefreshControlState();
		}

		private void pnlAdapterConfigHost_Paint(object sender, PaintEventArgs e)
		{
			using (Brush brush = new SolidBrush(Color.Red))
			{
				using (FontFamily fontFamily = new FontFamily(GenericFontFamilies.Monospace))
				{
					using (Font font = new Font(fontFamily, 10, FontStyle.Bold))
					{
						using (StringFormat stringFormat = new StringFormat())
						{
							stringFormat.Alignment = StringAlignment.Center;
							stringFormat.LineAlignment = StringAlignment.Center;

							e.Graphics.DrawString(this.PaintText.SafeToString(null, "Select an adapter type to proceed.", true), font, brush, new PointF(this.pnlAdapterConfigHost.Width / 2.0f, this.pnlAdapterConfigHost.Height / 2.0f), stringFormat);
						}
					}
				}
			}
		}

		private void ShowAdapterSpecificConfigurationUserControl(Type adapterType)
		{
			AdapterSpecificConfigurationAttribute adapterSpecificConfigurationAttribute;
			Type adapterSpecificConfigurationUcType;

			if ((object)this.CurrentAdapterSpecificConfigurationUc != null)
			{
				this.CurrentAdapterSpecificConfigurationUc.Visible = false;
				//this.CurrentAdapterSpecificConfigurationUc.Dispose();
				this.CurrentAdapterSpecificConfigurationUc = null;
			}

			if ((object)adapterType == null)
				return;

			adapterSpecificConfigurationAttribute = ReflectionFascade.Instance.GetOneAttribute<AdapterSpecificConfigurationAttribute>(adapterType);

			if ((object)adapterSpecificConfigurationAttribute == null)
				return;

			adapterSpecificConfigurationUcType = adapterSpecificConfigurationAttribute.GetUserControlType();

			if ((object)adapterSpecificConfigurationUcType == null)
				return;

			if (!this.DependencyManager.HasTypeResolution(adapterSpecificConfigurationUcType, this.AdapterDirection.ToString()))
				this.DependencyManager.AddResolution(adapterSpecificConfigurationUcType, this.AdapterDirection.ToString(), new SingletonDependencyResolution(new ActivatorDependencyResolution(adapterSpecificConfigurationUcType)));

			this.CurrentAdapterSpecificConfigurationUc = (BasePartialViewUserControl)this.DependencyManager.ResolveDependency(adapterSpecificConfigurationUcType, this.AdapterDirection.ToString());

			//this.CurrentAdapterSpecificConfigurationUc = (XBaseUserControl)Activator.CreateInstance(adapterSpecificConfigurationUcType);
			this.CurrentAdapterSpecificConfigurationUc.Parent = this.pnlAdapterConfigHost;
			this.CurrentAdapterSpecificConfigurationUc.Location = Point.Empty;
			this.CurrentAdapterSpecificConfigurationUc.Dock = DockStyle.Fill;
			this.CurrentAdapterSpecificConfigurationUc.Visible = true;
		}

		#endregion
	}

	public class _AdapterSettingsUserControl : BasePartialViewUserControl<IAdapterSettingsPartialView, AdapterSettingsSlaveController>
	{
		#region Constructors/Destructors

		public _AdapterSettingsUserControl()
		{
		}

		#endregion
	}
}