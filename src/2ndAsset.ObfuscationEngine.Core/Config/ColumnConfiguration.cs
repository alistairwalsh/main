/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using Solder.Framework;
using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Strategy;

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

		private readonly Dictionary<string, object> obfuscationStrategyConfiguration = new Dictionary<string, object>();
		private string columnName;
		private string obfuscationStrategyAqtn;

		#endregion

		#region Properties/Indexers/Events

		public Dictionary<string, object> ObfuscationStrategyConfiguration
		{
			get
			{
				return this.obfuscationStrategyConfiguration;
			}
		}

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

		public string ObfuscationStrategyAqtn
		{
			get
			{
				return this.obfuscationStrategyAqtn;
			}
			set
			{
				this.obfuscationStrategyAqtn = value;
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

		public virtual Type GetObfuscationStrategyConfigurationType()
		{
			return null;
		}

		public IObfuscationStrategy GetObfuscationStrategyInstance()
		{
			IObfuscationStrategy instance;
			Type type;

			type = this.GetObfuscationStrategyType();

			if ((object)type == null)
				return null;

			instance = (IObfuscationStrategy)Activator.CreateInstance(type);

			return instance;
		}

		public Type GetObfuscationStrategyType()
		{
			Type type;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ObfuscationStrategyAqtn))
				return null;

			type = Type.GetType(this.ObfuscationStrategyAqtn, false);

			return type;
		}

		public override sealed IEnumerable<Message> Validate()
		{
			return this.Validate(null);
		}

		public virtual IEnumerable<Message> Validate(int? columnIndex)
		{
			List<Message> messages;
			Type type;
			IObfuscationStrategy obfuscationStrategy;

			messages = new List<Message>();

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ColumnName))
				messages.Add(NewError(string.Format("Column[{0}] name is required.", columnIndex)));

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ObfuscationStrategyAqtn))
				messages.Add(NewError(string.Format("Column[{0}/{1}] obfuscation strategy AQTN is required.", columnIndex, this.ColumnName)));
			else
			{
				type = this.GetObfuscationStrategyType();

				if ((object)type == null)
					messages.Add(NewError(string.Format("Column[{0}/{1}] obfuscation strategy failed to load type from AQTN.", columnIndex, this.ColumnName)));
				else if (typeof(IObfuscationStrategy).IsAssignableFrom(type))
				{
					obfuscationStrategy = this.GetObfuscationStrategyInstance();

					if ((object)obfuscationStrategy == null)
						messages.Add(NewError(string.Format("Column[{0}/{1}] obfuscation strategy failed to instatiate type from AQTN.", columnIndex, this.ColumnName)));
					else
						messages.AddRange(obfuscationStrategy.ValidateConfiguration(this, columnIndex));
				}
				else
					messages.Add(NewError(string.Format("Column[{0}/{1}] obfuscation strategy loaded an unrecognized type via AQTN.", columnIndex, this.ColumnName)));
			}

			return messages;
		}

		#endregion
	}
}