/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Views
{
	public interface IAdapterSettingsView : IPartialView
	{
		#region Properties/Indexers/Events

		IAdoNetAdapterSettingsView AdoNetAdapterSettingsView
		{
			get;
		}

		IDelTextAdapterSettingsView DelTextAdapterSettingsView
		{
			get;
		}

		IEnumerable<IListItem<Type>> AdapterTypes
		{
			set;
		}

		Type SelectedAdapterType
		{
			get;
			set;
		}

		#endregion
	}
}