/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;
using System.IO;

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