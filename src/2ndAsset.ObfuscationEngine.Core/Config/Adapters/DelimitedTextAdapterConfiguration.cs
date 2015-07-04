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
	public class DelimitedTextAdapterConfiguration : ConfigurationObject
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

		public override IEnumerable<Message> Validate()
		{
			return this.Validate(null);
		}

		public IEnumerable<Message> Validate(string context)
		{
			List<Message> messages;

			messages = new List<Message>();

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.DelimitedTextFilePath))
				messages.Add(NewError(string.Format("Delimited text file path is required.")));

			if ((object)this.DelimitedTextSpec == null)
				messages.Add(NewError(string.Format("Delimited text specification is required.")));
			else
			{
				//if (DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.QuoteValue))
				//	messages.Add(NewError(string.Format("Delimited text quote value is required.")));

				if (DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.RecordDelimiter))
					messages.Add(NewError(string.Format("Delimited text record delimiter is required.")));

				if (DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.FieldDelimiter))
					messages.Add(NewError(string.Format("Delimited text field delimiter is required.")));
			}

			return messages;
		}

		#endregion
	}
}