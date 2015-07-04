/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Strategies;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	/// <summary>
	/// Returns an alternate value that is always null if NULL or the default value if NOT NULL.
	/// DATA TYPE: any
	/// </summary>
	public sealed class DefaultingObfuscationStrategy : ObfuscationStrategy<DefaultingObfuscationStrategyConfiguration>
	{
		#region Constructors/Destructors

		public DefaultingObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		private static object GetDefault(bool isNullable, Type valueType)
		{
			if ((object)valueType == null)
				throw new ArgumentNullException("valueType");

			if (valueType == typeof(String))
				return isNullable ? null : string.Empty;

			if (isNullable)
				valueType = ReflectionFascade.Instance.MakeNullableType(valueType);

			return DataTypeFascade.Instance.DefaultValue(valueType);
		}

		protected override object CoreGetObfuscatedValue(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, DefaultingObfuscationStrategyConfiguration> contextualConfiguration, HashResult hashResult, IMetaColumn metaColumn, object columnValue)
		{
			object value;

			if ((object)contextualConfiguration == null)
				throw new ArgumentNullException("contextualConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			value = GetDefault(metaColumn.ColumnIsNullable ?? contextualConfiguration.Item2.DefaultingCanBeNull ?? false, metaColumn.ColumnType);

			return value;
		}

		#endregion
	}
}