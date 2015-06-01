/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
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