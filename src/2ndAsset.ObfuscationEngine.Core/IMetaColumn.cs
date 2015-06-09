/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public interface IMetaColumn
	{
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
	}
}