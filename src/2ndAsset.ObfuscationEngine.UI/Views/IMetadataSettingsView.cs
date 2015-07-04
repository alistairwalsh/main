/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IMetadataSettingsView : IPartialView
	{
		#region Properties/Indexers/Events

		IEnumerable<IMetaColumnSpecView> MetaColumnSpecViews
		{
			get;
		}

		IMetaColumnSpecView SelectedMetaColumnSpecView
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		IMetaColumnSpecView AddMetaColumnSpecView(string columnName, string obfuscationStrategyAqtn);

		void ClearMetaColumnSpecViews();

		bool RemoveMetaColumnSpecView(IMetaColumnSpecView headerSpecView);

		#endregion
	}
}