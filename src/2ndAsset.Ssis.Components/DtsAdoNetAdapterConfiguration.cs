/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Data;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

namespace _2ndAsset.Ssis.Components
{
	internal class DtsAdoNetAdapterConfiguration : AdoNetAdapterConfiguration
	{
		#region Constructors/Destructors

		public DtsAdoNetAdapterConfiguration(Func<IUnitOfWork> dictionaryUnitOfWorkCallback, AdoNetAdapterConfiguration adoNetAdapterConfiguration)
		{
			if ((object)dictionaryUnitOfWorkCallback == null)
				throw new ArgumentNullException("dictionaryUnitOfWorkCallback");

			this.dictionaryUnitOfWorkCallback = dictionaryUnitOfWorkCallback;

			this.ConnectionAqtn = string.Format("{0}\r\n{1}", typeof(IDbConnection).AssemblyQualifiedName, Guid.NewGuid().ToString("N"));
			this.ConnectionString = Guid.NewGuid().ToString("N");

			if ((object)adoNetAdapterConfiguration != null)
			{
				this.PreExecuteCommandText = adoNetAdapterConfiguration.PreExecuteCommandText;
				this.PreExecuteCommandType = adoNetAdapterConfiguration.PreExecuteCommandType;
				this.ExecuteCommandText = adoNetAdapterConfiguration.ExecuteCommandText;
				this.ExecuteCommandType = adoNetAdapterConfiguration.ExecuteCommandType;
				this.PostExecuteCommandText = adoNetAdapterConfiguration.PostExecuteCommandText;
				this.PostExecuteCommandType = adoNetAdapterConfiguration.PostExecuteCommandType;
			}
		}

		#endregion

		#region Fields/Constants

		private readonly Func<IUnitOfWork> dictionaryUnitOfWorkCallback;

		#endregion

		#region Properties/Indexers/Events

		private Func<IUnitOfWork> DictionaryUnitOfWorkCallback
		{
			get
			{
				return this.dictionaryUnitOfWorkCallback;
			}
		}

		#endregion

		#region Methods/Operators

		public override IUnitOfWork GetUnitOfWork(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
		{
			return this.DictionaryUnitOfWorkCallback();
		}

		#endregion
	}
}