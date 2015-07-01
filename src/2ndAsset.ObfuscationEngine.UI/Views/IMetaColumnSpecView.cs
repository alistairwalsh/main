/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.ObfuscationEngine.Core;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IMetaColumnSpecView : IListView
	{
		#region Properties/Indexers/Events

		string ColumnName
		{
			get;
		}

		string DictionaryRef
		{
			get;
		}

		int? ExtentValue
		{
			get;
		}

		bool? IsColumnNullable
		{
			get;
		}

		ObfuscationStrategy? ObfuscationStrategy
		{
			get;
		}

		#endregion
	}
}