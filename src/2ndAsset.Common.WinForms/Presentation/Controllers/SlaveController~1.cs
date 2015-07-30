/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Presentation.Controllers
{
	public abstract class SlaveController<TView> : BaseController<TView>, ISlaveController
		where TView : class, IPartialView
	{
		#region Constructors/Destructors

		protected SlaveController()
		{
		}

		#endregion

		#region Methods/Operators

		protected IEnumerable<IEnumerable<object>> EmitPresentationEvent(Uri controllerActionUri, object controllerActionContext)
		{
			// BUBBLE
			IEnumerable<object> controllerActionResult;
			List<IEnumerable<object>> controllerActionResults;

			IBaseView current;

			controllerActionResults = new List<IEnumerable<object>>();
			current = this.View;

			while ((object)current != null)
			{
				if (this.DispatchPresentationEvent(current, controllerActionUri, controllerActionContext, out controllerActionResult))
					controllerActionResults.Add(controllerActionResult);

				current = current.ParentView;
			}

			if (controllerActionResults.Count < 1)
				return this.UnhandledEventDispatch(controllerActionUri, controllerActionContext);

			return controllerActionResults;
		}

		public override void InitializeView(TView view)
		{
			IEnumerable<object> controllerActionResult;

			base.InitializeView(view);

			this.DispatchPresentationEvent(this.View.FullView, ControllerAttachChildEventUri, this, out controllerActionResult);
		}

		#endregion
	}
}