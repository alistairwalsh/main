/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.ObfuscationEngine.Core.Adapter.Destination;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Source;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public sealed partial class ObfuscationController
	{
		#region Methods/Operators

		private static void _ApplyViewToDocumentAdoNet(IAdoNetAdapterSettingsView adoNetAdapterSettingsView, AdoNetAdapterConfiguration adoNetAdapterConfiguration)
		{
			if ((object)adoNetAdapterSettingsView == null)
				throw new ArgumentNullException("adoNetAdapterSettingsView");

			if ((object)adoNetAdapterConfiguration == null)
				throw new ArgumentNullException("adoNetAdapterConfiguration");

			if ((object)adoNetAdapterSettingsView.ConnectionType != null)
				adoNetAdapterConfiguration.ConnectionAqtn = adoNetAdapterSettingsView.ConnectionType.AssemblyQualifiedName;

			adoNetAdapterConfiguration.ConnectionString = adoNetAdapterSettingsView.ConnectionString;

			adoNetAdapterConfiguration.PreExecuteCommandText = adoNetAdapterSettingsView.PreExecuteCommandText;
			adoNetAdapterConfiguration.PreExecuteCommandType = adoNetAdapterSettingsView.PreExecuteCommandType;

			adoNetAdapterConfiguration.ExecuteCommandText = adoNetAdapterSettingsView.ExecuteCommandText;
			adoNetAdapterConfiguration.ExecuteCommandType = adoNetAdapterSettingsView.ExecuteCommandType;

			adoNetAdapterConfiguration.PostExecuteCommandText = adoNetAdapterSettingsView.PostExecuteCommandText;
			adoNetAdapterConfiguration.PostExecuteCommandType = adoNetAdapterSettingsView.PostExecuteCommandType;
		}

		private static void _ApplyViewToDocumentDelimitedText(IDelTextAdapterSettingsView delTextAdapterSettingsView, DelimitedTextAdapterConfiguration delimitedTextAdapterConfiguration)
		{
			if ((object)delTextAdapterSettingsView == null)
				throw new ArgumentNullException("delTextAdapterSettingsView");

			if ((object)delimitedTextAdapterConfiguration == null)
				throw new ArgumentNullException("delimitedTextAdapterConfiguration");

			delimitedTextAdapterConfiguration.DelimitedTextFilePath = delTextAdapterSettingsView.TextFilePath;
			delimitedTextAdapterConfiguration.DelimitedTextSpec.FirstRecordIsHeader = delTextAdapterSettingsView.IsFirstRowHeaders;
			delimitedTextAdapterConfiguration.DelimitedTextSpec.QuoteValue = UnescapeValue(delTextAdapterSettingsView.QuoteValue);
			delimitedTextAdapterConfiguration.DelimitedTextSpec.RecordDelimiter = UnescapeValue(delTextAdapterSettingsView.RecordDelimiter);
			delimitedTextAdapterConfiguration.DelimitedTextSpec.FieldDelimiter = UnescapeValue(delTextAdapterSettingsView.FieldDelimiter);

			foreach (IHeaderSpecView headerSpecView in delTextAdapterSettingsView.HeaderSpecViews)
			{
				HeaderSpec headerSpec;

				headerSpec = new HeaderSpec()
							{
								FieldType = headerSpecView.FieldType.GetValueOrDefault(),
								HeaderName = headerSpecView.HeaderName
							};

				delimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs.Add(headerSpec);
			}
		}

		private ObfuscationConfiguration ApplyViewToDocument()
		{
			ObfuscationConfiguration obfuscationConfiguration;

			obfuscationConfiguration = new ObfuscationConfiguration();
			obfuscationConfiguration.ConfigurationVersion = ObfuscationConfiguration.CurrentConfigurationVersion;
			obfuscationConfiguration.EngineVersion = ObfuscationConfiguration.CurrentEngineVersion;

			this.ApplyViewToDocumentAvalanche(obfuscationConfiguration);
			this.ApplyViewToDocumentDictionary(obfuscationConfiguration);
			this.ApplyViewToDocumentSource(obfuscationConfiguration);
			this.ApplyViewToDocumentMetadata(obfuscationConfiguration);
			this.ApplyViewToDocumentDestination(obfuscationConfiguration);

			return obfuscationConfiguration;
		}

		private void ApplyViewToDocumentAdoNetDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration = new AdoNetAdapterConfiguration();

			_ApplyViewToDocumentAdoNet(this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView, obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration);
		}

		private void ApplyViewToDocumentAdoNetSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration = new AdoNetAdapterConfiguration();

			_ApplyViewToDocumentAdoNet(this.View.SourceAdapterSettings.AdoNetAdapterSettingsView, obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration);
		}

		private void ApplyViewToDocumentAvalanche(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.HashConfiguration = new HashConfiguration();
			obfuscationConfiguration.HashConfiguration.Multiplier = this.View.AvalancheSettings.HashMultiplier;
			obfuscationConfiguration.HashConfiguration.Seed = this.View.AvalancheSettings.HashSeed;
		}

		private void ApplyViewToDocumentDelimitedTextDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration = new DelimitedTextAdapterConfiguration()
																									{
																										DelimitedTextSpec = new DelimitedTextSpec()
																									};

			_ApplyViewToDocumentDelimitedText(this.View.DestinationAdapterSettings.DelTextAdapterSettingsView, obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration);
		}

		private void ApplyViewToDocumentDelimitedTextSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration = new DelimitedTextAdapterConfiguration() { DelimitedTextSpec = new DelimitedTextSpec() };

			_ApplyViewToDocumentDelimitedText(this.View.SourceAdapterSettings.DelTextAdapterSettingsView, obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration);
		}

		private void ApplyViewToDocumentDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			Type type = null;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.DestinationAdapterConfiguration = new _LEGACY_AdapterConfiguration();

			type = this.View.DestinationAdapterSettings.SelectedAdapterType;

			if ((object)type == null)
			{
				// do nothing
			}
			else
			{
				obfuscationConfiguration.DestinationAdapterConfiguration.AdapterAqtn = this.View.DestinationAdapterSettings.SelectedAdapterType.AssemblyQualifiedName;

				if (type == typeof(DelimitedTextDestinationAdapter))
					this.ApplyViewToDocumentDelimitedTextDestination(obfuscationConfiguration);
				else if (type == typeof(SqlBulkCopyAdoNetDestinationAdapter) ||
						type == typeof(RecordCommandAdoNetDestinationAdapter))
					this.ApplyViewToDocumentAdoNetDestination(obfuscationConfiguration);
				else
				{
					// do nothing
				}
			}
		}

		private void ApplyViewToDocumentDictionary(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			foreach (IDictionarySpecView dictionarySpecView in this.View.DictionarySettings.DictionarySpecViews)
			{
				DictionaryConfiguration dictionaryConfiguration;

				dictionaryConfiguration = new DictionaryConfiguration()
										{
											DictionaryId = dictionarySpecView.DictionaryId,
											PreloadEnabled = dictionarySpecView.PreloadEnabled,
											RecordCount = dictionarySpecView.RecordCount,
											DictionaryAdapterConfiguration = new _LEGACY_AdapterConfiguration()
										};

				obfuscationConfiguration.DictionaryConfigurations.Add(dictionaryConfiguration);
			}
		}

		private void ApplyViewToDocumentMetadata(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.TableConfiguration = new TableConfiguration();

			foreach (IMetaColumnSpecView metaColumnSpecView in this.View.MetadataSettings.MetaColumnSpecViews)
			{
				ColumnConfiguration columnConfiguration;

				columnConfiguration = new ColumnConfiguration()
									{
										ColumnName = metaColumnSpecView.ColumnName ?? string.Empty,
										ObfuscationStrategyAqtn = metaColumnSpecView.ObfuscationStrategyAqtn
									};

				obfuscationConfiguration.TableConfiguration.ColumnConfigurations.Add(columnConfiguration);
			}
		}

		private void ApplyViewToDocumentSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			Type type = null;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.SourceAdapterConfiguration = new _LEGACY_AdapterConfiguration();

			type = this.View.SourceAdapterSettings.SelectedAdapterType;

			if ((object)type == null)
			{
				// do nothing
			}
			else
			{
				obfuscationConfiguration.SourceAdapterConfiguration.AdapterAqtn = this.View.SourceAdapterSettings.SelectedAdapterType.AssemblyQualifiedName;

				if (type == typeof(DelimitedTextSourceAdapter))
					this.ApplyViewToDocumentDelimitedTextSource(obfuscationConfiguration);
				else if (type == typeof(AdoNetSourceAdapter))
					this.ApplyViewToDocumentAdoNetSource(obfuscationConfiguration);
				else
				{
					// do nothing
				}
			}
		}

		#endregion
	}
}