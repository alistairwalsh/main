/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Collections.Generic;
using System.IO;

using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;

namespace _2ndAsset.ObfuscationEngine.Core.Support
{
	public abstract class RecordTextReader : WrappedTextReader
	{
		#region Constructors/Destructors

		protected RecordTextReader(TextReader innerTextReader)
			: base(innerTextReader)
		{
		}

		#endregion

		#region Methods/Operators

		public abstract IEnumerable<HeaderSpec> ReadHeaderSpecs();

		public abstract IEnumerable<IDictionary<string, object>> ReadRecords();

		#endregion
	}
}