/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Windows.Forms;

namespace _2ndAsset.Common.WinForms.Controls
{
	public class BaseUserControl : UserControl
	{
		#region Constructors/Destructors

		public BaseUserControl()
		{
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

		protected virtual void CoreTeardown()
		{
			// do nothing
		}

		protected override void Dispose(bool disposing)
		{
			this.CoreTeardown();
			base.Dispose(disposing);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.CoreSetup();
			this.CoreRefreshControlState();
		}

		#endregion
	}
}