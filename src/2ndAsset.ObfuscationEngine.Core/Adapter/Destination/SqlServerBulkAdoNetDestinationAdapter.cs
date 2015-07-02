/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Data;
using System.Data.SqlClient;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	public class SqlServerBulkAdoNetDestinationAdapter : AdoNetDestinationAdapter
	{
		#region Constructors/Destructors

		public SqlServerBulkAdoNetDestinationAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override void CorePublishImpl(TableConfiguration configuration, IUnitOfWork destinationUnitOfWork, IDataReader sourceDataReader, out long rowsCopied)
		{
			long _rowsCopied = 0;
			SqlRowsCopiedEventHandler callback;

			if ((object)configuration == null)
				throw new ArgumentNullException("configuration");

			if ((object)destinationUnitOfWork == null)
				throw new ArgumentNullException("destinationUnitOfWork");

			if ((object)sourceDataReader == null)
				throw new ArgumentNullException("sourceDataReader");

			if ((object)configuration.Parent.DestinationAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration"));

			if ((object)configuration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration.AdoNetAdapterConfiguration"));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(configuration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText"));

			using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy((SqlConnection)destinationUnitOfWork.Connection, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls, (SqlTransaction)destinationUnitOfWork.Transaction))
			{
				//callback = (sender, e) => Console.WriteLine(_rowsCopied = e.RowsCopied);

				foreach (ColumnConfiguration columnConfiguration in configuration.ColumnConfigurations)
					sqlBulkCopy.ColumnMappings.Add(columnConfiguration.ColumnName, columnConfiguration.ColumnName);

				sqlBulkCopy.EnableStreaming = true;
				sqlBulkCopy.BatchSize = 2500;
				sqlBulkCopy.NotifyAfter = 2500;
				//sqlBulkCopy.SqlRowsCopied += callback;
				sqlBulkCopy.DestinationTableName = configuration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText;

				sqlBulkCopy.WriteToServer(sourceDataReader);

				//sqlBulkCopy.SqlRowsCopied -= callback;
			}

			rowsCopied = _rowsCopied;
		}

		#endregion
	}
}