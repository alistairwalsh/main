/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using TextMetal.Middleware.Common.Utilities;
using TextMetal.Middleware.Data.UoW;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter
{
	public abstract class AdoNetDestinationAdapter : Adapter, IDestinationAdapter, IAdoNetAdapter
	{
		#region Constructors/Destructors

		protected AdoNetDestinationAdapter()
		{
		}

		#endregion

		#region Fields/Constants

		private IUnitOfWork destinationUnitOfWork;
		private IEnumerable<MetaColumn> upstreamMetadata;

		#endregion

		#region Properties/Indexers/Events

		private IUnitOfWork DestinationUnitOfWork
		{
			get
			{
				return this.destinationUnitOfWork;
			}
			set
			{
				this.destinationUnitOfWork = value;
			}
		}

		public IEnumerable<MetaColumn> UpstreamMetadata
		{
			set
			{
				this.upstreamMetadata = value;
			}
			protected get
			{
				return this.upstreamMetadata;
			}
		}

		#endregion

		#region Methods/Operators

		public override void Initialize(ObfuscationConfiguration configuration)
		{
			base.Initialize(configuration);

			if ((object)configuration.DestinationAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration"));

			if ((object)configuration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration.AdoNetAdapterConfiguration"));

			this.DestinationUnitOfWork = configuration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.GetUnitOfWork();
		}

		protected abstract void OnPublishImpl(TableConfiguration configuration, IUnitOfWork destinationUnitOfWork, IDataReader sourceDataReader, out long rowsCopied);

		public void PushData(TableConfiguration configuration, IEnumerable<IDictionary<string, object>> sourceDataEnumerable)
		{
			IDataReader sourceDataReader;
			long rowsCopied;
			int recordsAffected;

			if ((object)configuration == null)
				throw new ArgumentNullException("configuration");

			if ((object)sourceDataEnumerable == null)
				throw new ArgumentNullException("sourceDataEnumerable");

			if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(configuration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandText))
			{
				// prepare destination
				var resultsets = this.DestinationUnitOfWork.ExecuteResultsets(configuration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandType ?? CommandType.Text, configuration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandText, new IDbDataParameter[] { });
				Console.WriteLine("DESTINATION (pre): recordsAffected={0}", resultsets.First().RecordsAffected);
			}

			sourceDataReader = new EnumerableDictionaryDataReader(this.UpstreamMetadata, sourceDataEnumerable);

			this.OnPublishImpl(configuration, this.DestinationUnitOfWork, sourceDataReader, out rowsCopied);

			if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(configuration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandText))
			{
				// prepare destination
				var resultsets = this.DestinationUnitOfWork.ExecuteResultsets(configuration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandType ?? CommandType.Text, configuration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandText, new IDbDataParameter[] { });
				Console.WriteLine("DESTINATION (post): recordsAffected={0}", resultsets.First().RecordsAffected);
			}
		}

		public override void Terminate()
		{
			if ((object)this.DestinationUnitOfWork != null)
				this.DestinationUnitOfWork.Dispose();

			this.DestinationUnitOfWork = null;

			base.Terminate();
		}

		#endregion
	}
}