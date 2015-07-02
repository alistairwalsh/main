/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;

using Solder.Framework.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast
{
	public class AdoNetYieldingFascade : IAdoNetYieldingFascade
	{
		#region Constructors/Destructors

		/// <summary>
		/// Initializes a new instance of the AdoNetFascade class.
		/// </summary>
		/// <param name="reflectionFascade"> The reflection instance to use. </param>
		public AdoNetYieldingFascade(IReflectionFascade reflectionFascade)
		{
			if ((object)reflectionFascade == null)
				throw new ArgumentNullException("reflectionFascade");

			this.reflectionFascade = reflectionFascade;
		}

		/// <summary>
		/// Initializes a new instance of the AdoNetFascade class.
		/// </summary>
		private AdoNetYieldingFascade()
			: this(Solder.Framework.Utilities.ReflectionFascade.Instance)
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly AdoNetYieldingFascade instance = new AdoNetYieldingFascade();
		private readonly IReflectionFascade reflectionFascade;

		#endregion

		#region Properties/Indexers/Events

		public static AdoNetYieldingFascade Instance
		{
			get
			{
				return instance;
			}
		}

		private IReflectionFascade ReflectionFascade
		{
			get
			{
				return this.reflectionFascade;
			}
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Create a new data parameter from the data source.
		/// </summary>
		/// <param name="dbConnection"> The database connection. </param>
		/// <param name="dbTransaction"> An optional local database transaction. </param>
		/// <param name="parameterDirection"> Specifies the parameter direction. </param>
		/// <param name="parameterDbType"> Specifies the parameter provider-(in)dependent type. </param>
		/// <param name="parameterSize"> Specifies the parameter size. </param>
		/// <param name="parameterPrecision"> Specifies the parameter precision. </param>
		/// <param name="parameterScale"> Specifies the parameter scale. </param>
		/// <param name="parameterNullable"> Specifies the parameter nullable-ness. </param>
		/// <param name="parameterName"> Specifies the parameter name. </param>
		/// <param name="parameterValue"> Specifies the parameter value. </param>
		/// <returns> The data parameter with the specified properties set. </returns>
		public IDbDataParameter CreateParameter(IDbConnection dbConnection, IDbTransaction dbTransaction, ParameterDirection parameterDirection, DbType parameterDbType, int parameterSize, byte parameterPrecision, byte parameterScale, bool parameterNullable, string parameterName, object parameterValue)
		{
			IDbDataParameter dbDataParameter;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::CreateParameter(...): enter", typeof(AdoNetYieldingFascade).Name));

			if ((object)dbConnection == null)
				throw new ArgumentNullException("dbConnection");

			using (IDbCommand dbCommand = dbConnection.CreateCommand())
				dbDataParameter = dbCommand.CreateParameter();

			dbDataParameter.ParameterName = parameterName;
			dbDataParameter.Size = parameterSize;
			dbDataParameter.Value = parameterValue;
			dbDataParameter.Direction = parameterDirection;
			dbDataParameter.DbType = parameterDbType;
			this.ReflectionFascade.SetLogicalPropertyValue(dbDataParameter, "IsNullable", parameterNullable, true, false);
			dbDataParameter.Precision = parameterPrecision;
			dbDataParameter.Scale = parameterScale;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::CreateParameter(...): return parameter", typeof(AdoNetYieldingFascade).Name));

			return dbDataParameter;
		}

		/// <summary>
		/// Executes a command, returning a data reader, against a data source.
		/// This method DOES NOT DISPOSE OF CONNECTION/TRANSACTION - UP TO THE CALLER.
		/// This method DOES NOT DISPOSE OF DATA READER - UP TO THE CALLER.
		/// </summary>
		/// <param name="dbConnection"> The database connection. </param>
		/// <param name="dbTransaction"> An optional local database transaction. </param>
		/// <param name="commandType"> The type of the command. </param>
		/// <param name="commandText"> The SQL text or stored procedure name. </param>
		/// <param name="commandParameters"> The parameters to use during the operation. </param>
		/// <param name="commandBehavior"> The reader behavior. </param>
		/// <param name="commandTimeout"> The command timeout (use null for default). </param>
		/// <param name="commandPrepare"> Whether to prepare the command at the data source. </param>
		/// <returns> The data reader result. </returns>
		public IDataReader ExecuteReader(IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType, string commandText, IEnumerable<IDbDataParameter> commandParameters, CommandBehavior commandBehavior, int? commandTimeout, bool commandPrepare)
		{
			IDataReader dataReader;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteReader(...): enter", typeof(AdoNetYieldingFascade).Name));

			if ((object)dbConnection == null)
				throw new ArgumentNullException("dbConnection");

			using (IDbCommand dbCommand = dbConnection.CreateCommand())
			{
				dbCommand.Transaction = dbTransaction;
				dbCommand.CommandType = commandType;
				dbCommand.CommandText = commandText;

				if ((object)commandTimeout != null)
					dbCommand.CommandTimeout = (int)commandTimeout;

				// add parameters
				if ((object)commandParameters != null)
				{
					foreach (IDbDataParameter commandParameter in commandParameters)
					{
						if ((object)commandParameter.Value == null)
							commandParameter.Value = DBNull.Value;

						dbCommand.Parameters.Add(commandParameter);
					}
				}

				if (commandPrepare)
					dbCommand.Prepare();

				// do the database work
				dataReader = dbCommand.ExecuteReader(commandBehavior);

				// wrap reader with proxy
				dataReader = new WrappedDataReader(dataReader);

				// clean out parameters
				//dbCommand.Parameters.Clear();

				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteReader(...): return reader", typeof(AdoNetYieldingFascade).Name));

				return dataReader;
			}
		}

		/// <summary>
		/// Execute a command against a data source, mapping the data reader to an enumerable of record dictionaries.
		/// This method perfoms LAZY LOADING/DEFERRED EXECUTION.
		/// This method DOES NOT DISPOSE OF CONNECTION/TRANSACTION - UP TO THE CALLER.
		/// </summary>
		/// <param name="dbConnection"> The database connection. </param>
		/// <param name="dbTransaction"> An optional local database transaction. </param>
		/// <param name="commandType"> The type of the command. </param>
		/// <param name="commandText"> The SQL text or stored procedure name. </param>
		/// <param name="commandParameters"> The parameters to use during the operation. </param>
		/// ///
		/// <param name="recordsAffectedCallback"> Executed when the output count of records affected is available to return (post enumeration). </param>
		/// <returns> An enumerable of resultset instances, each containing an enumerable of dictionaries with record key/value pairs of schema metadata. </returns>
		public IEnumerable<IRecord> ExecuteRecords(IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType, string commandText, IEnumerable<IDbDataParameter> commandParameters, Action<int> recordsAffectedCallback)
		{
			IEnumerable<IRecord> records;
			IDataReader dataReader;

			// force no preparation
			const bool COMMAND_PREPARE = false;

			// force provider default timeout
			const object COMMAND_TIMEOUT = null; /*int?*/

			// force command behavior to default; the unit of work will manage connection lifetime
			const CommandBehavior COMMAND_BEHAVIOR = CommandBehavior.Default;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteRecords(...): enter", typeof(AdoNetYieldingFascade).Name));

			if ((object)dbConnection == null)
				throw new ArgumentNullException("dbConnection");

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteRecords(...): before yield", typeof(AdoNetYieldingFascade).Name));

			// MUST DISPOSE WITHIN A NEW YIELD STATE MACHINE
			using (dataReader = this.ExecuteReader(dbConnection, dbTransaction, commandType, commandText, commandParameters, COMMAND_BEHAVIOR, (int?)COMMAND_TIMEOUT, COMMAND_PREPARE))
			{
				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteRecords(...): use reader", typeof(AdoNetYieldingFascade).Name));

				records = this.GetRecordsFromReader(dataReader, recordsAffectedCallback);

				foreach (IRecord record in records)
				{
					OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteRecords(...): on yield", typeof(AdoNetYieldingFascade).Name));

					yield return record;
				}

				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteRecords(...): dispose reader", typeof(AdoNetYieldingFascade).Name));
			}

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteRecords(...): after yield", typeof(AdoNetYieldingFascade).Name));

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteRecords(...): leave", typeof(AdoNetYieldingFascade).Name));
		}

		/// <summary>
		/// Execute a command against a data source, mapping the data reader to an enumerable of resultsets, each with an enumerable of record dictionaries.
		/// This method perfoms LAZY LOADING/DEFERRED EXECUTION.
		/// This method DOES NOT DISPOSE OF CONNECTION/TRANSACTION - UP TO THE CALLER.
		/// </summary>
		/// <param name="dbConnection"> The database connection. </param>
		/// <param name="dbTransaction"> An optional local database transaction. </param>
		/// <param name="commandType"> The type of the command. </param>
		/// <param name="commandText"> The SQL text or stored procedure name. </param>
		/// <param name="commandParameters"> The parameters to use during the operation. </param>
		/// <returns> An enumerable of resultset instances, each containing an enumerable of dictionaries with record key/value pairs of data. </returns>
		public IEnumerable<IResultset> ExecuteResultsets(IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType, string commandText, IEnumerable<IDbDataParameter> commandParameters)
		{
			IEnumerable<IResultset> resultsets;
			IDataReader dataReader;

			// force no preparation
			const bool COMMAND_PREPARE = false;

			// force provider default timeout
			const object COMMAND_TIMEOUT = null; /*int?*/

			// force command behavior to default; the unit of work will manage connection lifetime
			const CommandBehavior COMMAND_BEHAVIOR = CommandBehavior.Default;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteResultsets(...): enter", typeof(AdoNetYieldingFascade).Name));

			if ((object)dbConnection == null)
				throw new ArgumentNullException("dbConnection");

			// DO NOT DISPOSE OF DATA READER HERE - THE YIELD STATE MACHINE BELOW WILL DO THIS
			dataReader = this.ExecuteReader(dbConnection, dbTransaction, commandType, commandText, commandParameters, COMMAND_BEHAVIOR, (int?)COMMAND_TIMEOUT, COMMAND_PREPARE);
			resultsets = this.GetResultsetsFromReader(dataReader);

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteResultsets(...): return resultsets", typeof(AdoNetYieldingFascade).Name));

			return resultsets;
		}

		/// <summary>
		/// Execute a command against a data source, mapping the data reader GetSchemaTable() result to an enumerable of enumerable of record dictionaries.
		/// This method perfoms LAZY LOADING/DEFERRED EXECUTION.
		/// This method DOES NOT DISPOSE OF CONNECTION/TRANSACTION - UP TO THE CALLER.
		/// </summary>
		/// <param name="dbConnection"> The database connection. </param>
		/// <param name="dbTransaction"> An optional local database transaction. </param>
		/// <param name="commandType"> The type of the command. </param>
		/// <param name="commandText"> The SQL text or stored procedure name. </param>
		/// <param name="commandParameters"> The parameters to use during the operation. </param>
		/// <param name="recordsAffectedCallback"> Executed when the output count of records affected is available to return (post enumeration). </param>
		/// <returns> An enumerable of resultset instances, each containing an enumerable of dictionaries with record key/value pairs of schema metadata. </returns>
		public IEnumerable<IRecord> ExecuteSchemaRecords(IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType, string commandText, IEnumerable<IDbDataParameter> commandParameters, Action<int> recordsAffectedCallback)
		{
			IEnumerable<IRecord> records;
			IDataReader dataReader;

			// force no preparation
			const bool COMMAND_PREPARE = false;

			// force provider default timeout
			const object COMMAND_TIMEOUT = null; /*int?*/

			// force command behavior to default; the unit of work will manage connection lifetime
			const CommandBehavior COMMAND_BEHAVIOR = CommandBehavior.Default;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteSchemaRecords(...): enter", typeof(AdoNetYieldingFascade).Name));

			if ((object)dbConnection == null)
				throw new ArgumentNullException("dbConnection");

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteSchemaRecords: before yield", typeof(AdoNetYieldingFascade).Name));

			// MUST DISPOSE WITHIN A NEW YIELD STATE MACHINE
			using (dataReader = this.ExecuteReader(dbConnection, dbTransaction, commandType, commandText, commandParameters, COMMAND_BEHAVIOR, (int?)COMMAND_TIMEOUT, COMMAND_PREPARE))
			{
				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteSchemaRecords: use reader", typeof(AdoNetYieldingFascade).Name));

				records = this.GetSchemaRecordsFromReader(dataReader, recordsAffectedCallback);

				foreach (IRecord record in records)
				{
					OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteSchemaRecords: on yield", typeof(AdoNetYieldingFascade).Name));

					yield return record;
				}

				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteSchemaRecords: dispose reader", typeof(AdoNetYieldingFascade).Name));
			}

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteSchemaRecords: after yield", typeof(AdoNetYieldingFascade).Name));

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteSchemaRecords(...): leave", typeof(AdoNetYieldingFascade).Name));
		}

		/// <summary>
		/// Execute a command against a data source, mapping the data reader GetSchemaTable() result to an resultsets, each with an enumerable of record dictionaries.
		/// This method perfoms LAZY LOADING/DEFERRED EXECUTION.
		/// This method DOES NOT DISPOSE OF CONNECTION/TRANSACTION - UP TO THE CALLER.
		/// </summary>
		/// <param name="dbConnection"> The database connection. </param>
		/// <param name="dbTransaction"> An optional local database transaction. </param>
		/// <param name="commandType"> The type of the command. </param>
		/// <param name="commandText"> The SQL text or stored procedure name. </param>
		/// <param name="commandParameters"> The parameters to use during the operation. </param>
		/// <returns> An enumerable of resultset instances, each containing an enumerable of dictionaries with record key/value pairs of schema metadata. </returns>
		public IEnumerable<IResultset> ExecuteSchemaResultsets(IDbConnection dbConnection, IDbTransaction dbTransaction, CommandType commandType, string commandText, IEnumerable<IDbDataParameter> commandParameters)
		{
			IEnumerable<IResultset> resultsets;
			IDataReader dataReader;

			// force no preparation
			const bool COMMAND_PREPARE = false;

			// force provider default timeout
			const object COMMAND_TIMEOUT = null; /*int?*/

			// force command behavior to default; the unit of work will manage connection lifetime
			const CommandBehavior COMMAND_BEHAVIOR = CommandBehavior.SchemaOnly;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteSchemaResultsets(...): enter", typeof(AdoNetYieldingFascade).Name));

			if ((object)dbConnection == null)
				throw new ArgumentNullException("dbConnection");

			// DO NOT DISPOSE OF DATA READER HERE - THE YIELD STATE MACHINE BELOW WILL DO THIS
			dataReader = this.ExecuteReader(dbConnection, dbTransaction, commandType, commandText, commandParameters, COMMAND_BEHAVIOR, (int?)COMMAND_TIMEOUT, COMMAND_PREPARE);
			resultsets = this.GetSchemaResultsetsFromReader(dataReader);

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::ExecuteSchemaResultsets(...): return resultsets", typeof(AdoNetYieldingFascade).Name));

			return resultsets;
		}

		/// <summary>
		/// Execute a command against a data source, mapping the data reader to an enumerable of record dictionaries.
		/// This method perfoms LAZY LOADING/DEFERRED EXECUTION.
		/// Note that THE DATA READER WILL NOT BE DISPOSED UPON ENUMERATION OR FOREACH BRANCH OUT.
		/// </summary>
		/// <param name="dataReader"> The target data reader. </param>
		/// <param name="recordsAffectedCallback"> Executed when the output count of records affected is available to return (post enumeration). </param>
		/// <returns> An enumerable of record dictionary instances, containing key/value pairs of data. </returns>
		public IEnumerable<IRecord> GetRecordsFromReader(IDataReader dataReader, Action<int> recordsAffectedCallback)
		{
			IRecord record;
			int recordsAffected;
			int recordIndex = 0;
			string key;
			object value;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetRecordsFromReader(...): enter", typeof(AdoNetYieldingFascade).Name));

			if ((object)dataReader == null)
				throw new ArgumentNullException("dataReader");

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetRecordsFromReader(...): before yield", typeof(AdoNetYieldingFascade).Name));

			while (dataReader.Read())
			{
				record = new Record();

				for (int columnIndex = 0; columnIndex < dataReader.FieldCount; columnIndex++)
				{
					key = dataReader.GetName(columnIndex);
					value = dataReader.GetValue(columnIndex).ChangeType<object>();

					if (record.ContainsKey(key) || (key ?? string.Empty).Length == 0)
						key = string.Format("Column_{0:0000}", columnIndex);

					record.Add(key, value);
				}

				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetRecordsFromReader(...): on yield", typeof(AdoNetYieldingFascade).Name));

				yield return record; // LAZY PROCESSING INTENT HERE / DO NOT FORCE EAGER LOAD
			}

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetRecordsFromReader(...): after yield", typeof(AdoNetYieldingFascade).Name));

			recordsAffected = dataReader.RecordsAffected;

			if ((object)recordsAffectedCallback != null)
				recordsAffectedCallback(recordsAffected);

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetRecordsFromReader(...): leave", typeof(AdoNetYieldingFascade).Name));
		}

		/// <summary>
		/// Execute a command against a data source, mapping the data reader to an enumerable of resultsets, each with an enumerable of records dictionaries.
		/// This method perfoms LAZY LOADING/DEFERRED EXECUTION.
		/// </summary>
		/// <param name="dataReader"> The target data reader. </param>
		/// <returns> An enumerable of resultset instances, each containing an enumerable of dictionaries with record key/value pairs of data. </returns>
		public IEnumerable<IResultset> GetResultsetsFromReader(IDataReader dataReader)
		{
			int resultsetIndex = 0;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetResultsetsFromReader(...): enter", typeof(AdoNetYieldingFascade).Name));

			if ((object)dataReader == null)
				throw new ArgumentNullException("dataReader");

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetResultsetsFromReader(...): before yield", typeof(AdoNetYieldingFascade).Name));

			using (dataReader)
			{
				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetResultsetsFromReader(...): use reader", typeof(AdoNetYieldingFascade).Name));

				do
				{
					Resultset resultset = new Resultset(resultsetIndex++); // prevent modified closure
					resultset.Records = this.GetRecordsFromReader(dataReader, (ra) => resultset.RecordsAffected = ra);

					OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetResultsetsFromReader(...): on yield", typeof(AdoNetYieldingFascade).Name));

					yield return resultset; // LAZY PROCESSING INTENT HERE / DO NOT FORCE EAGER LOAD
				}
				while (dataReader.NextResult());

				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetResultsetsFromReader(...): dispose reader", typeof(AdoNetYieldingFascade).Name));
			}

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetResultsetsFromReader(...): after yield", typeof(AdoNetYieldingFascade).Name));

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetResultsetsFromReader(...): leave", typeof(AdoNetYieldingFascade).Name));
		}

		/// <summary>
		/// Execute a command against a data source, mapping the data reader GetSchemaTable() result to an enumerable of record dictionaries.
		/// This method perfoms LAZY LOADING/DEFERRED EXECUTION.
		/// Note that THE DATA READER WILL NOT BE DISPOSED UPON ENUMERATION OR FOREACH BRANCH OUT.
		/// </summary>
		/// <param name="dataReader"> The target data reader. </param>
		/// <param name="recordsAffectedCallback"> Executed when the output count of records affected is available to return (post enumeration). </param>
		/// <returns> An enumerable of record dictionary instances, containing key/value pairs of schema metadata. </returns>
		public IEnumerable<IRecord> GetSchemaRecordsFromReader(IDataReader dataReader, Action<int> recordsAffectedCallback)
		{
			IRecord record;
			int recordsAffected;
			string key;
			object value;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaRecordsFromReader(...): enter", typeof(AdoNetYieldingFascade).Name));

			if ((object)dataReader == null)
				throw new ArgumentNullException("dataReader");

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaRecordsFromReader(...): before yield", typeof(AdoNetYieldingFascade).Name));

			using (DataTable dataTable = dataReader.GetSchemaTable())
			{
				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaRecordsFromReader(...): use table", typeof(AdoNetYieldingFascade).Name));

				if ((object)dataTable != null)
				{
					foreach (DataRow dataRow in dataTable.Rows)
					{
						record = new Record();

						for (int index = 0; index < dataTable.Columns.Count; index++)
						{
							key = dataTable.Columns[index].ColumnName;
							value = dataRow[index].ChangeType<object>();

							record.Add(key, value);
						}

						OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaRecordsFromReader(...): on yield", typeof(AdoNetYieldingFascade).Name));

						yield return record; // LAZY PROCESSING INTENT HERE / DO NOT FORCE EAGER LOAD
					}
				}

				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaRecordsFromReader(...): dispose table", typeof(AdoNetYieldingFascade).Name));
			}

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaRecordsFromReader(...): after yield", typeof(AdoNetYieldingFascade).Name));

			recordsAffected = dataReader.RecordsAffected;

			if ((object)recordsAffectedCallback != null)
				recordsAffectedCallback(recordsAffected);

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaRecordsFromReader(...): leave", typeof(AdoNetYieldingFascade).Name));
		}

		/// <summary>
		/// Execute a command against a data source, mapping the data reader GetSchemaTable() result to an enumerable of resultsets, each with an enumerable of records dictionaries.
		/// This method perfoms LAZY LOADING/DEFERRED EXECUTION.
		/// </summary>
		/// <param name="dataReader"> The target data reader. </param>
		/// <returns> An enumerable of resultset instances, each containing an enumerable of dictionaries with record key/value pairs of schema metadata. </returns>
		public IEnumerable<IResultset> GetSchemaResultsetsFromReader(IDataReader dataReader)
		{
			int resultsetIndex = 0;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaResultsetsFromReader(...): enter", typeof(AdoNetYieldingFascade).Name));

			if ((object)dataReader == null)
				throw new ArgumentNullException("dataReader");

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaResultsetsFromReader(...): before yield", typeof(AdoNetYieldingFascade).Name));

			using (dataReader)
			{
				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaResultsetsFromReader(...): use reader", typeof(AdoNetYieldingFascade).Name));

				do
				{
					Resultset resultset = new Resultset(resultsetIndex++); // prevent modified closure
					resultset.Records = this.GetSchemaRecordsFromReader(dataReader, (ra) => resultset.RecordsAffected = ra);

					OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaResultsetsFromReader(...): on yield", typeof(AdoNetYieldingFascade).Name));

					yield return resultset; // LAZY PROCESSING INTENT HERE / DO NOT FORCE EAGER LOAD
				}
				while (dataReader.NextResult());

				OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaResultsetsFromReader(...): dispose reader", typeof(AdoNetYieldingFascade).Name));
			}

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaResultsetsFromReader(...): after yield", typeof(AdoNetYieldingFascade).Name));

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetSchemaResultsetsFromReader(...): leave", typeof(AdoNetYieldingFascade).Name));
		}

		#endregion
	}
}