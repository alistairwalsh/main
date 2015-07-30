/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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