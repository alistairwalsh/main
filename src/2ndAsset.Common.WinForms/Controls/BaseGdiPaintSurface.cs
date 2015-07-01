/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Windows.Forms;

namespace _2ndAsset.Common.WinForms.Controls
{
	public class BaseGdiPaintSurface : _2ndAssetUserControl
	{
		#region Constructors/Destructors

		public BaseGdiPaintSurface()
		{
			this.SetStyle(ControlStyles.ResizeRedraw, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.UserPaint, true);
			this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		#endregion
	}
}