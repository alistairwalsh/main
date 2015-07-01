/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IExecutionView : IPartialView
	{
		#region Properties/Indexers/Events

		double? DurationSeconds
		{
			set;
		}

		bool? IsComplete
		{
			set;
		}

		long? RecordCount
		{
			set;
		}

		#endregion
	}
}