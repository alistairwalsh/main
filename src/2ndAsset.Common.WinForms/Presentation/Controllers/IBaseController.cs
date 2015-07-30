/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Presentation.Controllers
{
	public interface IBaseController
	{
		#region Properties/Indexers/Events

		IBaseView View
		{
			get;
		}

		#endregion

		#region Methods/Operators

		void InitializeView(IBaseView view);

		void TerminateView();

		#endregion
	}
}