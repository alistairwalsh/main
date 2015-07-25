/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.ObfuscationEngine.UI.Controllers;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Forms
{
	public partial class AdapterSettingsForm : _AdapterSettingsForm, IAdapterSettingsFullView
	{
		#region Constructors/Destructors

		public AdapterSettingsForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		IAdapterSettingsPartialView IAdapterSettingsFullView.AdapterSettingsPartialView
		{
			get
			{
				return this.adapterSettingsUc;
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

	public class _AdapterSettingsForm : BaseFullViewForm<IAdapterSettingsFullView, AdapterSettingsMasterController>
	{
		#region Constructors/Destructors

		public _AdapterSettingsForm()
		{
		}

		#endregion
	}
}