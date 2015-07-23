/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Controllers;

namespace _2ndAsset.Common.WinForms.Presentation.Views
{
	public interface IBaseView
	{
		#region Properties/Indexers/Events

		IBaseController Controller
		{
			get;
		}

		IBaseView ParentView
		{
			get;
		}

		#endregion
	}
}