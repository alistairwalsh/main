﻿/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Drawing;

using _2ndAsset.Common.WinForms.Presentation;

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