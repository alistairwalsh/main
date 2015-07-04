/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using Solder.Framework;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Strategies;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public abstract class ObfuscationStrategy<TObfuscationStrategyConfiguration> : IObfuscationStrategy
		where TObfuscationStrategyConfiguration : ObfuscationStrategyConfiguration, new()
	{
		#region Constructors/Destructors

		protected ObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected abstract object CoreGetObfuscatedValue(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, TObfuscationStrategyConfiguration> contextualConfiguration, HashResult hashResult, IMetaColumn metaColumn, object columnValue);

		protected virtual long CoreGetValueHashBucketSize(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, TObfuscationStrategyConfiguration> contextualConfiguration)
		{
			const long DEFAULT_HASH_BUCKET_SIZE = long.MaxValue;

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			if ((object)contextualConfiguration == null)
				throw new ArgumentNullException("contextualConfiguration");

			return DEFAULT_HASH_BUCKET_SIZE;
		}

		public object GetObfuscatedValue(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, IDictionary<string, object>> contextualConfiguration, HashResult hashResult, IMetaColumn metaColumn, object columnValue)
		{
			Tuple<ColumnConfiguration, TObfuscationStrategyConfiguration> _contextualConfiguration;
			TObfuscationStrategyConfiguration obfuscationStrategyConfiguration;
			object value;

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			if ((object)contextualConfiguration == null)
				throw new ArgumentNullException("contextualConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)columnValue == DBNull.Value)
				columnValue = null;

			obfuscationStrategyConfiguration = JObject.FromObject(contextualConfiguration.Item2).ToObject<TObfuscationStrategyConfiguration>();
			_contextualConfiguration = new Tuple<ColumnConfiguration, TObfuscationStrategyConfiguration>(contextualConfiguration.Item1, obfuscationStrategyConfiguration);
			value = this.CoreGetObfuscatedValue(oxymoronEngine, _contextualConfiguration, hashResult, metaColumn, columnValue);

			return value;
		}

		public long GetValueHashBucketSize(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, IDictionary<string, object>> contextualConfiguration)
		{
			long value;
			Tuple<ColumnConfiguration, TObfuscationStrategyConfiguration> _contextualConfiguration;
			TObfuscationStrategyConfiguration obfuscationStrategyConfiguration;

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			if ((object)contextualConfiguration == null)
				throw new ArgumentNullException("contextualConfiguration");

			obfuscationStrategyConfiguration = JObject.FromObject(contextualConfiguration.Item2).ToObject<TObfuscationStrategyConfiguration>();
			_contextualConfiguration = new Tuple<ColumnConfiguration, TObfuscationStrategyConfiguration>(contextualConfiguration.Item1, obfuscationStrategyConfiguration);
			value = this.CoreGetValueHashBucketSize(oxymoronEngine, _contextualConfiguration);

			return value;
		}

		public IEnumerable<Message> ValidateConfiguration(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, IDictionary<string, object>> contextualConfiguration)
		{
			Tuple<ColumnConfiguration, TObfuscationStrategyConfiguration> _contextualConfiguration;
			TObfuscationStrategyConfiguration obfuscationStrategyConfiguration;

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			if ((object)contextualConfiguration == null)
				throw new ArgumentNullException("contextualConfiguration");

			obfuscationStrategyConfiguration = JObject.FromObject(contextualConfiguration.Item2).ToObject<TObfuscationStrategyConfiguration>();
			_contextualConfiguration = new Tuple<ColumnConfiguration, TObfuscationStrategyConfiguration>(contextualConfiguration.Item1, obfuscationStrategyConfiguration);

			return _contextualConfiguration.Item2.Validate();
		}

		#endregion
	}
}