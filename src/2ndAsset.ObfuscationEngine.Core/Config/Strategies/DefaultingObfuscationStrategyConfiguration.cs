/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Collections.Generic;

using Solder.Framework;

namespace _2ndAsset.ObfuscationEngine.Core.Config.Strategies
{
	public class DefaultingObfuscationStrategyConfiguration : ObfuscationStrategyConfiguration
	{
		#region Constructors/Destructors

		public DefaultingObfuscationStrategyConfiguration()
		{
		}

		#endregion

		#region Fields/Constants

		private bool? defaultingCanBeNull;

		#endregion

		#region Properties/Indexers/Events

		public bool? DefaultingCanBeNull
		{
			get
			{
				return this.defaultingCanBeNull;
			}
			set
			{
				this.defaultingCanBeNull = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override IEnumerable<Message> Validate(int? columnIndex)
		{
			List<Message> messages;

			messages = new List<Message>();

			if ((object)this.DefaultingCanBeNull == null)
				messages.Add(NewError(string.Format("Column[{0}/{1}] defaulting can be null flag is required.", columnIndex, this.Parent.ColumnName)));

			return messages;
		}

		#endregion
	}
}