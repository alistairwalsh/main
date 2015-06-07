/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;

using TextMetal.Middleware.Common.Utilities;
using TextMetal.Middleware.Data.UoW;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public sealed class SubstitutionObfuscationStrategy : ObfuscationStrategy
	{
		#region Constructors/Destructors

		public SubstitutionObfuscationStrategy(DictionaryConfiguration dictionaryConfiguration, IDictionary<string, IDictionary<long, object>> substitutionCacheRoot)
		{
			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)substitutionCacheRoot == null)
				throw new ArgumentNullException("substitutionCacheRoot");

			this.dictionaryConfiguration = dictionaryConfiguration;
			this.substitutionCacheRoot = substitutionCacheRoot;
		}

		#endregion

		#region Fields/Constants

		private readonly DictionaryConfiguration dictionaryConfiguration;
		private readonly IDictionary<string, IDictionary<long, object>> substitutionCacheRoot;

		#endregion

		#region Properties/Indexers/Events

		private DictionaryConfiguration DictionaryConfiguration
		{
			get
			{
				return this.dictionaryConfiguration;
			}
		}

		private IDictionary<string, IDictionary<long, object>> SubstitutionCacheRoot
		{
			get
			{
				return this.substitutionCacheRoot;
			}
		}

		#endregion

		#region Methods/Operators

		private static object GetSubstitution(IDictionary<string, IDictionary<long, object>> substitutionCacheRoot,
			DictionaryConfiguration dictionaryConfiguration,
			long selectId, object value)
		{
			Type valueType;
			string _value;
			const bool SUBSTITUTION_CACHE_ENABLED = true;

			IDictionary<long, object> dictionaryCache;

			if ((object)substitutionCacheRoot == null)
				throw new ArgumentNullException("substitutionCacheRoot");

			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)value == null)
				return null;

			valueType = value.GetType();

			if (valueType != typeof(String))
				return null;

			_value = (String)value;

			if (DataTypeFascade.Instance.IsWhiteSpace(_value))
				return _value;

			_value = _value.Trim();

			if ((dictionaryConfiguration.RecordCount ?? 0L) <= 0L)
				return null;

			if (!SUBSTITUTION_CACHE_ENABLED || !substitutionCacheRoot.TryGetValue(dictionaryConfiguration.DictionaryId, out dictionaryCache))
			{
				dictionaryCache = new Dictionary<long, object>();

				if (SUBSTITUTION_CACHE_ENABLED)
					substitutionCacheRoot.Add(dictionaryConfiguration.DictionaryId, dictionaryCache);
			}

			if (!SUBSTITUTION_CACHE_ENABLED || !dictionaryCache.TryGetValue(selectId, out value))
			{
				if (dictionaryConfiguration.PreloadEnabled)
					throw new InvalidOperationException(string.Format("PreloadEnabled state fail."));

				using (IUnitOfWork unitOfWork = dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.GetUnitOfWork())
				{
					IDbDataParameter dbDataParameterKey;

					dbDataParameterKey = unitOfWork.CreateParameter(ParameterDirection.Input, DbType.Object, 0, 0, 0, false, "@ID", selectId);

					value = unitOfWork.ExecuteScalar<string>(dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType ?? CommandType.Text, dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText, new IDbDataParameter[] { dbDataParameterKey });
				}

				if (SUBSTITUTION_CACHE_ENABLED)
					dictionaryCache.Add(selectId, value);
			}

			return value;
		}

		protected override object CoreGetObfuscatedValue(long signHash, long valueHash, int? extentValue, MetaColumn metaColumn, object columnValue)
		{
			object value;
			long selectId;

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			selectId = valueHash;

			value = GetSubstitution(this.SubstitutionCacheRoot, this.DictionaryConfiguration, selectId, columnValue);

			return value;
		}

		#endregion
	}
}