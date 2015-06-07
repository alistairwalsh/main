/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Globalization;

namespace _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText
{
	public class HeaderSpec
	{
		#region Constructors/Destructors

		public HeaderSpec()
		{
		}

		#endregion

		#region Fields/Constants

		private NumberFormatInfo fieldNumberFormatSpec;
		private FieldType fieldType;
		private string headerName;

		#endregion

		#region Properties/Indexers/Events

		public NumberFormatInfo FieldNumberFormatSpec
		{
			get
			{
				return this.fieldNumberFormatSpec;
			}
			set
			{
				this.fieldNumberFormatSpec = value;
			}
		}

		public FieldType FieldType
		{
			get
			{
				return this.fieldType;
			}
			set
			{
				this.fieldType = value;
			}
		}

		public string HeaderName
		{
			get
			{
				return this.headerName;
			}
			set
			{
				this.headerName = value;
			}
		}

		#endregion
	}
}