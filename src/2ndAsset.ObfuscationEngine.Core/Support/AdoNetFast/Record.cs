/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com) (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

namespace _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast
{
	public sealed class Record : Dictionary<string, object>, IRecord
	{
		#region Constructors/Destructors

		public Record()
			: base(StringComparer.OrdinalIgnoreCase)
		{
		}

		#endregion
	}
}