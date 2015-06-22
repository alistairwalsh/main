/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;

using TextMetal.Middleware.Common.Utilities;
using TextMetal.Middleware.Data;
using TextMetal.Middleware.Data.UoW;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Source
{
	public class AdoNetSourceAdapter : SourceAdapter, IAdoNetAdapter
	{
		#region Constructors/Destructors

		public AdoNetSourceAdapter()
		{
		}

		#endregion

		#region Fields/Constants

		private IUnitOfWork sourceUnitOfWork;

		#endregion

		#region Properties/Indexers/Events

		private IUnitOfWork SourceUnitOfWork
		{
			get
			{
				return this.sourceUnitOfWork;
			}
			set
			{
				this.sourceUnitOfWork = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreInitialize(ObfuscationConfiguration obfuscationConfiguration)
		{
			IEnumerable<IResultset> resultsets;
			IEnumerable<IRecord> records;
			List<MetaColumn> metaColumns;

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration"));

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration.AdoNetAdapterConfiguration"));

			this.SourceUnitOfWork = obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.GetUnitOfWork();

			resultsets = this.SourceUnitOfWork.ExecuteSchemaResultsets(obfuscationConfiguration.Parent.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType ?? CommandType.Text, obfuscationConfiguration.Parent.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText, new IDbDataParameter[] { });

			if ((object)resultsets == null)
				throw new InvalidOperationException(string.Format("Resultsets were invalid."));

			metaColumns = new List<MetaColumn>();

			foreach (IResultset resultset in resultsets)
			{
				records = resultset.Records;

				if ((object)records == null)
					throw new InvalidOperationException(string.Format("Records were invalid."));

				int i = 0;
				foreach (IRecord record in records)
				{
					metaColumns.Add(new MetaColumn()
									{
										TableIndex = resultset.Index,
										ColumnIndex = i++,
										ColumnName = DataTypeFascade.Instance.ChangeType<string>(record["ColumnName"]),
										ColumnType = DataTypeFascade.Instance.ChangeType<Type>(record["DataType"]),
										ColumnIsNullable = DataTypeFascade.Instance.ChangeType<bool>(record["AllowDBNull"]),
										TagContext = record
									});
				}
			}

			this.UpstreamMetadata = metaColumns;
		}

		protected override IEnumerable<IDictionary<string, object>> CorePullData(TableConfiguration tableConfiguration)
		{
			IEnumerable<IDictionary<string, object>> sourceDataEnumerable;
			IDataReader dataReader;

			if ((object)tableConfiguration == null)
				throw new ArgumentNullException("tableConfiguration");

			if ((object)tableConfiguration.Parent.SourceAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration"));

			if ((object)tableConfiguration.Parent.SourceAdapterConfiguration.AdoNetAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration.AdoNetAdapterConfiguration"));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(tableConfiguration.Parent.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "SourceAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText"));

			sourceDataEnumerable = this.SourceUnitOfWork.ExecuteRecords(tableConfiguration.Parent.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType ?? CommandType.Text, tableConfiguration.Parent.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText, new IDbDataParameter[] { }, null);

			if ((object)sourceDataEnumerable == null)
				throw new InvalidOperationException(string.Format("Records were invalid."));

			return sourceDataEnumerable;
		}

		protected override void CoreTerminate()
		{
			if ((object)this.SourceUnitOfWork != null)
				this.SourceUnitOfWork.Dispose();

			this.SourceUnitOfWork = null;
		}

		#endregion
	}
}