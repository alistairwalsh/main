/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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

		protected override object CoreGetObfuscatedValue(IOxymoronEngine oxymoronEngine, ColumnConfiguration<DefaultingObfuscationStrategyConfiguration> columnConfiguration, IMetaColumn metaColumn, object columnValue)
		{
			object value;

			if ((object)columnConfiguration == null)
				throw new ArgumentNullException("columnConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)columnConfiguration.ObfuscationStrategySpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ObfuscationStrategyConfiguration"));

			value = GetDefault(metaColumn.ColumnIsNullable ?? columnConfiguration.ObfuscationStrategySpecificConfiguration.DefaultingCanBeNull ?? false, metaColumn.ColumnType);

			return value;
		}

		#endregion
	}
}