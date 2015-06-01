/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

// TODO: optimize this class

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

			this.Initialize();

			if ((object)upstreamMetadata != null)
			{
				this.ordinalLookup = upstreamMetadata.Select((mc, i) => new
																		{
																			Index = i,
																			Name = mc.ColumnName
																		}).ToDictionary(
																			p => p.Name,
																			p => p.Index,
																			StringComparer.InvariantCultureIgnoreCase);
			}
			else if (!this.IsEnumerableClosed)
			{
				this.ordinalLookup = this.TargetEnumerator.Current.Keys.Select((k, i) => new
																						{
																							Index = i,
																							Name = k
																						}).ToDictionary(
																							p => p.Name,
																							p => p.Index,
																							StringComparer.InvariantCultureIgnoreCase);
			}
			else
				this.ordinalLookup = new Dictionary<string, int>();
		}

		#endregion

		#region Fields/Constants

		private readonly Dictionary<string, int> ordinalLookup;
		private readonly IEnumerator<IDictionary<string, object>> targetEnumerator;
		private bool isEnumerableClosed;

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
				return this.HasRecord ? this.TargetEnumerator.Current.Values.ToArray()[i] : null;
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
				return this.IsEnumerableClosed;
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

		protected bool IsEnumerableClosed
		{
			get
			{
				return this.isEnumerableClosed;
			}
			set
			{
				if (this.isEnumerableClosed && !value)
					throw new InvalidOperationException(string.Format("Set IsEnumerableClosed already."));

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
			return this.HasRecord ? (Boolean)this.TargetEnumerator.Current.Values.ToArray()[i] : default(Boolean);
		}

		public virtual byte GetByte(int i)
		{
			return this.HasRecord ? (Byte)this.TargetEnumerator.Current.Values.ToArray()[i] : default(Byte);
		}

		public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return 0;
		}

		public virtual char GetChar(int i)
		{
			return this.HasRecord ? (Char)this.TargetEnumerator.Current.Values.ToArray()[i] : default(Char);
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
			return this.HasRecord ? (DateTime)this.TargetEnumerator.Current.Values.ToArray()[i] : default(DateTime);
		}

		public virtual decimal GetDecimal(int i)
		{
			return this.HasRecord ? (Decimal)this.TargetEnumerator.Current.Values.ToArray()[i] : default(Decimal);
		}

		public double GetDouble(int i)
		{
			return this.HasRecord ? (Double)this.TargetEnumerator.Current.Values.ToArray()[i] : default(Double);
		}

		public virtual Type GetFieldType(int i)
		{
			return this.HasRecord && (object)this.TargetEnumerator.Current.Values.ToArray()[i] != null ? this.TargetEnumerator.Current.Values.ToArray()[i].GetType() : null;
		}

		public virtual float GetFloat(int i)
		{
			return this.HasRecord ? (Single)this.TargetEnumerator.Current.Values.ToArray()[i] : default(Single);
		}

		public virtual Guid GetGuid(int i)
		{
			return this.HasRecord ? (Guid)this.TargetEnumerator.Current.Values.ToArray()[i] : default(Guid);
		}

		public virtual short GetInt16(int i)
		{
			return this.HasRecord ? (Int16)this.TargetEnumerator.Current.Values.ToArray()[i] : default(Int16);
		}

		public virtual int GetInt32(int i)
		{
			return this.HasRecord ? (Int32)this.TargetEnumerator.Current.Values.ToArray()[i] : default(Int32);
		}

		public virtual long GetInt64(int i)
		{
			return this.HasRecord ? (Int64)this.TargetEnumerator.Current.Values.ToArray()[i] : default(Int64);
		}

		public virtual string GetName(int i)
		{
			return this.HasRecord ? this.TargetEnumerator.Current.Keys.ToArray()[i] : null;
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
			return this.HasRecord ? (String)this.TargetEnumerator.Current.Values.ToArray()[i] : default(String);
		}

		public virtual object GetValue(int i)
		{
			return this.HasRecord ? (Object)this.TargetEnumerator.Current.Values.ToArray()[i] : default(Object);
		}

		public virtual int GetValues(object[] values)
		{
			return 0;
		}

		private void Initialize()
		{
			this.IsEnumerableClosed = !this.TargetEnumerator.MoveNext();
		}

		public virtual bool IsDBNull(int i)
		{
			return this.HasRecord ? (object)this.TargetEnumerator.Current.Values.ToArray()[i] == null : true;
		}

		public virtual bool NextResult()
		{
			return false;
		}

		public virtual bool Read()
		{
			return !(this.IsEnumerableClosed = !this.TargetEnumerator.MoveNext());
		}

		#endregion
	}
}