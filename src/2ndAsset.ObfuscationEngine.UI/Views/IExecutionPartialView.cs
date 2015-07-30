﻿/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IExecutionPartialView : IPartialView
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