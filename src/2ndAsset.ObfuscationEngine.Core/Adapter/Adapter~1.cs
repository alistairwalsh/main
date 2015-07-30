/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;

using Solder.Framework;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter
{
	public abstract class Adapter<TAdapterSpecificConfiguration> : IAdapter
		where TAdapterSpecificConfiguration : AdapterSpecificConfiguration, new()
	{
		#region Constructors/Destructors

		protected Adapter()
		{
		}

		#endregion

		#region Fields/Constants

		private AdapterConfiguration<TAdapterSpecificConfiguration> adapterConfiguration;
		private bool disposed;

		#endregion

		#region Properties/Indexers/Events

		protected AdapterConfiguration<TAdapterSpecificConfiguration> AdapterConfiguration
		{
			get
			{
				return this.adapterConfiguration;
			}
			private set
			{
				this.adapterConfiguration = value;
			}
		}

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

		protected abstract void CoreInitialize();

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

		public Type GetAdapterSpecificConfigurationType()
		{
			return typeof(TAdapterSpecificConfiguration);
		}

		public void Initialize(AdapterConfiguration adapterConfiguration)
		{
			AdapterConfiguration<TAdapterSpecificConfiguration> _adapterConfiguration;

			if ((object)adapterConfiguration == null)
				throw new ArgumentNullException("adapterConfiguration");

			if ((object)adapterConfiguration.AdapterSpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "AdapterSpecificConfiguration"));

			_adapterConfiguration = new AdapterConfiguration<TAdapterSpecificConfiguration>(adapterConfiguration);

			if ((object)_adapterConfiguration.AdapterSpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "AdapterSpecificConfiguration"));

			this.AdapterConfiguration = _adapterConfiguration;
			this.CoreInitialize();
		}

		public void Terminate()
		{
			this.CoreTerminate();
			this.AdapterConfiguration = null;
		}

		public IEnumerable<Message> ValidateAdapterSpecificConfiguration(AdapterConfiguration adapterConfiguration, string adapterContext)
		{
			AdapterConfiguration<TAdapterSpecificConfiguration> _adapterConfiguration;

			if ((object)adapterConfiguration == null)
				throw new ArgumentNullException("adapterConfiguration");

			if ((object)adapterConfiguration.AdapterSpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "AdapterSpecificConfiguration"));

			_adapterConfiguration = new AdapterConfiguration<TAdapterSpecificConfiguration>(adapterConfiguration);

			if ((object)_adapterConfiguration.AdapterSpecificConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "AdapterSpecificConfiguration"));

			return _adapterConfiguration.AdapterSpecificConfiguration.Validate(adapterContext);
		}

		#endregion
	}
}