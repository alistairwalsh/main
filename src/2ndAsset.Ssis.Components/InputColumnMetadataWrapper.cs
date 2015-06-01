/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

using _2ndAsset.ObfuscationEngine.Core;

namespace _2ndAsset.Ssis.Components
{
	public sealed class InputColumnMetadataWrapper : CustomPropertyWrapper
	{
		#region Constructors/Destructors

		public InputColumnMetadataWrapper(IDTSInputColumn100 dtsInputColumn100)
			: base((object)dtsInputColumn100 == null ? null : dtsInputColumn100.CustomPropertyCollection)
		{
			if ((object)dtsInputColumn100 == null)
				throw new ArgumentNullException("dtsInputColumn100");

			this.dtsInputColumn100 = dtsInputColumn100;
		}

		#endregion

		#region Fields/Constants

		private readonly IDTSInputColumn100 dtsInputColumn100;
		private string columnName;

		#endregion

		#region Properties/Indexers/Events

		public string ColumnName
		{
			get
			{
				return this.DtsInputColumn100.Name;
			}
		}

		protected IDTSInputColumn100 DtsInputColumn100
		{
			get
			{
				return this.dtsInputColumn100;
			}
		}

		public string DictionaryReference
		{
			get
			{
				return this.GetProperty<string>(Constants.INPUTCOLUMN_PROP_NAME_DICTIONARY_REFERENCE);
			}
			set
			{
				this.SetProperty<string>(Constants.INPUTCOLUMN_PROP_NAME_DICTIONARY_REFERENCE, value);
			}
		}

		public int ExtentValue
		{
			get
			{
				return this.GetProperty<int>(Constants.INPUTCOLUMN_PROP_NAME_EXTENT_VALUE);
			}
			set
			{
				this.SetProperty<int>(Constants.INPUTCOLUMN_PROP_NAME_EXTENT_VALUE, value);
			}
		}

		public bool IsColumnNullable
		{
			get
			{
				return this.GetProperty<bool>(Constants.INPUTCOLUMN_PROP_NAME_IS_COLUMN_NULLABLE);
			}
			set
			{
				this.SetProperty<bool>(Constants.INPUTCOLUMN_PROP_NAME_IS_COLUMN_NULLABLE, value);
			}
		}

		public ObfuscationStrategy ObfuscationStrategy
		{
			get
			{
				return this.GetProperty<ObfuscationStrategy>(Constants.INPUTCOLUMN_PROP_NAME_OBFUSCATION_STRATEGY);
			}
			set
			{
				this.SetProperty<ObfuscationStrategy>(Constants.INPUTCOLUMN_PROP_NAME_OBFUSCATION_STRATEGY, value);
			}
		}

		#endregion

		#region Methods/Operators

		public void CreateProperties()
		{
			if (!this.HasProperty(Constants.INPUTCOLUMN_PROP_NAME_OBFUSCATION_STRATEGY))
			{
				this.LetProperty<ObfuscationStrategy>(Constants.INPUTCOLUMN_PROP_NAME_OBFUSCATION_STRATEGY, ObfuscationStrategy.None,
					Constants.INPUTCOLUMN_PROP_DESC_OBFUSCATION_STRATEGY, false);
			}

			if (!this.HasProperty(Constants.INPUTCOLUMN_PROP_NAME_IS_COLUMN_NULLABLE))
			{
				this.LetProperty<bool>(Constants.INPUTCOLUMN_PROP_NAME_IS_COLUMN_NULLABLE, false,
					Constants.INPUTCOLUMN_PROP_DESC_IS_COLUMN_NULLABLE, false);
			}

			if (!this.HasProperty(Constants.INPUTCOLUMN_PROP_NAME_EXTENT_VALUE))
			{
				this.LetProperty<int>(Constants.INPUTCOLUMN_PROP_NAME_EXTENT_VALUE, 0,
					Constants.INPUTCOLUMN_PROP_NAME_EXTENT_VALUE, false);
			}

			if (!this.HasProperty(Constants.INPUTCOLUMN_PROP_NAME_DICTIONARY_REFERENCE))
			{
				this.LetProperty<string>(Constants.INPUTCOLUMN_PROP_NAME_DICTIONARY_REFERENCE, string.Empty,
					Constants.INPUTCOLUMN_PROP_DESC_DICTIONARY_REFERENCE, false);
			}
		}

		#endregion
	}
}