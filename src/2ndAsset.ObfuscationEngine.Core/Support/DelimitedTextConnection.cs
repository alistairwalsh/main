/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Data;

namespace _2ndAsset.ObfuscationEngine.Core.Support
{
	public class DelimitedTextConnection : IDbConnection
	{
		#region Constructors/Destructors

		public DelimitedTextConnection()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public int ConnectionTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string Database
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public ConnectionState State
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string ConnectionString
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

		public IDbTransaction BeginTransaction(IsolationLevel il)
		{
			throw new NotImplementedException();
		}

		public IDbTransaction BeginTransaction()
		{
			throw new NotImplementedException();
		}

		public void ChangeDatabase(string databaseName)
		{
			throw new NotImplementedException();
		}

		public void Close()
		{
			throw new NotImplementedException();
		}

		public IDbCommand CreateCommand()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public void Open()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}