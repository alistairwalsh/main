/*
	Copyright �2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.UI.Views
{
	public interface IDelTextAdapterSettingsPartialView : IAdapterSpecificSettingsPartialView
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