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
	public class BasePartialViewUserControl<TPartialView, TSlaveController> : BasePartialViewUserControl, IDispatchTargetProvider
		where TPartialView : class, IPartialView
		where TSlaveController : SlaveController<TPartialView>, new()
	{
		#region Constructors/Destructors

		public BasePartialViewUserControl()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly TSlaveController controller = new TSlaveController();

		#endregion

		#region Properties/Indexers/Events

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected new TPartialView _
		{
			get
			{
				return this as TPartialView;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected TSlaveController Controller
		{
			get
			{
				return this.controller;
			}
		}

		object IDispatchTargetProvider.Target
		{
			get
			{
				return this.Controller;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreSetup()
		{
			base.CoreSetup();

			if ((object)this._ != null) // prevent designer from barfing
				this.Controller.InitializeView(this._);
		}

		protected override void CoreTeardown()
		{
			if ((object)this._ != null) // prevent designer from barfing
				this.Controller.TerminateView();

			base.CoreTeardown();
		}

		#endregion
	}
}