/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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

		#region Fields/Constants

		private readonly IDictionary<IPartialView, ISlaveController> childMap = new Dictionary<IPartialView, ISlaveController>();

		#endregion

		#region Properties/Indexers/Events

		public IDictionary<IPartialView, ISlaveController> ChildMap
		{
			get
			{
				return this.childMap;
			}
		}

		#endregion

		#region Methods/Operators

		public void ApplyModelToView<TModel>(TModel model)
		{
			if ((object)model == null)
				throw new ArgumentNullException("model");

			// do nothing
		}

		public void ApplyViewToModel<TModel>(TModel model)
		{
			if ((object)model == null)
				throw new ArgumentNullException("model");

			// do nothing
		}

		[DispatchActionUri(Uri = URI_CONTROLLER_ATTACH_CHILD_EVENT)]
		public void AttachChild(IPartialView partialView, ISlaveController slaveController)
		{
			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)slaveController == null)
				throw new ArgumentNullException("slaveController");

			this.ChildMap.Add(partialView, slaveController);
		}

		protected IEnumerable<IEnumerable<object>> BroadcastPresentationEvent(Uri controllerActionUri, object controllerActionContext)
		{
			// TUNNEL
			IEnumerable<object> controllerActionResult;
			List<IEnumerable<object>> controllerActionResults;

			controllerActionResults = new List<IEnumerable<object>>();

			foreach (IPartialView partialView in this.ChildMap.Keys)
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
			this.ChildMap.Clear();

			base.TerminateView();
		}

		#endregion
	}
}