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

		private int columnIndex;
		private bool columnIsNullable;
		private string columnName;
		private Type columnType;
		private int metaTableIndex;
		private object tagContext;

		#endregion

		#region Properties/Indexers/Events

		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
			set
			{
				this.columnIndex = value;
			}
		}

		public bool ColumnIsNullable
		{
			get
			{
				return this.columnIsNullable;
			}
			set
			{
				this.columnIsNullable = value;
			}
		}

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

		public int MetaTableIndex
		{
			get
			{
				return this.metaTableIndex;
			}
			set
			{
				this.metaTableIndex = value;
			}
		}

		public object TagContext
		{
			get
			{
				return this.tagContext;
			}
			set
			{
				this.tagContext = value;
			}
		}

		#endregion
	}
}