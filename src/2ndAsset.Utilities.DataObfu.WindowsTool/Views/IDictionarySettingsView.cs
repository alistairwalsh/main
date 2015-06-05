/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Views
{
	public interface IDictionarySettingsView : IPartialView
	{
		#region Properties/Indexers/Events

		IEnumerable<IDictionarySpecView> DictionarySpecViews
		{
			get;
		}

		IDictionarySpecView SelectedDictionarySpecView
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		IDictionarySpecView AddDictionarySpecView(string dictionaryId, bool preloadEnabled, long? recordCount, IAdapterSettingsView adapterSettingsView);

		void ClearDictionarySpecViews();

		bool RemoveDictionarySpecView(IDictionarySpecView headerSpecView);

		#endregion
	}
}