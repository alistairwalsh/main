/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Windows.Forms;

namespace _2ndAsset.Common.WinForms.Controls
{
	public class BaseCheckBox : CheckBox
	{
		#region Constructors/Destructors

		public BaseCheckBox()
		{
		}

		#endregion

		#region Methods/Operators

		protected override void OnCheckedChanged(EventArgs e)
		{
			base.OnCheckedChanged(e);

			this.CoreSetParentFormDirty(true);
		}

		protected override void OnCheckStateChanged(EventArgs e)
		{
			base.OnCheckStateChanged(e);

			this.CoreSetParentFormDirty(true);
		}

		#endregion
	}
}