/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Presentation.Controllers
{
	public abstract class MasterController<TView> : BaseController<TView>, IMasterController
		where TView : class, IFullView
	{
		#region Constructors/Destructors

		protected MasterController()
		{
		}

		#endregion

		#region Methods/Operators

		protected IEnumerable<IEnumerable<object>> BroadcastPresentationEvent(Uri controllerActionUri, object controllerActionContext)
		{
			// TUNNEL
			IEnumerable<object> controllerActionResult;
			List<IEnumerable<object>> controllerActionResults;

			controllerActionResults = new List<IEnumerable<object>>();

			foreach (IPartialView partialView in this.View.PartialViews)
			{
				if (this.DispatchPresentationEvent(partialView, controllerActionUri, controllerActionContext, out controllerActionResult))
					controllerActionResults.Add(controllerActionResult);
			}

			if (controllerActionResults.Count < 1)
				return this.UnhandledEventDispatch(controllerActionUri, controllerActionContext);

			return controllerActionResults;
		}

		public virtual void ReadyView()
		{
			this.View.StatusText = "Ready.";
		}

		public override void TerminateView()
		{
			/*foreach (IPartialView partialView in this.View.PartialViews)
				partialView;*/

			this.View.PartialViews.Clear();

			base.TerminateView();
		}

		#endregion
	}
}