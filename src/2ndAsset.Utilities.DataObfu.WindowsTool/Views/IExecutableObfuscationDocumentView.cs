/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Views
{
	public interface IExecutableObfuscationDocumentView : IObfuscationDocumentView
	{
		#region Properties/Indexers/Events

		IExecutionPartialView ExecutionPartialView
		{
			get;
		}

		#endregion
	}
}