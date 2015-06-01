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

		public ObfuscationDataReader(IDataReader dataReader, TableConfiguration tableConfiguration)
			: base(dataReader)
		{
			if ((object)tableConfiguration == null)
				throw new ArgumentNullException("tableConfiguration");

			this.tableConfiguration = tableConfiguration;
			this.obfuscationMixIn = new ObfuscationMixIn(this.TableConfiguration);
		}

		#endregion

		#region Fields/Constants

		private readonly IObfuscationMixIn obfuscationMixIn;
		private readonly TableConfiguration tableConfiguration;

		#endregion

		#region Properties/Indexers/Events

		private IObfuscationMixIn ObfuscationMixIn
		{
			get
			{
				return this.obfuscationMixIn;
			}
		}

		private TableConfiguration TableConfiguration
		{
			get
			{
				return this.tableConfiguration;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				this.ObfuscationMixIn.Dispose();

			base.Dispose(disposing);
		}

		public override object GetValue(int i)
		{
			int columnIndex;
			string columnName;
			Type columnType;
			object columnValue, obfusscatedValue;
			bool columnIsNullable;

			columnIndex = i;
			columnName = this.GetName(i);
			columnType = this.GetFieldType(i);
			columnValue = base.GetValue(i);
			//columnIsNullable = base.GetSchemaTable().Columns[columnName].AllowDBNull;

			obfusscatedValue = this.ObfuscationMixIn.GetObfuscatedValue(columnIndex, columnName, columnType, columnValue);

			return obfusscatedValue;
		}

		#endregion
	}
}