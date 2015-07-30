/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IHeaderSpecListView : IListView
	{
		#region Properties/Indexers/Events

		FieldType? FieldType
		{
			get;
		}

		string HeaderName
		{
			get;
		}

		#endregion
	}
}