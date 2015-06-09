/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public sealed class ScriptObfuscationStrategy : ObfuscationStrategy
	{
		#region Constructors/Destructors

		public ScriptObfuscationStrategy()
		{
		}

		#endregion

		#region Methods/Operators

		protected override object CoreGetObfuscatedValue(long signHash, long valueHash, int? extentValue, IMetaColumn metaColumn, object columnValue)
		{
			return columnValue;
		}

		#endregion
	}
}