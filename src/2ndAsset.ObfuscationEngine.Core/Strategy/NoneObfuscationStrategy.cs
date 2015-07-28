/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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

		protected override object CoreGetObfuscatedValue(IOxymoronEngine oxymoronEngine, ColumnConfiguration<ObfuscationStrategyConfiguration> columnConfiguration, IMetaColumn metaColumn, object columnValue)
		{
			if ((object)columnConfiguration == null)
				throw new ArgumentNullException("columnConfiguration");

			if ((object)metaColumn == null)
				throw new ArgumentNullException("metaColumn");

			if ((object)columnConfiguration.ObfuscationStrategySpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "ObfuscationStrategyConfiguration"));

			return columnValue;
		}

		#endregion
	}
}