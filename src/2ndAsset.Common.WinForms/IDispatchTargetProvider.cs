/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace _2ndAsset.Common.WinForms
{
	public interface IDispatchTargetProvider
	{
		#region Properties/Indexers/Events

		object Target
		{
			get;
		}

		#endregion
	}
}