/*
	Copyright ©2002-2015 Daniel Bullington (info@2ndasset.com)
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Linq;

using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

using Solder.Framework.Serialization;
using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support.AdoNetFast.UoW;

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
				obfuscationConfiguration = new ObfuscationConfiguration()
											{
												ConfigurationVersion = ObfuscationConfiguration.CurrentConfigurationVersion,
												EngineVersion = ObfuscationConfiguration.CurrentEngineVersion
											};
			else
				obfuscationConfiguration = new JsonSerializationStrategy().GetObjectFromString<ObfuscationConfiguration>(this.ObfuscationConfigurationJsonText);

			if ((object)obfuscationConfiguration != null)
			{
				if ((object)obfuscationConfiguration.SourceAdapterConfiguration != null)
				{
					obfuscationConfiguration.SourceAdapterConfiguration.ResetAdapterSpecificConfiguration();
					obfuscationConfiguration.SourceAdapterConfiguration.AdapterAqtn = typeof(DtsSourceAdapter).AssemblyQualifiedName;
				}

				if ((object)obfuscationConfiguration.DestinationAdapterConfiguration != null)
				{
					obfuscationConfiguration.DestinationAdapterConfiguration.ResetAdapterSpecificConfiguration();
					obfuscationConfiguration.DestinationAdapterConfiguration.AdapterAqtn = typeof(DtsDestinationAdapter).AssemblyQualifiedName;
				}

				if ((object)obfuscationConfiguration.DictionaryConfigurations != null)
				{
					foreach (DictionaryConfiguration dictionaryConfiguration in obfuscationConfiguration.DictionaryConfigurations)
					{
						if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration != null)
						{
							var items = dictionaryConfiguration.DictionaryAdapterConfiguration.AdapterSpecificConfiguration.Select(kvp => new { KEY = kvp.Key, VAL = kvp.Value }).ToArray();

							dictionaryConfiguration.DictionaryAdapterConfiguration.ResetAdapterSpecificConfiguration();
							dictionaryConfiguration.DictionaryAdapterConfiguration.AdapterAqtn = typeof(DtsDictionaryAdapter).AssemblyQualifiedName;

							foreach (var item in items)
								dictionaryConfiguration.DictionaryAdapterConfiguration.AdapterSpecificConfiguration.Add(item.KEY, item.VAL);

							dictionaryConfiguration.DictionaryAdapterConfiguration.AdapterSpecificConfiguration.Add("DictionaryUnitOfWorkCallback", this.DictionaryUnitOfWorkCallback);
						}
					}
				}
			}

			return obfuscationConfiguration;
		}

		#endregion
	}
}