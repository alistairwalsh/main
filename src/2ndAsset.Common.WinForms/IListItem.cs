/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace _2ndAsset.Common.WinForms
{
	/// <summary>
	/// Represents a list item.
	/// </summary>
	public interface IListItem
	{
		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the list item text.
		/// </summary>
		string Text
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the list item value.
		/// </summary>
		object Value
		{
			get;
			set;
		}

		#endregion
	}
}