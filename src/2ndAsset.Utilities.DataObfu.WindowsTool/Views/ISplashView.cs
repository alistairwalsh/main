/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Drawing;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Views
{
	public interface ISplashView : IFullView
	{
		#region Properties/Indexers/Events

		Image AppLogo
		{
			set;
		}

		#endregion
	}
}