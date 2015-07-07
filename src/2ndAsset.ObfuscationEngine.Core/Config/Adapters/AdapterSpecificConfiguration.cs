/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;

using Newtonsoft.Json;

using Solder.Framework;

namespace _2ndAsset.ObfuscationEngine.Core.Config.Adapters
{
	public class AdapterSpecificConfiguration : ConfigurationObject
	{
		#region Constructors/Destructors

		public AdapterSpecificConfiguration()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[JsonIgnore]
		public new AdapterConfiguration Parent
		{
			get
			{
				return (AdapterConfiguration)base.Parent;
			}
			set
			{
				base.Parent = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override sealed IEnumerable<Message> Validate()
		{
			return this.Validate(null);
		}

		public virtual IEnumerable<Message> Validate(string adapterContext)
		{
			return new Message[] { };
		}

		#endregion
	}
}