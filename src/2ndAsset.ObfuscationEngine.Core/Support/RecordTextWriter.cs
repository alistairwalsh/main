/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Collections.Generic;
using System.IO;

namespace _2ndAsset.ObfuscationEngine.Core.Support
{
	public abstract class RecordTextWriter : WrappedTextWriter
	{
		#region Constructors/Destructors

		protected RecordTextWriter(TextWriter innerTextWriter)
			: base(innerTextWriter)
		{
		}

		#endregion

		#region Methods/Operators

		public abstract void WriteRecords(IEnumerable<IDictionary<string, object>> records);

		#endregion
	}
}