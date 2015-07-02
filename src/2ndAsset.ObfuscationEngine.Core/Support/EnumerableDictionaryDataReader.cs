/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace _2ndAsset.ObfuscationEngine.Core.Support
{
	public class EnumerableDictionaryDataReader : IDataReader
	{
		#region Constructors/Destructors

		public EnumerableDictionaryDataReader(IEnumerable<MetaColumn> upstreamMetadata, IEnumerable<IDictionary<string, object>> targetEnumerable)
		{
			if ((object)upstreamMetadata == null)
				throw new ArgumentNullException("upstreamMetadata");

			if ((object)targetEnumerable == null)
				throw new ArgumentNullException("targetEnumerable");

			this.targetEnumerator = targetEnumerable.GetEnumerator();

			if ((object)this.targetEnumerator == null)
				throw new InvalidOperationException("targetEnumerable");

			this.ordinalLookup = upstreamMetadata.Select((mc, i) => new
																	{
																		Index = i,
																		Name = mc.ColumnName
																	}).ToDictionary(
																		p => p.Name,
																		p => p.Index,
																		StringComparer.InvariantCultureIgnoreCase);
		}

		#endregion

		#region Fields/Constants

		private readonly Dictionary<string, int> ordinalLookup;
		private readonly IEnumerator<IDictionary<string, object>> targetEnumerator;
		private string[] currentKeys;
		private object[] currentValues;
		private bool? isEnumerableClosed;

		#endregion

		#region Properties/Indexers/Events

		public virtual object this[string name]
		{
			get
			{
				return this.HasRecord ? this.TargetEnumerator.Current[name] : null;
			}
		}

		public virtual object this[int i]
		{
			get
			{
				return this.HasRecord ? this.CurrentValues[i] : null;
			}
		}

		public virtual int Depth
		{
			get
			{
				return 1;
			}
		}

		public virtual int FieldCount
		{
			get
			{
				return this.HasRecord ? this.TargetEnumerator.Current.Keys.Count : -1;
			}
		}

		private bool HasRecord
		{
			get
			{
				return (object)this.TargetEnumerator.Current != null;
			}
		}

		public virtual bool IsClosed
		{
			get
			{
				return this.IsEnumerableClosed ?? false;
			}
		}

		private Dictionary<string, int> OrdinalLookup
		{
			get
			{
				return this.ordinalLookup;
			}
		}

		public virtual int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		protected IEnumerator<IDictionary<string, object>> TargetEnumerator
		{
			get
			{
				return this.targetEnumerator;
			}
		}

		private string[] CurrentKeys
		{
			get
			{
				return this.currentKeys;
			}
			set
			{
				this.currentKeys = value;
			}
		}

		private object[] CurrentValues
		{
			get
			{
				return this.currentValues;
			}
			set
			{
				this.currentValues = value;
			}
		}

		protected bool? IsEnumerableClosed
		{
			get
			{
				return this.isEnumerableClosed;
			}
			set
			{
				this.isEnumerableClosed = value;
			}
		}

		#endregion

		#region Methods/Operators

		public virtual void Close()
		{
			this.TargetEnumerator.Dispose();
		}

		public virtual void Dispose()
		{
			this.TargetEnumerator.Dispose();
		}

		public virtual bool GetBoolean(int i)
		{
			return this.HasRecord ? (Boolean)this.CurrentValues[i] : default(Boolean);
		}

		public virtual byte GetByte(int i)
		{
			return this.HasRecord ? (Byte)this.CurrentValues[i] : default(Byte);
		}

		public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return 0;
		}

		public virtual char GetChar(int i)
		{
			return this.HasRecord ? (Char)this.CurrentValues[i] : default(Char);
		}

		public virtual long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			return 0;
		}

		public virtual IDataReader GetData(int i)
		{
			return null;
		}

		public virtual string GetDataTypeName(int i)
		{
			return null;
		}

		public virtual DateTime GetDateTime(int i)
		{
			return this.HasRecord ? (DateTime)this.CurrentValues[i] : default(DateTime);
		}

		public virtual decimal GetDecimal(int i)
		{
			return this.HasRecord ? (Decimal)this.CurrentValues[i] : default(Decimal);
		}

		public double GetDouble(int i)
		{
			return this.HasRecord ? (Double)this.CurrentValues[i] : default(Double);
		}

		public virtual Type GetFieldType(int i)
		{
			return this.HasRecord && (object)this.CurrentValues[i] != null ? this.CurrentValues[i].GetType() : null;
		}

		public virtual float GetFloat(int i)
		{
			return this.HasRecord ? (Single)this.CurrentValues[i] : default(Single);
		}

		public virtual Guid GetGuid(int i)
		{
			return this.HasRecord ? (Guid)this.CurrentValues[i] : default(Guid);
		}

		public virtual short GetInt16(int i)
		{
			return this.HasRecord ? (Int16)this.CurrentValues[i] : default(Int16);
		}

		public virtual int GetInt32(int i)
		{
			return this.HasRecord ? (Int32)this.CurrentValues[i] : default(Int32);
		}

		public virtual long GetInt64(int i)
		{
			return this.HasRecord ? (Int64)this.CurrentValues[i] : default(Int64);
		}

		public virtual string GetName(int i)
		{
			return this.HasRecord ? this.CurrentKeys[i] : null;
		}

		public virtual int GetOrdinal(string name)
		{
			int value;

			if (this.OrdinalLookup.TryGetValue(name, out value))
				return value;

			return -1;
		}

		public virtual DataTable GetSchemaTable()
		{
			return null;
		}

		public virtual string GetString(int i)
		{
			return this.HasRecord ? (String)this.CurrentValues[i] : default(String);
		}

		public virtual object GetValue(int i)
		{
			return this.HasRecord ? (Object)this.CurrentValues[i] : default(Object);
		}

		public virtual int GetValues(object[] values)
		{
			return 0;
		}

		public virtual bool IsDBNull(int i)
		{
			return this.HasRecord ? (object)this.CurrentValues[i] == null : true;
		}

		public virtual bool NextResult()
		{
			return false;
		}

		public virtual bool Read()
		{
			if (!(this.IsEnumerableClosed ?? false))
			{
				var retval = !(bool)(this.IsEnumerableClosed = !this.TargetEnumerator.MoveNext());

				if (retval && this.HasRecord)
				{
					this.CurrentKeys = this.TargetEnumerator.Current.Keys.ToArray();
					this.CurrentValues = this.TargetEnumerator.Current.Values.ToArray();
				}

				return retval;
			}
			else
				return true;
		}

		#endregion
	}
}