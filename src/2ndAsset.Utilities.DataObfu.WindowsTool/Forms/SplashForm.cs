﻿/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Drawing;

using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Forms
{
	internal partial class SplashForm : _SplashForm, ISplashView
	{
		#region Constructors/Destructors

		public SplashForm()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		Image ISplashView.AppLogo
		{
			set
			{
				this.pbAppLogo.Image = value;
			}
		}

		#endregion

		#region Methods/Operators

		private void closeFormBy_Click(object sender, EventArgs e)
		{
			this.Controller.ProceedNow();
		}

		protected override void CoreSetup()
		{
			base.CoreSetup();
		}

		private void tmrMain_Tick(object sender, EventArgs e)
		{
			this.pbMain.Value += (int)(this.pbMain.Maximum * 0.10);

			if (this.pbMain.Value >= this.pbMain.Maximum)
			{
				this.tmrMain.Enabled = false;
				this.closeFormBy_Click(sender, e);
			}
		}

		#endregion
	}

	public class _SplashForm : _2ndAssetForm<ISplashView, SplashController>
	{
		#region Constructors/Destructors

		public _SplashForm()
		{
		}

		#endregion
	}
}