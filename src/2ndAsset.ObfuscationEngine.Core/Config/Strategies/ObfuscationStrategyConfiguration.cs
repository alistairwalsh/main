/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;

using Solder.Framework;

namespace _2ndAsset.ObfuscationEngine.Core.Config.Strategies
{
	public class ObfuscationStrategyConfiguration : ConfigurationObject
	{
		#region Constructors/Destructors

		public ObfuscationStrategyConfiguration()
		{
		}

		#endregion

		#region Methods/Operators

		public override IEnumerable<Message> Validate()
		{
			return new Message[] { };
		}

		#endregion
	}
}