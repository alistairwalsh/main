/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Views
{
	public interface IDelTextAdapterSettingsView : ISpecificAdapterSettingsView
	{
		#region Properties/Indexers/Events

		IEnumerable<IHeaderSpecView> HeaderSpecViews
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

		IHeaderSpecView SelectedHeaderSpecView
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

		IHeaderSpecView AddHeaderSpecView(string headerName, FieldType? fieldType);

		void ClearHeaderSpecViews();

		bool RemoveHeaderSpecView(IHeaderSpecView headerSpecView);

		#endregion
	}
}