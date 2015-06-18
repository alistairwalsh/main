/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Reflection;

using TextMetal.Middleware.Common.Utilities;

namespace _2ndAsset.Common.WinForms.Presentation
{
	public abstract class BaseController<TView> : IBaseController<TView>
		where TView : class, IFullView
	{
		#region Constructors/Destructors

		protected BaseController()
		{
		}

		#endregion

		#region Fields/Constants

		private TView view;

		#endregion

		#region Properties/Indexers/Events

		public TView View
		{
			get
			{
				return this.view;
			}
		}

		#endregion

		#region Methods/Operators

		public object DispatchAction(IPartialView partialView, Uri controllerActionUri, object context)
		{
			MethodInfo[] methodInfos;
			DispatchActionUriAttribute dispatchActionUriAttribute;
			Uri tempUri;

			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)controllerActionUri == null)
				throw new ArgumentNullException("controllerActionUri");

			if((object)this.View == null)
				throw new InvalidOperationException(string.Format("Cannot dispatch action URI '{0}' when the controller type '{1}' has never been initialized.", controllerActionUri, this.GetType().FullName));

			methodInfos = this.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

			if ((object)methodInfos != null)
			{
				foreach (MethodInfo methodInfo in methodInfos)
				{
					dispatchActionUriAttribute = ReflectionFascade.Instance.GetOneAttribute<DispatchActionUriAttribute>(methodInfo);

					if ((object)dispatchActionUriAttribute != null &&
						Uri.TryCreate(dispatchActionUriAttribute.Uri, UriKind.Absolute, out tempUri) &&
						tempUri == controllerActionUri)
						return methodInfo.Invoke(this, new object[] { partialView, context });
				}
			}

			return this.UnhandledActionDispatch(partialView, controllerActionUri, context);
		}

		public virtual void InitializeView(TView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			this.view = view;
		}

		public virtual void ReadyView()
		{
			this.View.StatusText = "Ready.";
		}

		public virtual void TerminateView()
		{
			this.view = null;
		}

		public virtual object UnhandledActionDispatch(IPartialView partialView, Uri controllerActionUri, object context)
		{
			throw new InvalidOperationException(string.Format("An action with the disptach URI '{0}' could not be found on the controller type '{1}'.", controllerActionUri, this.GetType().FullName));
		}

		#endregion
	}
}