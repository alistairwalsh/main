/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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