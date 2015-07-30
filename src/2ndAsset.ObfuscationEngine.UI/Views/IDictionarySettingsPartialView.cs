/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms.Presentation.Views;

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