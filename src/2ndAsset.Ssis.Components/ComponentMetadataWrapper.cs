/*
	Copyright ©2002-2015 Daniel Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.SqlServer.Dts.Pipeline.Wrapper;

using TextMetal.Middleware.Data.UoW;

using _2ndAsset.ObfuscationEngine.Core.Adapter.Destination;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Source;
using _2ndAsset.ObfuscationEngine.Core.Config;

namespace _2ndAsset.Ssis.Components
{
	public sealed class ComponentMetadataWrapper : CustomPropertyWrapper
	{
		#region Constructors/Destructors

		public ComponentMetadataWrapper(IDTSComponentMetaData100 dtsComponentMetaData100, Func<IUnitOfWork> dictionaryUnitOfWorkCallback)
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

		public IEnumerable<InputColumnMetadataWrapper> InputColumns
		{
			get
			{
				IDTSInput100 dtsInput100;

				dtsInput100 = this.DtsComponentMetaData100.InputCollection[Constants.COMPONENT_INPUT_DEFAULT_NAME];

				foreach (IDTSInputColumn100 dtsInputColumn100 in dtsInput100.InputColumnCollection)
					yield return new InputColumnMetadataWrapper(dtsInputColumn100);
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

		public IEnumerable<DictionaryMetadataWrapper> DictionaryConfiguration
		{
			get
			{
				string jsonData;
				IEnumerable<DictionaryMetadataWrapper> value;

				if (!this.HasProperty(Constants.COMPONENT_PROP_NAME_DICTIONARY_CONFIGURATION))
					return null;

				jsonData = this.GetProperty<string>(Constants.COMPONENT_PROP_NAME_DICTIONARY_CONFIGURATION);

				value = DictionaryMetadataWrapper.FromJson(jsonData);

				return value;
			}
			set
			{
				string jsonData;

				jsonData = DictionaryMetadataWrapper.ToJson(value);

				this.SetProperty<string>(Constants.COMPONENT_PROP_NAME_DICTIONARY_CONFIGURATION, jsonData);
			}
		}

		public long HashMuliplier
		{
			get
			{
				return this.GetProperty<long>(Constants.COMPONENT_PROP_NAME_SIGN_HASH_MULTIPLIER);
			}
			set
			{
				this.SetProperty<long>(Constants.COMPONENT_PROP_NAME_SIGN_HASH_MULTIPLIER, value);
			}
		}

		public long HashSeed
		{
			get
			{
				return this.GetProperty<long>(Constants.COMPONENT_PROP_NAME_SIGN_HASH_SEED);
			}
			set
			{
				this.SetProperty<long>(Constants.COMPONENT_PROP_NAME_SIGN_HASH_SEED, value);
			}
		}

		#endregion

		#region Methods/Operators

		public void CreateInputColumnProperties()
		{
			foreach (var inputColumns in this.InputColumns)
				inputColumns.CreateProperties();
		}

		public void CreateProperties()
		{
			this.LetProperty<long>(Constants.COMPONENT_PROP_NAME_SIGN_HASH_MULTIPLIER, 33,
				Constants.COMPONENT_PROP_DESC_SIGN_HASH_MULTIPLIER, true);

			this.LetProperty<long>(Constants.COMPONENT_PROP_NAME_SIGN_HASH_SEED, 5381,
				Constants.COMPONENT_PROP_DESC_SIGN_HASH_SEED, true);

			this.LetProperty<long>(Constants.COMPONENT_PROP_NAME_VALUE_HASH_MULTIPLIER, 33,
				Constants.COMPONENT_PROP_DESC_VALUE_HASH_MULTIPLIER, true);

			this.LetProperty<long>(Constants.COMPONENT_PROP_NAME_VALUE_HASH_SEED, 5381,
				Constants.COMPONENT_PROP_DESC_VALUE_HASH_SEED, true);

#if DEBUG
			this.LetProperty<bool>(Constants.COMPONENT_PROP_NAME_DEBUGGER_LAUNCH, false,
				Constants.COMPONENT_PROP_DESC_DEBUGGER_LAUNCH, false);
#endif

			this.LetProperty<string>(Constants.COMPONENT_PROP_NAME_DICTIONARY_CONFIGURATION, "[]",
				Constants.COMPONENT_PROP_DESC_DICTIONARY_CONFIGURATION, false, Constants.COMPONENT_PROP_TYPE_CONV_AQTN_DICTIONARY_CONFIGURATION, Constants.COMPONENT_PROP_UI_EDIT_AQTN_DICTIONARY_CONFIGURATION);

			this.CreateInputColumnProperties();
		}

		private IEnumerable<ColumnConfiguration> GetColumnConfigurations()
		{
			foreach (InputColumnMetadataWrapper inputColumn in this.InputColumns)
			{
				yield return new ColumnConfiguration()
							{
								ColumnName = inputColumn.ColumnName,
								ObfuscationStrategy = inputColumn.ObfuscationStrategy,
								ExtentValue = inputColumn.ExtentValue,
								IsColumnNullable = inputColumn.IsColumnNullable,
								DictionaryReference = inputColumn.DictionaryReference
							};
			}
		}

		private IEnumerable<DictionaryConfiguration> GetDictionaryConfigurations()
		{
			List<DictionaryConfiguration> temp;

			temp = new List<DictionaryConfiguration>(this.DictionaryConfiguration.Select(d => new DictionaryConfiguration()
																							{
																								DictionaryId = d.DictionaryId,
																								PreloadEnabled = false,
																								RecordCount = d.RecordCount,
																								DictionaryAdapterConfiguration = new AdapterConfiguration()
																																{
																																	AdapterAqtn = typeof(DtsAdoNetAdapterConfiguration).AssemblyQualifiedName,
																																	AdoNetAdapterConfiguration = new DtsAdoNetAdapterConfiguration(this.DictionaryUnitOfWorkCallback, null)
																																}
																							}));

			return temp;
		}

		public ObfuscationConfiguration GetObfuscationConfiguration()
		{
			ObfuscationConfiguration obfuscationConfiguration;

			obfuscationConfiguration = new ObfuscationConfiguration();

			obfuscationConfiguration.HashConfiguration = new HashConfiguration();
			obfuscationConfiguration.HashConfiguration.Multiplier = this.HashMuliplier;
			obfuscationConfiguration.HashConfiguration.Seed = this.HashSeed;

			obfuscationConfiguration.SourceAdapterConfiguration = new AdapterConfiguration() { AdapterAqtn = typeof(NullSourceAdapter).AssemblyQualifiedName };
			obfuscationConfiguration.DestinationAdapterConfiguration = new AdapterConfiguration() { AdapterAqtn = typeof(NullDestinationAdapter).AssemblyQualifiedName };

			obfuscationConfiguration.TableConfiguration = new TableConfiguration();

			foreach (var column in this.GetColumnConfigurations())
				obfuscationConfiguration.TableConfiguration.ColumnConfigurations.Add(column);

			foreach (var dictionary in this.GetDictionaryConfigurations())
				obfuscationConfiguration.DictionaryConfigurations.Add(dictionary);

			return obfuscationConfiguration;
		}

		#endregion
	}
}