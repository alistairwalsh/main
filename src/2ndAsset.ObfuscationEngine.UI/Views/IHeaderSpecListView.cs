/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IHeaderSpecListView : IListView
	{
		#region Properties/Indexers/Events

		FieldType? FieldType
		{
			get;
		}

		string HeaderName
		{
			get;
		}

		#endregion
	}
}