﻿/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.Common.WinForms.Presentation.Views
{
	public interface IDocumentFullView : IFullView
	{
		#region Properties/Indexers/Events

		string FilePath
		{
			get;
			set;
		}

		#endregion
	}
}