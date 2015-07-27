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

		public const string URI_CONTROLLER_ATTACH_CHILD_EVENT = "event://controller/attach-child";
		private static readonly Uri controllerAttachChildEventUri = new Uri(URI_CONTROLLER_ATTACH_CHILD_EVENT);
		private IBaseView view;

		#endregion

		#region Properties/Indexers/Events

		public static Uri ControllerAttachChildEventUri
		{
			get
			{
				return controllerAttachChildEventUri;
			}
		}

		public IBaseView View
		{
			get
			{
				return this.view;
			}
			private set
			{
				this.view = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected bool DispatchPresentationEvent(IBaseView targetView, Uri controllerActionUri, object controllerActionContext, out IEnumerable<object> controllerActionResult)
		{
			MethodInfo[] methodInfos;
			DispatchActionUriAttribute dispatchActionUriAttribute;
			Uri tempUri;
			object retval;
			List<object> controllerActionResults;
			IDispatchTargetProvider dispatchTargetProvider;
			object dispatchTarget;

			if ((object)controllerActionUri == null)
				throw new ArgumentNullException("controllerActionUri");

			if ((object)targetView == null)
				throw new ArgumentNullException("targetView");

			controllerActionResults = new List<object>();

			dispatchTarget = null;
			dispatchTargetProvider = targetView as IDispatchTargetProvider;

			if ((object)dispatchTargetProvider != null)
				dispatchTarget = dispatchTargetProvider.Target;

			if ((object)dispatchTarget != null)
			{
				methodInfos = dispatchTarget.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

				if ((object)methodInfos != null)
				{
					foreach (MethodInfo methodInfo in methodInfos)
					{
						dispatchActionUriAttribute = ReflectionFascade.Instance.GetOneAttribute<DispatchActionUriAttribute>(methodInfo);

						if ((object)dispatchActionUriAttribute != null &&
							Uri.TryCreate(dispatchActionUriAttribute.Uri, UriKind.Absolute, out tempUri) &&
							tempUri == controllerActionUri)
						{
							retval = methodInfo.Invoke(dispatchTarget, new object[] { /* the view of the controller raising event */ this.View, controllerActionContext });
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

			this.View = view;
		}

		public virtual void TerminateView()
		{
			this.View = null;
		}

		protected virtual IEnumerable<IEnumerable<object>> UnhandledEventDispatch(Uri controllerActionUri, object controllerActionContext)
		{
			throw new InvalidOperationException(string.Format("An unhandled event dispatch occured with URI '{0}'.", controllerActionUri));
		}

		#endregion
	}
}