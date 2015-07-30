/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Solder.Framework.Utilities;

namespace _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText
{
	public class DelimitedTextReader : RecordTextReader
	{
		#region Constructors/Destructors

		public DelimitedTextReader(TextReader innerTextReader, DelimitedTextSpec delimitedTextSpec)
			: base(innerTextReader)
		{
			if ((object)delimitedTextSpec == null)
				throw new ArgumentNullException("delimitedTextSpec");

			this.delimitedTextSpec = delimitedTextSpec;

			this.ResetParserState();
		}

		#endregion

		#region Fields/Constants

		private readonly DelimitedTextSpec delimitedTextSpec;
		private readonly _ParserState parserState = new _ParserState();

		#endregion

		#region Properties/Indexers/Events

		public DelimitedTextSpec DelimitedTextSpec
		{
			get
			{
				return this.delimitedTextSpec;
			}
		}

		private _ParserState ParserState
		{
			get
			{
				return this.parserState;
			}
		}

		#endregion

		#region Methods/Operators

		private static bool LookBehindFixup(StringBuilder targetStringBuilder, string targetValue)
		{
			if ((object)targetStringBuilder == null)
				throw new ArgumentNullException("targetStringBuilder");

			if ((object)targetValue == null)
				throw new ArgumentNullException("targetValue");

			if (DataTypeFascade.Instance.IsNullOrEmpty(targetValue))
				throw new ArgumentOutOfRangeException("targetValue");

			// look-behind CHECK
			if (targetStringBuilder.Length > 0 &&
				targetValue.Length > 0 &&
				targetStringBuilder.Length >= targetValue.Length)
			{
				int sb_length;
				int rd_length;
				int matches;

				sb_length = targetStringBuilder.Length;
				rd_length = targetValue.Length;
				matches = 0;

				for (int i = 0; i < rd_length; i++)
				{
					if (targetStringBuilder[sb_length - rd_length + i] != targetValue[i])
						return false; // look-behind NO MATCH...

					matches++;
				}

				if (matches != rd_length)
					throw new InvalidOperationException(string.Format("Something went sideways."));

				targetStringBuilder.Remove(sb_length - rd_length, rd_length);

				// look-behind MATCHED: stop
				return true;
			}

			return false; // not enough buffer space to care
		}

		private bool ParserStateMachine()
		{
			string tempStringValue;
			bool matchedRecordDelimiter, matchedFieldDelimiter;

			// now determine what to do based on parser state
			matchedRecordDelimiter = !this.ParserState.isQuotedValue &&
									!DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.RecordDelimiter) &&
									LookBehindFixup(this.ParserState.transientStringBuilder, this.DelimitedTextSpec.RecordDelimiter);

			if (!matchedRecordDelimiter)
			{
				matchedFieldDelimiter = !this.ParserState.isQuotedValue &&
										!DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.FieldDelimiter) &&
										LookBehindFixup(this.ParserState.transientStringBuilder, this.DelimitedTextSpec.FieldDelimiter);
			}
			else
				matchedFieldDelimiter = false;

			if (matchedRecordDelimiter || matchedFieldDelimiter || this.ParserState.isEOF)
			{
				// RECORD_DELIMITER | FIELD_DELIMITER | EOF

				// get string and clear for exit
				tempStringValue = this.ParserState.transientStringBuilder.ToString();
				this.ParserState.transientStringBuilder.Clear();

				// common logic to store value of field in record
				if (this.ParserState.isHeaderRecord)
				{
					// stash header if FRIS enabled and zeroth record
					this.ParserState.record.Add(tempStringValue, this.ParserState.fieldIndex.ToString("0000"));
				}
				else
				{
					HeaderSpec headerSpec;
					Type fieldType;
					object fieldValue;

					// check header array and field index validity
					if ((object)this.DelimitedTextSpec.HeaderSpecs == null ||
						this.ParserState.fieldIndex >= this.DelimitedTextSpec.HeaderSpecs.Count)
						throw new InvalidOperationException(string.Format("Delimited text reader parse state failure: field index '{0}' exceeded known field indices '{1}' at character index '{2}'.", this.ParserState.fieldIndex, (object)this.DelimitedTextSpec.HeaderSpecs != null ? (this.DelimitedTextSpec.HeaderSpecs.Count - 1) : (int?)null, this.ParserState.characterIndex));

					headerSpec = this.DelimitedTextSpec.HeaderSpecs[this.ParserState.fieldIndex];

					switch (headerSpec.FieldType)
					{
						case FieldType.Number:
							fieldType = typeof(Nullable<Decimal>);
							break;
						case FieldType.DateTime:
							fieldType = typeof(Nullable<DateTime>);
							break;
						case FieldType.TimeSpan:
							fieldType = typeof(Nullable<TimeSpan>);
							break;
						case FieldType.Boolean:
							fieldType = typeof(Nullable<Boolean>);
							break;
						default:
							fieldType = typeof(String);
							break;
					}

					if (DataTypeFascade.Instance.IsNullOrWhiteSpace(tempStringValue))
						fieldValue = DataTypeFascade.Instance.DefaultValue(fieldType);
					else
						DataTypeFascade.Instance.TryParse(fieldType, tempStringValue, out fieldValue);

					// lookup header name (key) by index and commit value to record
					this.ParserState.record.Add(headerSpec.HeaderName, fieldValue);
				}

				// handle blank records (we assume that an records with valid record delimiter is OK)
				if (DataTypeFascade.Instance.IsNullOrEmpty(tempStringValue) && this.ParserState.record.Keys.Count == 1)
					this.ParserState.record = null;

				// now what to do?
				if (this.ParserState.isEOF)
					return true;
				else if (matchedRecordDelimiter)
				{
					// advance record index
					this.ParserState.recordIndex++;

					// reset field index
					this.ParserState.fieldIndex = 0;

					// reset value index
					this.ParserState.valueIndex = 0;

					return true;
				}
				else if (matchedFieldDelimiter)
				{
					// advance field index
					this.ParserState.fieldIndex++;

					// reset value index
					this.ParserState.valueIndex = 0;
				}
			}
			else if (!this.ParserState.isEOF &&
					!this.ParserState.isQuotedValue &&
					!DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.QuoteValue) &&
					LookBehindFixup(this.ParserState.transientStringBuilder, this.DelimitedTextSpec.QuoteValue))
			{
				// BEGIN::QUOTE_VALUE
				this.ParserState.isQuotedValue = true;
			}
			//else if (!this.ParserState.isEOF &&
			//	this.ParserState.isQuotedValue &&
			//	!DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.QuoteValue) &&
			//	LookBehindFixup(this.ParserState.transientStringBuilder, this.DelimitedTextSpec.QuoteValue) &&
			//	this.ParserState.peekNextCharacter.ToString() == this.DelimitedTextSpec.QuoteValue)
			//{
			//	// unescape::QUOTE_VALUE
			//	this.ParserState.transientStringBuilder.Append("'");
			//}
			else if (!this.ParserState.isEOF &&
					this.ParserState.isQuotedValue &&
					!DataTypeFascade.Instance.IsNullOrEmpty(this.DelimitedTextSpec.QuoteValue) &&
					LookBehindFixup(this.ParserState.transientStringBuilder, this.DelimitedTextSpec.QuoteValue))
			{
				// END::QUOTE_VALUE
				this.ParserState.isQuotedValue = false;
			}
			else if (!this.ParserState.isEOF)
			{
				// {field_data}

				// advance content index
				this.ParserState.contentIndex++;

				// advance value index
				this.ParserState.valueIndex++;
			}
			else
			{
				// {unknown_parser_state_error}
				throw new InvalidOperationException(string.Format("Unknown parser state error at character index '{0}'.", this.ParserState.characterIndex));
			}

			return false;
		}

		public override IEnumerable<HeaderSpec> ReadHeaderSpecs()
		{
			if (this.ParserState.recordIndex == 0 &&
				this.DelimitedTextSpec.FirstRecordIsHeader)
			{
				var y = this.ResumableParserMainLoop(true);

				y.SingleOrDefault(); // force a single enumeration - yield return is a brain fyck
			}

			return this.DelimitedTextSpec.HeaderSpecs;
		}

		public override IEnumerable<IDictionary<string, object>> ReadRecords()
		{
			return this.ResumableParserMainLoop(false);
		}

		private void ResetParserState()
		{
			this.ParserState.record = new Dictionary<string, object>();
			this.ParserState.transientStringBuilder = new StringBuilder();
			this.ParserState.readCurrentCharacter = '\0';
			this.ParserState.peekNextCharacter = '\0';
			this.ParserState.characterIndex = 0;
			this.ParserState.contentIndex = 0;
			this.ParserState.recordIndex = 0;
			this.ParserState.fieldIndex = 0;
			this.ParserState.valueIndex = 0;
			this.ParserState.isQuotedValue = false;
			this.ParserState.isEOF = false;

			this.DelimitedTextSpec.AssertValid();
		}

		private IEnumerable<IDictionary<string, object>> ResumableParserMainLoop(bool once)
		{
			int __value;
			char ch;

			// main loop - character stream
			while (!this.ParserState.isEOF)
			{
				// read the next byte
				__value = this.InnerTextReader.Read();
				ch = (char)__value;

				// check for -1 (EOF)
				if (__value == -1)
				{
					this.ParserState.isEOF = true; // set terminal state

					// sanity check - should never end with an open quote value
					if (this.ParserState.isQuotedValue)
						throw new InvalidOperationException(string.Format("Delimited text reader parse state failure: end of file encountered while reading open quoted value."));
				}
				else
				{
					// append character to temp buffer
					this.ParserState.readCurrentCharacter = ch;
					this.ParserState.transientStringBuilder.Append(ch);

					// advance character index
					this.ParserState.characterIndex++;
				}

				// eval on every loop
				this.ParserState.isHeaderRecord = this.ParserState.recordIndex == 0 && this.DelimitedTextSpec.FirstRecordIsHeader;

				// peek the next byte
				__value = this.InnerTextReader.Peek();
				ch = (char)__value;
				this.ParserState.peekNextCharacter = ch;

				if (this.ParserStateMachine())
				{
					if ((object)this.ParserState.record != null)
					{
						if (this.ParserState.isHeaderRecord)
						{
							string[] headerNames;
							HeaderSpec headerSpec;

							headerNames = this.ParserState.record.Keys.ToArray();

							// stash parsed header names into header specs member
							if ((object)this.DelimitedTextSpec.HeaderSpecs != null &&
								headerNames.Length == this.DelimitedTextSpec.HeaderSpecs.Count)
							{
								if ((object)headerNames != null)
								{
									for (int headerIndex = 0; headerIndex < headerNames.Length; headerIndex++)
									{
										headerSpec = this.DelimitedTextSpec.HeaderSpecs[headerIndex];

										if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(headerSpec.HeaderName) &&
											headerSpec.HeaderName.ToLower() != headerNames[headerIndex].ToLower())
											throw new InvalidOperationException(string.Format("Header name mismatch: '{0}' <> '{1}'.", headerSpec.HeaderName, headerNames[headerIndex]));

										headerSpec.HeaderName = headerNames[headerIndex];
									}
								}
							}
							else
							{
								// reset header specs because they do not match in length
								this.DelimitedTextSpec.HeaderSpecs.Clear();

								if ((object)headerNames != null)
								{
									foreach (string headerName in headerNames)
									{
										this.DelimitedTextSpec.HeaderSpecs.Add(new HeaderSpec()
																				{
																					HeaderName = headerName,
																					FieldType = FieldType.String
																				});
									}
								}
							}
						}
						else
						{
							// aint this some shhhhhhhh?
							yield return this.ParserState.record;
						}
					}

					this.ParserState.record = new Dictionary<string, object>();

					if (once) // state-based resumption of loop ;)
						break;
				}
			}
		}

		#endregion

		#region Classes/Structs/Interfaces/Enums/Delegates

		private sealed class _ParserState
		{
			#region Fields/Constants

			public long characterIndex;
			public long contentIndex;
			public int fieldIndex;
			public bool isEOF;
			public bool isHeaderRecord;
			public bool isQuotedValue;
			public char peekNextCharacter;
			public char readCurrentCharacter;
			public IDictionary<string, object> record;
			public long recordIndex;
			public StringBuilder transientStringBuilder;
			public long valueIndex;

			#endregion
		}

		#endregion
	}
}