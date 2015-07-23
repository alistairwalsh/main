/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
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
			this.DispatchPresentationEvent(Constants.ExecuteObfuscationEventUri, null);
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