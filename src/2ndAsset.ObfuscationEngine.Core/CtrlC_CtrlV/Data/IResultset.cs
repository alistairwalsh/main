/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Collections.Generic;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Data
{
	public interface IResultset
	{
		#region Properties/Indexers/Events

		int Index
		{
			get;
		}

		IEnumerable<IRecord> Records
		{
			get;
		}

		int? RecordsAffected
		{
			get;
		}

		#endregion
	}
}