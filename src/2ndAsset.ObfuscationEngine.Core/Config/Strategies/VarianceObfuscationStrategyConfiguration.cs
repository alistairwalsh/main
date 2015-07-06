/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;

using Solder.Framework;

namespace _2ndAsset.ObfuscationEngine.Core.Config.Strategies
{
	public class VarianceObfuscationStrategyConfiguration : ObfuscationStrategyConfiguration
	{
		#region Constructors/Destructors

		public VarianceObfuscationStrategyConfiguration()
		{
		}

		#endregion

		#region Fields/Constants

		private long? variancePercentValue;

		#endregion

		#region Properties/Indexers/Events

		public long? VariancePercentValue
		{
			get
			{
				return this.variancePercentValue;
			}
			set
			{
				this.variancePercentValue = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override IEnumerable<Message> Validate(int? columnIndex)
		{
			List<Message> messages;

			messages = new List<Message>();

			if ((object)this.VariancePercentValue == null)
				messages.Add(NewError(string.Format("Column[{0}/{1}] variance percent value is required.", columnIndex, this.Parent.ColumnName)));
			else if (!((int)this.VariancePercentValue >= -100 && (int)this.VariancePercentValue <= 100))
				messages.Add(NewError(string.Format("Column[{0}/{1}] variance percent value must be between -100 and +100.", columnIndex, this.Parent.ColumnName)));

			return messages;
		}

		#endregion
	}
}