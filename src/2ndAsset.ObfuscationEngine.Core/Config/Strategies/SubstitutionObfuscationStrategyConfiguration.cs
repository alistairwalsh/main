/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;

using Solder.Framework;

namespace _2ndAsset.ObfuscationEngine.Core.Config.Strategies
{
	public class SubstitutionObfuscationStrategyConfiguration : ObfuscationStrategyConfiguration
	{
		#region Constructors/Destructors

		public SubstitutionObfuscationStrategyConfiguration()
		{
		}

		#endregion

		#region Fields/Constants

		private string dictionaryReference;

		#endregion

		#region Properties/Indexers/Events

		public string DictionaryReference
		{
			get
			{
				return this.dictionaryReference;
			}
			set
			{
				this.dictionaryReference = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override IEnumerable<Message> Validate()
		{
			/*if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.DictionaryReference))
				messages.Add(NewError(string.Format("Column[{0}/{1}] dictionary reference is required.", columnIndex, this.ColumnName)));
			else if (this.Parent.Parent.DictionaryConfigurations.Count(d => d.DictionaryId.SafeToString().Trim().ToLower() == this.DictionaryReference.SafeToString().Trim().ToLower()) != 1)
				messages.Add(NewError(string.Format("Column[{0}/{1}] dictionary reference lookup failed.", columnIndex, this.ColumnName)));*/

			return new Message[] { };
		}

		#endregion
	}
}