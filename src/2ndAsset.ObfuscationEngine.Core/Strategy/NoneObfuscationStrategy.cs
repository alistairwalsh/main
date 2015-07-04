/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Strategies;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	/// <summary>
	/// Returns un-obfuscated, original value.
	/// DATA TYPE: any
	/// </summary>
	public sealed class NoneObfuscationStrategy : ObfuscationStrategy<ObfuscationStrategyConfiguration>
	{
		#region Constructors/Destructors

		public NoneObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetObfuscatedValue(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, ObfuscationStrategyConfiguration> contextualConfiguration, HashResult hashResult, IMetaColumn metaColumn, object columnValue)
		{
			object value;

			if ((object)contextualConfiguration == null)
				throw new ArgumentNullException("contextualConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			value = columnValue;

			return value;
		}

		#endregion
	}
}