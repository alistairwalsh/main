/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using Solder.Framework;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Strategy
{
	public interface IObfuscationStrategy
	{
		#region Methods/Operators

		object GetObfuscatedValue(IOxymoronEngine oxymoronEngine, ColumnConfiguration columnConfiguration, IMetaColumn metaColumn, object columnValue);

		Type GetObfuscationStrategySpecificConfigurationType();

		IEnumerable<Message> ValidateObfuscationStrategySpecificConfiguration(ColumnConfiguration columnConfiguration, int? columnIndex);

		#endregion
	}
}