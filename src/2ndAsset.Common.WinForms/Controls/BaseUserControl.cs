/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.ComponentModel;
using System.Windows.Forms;

using _2ndAsset.Common.WinForms.Forms;

namespace _2ndAsset.Common.WinForms.Controls
{
	public class BaseUserControl : UserControl
	{
		#region Constructors/Destructors

		public BaseUserControl()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected BaseForm CoreOwnerForm
		{
			get
			{
				return this.CoreGetParent<BaseForm>();
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected BaseUserControl CoreParentUserControl
		{
			get
			{
				return this.CoreGetParent<BaseUserControl>();
			}
		}

		#endregion

		#region Methods/Operators

		protected virtual void CoreRefreshControlState()
		{
			// do nothing
		}

		protected virtual void CoreSetup()
		{
			// do nothing
		}

		protected virtual void CoreShown()
		{
			// do nothing
		}

		protected virtual void CoreTeardown()
		{
			// do nothing
		}

		protected override void Dispose(bool disposing)
		{
			//this.CoreTeardown();
			base.Dispose(disposing);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.CoreSetup();
			this.CoreRefreshControlState();
		}

		#endregion
	}
}