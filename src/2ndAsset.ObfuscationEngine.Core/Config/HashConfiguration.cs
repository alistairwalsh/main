/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using TextMetal.Middleware.Common;

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public class HashConfiguration : ConfigurationObject
	{
		#region Constructors/Destructors

		public HashConfiguration()
		{
		}

		#endregion

		#region Fields/Constants

		private long? multiplier;
		private long? seed;

		#endregion

		#region Properties/Indexers/Events

		public long? Multiplier
		{
			get
			{
				return this.multiplier;
			}
			set
			{
				this.multiplier = value;
			}
		}

		public long? Seed
		{
			get
			{
				return this.seed;
			}
			set
			{
				this.seed = value;
			}
		}

		#endregion

		#region Methods/Operators

		public HashConfiguration Clone()
		{
			return new HashConfiguration() { Multiplier = this.Multiplier, Seed = this.Seed };
		}

		public override IEnumerable<Message> Validate()
		{
			List<Message> messages;

			messages = new List<Message>();

			if ((object)this.Multiplier == null)
				messages.Add(NewError(string.Format("Hash multiplier is required.")));

			if ((object)this.Seed == null)
				messages.Add(NewError(string.Format("Hash seed is required.")));

			return messages;
		}

		#endregion
	}
}