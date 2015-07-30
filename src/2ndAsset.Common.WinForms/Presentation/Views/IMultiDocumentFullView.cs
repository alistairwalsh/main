/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

namespace _2ndAsset.Common.WinForms.Presentation.Views
{
	public interface IMultiDocumentFullView : IFullView
	{
		#region Properties/Indexers/Events

		IList<IDocumentFullView> DocumentViews
		{
			get;
		}

		#endregion

		#region Methods/Operators

		IDocumentFullView CreateDocumentView(Uri viewUri, string documentFilePath);

		#endregion
	}
}