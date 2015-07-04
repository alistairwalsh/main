/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
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

		object GetObfuscatedValue(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, IDictionary<string, object>> contextualConfiguration, HashResult hashResult, IMetaColumn metaColumn, object columnValue);

		long GetValueHashBucketSize(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, IDictionary<string, object>> contextualConfiguration);

		IEnumerable<Message> ValidateConfiguration(IOxymoronEngine oxymoronEngine, Tuple<ColumnConfiguration, IDictionary<string, object>> contextualConfiguration);

		#endregion
	}
}