/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.ObfuscationEngine.Core
{
	public sealed class MetaColumn
	{
		#region Constructors/Destructors

		public MetaColumn()
		{
		}

		#endregion

		#region Fields/Constants

		private string columnName;
		private Type columnType;
		private bool isNullable;
		private int resultsetIndex;
		private object tag;

		#endregion

		#region Properties/Indexers/Events

		public string ColumnName
		{
			get
			{
				return this.columnName;
			}
			set
			{
				this.columnName = value;
			}
		}

		public Type ColumnType
		{
			get
			{
				return this.columnType;
			}
			set
			{
				this.columnType = value;
			}
		}

		public bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
			set
			{
				this.isNullable = value;
			}
		}

		public int ResultsetIndex
		{
			get
			{
				return this.resultsetIndex;
			}
			set
			{
				this.resultsetIndex = value;
			}
		}

		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		#endregion
	}
}