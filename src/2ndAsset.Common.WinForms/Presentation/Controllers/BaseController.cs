/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Reflection;

using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Presentation.Controllers
{
	public abstract class BaseController : IBaseController
	{
		#region Constructors/Destructors

		protected BaseController()
		{
		}

		#endregion

		#region Fields/Constants

		private IBaseView view;

		#endregion

		#region Properties/Indexers/Events

		public IBaseView View
		{
			get
			{
				return this.view;
			}
		}

		#endregion

		#region Methods/Operators

		protected abstract object DispatchPresentationEvent(Uri controllerActionUri, IDictionary<string, object> controllerActionContext);

		protected bool DispatchPresentationEvent(IBaseView targetView, Uri controllerActionUri, IDictionary<string, object> controllerActionContext, out object controllerActionResult)
		{
			MethodInfo[] methodInfos;
			DispatchActionUriAttribute dispatchActionUriAttribute;
			Uri tempUri;
			object retval;
			List<object> controllerActionResults;

			if ((object)controllerActionUri == null)
				throw new ArgumentNullException("controllerActionUri");

			if ((object)targetView == null)
				throw new ArgumentNullException("targetView");

			controllerActionResults = new List<object>();

			if ((object)targetView.Controller != null)
			{
				methodInfos = targetView.Controller.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

				if ((object)methodInfos != null)
				{
					foreach (MethodInfo methodInfo in methodInfos)
					{
						dispatchActionUriAttribute = ReflectionFascade.Instance.GetOneAttribute<DispatchActionUriAttribute>(methodInfo);

						if ((object)dispatchActionUriAttribute != null &&
							Uri.TryCreate(dispatchActionUriAttribute.Uri, UriKind.Absolute, out tempUri) &&
							tempUri == controllerActionUri)
						{
							retval = methodInfo.Invoke(targetView.Controller, new object[] { /* the view of the controller raising event */ this.View, controllerActionContext });
							controllerActionResults.Add(retval);
						}
					}
				}
			}

			controllerActionResult = controllerActionResults.ToArray();
			return controllerActionResults.Count < 1;
		}

		public virtual void InitializeView(IBaseView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			this.view = view;
		}

		public virtual void TerminateView()
		{
			this.view = null;
		}

		protected virtual object UnhandledEventDispatch(Uri controllerActionUri, IDictionary<string, object> controllerActionContext)
		{
			throw new InvalidOperationException(string.Format("An unhandled event dispatch occured with URI '{0}'.", controllerActionUri));
		}

		public virtual void ApplyViewToModel(object model)
		{
			// do nothing
		}

		public virtual void ApplyModelToView(object model)
		{
			// do nothing
		}

		#endregion
	}
}