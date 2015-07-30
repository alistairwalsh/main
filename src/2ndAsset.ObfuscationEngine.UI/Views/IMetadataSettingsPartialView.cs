/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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