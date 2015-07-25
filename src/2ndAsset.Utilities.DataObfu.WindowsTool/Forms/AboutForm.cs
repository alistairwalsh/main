/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Drawing;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Forms
{
	public partial class AboutForm : _AboutForm, IAboutView
	{
		#region Constructors/Destructors

		public AboutForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		Image IAboutView.AppLogo
		{
			set
			{
				this.pbAppLogo.Image = value;
			}
		}

		string IAboutView.Company
		{
			set
			{
				this.lblCompany.CoreSetValue(value);
			}
		}

		string IAboutView.Configuration
		{
			set
			{
				this.lblConfiguration.CoreSetValue(value);
			}
		}

		string IAboutView.Copyright
		{
			set
			{
				this.lblCopyright.CoreSetValue(value);
			}
		}

		string IAboutView.Description
		{
			set
			{
				this.txtBxDescription.CoreSetValue(value);
			}
		}

		string IAboutView.InformationalVersion
		{
			set
			{
				this.lblInformationalVersion.CoreSetValue(value);
			}
		}

		string IAboutView.Product
		{
			set
			{
				this.lblProduct.CoreSetValue(value);
			}
		}

		string IAboutView.Title
		{
			set
			{
				this.lblTitle.CoreSetValue(value);
			}
		}

		string IAboutView.Trademark
		{
			set
			{
				this.lblTrademark.CoreSetValue(value);
			}
		}

		string IAboutView.Version
		{
			set
			{
				this.lblVersion.CoreSetValue(value);
			}
		}

		string IAboutView.Win32FileVersion
		{
			set
			{
				this.lblWin32FileVersion.CoreSetValue(value);
			}
		}

		#endregion

		#region Methods/Operators

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Controller.ProceedNow();
		}

		protected override void CoreSetup()
		{
			base.CoreSetup();
		}

		#endregion
	}

	public class _AboutForm : BaseFullViewForm<IAboutView, AboutController>
	{
		#region Constructors/Destructors

		public _AboutForm()
		{
		}

		#endregion
	}
}