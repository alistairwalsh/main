/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.UI.Views.Adapters
{
	public interface IDelimitedTextAdapterSettingsPartialView : IAdapterSpecificSettingsPartialView
	{
		#region Properties/Indexers/Events

		IEnumerable<IHeaderSpecListView> HeaderSpecViews
		{
			get;
		}

		string FieldDelimiter
		{
			get;
			set;
		}

		bool IsFirstRowHeaders
		{
			get;
			set;
		}

		string QuoteValue
		{
			get;
			set;
		}

		string RecordDelimiter
		{
			get;
			set;
		}

		IHeaderSpecListView SelectedHeaderSpecListView
		{
			get;
			set;
		}

		string TextFilePath
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		IHeaderSpecListView AddHeaderSpecView(string headerName, FieldType? fieldType);

		void ClearHeaderSpecViews();

		bool RemoveHeaderSpecView(IHeaderSpecListView headerSpecListView);

		#endregion
	}
}