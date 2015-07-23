/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.Common.WinForms.Presentation.Views
{
	public static class ExtensionMethods
	{
		#region Methods/Operators

		public static T CoreGetParent<T>(this IBaseView view)
			where T : class, IBaseView
		{
			// Iterative method...
			IBaseView current;

			if ((object)view == null)
				throw new ArgumentNullException("view");

			current = view;

			do
			{
				current = current.ParentView;

				if (current is T)
					return (T)current;
			}
			while ((object)current != null);

			return null;
		}

		#endregion
	}
}