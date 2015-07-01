/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Data.UoW;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	public class UpdateAdoNetDestinationAdapter : AdoNetDestinationAdapter
	{
		#region Constructors/Destructors

		public UpdateAdoNetDestinationAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override void CorePublishImpl(TableConfiguration configuration, IUnitOfWork destinationUnitOfWork, IDataReader sourceDataReader, out long rowsCopied)
		{
			long _rowsCopied = 0;

			if ((object)configuration == null)
				throw new ArgumentNullException("configuration");

			if ((object)destinationUnitOfWork == null)
				throw new ArgumentNullException("destinationUnitOfWork");

			if ((object)sourceDataReader == null)
				throw new ArgumentNullException("sourceDataReader");

			{
				IDbDataParameter commandParameter;
				IDictionary<string, IDbDataParameter> commandParameters;

				commandParameters = new Dictionary<string, IDbDataParameter>();

				while (sourceDataReader.Read())
				{
					commandParameters.Clear();

					foreach (ColumnConfiguration columnConfiguration in configuration.ColumnConfigurations)
					{
						commandParameter = destinationUnitOfWork.CreateParameter(ParameterDirection.Input, DbType.AnsiString, 0, 0, 0, true, string.Format("@{0}", columnConfiguration.ColumnName), sourceDataReader[columnConfiguration.ColumnName]);
						commandParameters.Add(columnConfiguration.ColumnName, commandParameter);
					}

					var resultsets = destinationUnitOfWork.ExecuteResultsets(configuration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType ?? CommandType.Text, configuration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText, commandParameters.Values.ToArray());
					resultsets.ToArray();
					//Console.WriteLine("DESTINATION (update): recordsAffected={0}", resultsets.First().RecordsAffected);

					_rowsCopied++;
				}
			}

			rowsCopied = _rowsCopied;

			Console.WriteLine("DESTINATION (update): rowsCopied={0}", rowsCopied);
		}

		#endregion
	}
}