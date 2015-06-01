/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.ComponentModel;
using System.Windows.Forms;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controls
{
	public partial class AvalancheSettingsUserControl : _AvalancheSettingsUserControl, IAvalancheSettingsView
	{
		#region Constructors/Destructors

		public AvalancheSettingsUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		long? IAvalancheSettingsView.HashMultiplier
		{
			get
			{
				return this.txtBxHashMultiplier.CoreGetValue<long?>();
			}
			set
			{
				this.txtBxHashMultiplier.CoreSetValue<long?>(value, null);
			}
		}

		long? IAvalancheSettingsView.HashSeed
		{
			get
			{
				return this.txtBxHashSeed.CoreGetValue<long?>();
			}
			set
			{
				this.txtBxHashSeed.CoreSetValue<long?>(value, null);
			}
		}

		#endregion

		#region Methods/Operators

		private void _all_txtBx_Validating(object sender, CancelEventArgs e)
		{
			TextBox textBox;
			bool isValid;

			textBox = (TextBox)sender;

			if (!textBox.CoreIsEmpty())
			{
				isValid = textBox.CoreIsValid<long?>();
				textBox.CoreInputValidation(isValid);
			}
		}

		private void btnRegenerateHashValues_Click(object sender, EventArgs e)
		{
			this.FullView.DispatchControllerAction(this, new Uri("action://obfuscation/hash-settings/regenerate-all"), null);
		}

		#endregion
	}

	public class _AvalancheSettingsUserControl : _2ndAssetUserControl<IAvalancheSettingsView>
	{
		#region Constructors/Destructors

		public _AvalancheSettingsUserControl()
		{
		}

		#endregion
	}
}