/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Data;

using TextMetal.Middleware.Data;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Support
{
	public sealed class ObfuscationDataReader : WrappedDataReader
	{
		#region Constructors/Destructors

		public ObfuscationDataReader(IDataReader dataReader, ObfuscationConfiguration obfuscationConfiguration)
			: base(dataReader)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			this.obfuscationConfiguration = obfuscationConfiguration;
			this.oxymoronEngine = new OxymoronEngine(this.ObfuscationConfiguration);
		}

		#endregion

		#region Fields/Constants

		private readonly IOxymoronEngine oxymoronEngine;
		private readonly ObfuscationConfiguration obfuscationConfiguration;

		#endregion

		#region Properties/Indexers/Events

		private IOxymoronEngine OxymoronEngine
		{
			get
			{
				return this.oxymoronEngine;
			}
		}

		private ObfuscationConfiguration ObfuscationConfiguration
		{
			get
			{
				return this.obfuscationConfiguration;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				this.OxymoronEngine.Dispose();

			base.Dispose(disposing);
		}

		public override object GetValue(int i)
		{
			int columnIndex;
			string columnName;
			Type columnType;
			object columnValue, obfusscatedValue;

			IMetaColumn metaColumn;

			columnIndex = i;
			columnName = this.GetName(i);
			columnType = this.GetFieldType(i);
			columnValue = base.GetValue(i);
			//columnIsNullable = base.GetSchemaTable().Columns[columnName].AllowDBNull;

			metaColumn = new MetaColumn()
			{
				ColumnIndex = columnIndex,
				ColumnName = columnName,
				ColumnType = columnType,
				ColumnIsNullable = null,
				TableIndex = 0,
				TagContext = null
			};

			obfusscatedValue = this.OxymoronEngine.GetObfuscatedValue(metaColumn, columnValue);

			return obfusscatedValue;
		}

		#endregion
	}
}