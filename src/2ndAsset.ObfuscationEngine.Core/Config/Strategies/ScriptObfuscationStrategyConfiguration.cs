/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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