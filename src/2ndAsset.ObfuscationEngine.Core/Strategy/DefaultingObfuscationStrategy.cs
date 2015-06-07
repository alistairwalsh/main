/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using TextMetal.Middleware.Common.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public sealed class DefaultingObfuscationStrategy : ObfuscationStrategy
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

		protected override object CoreGetObfuscatedValue(long signHash, long valueHash, int? extentValue, MetaColumn metaColumn, object columnValue)
		{
			object value;

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			value = GetDefault(metaColumn.ColumnIsNullable, metaColumn.ColumnType);

			return value;
		}

		#endregion
	}
}