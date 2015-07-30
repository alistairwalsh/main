/*
	Copyright ©2002-2015 Daniel Bullington (dpbullington@gmail.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;
using _2ndAsset.ObfuscationEngine.Core.Support;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

namespace _2ndAsset.ObfuscationEngine.Core.Adapter.Destination
{
	[AdapterSpecificConfiguration(SpecificConfigurationAqtn = "", UserControlAqtn = "_2ndAsset.ObfuscationEngine.UI.Controls.Adapters.AdoNetAdapterSettingsUserControl, 2ndAsset.ObfuscationEngine.UI")]
	public abstract class AdoNetDestinationAdapter : DestinationAdapter<AdoNetAdapterConfiguration>, IAdoNetAdapter
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

		protected override void CoreInitialize()
		{
			IEnumerable<IResultset> resultsets;

			this.DestinationUnitOfWork = this.AdapterConfiguration.AdapterSpecificConfiguration.GetUnitOfWork();

			if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.PreExecuteCommandText))
			{
				resultsets = this.DestinationUnitOfWork.ExecuteResultsets(this.AdapterConfiguration.AdapterSpecificConfiguration.PreExecuteCommandType ?? CommandType.Text, this.AdapterConfiguration.AdapterSpecificConfiguration.PreExecuteCommandText, new IDbDataParameter[] { });

				if ((object)resultsets == null)
					throw new InvalidOperationException(string.Format("Resultsets were invalid."));

				resultsets.ToArray();
			}
		}

		protected abstract void CorePublishImpl(TableConfiguration configuration, IUnitOfWork destinationUnitOfWork, IDataReader sourceDataReader, out long rowsCopied);

		protected override void CorePushData(TableConfiguration tableConfiguration, IEnumerable<IDictionary<string, object>> sourceDataEnumerable)
		{
			IDataReader sourceDataReader;
			long rowsCopied;

			if ((object)tableConfiguration == null)
				throw new ArgumentNullException("tableConfiguration");

			if ((object)sourceDataEnumerable == null)
				throw new ArgumentNullException("sourceDataEnumerable");

			sourceDataReader = new EnumerableDictionaryDataReader(this.UpstreamMetadata, sourceDataEnumerable);

			this.CorePublishImpl(tableConfiguration, this.DestinationUnitOfWork, sourceDataReader, out rowsCopied);
		}

		protected override void CoreTerminate()
		{
			IEnumerable<IResultset> resultsets;

			if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(this.AdapterConfiguration.AdapterSpecificConfiguration.PostExecuteCommandText))
			{
				resultsets = this.DestinationUnitOfWork.ExecuteResultsets(this.AdapterConfiguration.AdapterSpecificConfiguration.PostExecuteCommandType ?? CommandType.Text, this.AdapterConfiguration.AdapterSpecificConfiguration.PostExecuteCommandText, new IDbDataParameter[] { });

				if ((object)resultsets == null)
					throw new InvalidOperationException(string.Format("Resultsets were invalid."));

				resultsets.ToArray();
			}

			if ((object)this.DestinationUnitOfWork != null)
				this.DestinationUnitOfWork.Dispose();

			this.DestinationUnitOfWork = null;
		}

		#endregion
	}
}