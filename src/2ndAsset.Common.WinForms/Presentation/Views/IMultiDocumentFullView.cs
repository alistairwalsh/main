/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
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