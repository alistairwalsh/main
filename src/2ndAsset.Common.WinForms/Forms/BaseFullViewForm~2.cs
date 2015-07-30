/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Forms
{
	public class BaseFullViewForm<TFullView, TMasterController> : BaseFullViewForm, IDispatchTargetProvider
		where TFullView : class, IFullView
		where TMasterController : MasterController<TFullView>, new()
	{
		#region Constructors/Destructors

		public BaseFullViewForm()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly TMasterController controller = new TMasterController();

		#endregion

		#region Properties/Indexers/Events

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected new TFullView _
		{
			get
			{
				return this as TFullView;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected TMasterController Controller
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

		protected override void CoreShown()
		{
			base.CoreShown();

			if ((object)this._ != null) // prevent designer from barfing
				this.Controller.ReadyView();
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