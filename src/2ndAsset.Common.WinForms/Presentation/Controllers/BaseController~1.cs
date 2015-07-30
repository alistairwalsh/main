/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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

		public new TView View
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

		public override sealed void InitializeView(IBaseView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);
		}

		#endregion
	}
}