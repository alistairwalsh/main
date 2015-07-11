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
	public partial class ObfuscationDocumentController<TObfuscationDocumentView>
		where TObfuscationDocumentView : class, IObfuscationDocumentView
	{
		#region Methods/Operators

		private static void _ApplyViewToDocumentAdoNet(IAdoNetAdapterSettingsPartialView adoNetAdapterSettingsPartialView, AdoNetAdapterConfiguration adoNetAdapterConfiguration)
		{
			if ((object)adoNetAdapterSettingsPartialView == null)
				throw new ArgumentNullException("adoNetAdapterSettingsPartialView");

			if ((object)adoNetAdapterConfiguration == null)
				throw new ArgumentNullException("adoNetAdapterConfiguration");

			if ((object)adoNetAdapterSettingsPartialView.ConnectionType != null)
				adoNetAdapterConfiguration.ConnectionAqtn = adoNetAdapterSettingsPartialView.ConnectionType.AssemblyQualifiedName;

			adoNetAdapterConfiguration.ConnectionString = adoNetAdapterSettingsPartialView.ConnectionString;

			adoNetAdapterConfiguration.PreExecuteCommandText = adoNetAdapterSettingsPartialView.PreExecuteCommandText;
			adoNetAdapterConfiguration.PreExecuteCommandType = adoNetAdapterSettingsPartialView.PreExecuteCommandType;

			adoNetAdapterConfiguration.ExecuteCommandText = adoNetAdapterSettingsPartialView.ExecuteCommandText;
			adoNetAdapterConfiguration.ExecuteCommandType = adoNetAdapterSettingsPartialView.ExecuteCommandType;

			adoNetAdapterConfiguration.PostExecuteCommandText = adoNetAdapterSettingsPartialView.PostExecuteCommandText;
			adoNetAdapterConfiguration.PostExecuteCommandType = adoNetAdapterSettingsPartialView.PostExecuteCommandType;
		}

		private static void _ApplyViewToDocumentDelimitedText(IDelTextAdapterSettingsPartialView delTextAdapterSettingsPartialView, DelimitedTextAdapterConfiguration delimitedTextAdapterConfiguration)
		{
			if ((object)delTextAdapterSettingsPartialView == null)
				throw new ArgumentNullException("delTextAdapterSettingsPartialView");

			if ((object)delimitedTextAdapterConfiguration == null)
				throw new ArgumentNullException("delimitedTextAdapterConfiguration");

			delimitedTextAdapterConfiguration.DelimitedTextFilePath = delTextAdapterSettingsPartialView.TextFilePath;
			delimitedTextAdapterConfiguration.DelimitedTextSpec.FirstRecordIsHeader = delTextAdapterSettingsPartialView.IsFirstRowHeaders;
			delimitedTextAdapterConfiguration.DelimitedTextSpec.QuoteValue = UnescapeValue(delTextAdapterSettingsPartialView.QuoteValue);
			delimitedTextAdapterConfiguration.DelimitedTextSpec.RecordDelimiter = UnescapeValue(delTextAdapterSettingsPartialView.RecordDelimiter);
			delimitedTextAdapterConfiguration.DelimitedTextSpec.FieldDelimiter = UnescapeValue(delTextAdapterSettingsPartialView.FieldDelimiter);

			foreach (IHeaderSpecListView headerSpecView in delTextAdapterSettingsPartialView.HeaderSpecViews)
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

		protected ObfuscationConfiguration ApplyViewToDocument()
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
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration = new AdoNetAdapterConfiguration();

			//_ApplyViewToDocumentAdoNet(this.View.ObfuscationPartialView.DestinationAdapterSettings.AdoNetAdapterSettingsView, obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration);
		}

		private void ApplyViewToDocumentAdoNetSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration = new AdoNetAdapterConfiguration();

			//_ApplyViewToDocumentAdoNet(this.View.ObfuscationPartialView.SourceAdapterSettings.AdoNetAdapterSettingsView, obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration);
		}

		private void ApplyViewToDocumentAvalanche(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.HashConfiguration = new HashConfiguration();
			obfuscationConfiguration.HashConfiguration.Multiplier = this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashMultiplier;
			obfuscationConfiguration.HashConfiguration.Seed = this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashSeed;
		}

		private void ApplyViewToDocumentDelimitedTextDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration = new DelimitedTextAdapterConfiguration()
			//																						{
			//																							DelimitedTextSpec = new DelimitedTextSpec()
			//																						};

			//_ApplyViewToDocumentDelimitedText(this.View.ObfuscationPartialView.DestinationAdapterSettings.DelTextAdapterSettingsView, obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration);
		}

		private void ApplyViewToDocumentDelimitedTextSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration = new DelimitedTextAdapterConfiguration() { DelimitedTextSpec = new DelimitedTextSpec() };

			//_ApplyViewToDocumentDelimitedText(this.View.ObfuscationPartialView.SourceAdapterSettings.DelTextAdapterSettingsView, obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration);
		}

		private void ApplyViewToDocumentDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			Type type = null;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.DestinationAdapterConfiguration = new AdapterConfiguration();

			type = this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType;

			if ((object)type == null)
			{
				// do nothing
			}
			else
			{
				obfuscationConfiguration.DestinationAdapterConfiguration.AdapterAqtn = this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType.AssemblyQualifiedName;

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

			foreach (IDictionarySpecListView dictionarySpecView in this.View.ObfuscationPartialView.DictionarySettingsPartialView.DictionarySpecListViews)
			{
				DictionaryConfiguration dictionaryConfiguration;

				dictionaryConfiguration = new DictionaryConfiguration()
										{
											DictionaryId = dictionarySpecView.DictionaryId,
											PreloadEnabled = dictionarySpecView.PreloadEnabled,
											RecordCount = dictionarySpecView.RecordCount,
											DictionaryAdapterConfiguration = new AdapterConfiguration()
										};

				obfuscationConfiguration.DictionaryConfigurations.Add(dictionaryConfiguration);
			}
		}

		private void ApplyViewToDocumentMetadata(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.TableConfiguration = new TableConfiguration();

			foreach (IMetaColumnSpecListView metaColumnSpecView in this.View.ObfuscationPartialView.MetadataSettingsPartialView.MetaColumnSpecListViews)
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

			obfuscationConfiguration.SourceAdapterConfiguration = new AdapterConfiguration();

			type = this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType;

			if ((object)type == null)
			{
				// do nothing
			}
			else
			{
				obfuscationConfiguration.SourceAdapterConfiguration.AdapterAqtn = this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType.AssemblyQualifiedName;

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