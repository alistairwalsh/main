/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;

using Solder.Framework;
using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.Core.Config.Adapters
{
	public class DelimitedTextAdapterConfiguration : AdapterSpecificConfiguration
	{
		#region Constructors/Destructors

		public DelimitedTextAdapterConfiguration()
		{
		}

		#endregion

		#region Fields/Constants

		private string delimitedTextFilePath;
		private DelimitedTextSpec delimitedTextSpec;

		#endregion

		#region Properties/Indexers/Events

		public string DelimitedTextFilePath
		{
			get
			{
				return this.delimitedTextFilePath;
			}
			set
			{
				this.delimitedTextFilePath = value;
			}
		}

		public DelimitedTextSpec DelimitedTextSpec
		{
			get
			{
				return this.delimitedTextSpec;
			}
			set
			{
				this.delimitedTextSpec = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override IEnumerable<Message> Validate(string adapterContext)
		{
			List<Message> messages;

			messages = new List<Message>();

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.DelimitedTextFilePath))
				messages.Add(NewError(string.Format("{0} adapter delimited text file path is required.", adapterContext)));

			if ((object)this.DelimitedTextSpec == null)
				messages.Add(NewError(string.Format("{0} adapter delimited text specification is required.", adapterContext)));
			else
			{
				//if (DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.QuoteValue))
				//	messages.Add(NewError(string.Format("{0} adapter delimited text quote value is required.", adapterContext)));

				if (DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.RecordDelimiter))
					messages.Add(NewError(string.Format("{0} adapter delimited text record delimiter is required.", adapterContext)));

				if (DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.FieldDelimiter))
					messages.Add(NewError(string.Format("{0} adapter delimited text field delimiter is required.", adapterContext)));
			}

			return messages;
		}

		#endregion
	}
}