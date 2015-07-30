/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.ObfuscationEngine.UI;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers
{
	public sealed class ExecutionController : SlaveController<IExecutionPartialView>
	{
		#region Constructors/Destructors

		public ExecutionController()
		{
		}

		#endregion

		#region Methods/Operators

		public void Execute()
		{
			this.EmitPresentationEvent(Constants.ExecuteObfuscationEventUri, null);
		}

		public override void InitializeView(IExecutionPartialView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);
		}

		#endregion
	}
}