/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.Common.WinForms.Presentation
{
	public interface IBaseController<TView>
		where TView : class, IBaseView
	{
		#region Properties/Indexers/Events

		TView View
		{
			get;
		}

		#endregion

		#region Methods/Operators

		object DispatchAction(IPartialView partialView, Uri controllerActionUri, object context);

		void InitializeView(TView view);

		void ReadyView();

		void TerminateView();

		object UnhandledActionDispatch(IPartialView partialView, Uri controllerActionUri, object context);

		#endregion
	}
}