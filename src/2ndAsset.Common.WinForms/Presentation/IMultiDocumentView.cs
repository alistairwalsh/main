/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

namespace _2ndAsset.Common.WinForms.Presentation
{
	public interface IMultiDocumentView : IFullView
	{
		#region Properties/Indexers/Events

		IList<IDocumentView> DocumentViews
		{
			get;
		}

		#endregion

		#region Methods/Operators

		IDocumentView CreateDocumentView(Uri viewUri, string documentFilePath);

		#endregion
	}
}