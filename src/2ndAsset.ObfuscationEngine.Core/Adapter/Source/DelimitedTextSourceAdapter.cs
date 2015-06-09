/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using TextMetal.Middleware.Common.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Source
{
	public sealed class DelimitedTextSourceAdapter : SourceAdapter, IDelimitedTextAdapter
	{
		#region Constructors/Destructors

		public DelimitedTextSourceAdapter()
		{
		}

		#endregion

		#region Fields/Constants

		private RecordTextReader delimitedTextReader;

		#endregion

		#region Properties/Indexers/Events

		private RecordTextReader DelimitedTextReader
		{
			get
			{
				return this.delimitedTextReader;
			}
			set
			{
				this.delimitedTextReader = value;
			}
		}

		#endregion

		#region Methods/Operators

		private static Type GetColumnTypeFromFieldType(FieldType fieldType)
		{
			switch (fieldType)
			{
				case FieldType.String:
					return typeof(String);
				case FieldType.Number:
					return typeof(Double?);
				case FieldType.DateTime:
					return typeof(DateTime?);
				case FieldType.TimeSpan:
					return typeof(TimeSpan?);
				case FieldType.Boolean:
					return typeof(Boolean?);
				default:
					throw new ArgumentOutOfRangeException("fieldType");
			}
		}

		protected override void CoreInitialize(ObfuscationConfiguration obfuscationConfiguration)
		{
			IEnumerable<HeaderSpec> headerSpecs;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration"));

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration.DelimitedTextAdapterConfiguration"));

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration.DelimitedTextAdapterConfiguration:DelimitedTextSpec"));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration.DelimitedTextAdapterConfiguration:DelimitedTextFilePath"));

			this.DelimitedTextReader = new DelimitedTextReader(new StreamReader(File.Open(obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath, FileMode.Open, FileAccess.Read, FileShare.None)), obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec);
			headerSpecs = this.DelimitedTextReader.ReadHeaderSpecs();

			this.UpstreamMetadata = headerSpecs.Select((hs, i) => new MetaColumn()
															{
																TableIndex = 0,
																ColumnIndex = i,
																ColumnName = hs.HeaderName,
																ColumnType = GetColumnTypeFromFieldType(hs.FieldType),
																ColumnIsNullable = true,
																TagContext = hs
															});
		}

		protected override IEnumerable<IDictionary<string, object>> CorePullData(TableConfiguration tableConfiguration)
		{
			IEnumerable<IDictionary<string, object>> sourceDataEnumerable;

			if ((object)tableConfiguration == null)
				throw new ArgumentNullException("tableConfiguration");

			sourceDataEnumerable = new ObfuscationRecordTextReader(this.DelimitedTextReader, tableConfiguration.Parent).ReadRecords();

			return sourceDataEnumerable;
		}

		protected override void CoreTerminate()
		{
			if ((object)this.DelimitedTextReader != null)
				this.DelimitedTextReader.Dispose();

			this.DelimitedTextReader = null;
		}

		#endregion
	}
}