/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

using TextMetal.Middleware.Common.Utilities;
using TextMetal.Middleware.Data.UoW;
using TextMetal.Middleware.Solder.Serialization;

using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.Ssis.Components
{
	public sealed class __ComponentMetadataWrapper : CustomPropertyWrapper
	{
		#region Constructors/Destructors

		public __ComponentMetadataWrapper(IDTSComponentMetaData100 dtsComponentMetaData100, Func<IUnitOfWork> dictionaryUnitOfWorkCallback)
			: base((object)dtsComponentMetaData100 == null ? null : dtsComponentMetaData100.CustomPropertyCollection)
		{
			if ((object)dtsComponentMetaData100 == null)
				throw new ArgumentNullException("dtsComponentMetaData100");

			if ((object)dictionaryUnitOfWorkCallback == null)
				throw new ArgumentNullException("dictionaryUnitOfWorkCallback");

			this.dtsComponentMetaData100 = dtsComponentMetaData100;
			this.dictionaryUnitOfWorkCallback = dictionaryUnitOfWorkCallback;
		}

		#endregion

		#region Fields/Constants

		private readonly Func<IUnitOfWork> dictionaryUnitOfWorkCallback;
		private readonly IDTSComponentMetaData100 dtsComponentMetaData100;

		#endregion

		#region Properties/Indexers/Events

		public bool DebuggerLaunch
		{
			get
			{
				if (!this.HasProperty(Constants.COMPONENT_PROP_NAME_DEBUGGER_LAUNCH))
					return false;

				return this.GetProperty<bool>(Constants.COMPONENT_PROP_NAME_DEBUGGER_LAUNCH);
			}
			set
			{
				this.SetProperty<bool>(Constants.COMPONENT_PROP_NAME_DEBUGGER_LAUNCH, value);
			}
		}

		private Func<IUnitOfWork> DictionaryUnitOfWorkCallback
		{
			get
			{
				return this.dictionaryUnitOfWorkCallback;
			}
		}

		protected IDTSComponentMetaData100 DtsComponentMetaData100
		{
			get
			{
				return this.dtsComponentMetaData100;
			}
		}

		public string ObfuscationConfigurationJsonText
		{
			get
			{
				if (!this.HasProperty(Constants.COMPONENT_PROP_NAME_ObfuscationConfigurationJsonText))
					return null;

				return this.GetProperty<string>(Constants.COMPONENT_PROP_NAME_ObfuscationConfigurationJsonText);
			}
			set
			{
				this.SetProperty<string>(Constants.COMPONENT_PROP_NAME_ObfuscationConfigurationJsonText, value);
			}
		}

		#endregion

		#region Methods/Operators

		public void CreateInputColumnProperties()
		{
		}

		public void CreateProperties()
		{
#if DEBUG
			this.LetProperty<bool>(Constants.COMPONENT_PROP_NAME_DEBUGGER_LAUNCH, false,
				Constants.COMPONENT_PROP_DESC_DEBUGGER_LAUNCH, false);

#endif
			this.LetProperty<string>(Constants.COMPONENT_PROP_NAME_ObfuscationConfigurationJsonText, "{}",
				Constants.COMPONENT_PROP_DESC_ObfuscationConfigurationJsonText, true);
		}

		public ObfuscationConfiguration GetObfuscationConfiguration()
		{
			ObfuscationConfiguration obfuscationConfiguration;

			if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.ObfuscationConfigurationJsonText))
				obfuscationConfiguration = new ObfuscationConfiguration();
			else
				obfuscationConfiguration = new JsonSerializationStrategy().GetObjectFromString<ObfuscationConfiguration>(this.ObfuscationConfigurationJsonText);

			if ((object)obfuscationConfiguration != null)
			{
				if ((object)obfuscationConfiguration.SourceAdapterConfiguration != null)
				{
					obfuscationConfiguration.SourceAdapterConfiguration.AdapterAqtn = typeof(DtsSourceAdapter).AssemblyQualifiedName;
					obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration = null;
					obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration = null;
				}

				if ((object)obfuscationConfiguration.DestinationAdapterConfiguration != null)
				{
					obfuscationConfiguration.DestinationAdapterConfiguration.AdapterAqtn = typeof(DtsDestinationAdapter).AssemblyQualifiedName;
					obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration = null;
					obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration = null;
				}

				if ((object)obfuscationConfiguration.DictionaryConfigurations != null)
				{
					foreach (DictionaryConfiguration dictionaryConfiguration in obfuscationConfiguration.DictionaryConfigurations)
					{
						if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration != null)
						{
							dictionaryConfiguration.DictionaryAdapterConfiguration.AdapterAqtn = typeof(DtsDictionaryAdapter).AssemblyQualifiedName;
							dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration = new DtsAdoNetAdapterConfiguration(this.DictionaryUnitOfWorkCallback, dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration);
							dictionaryConfiguration.DictionaryAdapterConfiguration.DelimitedTextAdapterConfiguration = null;
						}
					}
				}
			}

			return obfuscationConfiguration;
		}

		#endregion
	}
}