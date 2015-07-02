/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using Solder.Framework;
using Solder.Framework.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Config
{
	public class ColumnConfiguration : ConfigurationObject
	{
		#region Constructors/Destructors

		public ColumnConfiguration()
		{
		}

		#endregion

		#region Fields/Constants

		private string columnName;
		private string dictionaryReference;
		private int? extentValue;
		private bool? isColumnNullable;
		private ObfuscationStrategy obfuscationStrategy;

		#endregion

		#region Properties/Indexers/Events

		public string ColumnName
		{
			get
			{
				return this.columnName;
			}
			set
			{
				this.columnName = value;
			}
		}

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

		public int? ExtentValue
		{
			get
			{
				return this.extentValue;
			}
			set
			{
				this.extentValue = value;
			}
		}

		public bool? IsColumnNullable
		{
			get
			{
				return this.isColumnNullable;
			}
			set
			{
				this.isColumnNullable = value;
			}
		}

		public ObfuscationStrategy ObfuscationStrategy
		{
			get
			{
				return this.obfuscationStrategy;
			}
			set
			{
				this.obfuscationStrategy = value;
			}
		}

		[JsonIgnore]
		public new TableConfiguration Parent
		{
			get
			{
				return (TableConfiguration)base.Parent;
			}
			set
			{
				base.Parent = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override bool Equals(object obj)
		{
			ColumnConfiguration other;

			other = obj as ColumnConfiguration;

			if ((object)other != null)
				return other.ColumnName.SafeToString().ToLower() == this.ColumnName.SafeToString().ToLower();

			return false;
		}

		public override int GetHashCode()
		{
			return this.ColumnName.SafeToString().ToLower().GetHashCode();
		}

		public override IEnumerable<Message> Validate()
		{
			return this.Validate(null);
		}

		public IEnumerable<Message> Validate(int? columnIndex)
		{
			List<Message> messages;

			messages = new List<Message>();

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ColumnName))
				messages.Add(NewError(string.Format("Column[{0}] name is required.", columnIndex)));

			if (!Enum.IsDefined(typeof(ObfuscationStrategy), this.ObfuscationStrategy))
				messages.Add(NewError(string.Format("Column[{0}/{1}] obfuscation strategy is invalid.", columnIndex, this.ColumnName)));

			if (this.ObfuscationStrategy != ObfuscationStrategy.None)
			{
				if (this.ObfuscationStrategy == ObfuscationStrategy.Masking)
				{
					if ((object)this.ExtentValue == null)
						messages.Add(NewError(string.Format("Column[{0}/{1}] masking extent is required.", columnIndex)));
					else if (!((int)this.ExtentValue >= -100 && (int)this.ExtentValue <= 100))
						messages.Add(NewError(string.Format("Column[{0}/{1}] masking extent must be between -100 and +100.", columnIndex, this.ColumnName)));
				}
				else if (this.ObfuscationStrategy == ObfuscationStrategy.Substitution)
				{
					if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.DictionaryReference))
						messages.Add(NewError(string.Format("Column[{0}/{1}] dictionary reference is required.", columnIndex, this.ColumnName)));
					else if (this.Parent.Parent.DictionaryConfigurations.Count(d => d.DictionaryId.SafeToString().Trim().ToLower() == this.DictionaryReference.SafeToString().Trim().ToLower()) != 1)
						messages.Add(NewError(string.Format("Column[{0}/{1}] dictionary reference lookup failed.", columnIndex, this.ColumnName)));
				}
				else if (this.ObfuscationStrategy == ObfuscationStrategy.Variance)
				{
					if ((object)this.ExtentValue == null)
						messages.Add(NewError(string.Format("Column[{0}/{1}] variance extent is required.", columnIndex, this.ColumnName)));
					else if (!((int)this.ExtentValue > 0 && (int)this.ExtentValue <= 100))
						messages.Add(NewError(string.Format("Column[{0}/{1}] variance extent must be between 0 and 100.", columnIndex, this.ColumnName)));
				}
				else if (this.ObfuscationStrategy == ObfuscationStrategy.Defaulting)
				{
					if ((object)this.IsColumnNullable == null)
						messages.Add(NewError(string.Format("Column[{0}/{1}] is nullable flag is required.", columnIndex, this.ColumnName)));
				}
			}

			return messages;
		}

		#endregion
	}
}