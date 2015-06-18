/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using TextMetal.Middleware.Common.Utilities;
using TextMetal.Middleware.Data;
using TextMetal.Middleware.Data.UoW;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	public sealed class AdoNetDictionaryAdapter : DictionaryAdapter, IAdoNetAdapter
	{
		#region Constructors/Destructors

		public AdoNetDictionaryAdapter()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetAlternativeValueFromId(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId)
		{
			object value;

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)surrogateId == null)
				throw new ArgumentNullException("surrogateId");

			using (IUnitOfWork unitOfWork = dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.GetUnitOfWork())
			{
				IDbDataParameter dbDataParameterKey;

				dbDataParameterKey = unitOfWork.CreateParameter(ParameterDirection.Input, DbType.Object, 0, 0, 0, false, "@ID", surrogateId);

				value = unitOfWork.ExecuteScalar<string>(dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType ?? CommandType.Text, dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText, new IDbDataParameter[] { dbDataParameterKey });
			}

			return value;
		}

		protected override void CoreInitialize(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");
		}

		private static IEnumerable<IRecord> WrapRecordCounter(DictionaryConfiguration dictionaryConfiguration, IEnumerable<IRecord> records, Action<string, long, bool, double> recordProcessCallback)
		{
			long recordCount = 0;
			DateTime startUtc;

			startUtc = DateTime.UtcNow;

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)records == null)
				throw new ArgumentNullException("records");

			foreach (IRecord record in records)
			{
				recordCount++;

				if ((recordCount % 1000) == 0)
				{
					//Thread.Sleep(250);
					if ((object)recordProcessCallback != null)
						recordProcessCallback(dictionaryConfiguration.DictionaryId, recordCount, false, (DateTime.UtcNow - startUtc).TotalSeconds);
				}

				yield return record;
			}

			if ((object)recordProcessCallback != null)
				recordProcessCallback(dictionaryConfiguration.DictionaryId, recordCount, true, (DateTime.UtcNow - startUtc).TotalSeconds);
		}

		protected override void CoreInitializePreloadCache(DictionaryConfiguration dictionaryConfiguration, IDictionary<string, IDictionary<long, object>> substitutionCacheRoot)
		{
			IEnumerable<IRecord> records;
			IDictionary<long, object> dictionaryCache;

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)substitutionCacheRoot == null)
				throw new ArgumentNullException("substitutionCacheRoot");

			using (IUnitOfWork unitOfWork = dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.GetUnitOfWork())
			{
				records = unitOfWork.ExecuteRecords(dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType ?? CommandType.Text, dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText, new IDbDataParameter[] { }, null);

				if ((object)records == null)
					throw new InvalidOperationException(string.Format("Records were invalid."));

				//records = WrapRecordCounter(dictionaryConfiguration, records, (d, x, y, z) => Console.WriteLine("dictionary[{0}]: {1} {2} {3}", d, x, y, z));
				dictionaryCache = new Dictionary<long, object>();

				foreach (IRecord record in records)
				{
					long id = record[record.Keys.ToArray()[0]].ChangeType<long>();
					object value = record[record.Keys.ToArray()[1]].ChangeType<string>();
					dictionaryCache.Add(id, value);
				}

				substitutionCacheRoot.Add(dictionaryConfiguration.DictionaryId, dictionaryCache);
			}
		}

		protected override void CoreTerminate()
		{
		}

		#endregion
	}
}