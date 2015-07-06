/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;

using Solder.Framework;
using Solder.Framework.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Config.Strategies
{
	public class ScriptObfuscationStrategyConfiguration : ObfuscationStrategyConfiguration
	{
		#region Constructors/Destructors

		public ScriptObfuscationStrategyConfiguration()
		{
		}

		#endregion

		#region Fields/Constants

		private string sourceCode;

		#endregion

		#region Properties/Indexers/Events

		public string SourceCode
		{
			get
			{
				return this.sourceCode;
			}
			set
			{
				this.sourceCode = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override IEnumerable<Message> Validate(int? columnIndex)
		{
			List<Message> messages;

			messages = new List<Message>();

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.SourceCode))
				messages.Add(NewError(string.Format("Column[{0}/{1}] source code is required.", columnIndex, this.Parent.ColumnName)));

			return messages;
		}

		#endregion
	}
}