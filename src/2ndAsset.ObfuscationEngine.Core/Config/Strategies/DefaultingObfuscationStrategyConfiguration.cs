/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
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

		public override IEnumerable<Message> Validate()
		{
			/*if ((object)this.ExtentValue == null)
						messages.Add(NewError(string.Format("Column[{0}/{1}] masking extent is required.", columnIndex)));
					else if (!((int)this.ExtentValue >= -100 && (int)this.ExtentValue <= 100))
						messages.Add(NewError(string.Format("Column[{0}/{1}] masking extent must be between -100 and +100.", columnIndex, this.ColumnName)));*/
			return new Message[] { };
		}

		#endregion
	}
}