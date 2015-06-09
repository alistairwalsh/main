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

		protected virtual void CoreDispose(bool disposing)
		{
			if (disposing)
				this.Terminate();
		}

		protected abstract void CoreInitialize(ObfuscationConfiguration obfuscationConfiguration);

		protected abstract void CoreTerminate();

		public void Dispose()
		{
			if (this.Disposed)
				return;

			try
			{
				this.CoreDispose(true);
			}
			finally
			{
				this.Disposed = true;
				GC.SuppressFinalize(this);
			}
		}

		public void Initialize(ObfuscationConfiguration obfuscationConfiguration)
		{
			this.CoreInitialize(obfuscationConfiguration);
		}

		public void Terminate()
		{
			this.CoreTerminate();
		}

		#endregion
	}
}