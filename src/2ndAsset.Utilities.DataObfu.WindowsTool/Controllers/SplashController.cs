/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Drawing;
using System.IO;

using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers
{
	public sealed class SplashController : MasterController<ISplashView>
	{
		#region Constructors/Destructors

		public SplashController()
		{
		}

		#endregion

		#region Methods/Operators

		public override void InitializeView(ISplashView view)
		{
			const string RESOURCE_NAME = "_2ndAsset.Utilities.DataObfu.WindowsTool.Images.SplashScreen.png";
			Stream stream;
			Image image;

			base.InitializeView(view);

			this.View.ViewText = string.Format("Launching {0} Studio...", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);
			this.View.StatusText = "Ready";

			stream = this.GetType().Assembly.GetManifestResourceStream(RESOURCE_NAME);

			if ((object)stream == null)
				throw new InvalidOperationException(string.Format("Manifest resource name '{0}' was not found in assembly '{1}'.", RESOURCE_NAME, this.GetType().Assembly));

			image = Image.FromStream(stream);

			this.View.AppLogo = image;
			// DO NOT DISPOSE (owner cleans up)
		}

		public void ProceedNow()
		{
			this.View.CloseView(false);
		}

		#endregion
	}
}