/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace _2ndAsset.ObfuscationEngine.UI.Forms
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