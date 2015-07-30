/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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