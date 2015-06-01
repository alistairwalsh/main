/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter
{
	public abstract class Adapter : IAdapter
	{
		#region Constructors/Destructors

		protected Adapter()
		{
		}

		#endregion

		#region Fields/Constants

		private bool disposed;

		#endregion

		#region Properties/Indexers/Events

		public bool Disposed
		{
			get
			{
				return this.disposed;
			}
			private set
			{
				this.disposed = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
				this.Terminate();
		}

		public void Dispose()
		{
			if (this.Disposed)
				return;

			try
			{
				this.Dispose(true);
			}
			finally
			{
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		public virtual void Initialize(ObfuscationConfiguration configuration)
		{
			// do nothing
		}

		public virtual void Terminate()
		{
			// do nothing
		}

		#endregion
	}
}