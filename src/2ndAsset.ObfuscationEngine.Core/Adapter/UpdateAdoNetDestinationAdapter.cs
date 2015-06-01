/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;

using TextMetal.Middleware.Data.UoW;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter
{
	public sealed class UpdateAdoNetDestinationAdapter : AdoNetDestinationAdapter
	{
		#region Constructors/Destructors

		public UpdateAdoNetDestinationAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override void OnPublishImpl(TableConfiguration configuration, IUnitOfWork destinationUnitOfWork, IDataReader sourceDataReader, out long rowsCopied)
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
				string sql = string.Format(@"do_something");

				commandParameters = new Dictionary<string, IDbDataParameter>();

				while (sourceDataReader.Read())
				{
					foreach (ColumnConfiguration columnConfiguration in configuration.ColumnConfigurations)
					{
						commandParameter = destinationUnitOfWork.CreateParameter(ParameterDirection.Input, DbType.Object, 0, 0, 0, true, columnConfiguration.ColumnName, sourceDataReader[columnConfiguration.ColumnName]);
						commandParameters.Add(columnConfiguration.ColumnName, commandParameter);
					}
				}
			}

			rowsCopied = _rowsCopied;
		}

		#endregion
	}
}