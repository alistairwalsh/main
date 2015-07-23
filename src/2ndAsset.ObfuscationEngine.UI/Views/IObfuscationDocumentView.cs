/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IObfuscationDocumentView : IDocumentView
	{
		#region Properties/Indexers/Events

		IObfuscationPartialView ObfuscationPartialView
		{
			get;
		}

		#endregion

		#region Methods/Operators

		bool TryGetDatabaseConnection(ref Type connectionType, ref string connectionString);

		#endregion
	}
}