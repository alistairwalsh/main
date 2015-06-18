/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Forms
{
	public interface IDataConnectionConfiguration
	{
		#region Methods/Operators

		string GetSelectedProvider();

		string GetSelectedSource();

		void SaveSelectedProvider(string provider);

		void SaveSelectedSource(string provider);

		#endregion
	}
}