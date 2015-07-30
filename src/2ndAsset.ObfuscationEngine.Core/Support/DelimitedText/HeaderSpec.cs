/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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