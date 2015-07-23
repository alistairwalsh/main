﻿/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Presentation.Controllers
{
	public abstract class MasterController<TView> : BaseController<TView>
		where TView : class, IFullView
	{
		#region Constructors/Destructors

		protected MasterController()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object DispatchPresentationEvent(Uri controllerActionUri, IDictionary<string, object> controllerActionContext)
		{
			// BUBBLE
			object controllerActionResult;
			IBaseView current;

			current = this.View;

			while ((object)current != null)
			{
				if (this.DispatchPresentationEvent(current, controllerActionUri, controllerActionContext, out controllerActionResult))
					return controllerActionResult;

				current = current.ParentView;
			}

			return this.UnhandledEventDispatch(controllerActionUri, controllerActionContext);
		}

		public virtual void ReadyView()
		{
			this.View.StatusText = "Ready.";
		}

		#endregion
	}
}