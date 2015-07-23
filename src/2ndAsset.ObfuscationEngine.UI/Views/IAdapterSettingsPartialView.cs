/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.Core.Adapter;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IAdapterSettingsPartialView : IPartialView
	{
		#region Properties/Indexers/Events

		IAdapterSpecificSettingsPartialView CurrentAdapterSpecificSettingsPartialView
		{
			get;
		}

		AdapterDirection AdapterDirection
		{
			get;
			set;
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