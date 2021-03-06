﻿/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.Core.Adapter;
using _2ndAsset.ObfuscationEngine.UI.Views.Adapters;

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