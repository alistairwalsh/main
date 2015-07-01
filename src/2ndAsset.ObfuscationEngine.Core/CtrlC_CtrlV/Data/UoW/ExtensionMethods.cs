/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Data.UoW
{
	/// <summary>
	/// Provides extension methods for unit of work instances.
	/// </summary>
	public static class ExtensionMethods
	{
		#region Methods/Operators

		/// <summary>
		/// An extension method to create a new data parameter from the data source.
		/// </summary>
		/// <param name="unitOfWork"> The target unit of work. </param>
		/// <param name="parameterDirection"> Specifies the parameter direction. </param>
		/// <param name="dbType"> Specifies the parameter provider-(in)dependent type. </param>
		/// <param name="parameterSize"> Specifies the parameter size. </param>
		/// <param name="parameterPrecision"> Specifies the parameter precision. </param>
		/// <param name="parameterScale"> Specifies the parameter scale. </param>
		/// <param name="parameterNullable"> Specifies the parameter nullable-ness. </param>
		/// <param name="parameterName"> Specifies the parameter name. </param>
		/// <param name="parameterValue"> Specifies the parameter value. </param>
		/// <returns> The data parameter with the specified properties set. </returns>
		public static IDbDataParameter CreateParameter(this IUnitOfWork unitOfWork, ParameterDirection parameterDirection, DbType dbType, int parameterSize, byte parameterPrecision, byte parameterScale, bool parameterNullable, string parameterName, object parameterValue)
		{
			IDbDataParameter dbDataParameter;

			if ((object)unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");

			dbDataParameter = AdoNetFascade.Instance.CreateParameter(unitOfWork.Connection, unitOfWork.Transaction, parameterDirection, dbType, parameterSize, parameterPrecision, parameterScale, parameterNullable, parameterName, parameterValue);

			return dbDataParameter;
		}

		public static IEnumerable<IRecord> ExecuteRecords(this IUnitOfWork unitOfWork, CommandType commandType, string commandText, IEnumerable<IDbDataParameter> commandParameters, Action<int> recordsAffectedCallback)
		{
			IEnumerable<IRecord> records;

			if ((object)unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");

			// DO NOT DISPOSE OF DATA READER HERE - THE YIELD STATE MACHINE BELOW WILL DO THIS
			records = AdoNetFascade.Instance.ExecuteRecords(unitOfWork.Connection, unitOfWork.Transaction, commandType, commandText, commandParameters, recordsAffectedCallback);

			return records;
		}

		/// <summary>
		/// An extension method to execute a resultset/records query operation against a target unit of work.
		/// DO NOT DISPOSE OF UNIT OF WORK CONTEXT - UP TO THE CALLER.
		/// </summary>
		/// <param name="unitOfWork"> The target unit of work. </param>
		/// <param name="commandType"> The type of the command. </param>
		/// <param name="commandText"> The SQL text or stored procedure name. </param>
		/// <param name="commandParameters"> The parameters to use during the operation. </param>
		/// <returns> An enumerable of resultset instances, each containing an enumerable of dictionaries with record key/value pairs of data. </returns>
		public static IEnumerable<IResultset> ExecuteResultsets(this IUnitOfWork unitOfWork, CommandType commandType, string commandText, IEnumerable<IDbDataParameter> commandParameters)
		{
			IEnumerable<IResultset> resultsets;

			if ((object)unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");

			// DO NOT DISPOSE OF DATA READER HERE - THE YIELD STATE MACHINE BELOW WILL DO THIS
			resultsets = AdoNetFascade.Instance.ExecuteResultsets(unitOfWork.Connection, unitOfWork.Transaction, commandType, commandText, commandParameters);

			return resultsets;
		}

		public static TValue ExecuteScalar<TValue>(this IUnitOfWork unitOfWork, CommandType commandType, string commandText, IEnumerable<IDbDataParameter> commandParameters)
		{
			IEnumerable<IRecord> records;
			IRecord record;

			object dbValue;

			if ((object)unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");

			records = unitOfWork.ExecuteRecords(commandType, commandText, commandParameters, null);

			if ((object)records == null)
				return default(TValue);

			record = records.SingleOrDefault();

			if ((object)record == null)
				return default(TValue);

			if (record.Count != 1)
				return default(TValue);

			if (record.Keys.Count != 1)
				return default(TValue);

			dbValue = record[record.Keys.First()];

			return dbValue.ChangeType<TValue>();
		}

		public static IEnumerable<IRecord> ExecuteSchemaRecords(this IUnitOfWork unitOfWork, CommandType commandType, string commandText, IEnumerable<IDbDataParameter> commandParameters, Action<int> recordsAffectedCallback)
		{
			IEnumerable<IRecord> records;

			if ((object)unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");

			// DO NOT DISPOSE OF DATA READER HERE - THE YIELD STATE MACHINE BELOW WILL DO THIS
			records = AdoNetFascade.Instance.ExecuteSchemaRecords(unitOfWork.Connection, unitOfWork.Transaction, commandType, commandText, commandParameters, recordsAffectedCallback);

			return records;
		}

		/// <summary>
		/// An extension method to execute a resultset/records query operation against a target unit of work.
		/// DO NOT DISPOSE OF UNIT OF WORK CONTEXT - UP TO THE CALLER.
		/// </summary>
		/// <param name="unitOfWork"> The target unit of work. </param>
		/// <param name="commandType"> The type of the command. </param>
		/// <param name="commandText"> The SQL text or stored procedure name. </param>
		/// <param name="commandParameters"> The parameters to use during the operation. </param>
		/// <returns> An enumerable of resultset instances, each containing an enumerable of dictionaries with record key/value pairs of schema metadata. </returns>
		public static IEnumerable<IResultset> ExecuteSchemaResultsets(this IUnitOfWork unitOfWork, CommandType commandType, string commandText, IEnumerable<IDbDataParameter> commandParameters)
		{
			IEnumerable<IResultset> resultsets;

			if ((object)unitOfWork == null)
				throw new ArgumentNullException("unitOfWork");

			resultsets = AdoNetFascade.Instance.ExecuteSchemaResultsets(unitOfWork.Connection, unitOfWork.Transaction, commandType, commandText, commandParameters);

			return resultsets;
		}

		/// <summary>
		/// An extension method to extract outputs from a record dictionary.
		/// </summary>
		/// <param name="dbDataParameters"> The target enumerable of data paramters. </param>
		/// <returns> A dictionary with record key/value pairs of OUTPUT data. </returns>
		public static IRecord GetOutputAsRecord(this IEnumerable<IDbDataParameter> dbDataParameters)
		{
			IRecord output;

			if ((object)dbDataParameters == null)
				throw new ArgumentNullException("dbDataParameters");

			output = new Record();

			foreach (IDbDataParameter dbDataParameter in dbDataParameters)
			{
				if (dbDataParameter.Direction != ParameterDirection.InputOutput &&
					dbDataParameter.Direction != ParameterDirection.Output &&
					dbDataParameter.Direction != ParameterDirection.ReturnValue)
					continue;

				output.Add(dbDataParameter.ParameterName, dbDataParameter.Value);
			}

			return output;
		}

		#endregion
	}
}