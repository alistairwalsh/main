/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.ComponentModel;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Controls
{
	public class BaseUserControl<TPartialView, TSlaveController> : XBaseUserControl
		where TPartialView : class, IPartialView
		where TSlaveController : SlaveController<TPartialView>, new()
	{
		#region Constructors/Destructors

		public BaseUserControl()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly TSlaveController controller = new TSlaveController();

		#endregion

		#region Properties/Indexers/Events

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public TSlaveController Controller
		{
			get
			{
				return this.controller;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new TPartialView PartialView
		{
			get
			{
				return base.PartialView as TPartialView;
			}
		}

		#endregion

		#region Methods/Operators

		protected override IBaseController CoreGetController()
		{
			return this.Controller;
		}

		protected override void CoreSetup()
		{
			base.CoreSetup();

			if ((object)this.PartialView != null)
				this.Controller.InitializeView(this.PartialView);
		}

		protected override void CoreTeardown()
		{
			if ((object)this.PartialView != null)
				this.Controller.TerminateView();

			base.CoreTeardown();
		}

		#endregion
	}
}