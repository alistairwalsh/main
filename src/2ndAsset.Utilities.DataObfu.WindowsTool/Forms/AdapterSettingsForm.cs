/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Forms
{
	public partial class AdapterSettingsForm : _AdapterSettingsForm, IAdapterSettingsView2
	{
		#region Constructors/Destructors

		public AdapterSettingsForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		IAdapterSettingsView IAdapterSettingsView2.DictionaryAdapterSettings
		{
			get
			{
				return this.dictionaryAdapterSettingsUc;
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

	public class _AdapterSettingsForm : _2ndAssetForm<IAdapterSettingsView2, AdapterSettingsController>
	{
		#region Constructors/Destructors

		public _AdapterSettingsForm()
		{
		}

		#endregion
	}
}