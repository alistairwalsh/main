/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Data.UoW;
using _2ndAsset.ObfuscationEngine.Core.CtrlC_CtrlV.Utilities;
using _2ndAsset.ObfuscationEngine.Core.Support;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	public abstract class AdoNetDestinationAdapter : DestinationAdapter, IAdoNetAdapter
	{
		#region Constructors/Destructors

		protected AdoNetDestinationAdapter()
		{
		}

		#endregion

		#region Fields/Constants

		private IUnitOfWork destinationUnitOfWork;

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

		#endregion

		#region Methods/Operators

		protected override void CoreInitialize(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration"));

			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration == null)
				throw new InvalidOperationException(string.Format("Configuration missing: '{0}'.", "DestinationAdapterConfiguration.AdoNetAdapterConfiguration"));

			this.DestinationUnitOfWork = obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.GetUnitOfWork();
		}

		protected abstract void CorePublishImpl(TableConfiguration configuration, IUnitOfWork destinationUnitOfWork, IDataReader sourceDataReader, out long rowsCopied);

		protected override void CorePushData(TableConfiguration tableConfiguration, IEnumerable<IDictionary<string, object>> sourceDataEnumerable)
		{
			IDataReader sourceDataReader;
			long rowsCopied;
			int recordsAffected;

			if ((object)tableConfiguration == null)
				throw new ArgumentNullException("tableConfiguration");

			if ((object)sourceDataEnumerable == null)
				throw new ArgumentNullException("sourceDataEnumerable");

			if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(tableConfiguration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandText))
			{
				// prepare destination
				var resultsets = this.DestinationUnitOfWork.ExecuteResultsets(tableConfiguration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandType ?? CommandType.Text, tableConfiguration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandText, new IDbDataParameter[] { });
				Console.WriteLine("DESTINATION (pre): recordsAffected={0}", resultsets.First().RecordsAffected);
			}

			sourceDataReader = new EnumerableDictionaryDataReader(this.UpstreamMetadata, sourceDataEnumerable);

			this.CorePublishImpl(tableConfiguration, this.DestinationUnitOfWork, sourceDataReader, out rowsCopied);

			if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(tableConfiguration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandText))
			{
				// prepare destination
				var resultsets = this.DestinationUnitOfWork.ExecuteResultsets(tableConfiguration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandType ?? CommandType.Text, tableConfiguration.Parent.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandText, new IDbDataParameter[] { });
				Console.WriteLine("DESTINATION (post): recordsAffected={0}", resultsets.First().RecordsAffected);
			}
		}

		protected override void CoreTerminate()
		{
			if ((object)this.DestinationUnitOfWork != null)
				this.DestinationUnitOfWork.Dispose();

			this.DestinationUnitOfWork = null;
		}

		#endregion
	}
}