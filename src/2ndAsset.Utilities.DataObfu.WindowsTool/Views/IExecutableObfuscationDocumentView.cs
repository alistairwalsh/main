/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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