/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;
using _2ndAsset.ObfuscationEngine.Core.Support;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Source
{
	[AdapterSpecificConfiguration(SpecificConfigurationAqtn = "", UserControlAqtn = "_2ndAsset.ObfuscationEngine.UI.Controls.Adapters.DelimitedTextAdapterSettingsUserControl, 2ndAsset.ObfuscationEngine.UI")]
	public class DelimitedTextSourceAdapter : SourceAdapter<DelimitedTextAdapterConfiguration>, IDelimitedTextAdapter
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

		protected override void CoreInitialize()
		{
			IEnumerable<HeaderSpec> headerSpecs;

			if ((object)this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DelimitedTextSpec"));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextFilePath))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DelimitedTextFilePath"));

			this.DelimitedTextReader = new DelimitedTextReader(new StreamReader(File.Open(this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextFilePath, FileMode.Open, FileAccess.Read, FileShare.None)), this.AdapterConfiguration.AdapterSpecificConfiguration.DelimitedTextSpec);
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

			sourceDataEnumerable = this.DelimitedTextReader.ReadRecords();

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