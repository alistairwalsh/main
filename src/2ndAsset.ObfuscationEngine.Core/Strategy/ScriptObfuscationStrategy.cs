/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public sealed class ScriptObfuscationStrategy : ObfuscationStrategy<ColumnConfiguration>
	{
		#region Constructors/Destructors

		public ScriptObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetObfuscatedValue(ColumnConfiguration configurationContext, HashResult hashResult, IMetaColumn metaColumn, object columnValue)
		{
			if ((object)configurationContext == null)
				throw new ArgumentNullException("configurationContext");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			return columnValue;
		}

		#endregion
	}
}