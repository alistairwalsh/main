/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IMetaColumnSpecListView : IListView
	{
		#region Properties/Indexers/Events

		string ColumnName
		{
			get;
		}

		bool? IsColumnNullable
		{
			get;
		}

		string ObfuscationStrategyAqtn
		{
			get;
		}

		#endregion
	}
}