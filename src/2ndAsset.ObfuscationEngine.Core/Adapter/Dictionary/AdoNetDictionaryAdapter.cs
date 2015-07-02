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
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	public class AdoNetDictionaryAdapter : DictionaryAdapter, IAdoNetAdapter
	{
		#region Constructors/Destructors

		public AdoNetDictionaryAdapter()
		{
		}

		#endregion

		#region Fields/Constants

		private IUnitOfWork dictionaryUnitOfWork;

		#endregion

		#region Properties/Indexers/Events

		private IUnitOfWork DictionaryUnitOfWork
		{
			get
			{
				return this.dictionaryUnitOfWork;
			}
			set
			{
				this.dictionaryUnitOfWork = value;
			}
		}

		#endregion

		#region Methods/Operators

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

		protected override object CoreGetAlternativeValueFromId(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId)
		{
			object value;

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)surrogateId == null)
				throw new ArgumentNullException("surrogateId");

			if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DictionaryAdapterConfiguration"));

			if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DictionaryAdapterConfiguration.AdoNetAdapterConfiguration"));

			IDbDataParameter dbDataParameterKey;

			dbDataParameterKey = this.DictionaryUnitOfWork.CreateParameter(ParameterDirection.Input, DbType.Object, 0, 0, 0, false, "@ID", surrogateId);

			value = this.DictionaryUnitOfWork.ExecuteScalar<string>(dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType ?? CommandType.Text, dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText, new IDbDataParameter[] { dbDataParameterKey });

			return value;
		}

		protected override void CoreInitialize(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");
		}

		protected override void CoreInitializePreloadCache(DictionaryConfiguration dictionaryConfiguration, IDictionary<string, IDictionary<long, object>> substitutionCacheRoot)
		{
			IEnumerable<IRecord> records;
			IDictionary<long, object> dictionaryCache;

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)substitutionCacheRoot == null)
				throw new ArgumentNullException("substitutionCacheRoot");

			this.DictionaryUnitOfWork = dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.GetUnitOfWork();

			if (dictionaryConfiguration.PreloadEnabled)
			{
				records = this.DictionaryUnitOfWork.ExecuteRecords(dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType ?? CommandType.Text, dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText, new IDbDataParameter[] { }, null);

				if ((object)records == null)
					throw new InvalidOperationException(string.Format("Records were invalid."));

				//records = WrapRecordCounter(dictionaryConfiguration, records, (d, x, y, z) => Console.WriteLine("dictionary[{0}]: {1} {2} {3}", d, x, y, z));
				dictionaryCache = new Dictionary<long, object>();

				foreach (IRecord record in records)
				{
					object[] values = record.Values.ToArray();
					long id = values[0].ChangeType<long>();
					object value = values[1].ChangeType<string>();

					dictionaryCache.Add(id, value);
				}

				substitutionCacheRoot.Add(dictionaryConfiguration.DictionaryId, dictionaryCache);
			}
		}

		protected override void CoreTerminate()
		{
			if ((object)this.DictionaryUnitOfWork != null)
				this.DictionaryUnitOfWork.Dispose();

			this.DictionaryUnitOfWork = null;
		}

		#endregion
	}
}