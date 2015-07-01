/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Data
{
	public class WrappedDataReader : IDataReader
	{
		#region Constructors/Destructors

		public WrappedDataReader(IDataReader innerDataReader)
		{
			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::.ctor(...)", typeof(WrappedDataReader).Name));

			if ((object)innerDataReader == null)
				throw new ArgumentNullException("innerDataReader");

			this.innerDataReader = innerDataReader;
		}

		#endregion

		#region Fields/Constants

		private readonly IDataReader innerDataReader;

		#endregion

		#region Properties/Indexers/Events

		public virtual object this[string name]
		{
			get
			{
				return this.InnerDataReader[name];
			}
		}

		public virtual object this[int i]
		{
			get
			{
				return this.InnerDataReader[i];
			}
		}

		public virtual int Depth
		{
			get
			{
				return this.InnerDataReader.Depth;
			}
		}

		public virtual int FieldCount
		{
			get
			{
				return this.InnerDataReader.FieldCount;
			}
		}

		protected IDataReader InnerDataReader
		{
			get
			{
				return this.innerDataReader;
			}
		}

		public virtual bool IsClosed
		{
			get
			{
				return this.InnerDataReader.IsClosed;
			}
		}

		public virtual int RecordsAffected
		{
			get
			{
				return this.InnerDataReader.RecordsAffected;
			}
		}

		#endregion

		#region Methods/Operators

		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize((object)this);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize((object)this);
		}

		protected virtual void Dispose(bool disposing)
		{
			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::Dispose(...): enter", typeof(WrappedDataReader).Name));

			if (disposing)
				this.InnerDataReader.Dispose();

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::Dispose(...): leave", typeof(WrappedDataReader).Name));
		}

		public virtual bool GetBoolean(int i)
		{
			return this.InnerDataReader.GetBoolean(i);
		}

		public virtual byte GetByte(int i)
		{
			return this.InnerDataReader.GetByte(i);
		}

		public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return this.InnerDataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
		}

		public virtual char GetChar(int i)
		{
			return this.InnerDataReader.GetChar(i);
		}

		public virtual long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
		{
			return this.InnerDataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
		}

		public virtual IDataReader GetData(int i)
		{
			return this.InnerDataReader.GetData(i);
		}

		public virtual string GetDataTypeName(int i)
		{
			return this.InnerDataReader.GetDataTypeName(i);
		}

		public virtual DateTime GetDateTime(int i)
		{
			return this.InnerDataReader.GetDateTime(i);
		}

		public virtual decimal GetDecimal(int i)
		{
			return this.InnerDataReader.GetDecimal(i);
		}

		public double GetDouble(int i)
		{
			return this.InnerDataReader.GetDouble(i);
		}

		public virtual Type GetFieldType(int i)
		{
			Type retval;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetFieldType(...): enter", typeof(WrappedDataReader).Name));

			retval = this.InnerDataReader.GetFieldType(i);

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetFieldType(...): return name", typeof(WrappedDataReader).Name));

			return retval;
		}

		public virtual float GetFloat(int i)
		{
			return this.InnerDataReader.GetFloat(i);
		}

		public virtual Guid GetGuid(int i)
		{
			return this.InnerDataReader.GetGuid(i);
		}

		public virtual short GetInt16(int i)
		{
			return this.InnerDataReader.GetInt16(i);
		}

		public virtual int GetInt32(int i)
		{
			return this.InnerDataReader.GetInt32(i);
		}

		public virtual long GetInt64(int i)
		{
			return this.InnerDataReader.GetInt64(i);
		}

		public virtual string GetName(int i)
		{
			string retval;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetName(...): enter", typeof(WrappedDataReader).Name));

			retval = this.InnerDataReader.GetName(i);

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetName(...): return name", typeof(WrappedDataReader).Name));

			return retval;
		}

		public virtual int GetOrdinal(string name)
		{
			int retval;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetOrdinal(...): enter", typeof(WrappedDataReader).Name));

			retval = this.InnerDataReader.GetOrdinal(name);

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetOrdinal(...): return value", typeof(WrappedDataReader).Name));

			return retval;
		}

		public virtual DataTable GetSchemaTable()
		{
			return this.InnerDataReader.GetSchemaTable();
		}

		public virtual string GetString(int i)
		{
			return this.InnerDataReader.GetString(i);
		}

		public virtual object GetValue(int i)
		{
			object retval;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetValue(...): enter", typeof(WrappedDataReader).Name));

			retval = this.InnerDataReader.GetValue(i);

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::GetValue(...): return value", typeof(WrappedDataReader).Name));

			return retval;
		}

		public virtual int GetValues(object[] values)
		{
			return this.InnerDataReader.GetValues(values);
		}

		public virtual bool IsDBNull(int i)
		{
			return this.InnerDataReader.IsDBNull(i);
		}

		public virtual bool NextResult()
		{
			bool retval;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::NextResult(...): enter", typeof(WrappedDataReader).Name));

			retval = this.InnerDataReader.NextResult();

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::NextResult(...): return flag", typeof(WrappedDataReader).Name));

			return retval;
		}

		public virtual bool Read()
		{
			bool retval;

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::Read(...): enter", typeof(WrappedDataReader).Name));

			retval = this.InnerDataReader.Read();

			OnlyWhen._PROFILE_ThenPrint(string.Format("{0}::Read(...): return flag", typeof(WrappedDataReader).Name));

			return retval;
		}

		#endregion
	}
}