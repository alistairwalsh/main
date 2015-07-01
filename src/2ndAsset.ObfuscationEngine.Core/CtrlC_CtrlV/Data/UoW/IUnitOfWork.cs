/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Data;

namespace _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Data.UoW
{
	public interface IUnitOfWork : IDisposable
	{
		#region Properties/Indexers/Events

		/// <summary>
		/// Gets a value indicating whether the current instance has been completed.
		/// </summary>
		bool Completed
		{
			get;
		}

		/// <summary>
		/// Gets the underlying ADO.NET connection.
		/// </summary>
		IDbConnection Connection
		{
			get;
		}

		/// <summary>
		/// Gets a value indicating whether the current instance has been disposed.
		/// </summary>
		bool Disposed
		{
			get;
		}

		/// <summary>
		/// Gets a value indicating whether the current instance has been diverged.
		/// </summary>
		bool Diverged
		{
			get;
		}

		/// <summary>
		/// Gets the underlying ADO.NET transaction.
		/// </summary>
		IDbTransaction Transaction
		{
			get;
		}

		/// <summary>
		/// Gets the context object.
		/// </summary>
		IDisposable Context
		{
			get;
			set;
		}

		#endregion

		#region Methods/Operators

		/// <summary>
		/// Indicates that all operations within the unit of work have completed successfully. This method should only be called once.
		/// </summary>
		void Complete();

		/// <summary>
		/// Indicates that at least one operation within the unit of work cause a failure in data concurrency or nullipotency. This forces the entire unit of work to yield an incomplete status. This method can be called any number of times.
		/// </summary>
		void Divergent();

		#endregion
	}
}