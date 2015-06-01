/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Views
{
	public interface IDictionarySpecView : IListView
	{
		#region Properties/Indexers/Events

		string AdapterType
		{
			get;
		}

		IAdapterSettingsView DictionaryAdapterSettings
		{
			get;
		}

		string DictionaryId
		{
			get;
		}

		bool PreloadEnabled
		{
			get;
		}

		long? RecordCount
		{
			get;
		}

		#endregion
	}
}