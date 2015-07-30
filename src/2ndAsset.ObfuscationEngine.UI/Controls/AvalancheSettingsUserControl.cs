/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;
using System.Windows.Forms;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.ObfuscationEngine.UI.Controllers;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	public partial class AvalancheSettingsUserControl : _AvalancheSettingsUserControl, IAvalancheSettingsPartialView
	{
		#region Constructors/Destructors

		public AvalancheSettingsUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		long? IAvalancheSettingsPartialView.HashMultiplier
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

		long? IAvalancheSettingsPartialView.HashSeed
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
			this.Controller.RegenerateHashSettings();
		}

		#endregion
	}

	public class _AvalancheSettingsUserControl : BasePartialViewUserControl<IAvalancheSettingsPartialView, AvalancheSettingsSlaveController>
	{
		#region Constructors/Destructors

		public _AvalancheSettingsUserControl()
		{
		}

		#endregion
	}
}