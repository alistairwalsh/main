/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public sealed class ObfuscationSlaveController : SlaveController<IObfuscationPartialView>
	{
		#region Constructors/Destructors

		public ObfuscationSlaveController()
		{
		}

		#endregion

		#region Methods/Operators

		public override void InitializeView(IObfuscationPartialView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);

			this.View.ConfigurationVersion = ObfuscationConfiguration.CurrentConfigurationVersion.ToString();
		}

		#endregion
	}
}