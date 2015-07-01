/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Diagnostics;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public static class OnlyWhen
	{
		#region Methods/Operators

		[Conditional("PROFILE")]
		public static void _PROFILE_ThenPrint(string message)
		{
			/* THIS METHOD SHOULD NOT BE DEFINED IN RELEASE/PRODUCTION BUILDS */
			Debug.WriteLine(message);
		}

		#endregion
	}
}