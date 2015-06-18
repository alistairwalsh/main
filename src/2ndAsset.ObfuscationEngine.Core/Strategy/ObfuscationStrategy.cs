/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public abstract class ObfuscationStrategy<TConfigurationContext> : IObfuscationStrategy<TConfigurationContext>
		where TConfigurationContext : class, IConfigurationObject, new()
	{
		#region Constructors/Destructors

		protected ObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected abstract object CoreGetObfuscatedValue(TConfigurationContext configurationContext, HashResult hashResult, IMetaColumn metaColumn, object columnValue);

		public object GetObfuscatedValue(TConfigurationContext configurationContext, HashResult hashResult, IMetaColumn metaColumn, object columnValue)
		{
			object value;

			if ((object)configurationContext == null)
				throw new ArgumentNullException("configurationContext");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)columnValue == DBNull.Value)
				columnValue = null;

			value = this.CoreGetObfuscatedValue(configurationContext, hashResult, metaColumn, columnValue);

			return value;
		}

		#endregion
	}
}