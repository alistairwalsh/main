﻿/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.Core.Support
{
	public sealed class ObfuscationRecordTextReader : RecordTextReader
	{
		#region Constructors/Destructors

		public ObfuscationRecordTextReader(RecordTextReader innerTextReader, ObfuscationConfiguration obfuscationConfiguration)
			: base(innerTextReader)
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

		protected new RecordTextReader InnerTextReader
		{
			get
			{
				return (RecordTextReader)base.InnerTextReader;
			}
		}

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

		public override IEnumerable<HeaderSpec> ReadHeaderSpecs()
		{
			return this.InnerTextReader.ReadHeaderSpecs();
		}

		public override IEnumerable<IDictionary<string, object>> ReadRecords()
		{
			int columnIndex;
			string columnName;
			Type columnType;
			object columnValue, obfusscatedValue;
			bool columnIsNullable;

			IEnumerable<IDictionary<string, object>> records;
			IDictionary<string, object> obfuscatedRecord;
			IMetaColumn metaColumn;

			using (this)
			{
				records = this.InnerTextReader.ReadRecords();

				foreach (IDictionary<string, object> record in records)
				{
					obfuscatedRecord = new Dictionary<string, object>();

					columnIndex = 0;
					foreach (KeyValuePair<string, object> field in record)
					{
						columnName = field.Key;
						columnValue = record[field.Key];
						columnType = (columnValue ?? new object()).GetType();

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
						obfuscatedRecord.Add(columnName, obfusscatedValue);
						columnIndex++;
					}

					yield return obfuscatedRecord;
				}
			}
		}

		#endregion
	}
}