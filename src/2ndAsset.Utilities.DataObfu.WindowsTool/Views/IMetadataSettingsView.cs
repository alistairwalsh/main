/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.ObfuscationEngine.Core;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Views
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

		IMetaColumnSpecView AddMetaColumnSpecView(string columnName, bool? isColumnNullable, ObfuscationStrategy? obfuscationStrategy, string dictionaryRef, int? extentValue);

		void ClearMetaColumnSpecViews();

		bool RemoveMetaColumnSpecView(IMetaColumnSpecView headerSpecView);

		#endregion
	}
}