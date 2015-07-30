/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using Solder.Framework;

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