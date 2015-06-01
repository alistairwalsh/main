/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Drawing;

using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Views
{
	public interface IAboutView : IFullView
	{
		#region Properties/Indexers/Events

		Image AppLogo
		{
			set;
		}

		string Company
		{
			set;
		}

		string Configuration
		{
			set;
		}

		string Copyright
		{
			set;
		}

		string Description
		{
			set;
		}

		string InformationalVersion
		{
			set;
		}

		string Product
		{
			set;
		}

		string Title
		{
			set;
		}

		string Trademark
		{
			set;
		}

		string Version
		{
			set;
		}

		string Win32FileVersion
		{
			set;
		}

		#endregion
	}
}