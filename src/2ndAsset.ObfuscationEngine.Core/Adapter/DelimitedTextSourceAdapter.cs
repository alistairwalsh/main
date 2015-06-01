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

namespace _2ndAsset.ObfuscationEngine.Core.Adapter
{
	public sealed class DelimitedTextSourceAdapter : Adapter, ISourceAdapter, IDelimitedTextAdapter
	{
		#region Constructors/Destructors

		public DelimitedTextSourceAdapter()
		{
		}

		#endregion

		#region Fields/Constants

		private RecordTextReader delimitedTextReader;
		private IEnumerable<MetaColumn> upstreamMetadata;

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

		public IEnumerable<MetaColumn> UpstreamMetadata
		{
			get
			{
				return this.upstreamMetadata;
			}
			private set
			{
				this.upstreamMetadata = value;
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

		public override void Initialize(ObfuscationConfiguration configuration)
		{
			IEnumerable<HeaderSpec> headerSpecs;

			base.Initialize(configuration);

			if ((object)configuration == null)
				throw new ArgumentNullException("configuration");

			if ((object)configuration.SourceAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration"));

			if ((object)configuration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration.DelimitedTextAdapterConfiguration"));

			if ((object)configuration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration.DelimitedTextAdapterConfiguration:DelimitedTextSpec"));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(configuration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration.DelimitedTextAdapterConfiguration:DelimitedTextFilePath"));

			this.DelimitedTextReader = new DelimitedTextReader(new StreamReader(File.Open(configuration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath, FileMode.Open, FileAccess.Read, FileShare.None)), configuration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec);
			headerSpecs = this.DelimitedTextReader.ReadHeaderSpecs();

			this.UpstreamMetadata = headerSpecs.Select(hs => new MetaColumn()
															{
																ColumnName = hs.HeaderName,
																ColumnType = GetColumnTypeFromFieldType(hs.FieldType),
																IsNullable = true,
																Tag = hs
															});
		}

		public IEnumerable<IDictionary<string, object>> PullData(TableConfiguration configuration)
		{
			IEnumerable<IDictionary<string, object>> sourceDataEnumerable;

			if ((object)configuration == null)
				throw new ArgumentNullException("configuration");

			sourceDataEnumerable = new ObfuscationRecordTextReader(this.DelimitedTextReader, configuration).ReadRecords();

			return sourceDataEnumerable;
		}

		public override void Terminate()
		{
			if ((object)this.DelimitedTextReader != null)
				this.DelimitedTextReader.Dispose();

			this.DelimitedTextReader = null;

			base.Terminate();
		}

		#endregion
	}
}