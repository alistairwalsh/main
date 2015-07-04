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

		public override IEnumerable<Message> Validate()
		{
			/*if ((object)this.ExtentValue == null)
						messages.Add(NewError(string.Format("Column[{0}/{1}] variance extent is required.", columnIndex, this.ColumnName)));
					else if (!((int)this.ExtentValue > 0 && (int)this.ExtentValue <= 100))
						messages.Add(NewError(string.Format("Column[{0}/{1}] variance extent must be between 0 and 100.", columnIndex, this.ColumnName)));*/
			return new Message[] { };
		}

		#endregion
	}
}