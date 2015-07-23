/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Presentation.Controllers
{
	public abstract class BaseController<TView> : BaseController, IBaseController<TView>
		where TView : class, IBaseView
	{
		#region Constructors/Destructors

		protected BaseController()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		new public TView View
		{
			get
			{
				return (TView)base.View;
			}
		}

		#endregion

		#region Methods/Operators

		public virtual void InitializeView(TView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);
		}

		#endregion
	}
}