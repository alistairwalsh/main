/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IDictionarySpecListView : IListView
	{
		#region Properties/Indexers/Events

		string AdapterType
		{
			get;
		}

		IAdapterSettingsPartialView DictionaryAdapterSettingsPartialView
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