/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Solder.Framework.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText
{
	public class DelimitedTextWriter : RecordTextWriter
	{
		#region Constructors/Destructors

		public DelimitedTextWriter(TextWriter innerTextWriter, DelimitedTextSpec delimitedTextSpec)
			: base(innerTextWriter)
		{
			if ((object)delimitedTextSpec == null)
				throw new ArgumentNullException("delimitedTextSpec");

			this.innerTextWriter = innerTextWriter;
			this.delimitedTextSpec = delimitedTextSpec;
		}

		#endregion

		#region Fields/Constants

		private readonly DelimitedTextSpec delimitedTextSpec;
		private readonly TextWriter innerTextWriter;

		#endregion

		#region Properties/Indexers/Events

		public DelimitedTextSpec DelimitedTextSpec
		{
			get
			{
				return this.delimitedTextSpec;
			}
		}

		public override Encoding Encoding
		{
			get
			{
				return this.InnerTextWriter.Encoding;
			}
		}

		#endregion

		#region Methods/Operators

		private void WriteField(StringBuilder transientStringBuilder, bool first, object fieldValue)
		{
			if ((object)transientStringBuilder == null)
				throw new ArgumentNullException("transientStringBuilder");

			if (!first && !DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.FieldDelimiter))
				transientStringBuilder.Append(this.DelimitedTextSpec.FieldDelimiter);

			if (!DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.QuoteValue))
				transientStringBuilder.Append(this.DelimitedTextSpec.QuoteValue);

			transientStringBuilder.Append(fieldValue);

			if (!DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.QuoteValue))
				transientStringBuilder.Append(this.DelimitedTextSpec.QuoteValue);
		}

		public override void WriteRecords(IEnumerable<IDictionary<string, object>> records)
		{
			bool first;
			StringBuilder transientStringBuilder;
			string tempStringValue;

			if ((object)records == null)
				throw new ArgumentNullException("records");

			transientStringBuilder = new StringBuilder();

			if ((object)this.DelimitedTextSpec.HeaderSpecs != null &&
				this.DelimitedTextSpec.FirstRecordIsHeader)
			{
				first = true;
				foreach (HeaderSpec headerSpec in this.DelimitedTextSpec.HeaderSpecs)
				{
					this.WriteField(transientStringBuilder, first, headerSpec.HeaderName);

					if (first)
						first = false;
				}
			}

			if (!DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.RecordDelimiter))
				transientStringBuilder.Append(this.DelimitedTextSpec.RecordDelimiter);

			tempStringValue = transientStringBuilder.ToString();
			transientStringBuilder.Clear();
			this.InnerTextWriter.Write(tempStringValue);

			foreach (IDictionary<string, object> record in records)
			{
				first = true;
				foreach (KeyValuePair<string, object> field in record)
				{
					this.WriteField(transientStringBuilder, first, field.Value);

					if (first)
						first = false;
				}

				if (!DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.RecordDelimiter))
					transientStringBuilder.Append(this.DelimitedTextSpec.RecordDelimiter);

				tempStringValue = transientStringBuilder.ToString();
				transientStringBuilder.Clear();
				this.InnerTextWriter.Write(tempStringValue);
			}
		}

		#endregion
	}
}