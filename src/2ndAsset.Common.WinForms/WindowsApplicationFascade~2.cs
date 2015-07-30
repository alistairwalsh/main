/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Windows.Forms;

namespace _2ndAsset.Common.WinForms
{
	public abstract class WindowsApplicationFascade<TMainForm, TSplashForm> : WindowsApplicationFascade<TMainForm>
		where TMainForm : Form, new()
		where TSplashForm : Form, new()
	{
		#region Constructors/Destructors

		protected WindowsApplicationFascade()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public bool ShowSplashScreen
		{
			get
			{
				return this.AppConfigFascade.GetAppSetting<bool>(string.Format("{0}::ShowSplashScreen", this.GetType().Namespace));
			}
		}

		#endregion

		#region Methods/Operators

		protected override sealed void OnRunApplication()
		{
			if (!this.ShowSplashScreen)
				base.OnRunApplication();
			else
				Application.Run(new SplashApplicationContext(new TMainForm(), new TSplashForm()));
		}

		#endregion
	}
}