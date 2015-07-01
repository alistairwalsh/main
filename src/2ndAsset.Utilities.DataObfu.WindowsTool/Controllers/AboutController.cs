/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Drawing;
using System.IO;

using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers
{
	public sealed class AboutController : BaseController<IAboutView>
	{
		#region Constructors/Destructors

		public AboutController()
		{
		}

		#endregion

		#region Methods/Operators

		public override void InitializeView(IAboutView view)
		{
			const string RESOURCE_NAME = "_2ndAsset.Utilities.DataObfu.WindowsTool.Images.SplashScreen.png";
			Stream stream;
			Image image;

			base.InitializeView(view);

			this.View.ViewText = string.Format("About {0} Studio", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);
			this.View.StatusText = "Ready";

			this.View.Version = ExecutableApplicationFascade.Current.AssemblyInformationFascade.AssemblyVersion;
			this.View.Company = ExecutableApplicationFascade.Current.AssemblyInformationFascade.Company;
			this.View.Configuration = ExecutableApplicationFascade.Current.AssemblyInformationFascade.Configuration;
			this.View.Copyright = ExecutableApplicationFascade.Current.AssemblyInformationFascade.Copyright;
			this.View.InformationalVersion = ExecutableApplicationFascade.Current.AssemblyInformationFascade.InformationalVersion;
			this.View.Product = string.Format("{0} Studio", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);
			this.View.Title = ExecutableApplicationFascade.Current.AssemblyInformationFascade.Title;
			this.View.Trademark = ExecutableApplicationFascade.Current.AssemblyInformationFascade.Trademark;
			this.View.Win32FileVersion = ExecutableApplicationFascade.Current.AssemblyInformationFascade.Win32FileVersion;
			this.View.Description = ExecutableApplicationFascade.Current.AssemblyInformationFascade.Description;

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