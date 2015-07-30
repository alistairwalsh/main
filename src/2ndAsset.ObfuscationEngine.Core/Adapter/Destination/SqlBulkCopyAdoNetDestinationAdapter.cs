/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;
using System.Data.SqlClient;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	public class SqlBulkCopyAdoNetDestinationAdapter : AdoNetDestinationAdapter
	{
		#region Constructors/Destructors

		public SqlBulkCopyAdoNetDestinationAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override void CorePublishImpl(TableConfiguration configuration, IUnitOfWork destinationUnitOfWork, IDataReader sourceDataReader, out long rowsCopied)
		{
			long _rowsCopied = 0;
			//SqlRowsCopiedEventHandler callback;

			if ((object)configuration == null)
				throw new ArgumentNullException("configuration");

			if ((object)destinationUnitOfWork == null)
				throw new ArgumentNullException("destinationUnitOfWork");

			if ((object)sourceDataReader == null)
				throw new ArgumentNullException("sourceDataReader");

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandText))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ExecuteCommandText"));

			using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy((SqlConnection)destinationUnitOfWork.Connection, SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls, (SqlTransaction)destinationUnitOfWork.Transaction))
			{
				//callback = (sender, e) => Console.WriteLine(_rowsCopied = e.RowsCopied);

				foreach (ColumnConfiguration columnConfiguration in configuration.ColumnConfigurations)
					sqlBulkCopy.ColumnMappings.Add(columnConfiguration.ColumnName, columnConfiguration.ColumnName);

				sqlBulkCopy.EnableStreaming = true;
				sqlBulkCopy.BatchSize = 2500;
				sqlBulkCopy.NotifyAfter = 2500;
				//sqlBulkCopy.SqlRowsCopied += callback;
				sqlBulkCopy.DestinationTableName = this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandText;

				sqlBulkCopy.WriteToServer(sourceDataReader);

				//sqlBulkCopy.SqlRowsCopied -= callback;
			}

			rowsCopied = _rowsCopied;
		}

		#endregion
	}
}