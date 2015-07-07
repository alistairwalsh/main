/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

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

		#region Fields/Constants

		private const long DEFAULT_HASH_BUCKET_SIZE = long.MaxValue;

		#endregion

		#region Methods/Operators

		protected abstract object CoreGetObfuscatedValue(IOxymoronEngine oxymoronEngine, ColumnConfiguration<TObfuscationStrategyConfiguration> columnConfiguration, IMetaColumn metaColumn, object columnValue);

		public object GetObfuscatedValue(IOxymoronEngine oxymoronEngine, ColumnConfiguration columnConfiguration, IMetaColumn metaColumn, object columnValue)
		{
			ColumnConfiguration<TObfuscationStrategyConfiguration> _columnConfiguration;
			object value;

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			if ((object)columnConfiguration == null)
				throw new ArgumentNullException("columnConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)columnValue == DBNull.Value)
				columnValue = null;

			if ((object)columnConfiguration.ObfuscationStrategySpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ObfuscationStrategyConfiguration"));

			_columnConfiguration = new ColumnConfiguration<TObfuscationStrategyConfiguration>(columnConfiguration);

			if ((object)_columnConfiguration.ObfuscationStrategySpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ObfuscationStrategyConfiguration"));

			value = this.CoreGetObfuscatedValue(oxymoronEngine, _columnConfiguration, metaColumn, columnValue);

			return value;
		}

		public Type GetObfuscationStrategySpecificConfigurationType()
		{
			return typeof(TObfuscationStrategyConfiguration);
		}

		protected long GetSignHash(IOxymoronEngine oxymoronEngine, object value)
		{
			long hash;

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			hash = oxymoronEngine.GetBoundedHash(DEFAULT_HASH_BUCKET_SIZE, value);

			return hash;
		}

		protected long GetValueHash(IOxymoronEngine oxymoronEngine, long? size, object value)
		{
			long hash;

			if ((object)oxymoronEngine == null)
				throw new ArgumentNullException("oxymoronEngine");

			hash = oxymoronEngine.GetBoundedHash(size ?? DEFAULT_HASH_BUCKET_SIZE, value);

			return hash;
		}

		public IEnumerable<Message> ValidateObfuscationStrategySpecificConfiguration(ColumnConfiguration columnConfiguration, int? colummIndex)
		{
			ColumnConfiguration<TObfuscationStrategyConfiguration> _columnConfiguration;

			if ((object)columnConfiguration == null)
				throw new ArgumentNullException("columnConfiguration");

			if ((object)columnConfiguration.ObfuscationStrategySpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ObfuscationStrategyConfiguration"));

			_columnConfiguration = new ColumnConfiguration<TObfuscationStrategyConfiguration>(columnConfiguration);

			if ((object)_columnConfiguration.ObfuscationStrategySpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ObfuscationStrategyConfiguration"));

			return _columnConfiguration.ObfuscationStrategySpecificConfiguration.Validate(colummIndex);
		}

		#endregion
	}
}