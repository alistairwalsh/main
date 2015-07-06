/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using _2ndAsset.ObfuscationEngine.Core.Config.Strategies;

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public sealed class ColumnConfiguration<TObfuscationStrategyConfiguration> : ColumnConfiguration
		where TObfuscationStrategyConfiguration : ObfuscationStrategyConfiguration, new()
	{
		#region Constructors/Destructors

		public ColumnConfiguration(ColumnConfiguration columnConfiguration)
		{
			if ((object)columnConfiguration == null)
				throw new ArgumentNullException("columnConfiguration");

			if ((object)base.ObfuscationStrategyConfiguration != null &&
				(object)columnConfiguration.ObfuscationStrategyConfiguration != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in columnConfiguration.ObfuscationStrategyConfiguration)
					base.ObfuscationStrategyConfiguration.Add(keyValuePair.Key, keyValuePair.Value);
			}

			this.ColumnName = columnConfiguration.ColumnName;
			this.ObfuscationStrategyAqtn = columnConfiguration.ObfuscationStrategyAqtn;
			this.Parent = columnConfiguration.Parent;
			this.Surround = columnConfiguration.Surround;
		}

		#endregion

		#region Fields/Constants

		private bool frozen;
		private TObfuscationStrategyConfiguration obfuscationStrategyConfiguration;

		#endregion

		#region Properties/Indexers/Events

		public bool Frozen
		{
			get
			{
				return this.frozen;
			}
			set
			{
				this.frozen = value;
			}
		}

		public new TObfuscationStrategyConfiguration ObfuscationStrategyConfiguration
		{
			get
			{
				this.ApplyObfuscationStrategyConfiguration(); // special case
				return this.obfuscationStrategyConfiguration;
			}
			set
			{
				this.EnsureParentOnPropertySet(this.obfuscationStrategyConfiguration, value);
				this.obfuscationStrategyConfiguration = value;
			}
		}

		#endregion

		#region Methods/Operators

		public void ApplyObfuscationStrategyConfiguration()
		{
			if ((object)base.ObfuscationStrategyConfiguration != null && !this.Frozen)
			{
				this.ObfuscationStrategyConfiguration = JObject.FromObject(base.ObfuscationStrategyConfiguration).ToObject<TObfuscationStrategyConfiguration>();
				this.Frozen = true;
			}
		}

		#endregion
	}
}