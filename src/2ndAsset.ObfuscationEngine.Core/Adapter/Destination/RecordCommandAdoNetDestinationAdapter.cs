/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	public class RecordCommandAdoNetDestinationAdapter : AdoNetDestinationAdapter
	{
		#region Constructors/Destructors

		public RecordCommandAdoNetDestinationAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override void CorePublishImpl(TableConfiguration configuration, IUnitOfWork destinationUnitOfWork, IDataReader sourceDataReader, out long rowsCopied)
		{
			IEnumerable<IResultset> resultsets;
			long _rowsCopied = 0;

			if ((object)configuration == null)
				throw new ArgumentNullException("configuration");

			if ((object)destinationUnitOfWork == null)
				throw new ArgumentNullException("destinationUnitOfWork");

			if ((object)sourceDataReader == null)
				throw new ArgumentNullException("sourceDataReader");

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandText))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ExecuteCommandText"));

			// ?
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

					resultsets = destinationUnitOfWork.ExecuteResultsets(this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandType ?? CommandType.Text, this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandText, commandParameters.Values.ToArray());
					resultsets.ToArray();

					_rowsCopied++;
				}
			}

			rowsCopied = _rowsCopied;

			Console.WriteLine("DESTINATION (update): rowsCopied={0}", rowsCopied);
		}

		#endregion
	}
}