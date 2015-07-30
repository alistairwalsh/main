/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace _2ndAsset.Common.WinForms
{
	/// <summary>
	/// Represents a list item with a strongly typed value.
	/// </summary>
	/// <typeparam name="TValue"> The type of the list item value. </typeparam>
	public interface IListItem<TValue> : IListItem
	{
		#region Properties/Indexers/Events

		/// <summary>
		/// Gets the list item value.
		/// </summary>
		new TValue Value
		{
			get;
			set;
		}

		#endregion
	}
}