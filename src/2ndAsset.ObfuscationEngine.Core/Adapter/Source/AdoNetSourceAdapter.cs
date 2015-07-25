/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Source
{
	[AdapterSpecificConfiguration(SpecificConfigurationAqtn = "", UserControlAqtn = "_2ndAsset.ObfuscationEngine.UI.Controls.Adapters.AdoNetAdapterSettingsUserControl, 2ndAsset.ObfuscationEngine.UI")]
	public class AdoNetSourceAdapter : SourceAdapter<AdoNetAdapterConfiguration>, IAdoNetAdapter
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

		protected override void CoreInitialize()
		{
			IEnumerable<IResultset> resultsets;
			IEnumerable<IRecord> records;
			List<MetaColumn> metaColumns;

			this.SourceUnitOfWork = this.AdapterConfiguration.AdapterSpecificConfiguration.GetUnitOfWork();

			if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.PreExecuteCommandText))
			{
				resultsets = this.SourceUnitOfWork.ExecuteSchemaResultsets(this.AdapterConfiguration.AdapterSpecificConfiguration.PreExecuteCommandType ?? CommandType.Text, this.AdapterConfiguration.AdapterSpecificConfiguration.PreExecuteCommandText, new IDbDataParameter[] { });

				if ((object)resultsets == null)
					throw new InvalidOperationException(string.Format("Resultsets were invalid."));

				resultsets.ToArray();
			}

			resultsets = this.SourceUnitOfWork.ExecuteSchemaResultsets(this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandType ?? CommandType.Text, this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandText, new IDbDataParameter[] { });

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
			AdapterConfiguration<AdoNetAdapterConfiguration> adapterConfiguration;
			IEnumerable<IDictionary<string, object>> sourceDataEnumerable;

			if ((object)tableConfiguration == null)
				throw new ArgumentNullException("tableConfiguration");

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandText))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ExecuteCommandText"));

			sourceDataEnumerable = this.SourceUnitOfWork.ExecuteRecords(this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandType ?? CommandType.Text, this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandText, new IDbDataParameter[] { }, null);

			if ((object)sourceDataEnumerable == null)
				throw new InvalidOperationException(string.Format("Records were invalid."));

			return sourceDataEnumerable;
		}

		protected override void CoreTerminate()
		{
			IEnumerable<IResultset> resultsets;

			if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.PostExecuteCommandText))
			{
				resultsets = this.SourceUnitOfWork.ExecuteSchemaResultsets(this.AdapterConfiguration.AdapterSpecificConfiguration.PostExecuteCommandType ?? CommandType.Text, this.AdapterConfiguration.AdapterSpecificConfiguration.PostExecuteCommandText, new IDbDataParameter[] { });

				if ((object)resultsets == null)
					throw new InvalidOperationException(string.Format("Resultsets were invalid."));

				resultsets.ToArray();
			}

			if ((object)this.SourceUnitOfWork != null)
				this.SourceUnitOfWork.Dispose();

			this.SourceUnitOfWork = null;
		}

		#endregion
	}
}