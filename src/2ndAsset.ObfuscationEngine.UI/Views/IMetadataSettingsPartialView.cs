/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IMetadataSettingsPartialView : IPartialView
	{
		#region Properties/Indexers/Events

		IEnumerable<IMetaColumnSpecListView> MetaColumnSpecListViews
		{
			get;
		}

		IMetaColumnSpecListView SelectedMetaColumnSpecListView
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		IMetaColumnSpecListView AddMetaColumnSpecView(string columnName, string obfuscationStrategyAqtn);

		void ClearMetaColumnSpecViews();

		bool RemoveMetaColumnSpecView(IMetaColumnSpecListView metaColumnSpecListView);

		#endregion
	}
}