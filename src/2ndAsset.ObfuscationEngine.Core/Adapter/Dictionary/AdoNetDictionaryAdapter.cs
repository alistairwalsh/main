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

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary
{
	public class AdoNetDictionaryAdapter : DictionaryAdapter<AdoNetAdapterConfiguration>, IAdoNetAdapter
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

		protected override object CoreGetAlternativeValueFromId(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId)
		{
			object value;
			IDbDataParameter dbDataParameterKey;

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)surrogateId == null)
				throw new ArgumentNullException("surrogateId");

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandText))
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ExecuteCommandText"));

			dbDataParameterKey = this.DictionaryUnitOfWork.CreateParameter(ParameterDirection.Input, DbType.Object, 0, 0, 0, false, "@ID", surrogateId);

			value = this.DictionaryUnitOfWork.ExecuteScalar<string>(this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandType ?? CommandType.Text, this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandText, new IDbDataParameter[] { dbDataParameterKey });

			return value;
		}

		protected override void CoreInitialize()
		{
			this.DictionaryUnitOfWork = this.AdapterConfiguration.AdapterSpecificConfiguration.GetUnitOfWork();
		}

		protected override void CorePreloadCache(DictionaryConfiguration dictionaryConfiguration, IDictionary<string, IDictionary<long, object>> substitutionCacheRoot)
		{
			IEnumerable<IRecord> records;
			IDictionary<long, object> dictionaryCache;

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)substitutionCacheRoot == null)
				throw new ArgumentNullException("substitutionCacheRoot");

			if (dictionaryConfiguration.PreloadEnabled)
			{
				if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandText))
					throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ExecuteCommandText"));

				records = this.DictionaryUnitOfWork.ExecuteRecords(this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandType ?? CommandType.Text, this.AdapterConfiguration.AdapterSpecificConfiguration.ExecuteCommandText, new IDbDataParameter[] { }, null);

				if ((object)records == null)
					throw new InvalidOperationException(string.Format("Records were invalid."));

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