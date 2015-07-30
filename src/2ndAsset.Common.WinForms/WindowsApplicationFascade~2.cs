/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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