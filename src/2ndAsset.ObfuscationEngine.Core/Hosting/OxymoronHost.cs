/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.ObfuscationEngine.Core.Hosting
{
	public abstract class OxymoronHost : IOxymoronHost
	{
		#region Constructors/Destructors

		protected OxymoronHost()
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
			{
				// do nothing
			}
		}

		protected abstract object CoreGetValueForIdViaDictionaryResolution(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId);

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

		public object GetValueForIdViaDictionaryResolution(DictionaryConfiguration dictionaryConfiguration, IMetaColumn metaColumn, object surrogateId)
		{
			return this.CoreGetValueForIdViaDictionaryResolution(dictionaryConfiguration, metaColumn, surrogateId);
		}

		#endregion
	}
}