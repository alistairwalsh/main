/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IDictionarySettingsPartialView : IPartialView
	{
		#region Properties/Indexers/Events

		IEnumerable<IDictionarySpecListView> DictionarySpecListViews
		{
			get;
		}

		IDictionarySpecListView SelectedDictionarySpecListView
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		IDictionarySpecListView AddDictionarySpecView(string dictionaryId, bool preloadEnabled, long? recordCount, IAdapterSettingsPartialView adapterSettingsPartialView);

		void ClearDictionarySpecViews();

		bool RemoveDictionarySpecView(IDictionarySpecListView dictionarySpecListView);

		#endregion
	}
}