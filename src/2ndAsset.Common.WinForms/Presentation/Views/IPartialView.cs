/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace _2ndAsset.Common.WinForms.Presentation.Views
{
	public interface IPartialView : IBaseView
	{
		#region Properties/Indexers/Events

		IFullView FullView
		{
			get;
		}

		#endregion
	}
}