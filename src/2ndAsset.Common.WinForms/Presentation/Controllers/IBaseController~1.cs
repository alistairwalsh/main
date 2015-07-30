/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Presentation.Controllers
{
	public interface IBaseController<TView> : IBaseController
		where TView : class, IBaseView
	{
		#region Properties/Indexers/Events

		new TView View
		{
			get;
		}

		#endregion

		#region Methods/Operators

		void InitializeView(TView view);

		#endregion
	}
}