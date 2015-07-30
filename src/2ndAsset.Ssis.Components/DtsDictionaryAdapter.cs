/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary;

namespace _2ndAsset.Ssis.Components
{
	internal sealed class DtsDictionaryAdapter : AdoNetDictionaryAdapter<DtsAdoNetAdapterConfiguration>
	{
		#region Constructors/Destructors

		public DtsDictionaryAdapter()
		{
		}

		#endregion
	}
}