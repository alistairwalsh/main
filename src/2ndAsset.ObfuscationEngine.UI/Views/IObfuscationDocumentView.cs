/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IObfuscationDocumentView : IDocumentFullView
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