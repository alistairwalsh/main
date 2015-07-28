/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;

namespace _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText
{
	internal class DelimitedTextCommand : IDbCommand
	{
		#region Constructors/Destructors

		public DelimitedTextCommand()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public IDataParameterCollection Parameters
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string CommandText
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public int CommandTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public CommandType CommandType
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public IDbConnection Connection
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public IDbTransaction Transaction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public UpdateRowSource UpdatedRowSource
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion

		#region Methods/Operators

		public void Cancel()
		{
			throw new NotImplementedException();
		}

		public IDbDataParameter CreateParameter()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public int ExecuteNonQuery()
		{
			throw new NotImplementedException();
		}

		public IDataReader ExecuteReader(CommandBehavior behavior)
		{
			throw new NotImplementedException();
		}

		public IDataReader ExecuteReader()
		{
			throw new NotImplementedException();
		}

		public object ExecuteScalar()
		{
			throw new NotImplementedException();
		}

		public void Prepare()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}