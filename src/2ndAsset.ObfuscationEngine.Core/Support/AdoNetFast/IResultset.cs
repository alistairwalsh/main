﻿/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com) (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Collections.Generic;

namespace _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast
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