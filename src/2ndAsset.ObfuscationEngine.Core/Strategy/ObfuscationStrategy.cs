/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public abstract class ObfuscationStrategy : IObfuscationStrategy
	{
		#region Constructors/Destructors

		protected ObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected abstract object CoreGetObfuscatedValue(long signHash, long valueHash, int? extentValue, IMetaColumn metaColumn, object columnValue);

		public object GetObfuscatedValue(long signHash, long valueHash, int? extentValue, IMetaColumn metaColumn, object columnValue)
		{
			object value;

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)columnValue == DBNull.Value)
				columnValue = null;

			value = this.CoreGetObfuscatedValue(signHash, valueHash, extentValue, metaColumn, columnValue);

			return value;
		}

		#endregion
	}
}