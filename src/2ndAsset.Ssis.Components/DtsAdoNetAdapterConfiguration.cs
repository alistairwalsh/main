/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Data;

using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

namespace _2ndAsset.Ssis.Components
{
	internal class DtsAdoNetAdapterConfiguration : AdoNetAdapterConfiguration
	{
		#region Constructors/Destructors

		public DtsAdoNetAdapterConfiguration()
		{
		}

		#endregion

		#region Fields/Constants

		private Func<IUnitOfWork> dictionaryUnitOfWorkCallback;

		#endregion

		#region Properties/Indexers/Events

		public Func<IUnitOfWork> DictionaryUnitOfWorkCallback
		{
			get
			{
				return this.dictionaryUnitOfWorkCallback;
			}
			set
			{
				this.dictionaryUnitOfWorkCallback = value;
			}
		}

		#endregion

		#region Methods/Operators

		public override IUnitOfWork GetUnitOfWork(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
		{
			if ((object)this.DictionaryUnitOfWorkCallback != null)
				return this.DictionaryUnitOfWorkCallback();
			else
				return null;
		}

		#endregion
	}
}