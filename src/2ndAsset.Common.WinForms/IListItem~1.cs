/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
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