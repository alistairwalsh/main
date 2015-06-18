/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;

using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.Common.WinForms.Forms
{
	public class _2ndAssetForm<TFullView, TController> : x_2ndAssetForm
		where TFullView : class, IFullView
		where TController : class, IBaseController<TFullView>, new()
	{
		#region Constructors/Destructors

		public _2ndAssetForm()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly TController controller = new TController();

		#endregion

		#region Properties/Indexers/Events

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TController Controller
		{
			get
			{
				return this.controller;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new TFullView FullView
		{
			get
			{
				return base.FullView as TFullView;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreSetup()
		{
			base.CoreSetup();

			if ((object)this.FullView != null)
				this.Controller.InitializeView(this.FullView);
		}

		protected override void CoreShown()
		{
			base.CoreShown();

			if ((object)this.FullView != null)
				this.Controller.ReadyView();
		}

		protected override void CoreTeardown()
		{
			if ((object)this.FullView != null)
				this.Controller.TerminateView();

			base.CoreTeardown();
		}

		protected override object OnDispatchControllerAction(IPartialView partialView, Uri controllerActionUri, object context)
		{
			return this.Controller.DispatchAction(partialView, controllerActionUri, context);
		}

		#endregion
	}
}