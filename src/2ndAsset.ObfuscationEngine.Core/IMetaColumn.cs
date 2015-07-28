/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public interface IMetaColumn
	{
		#region Properties/Indexers/Events

		int ColumnIndex
		{
			get;
		}

		bool? ColumnIsNullable
		{
			get;
		}

		string ColumnName
		{
			get;
		}

		Type ColumnType
		{
			get;
		}

		int TableIndex
		{
			get;
		}

		object TagContext
		{
			get;
		}

		#endregion
	}
}