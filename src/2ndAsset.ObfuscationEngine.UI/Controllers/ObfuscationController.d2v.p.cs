/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Adapter.Destination;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary;
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

		private static void _ApplyDocumentToViewAdoNet(AdoNetAdapterConfiguration adoNetAdapterConfiguration, IAdoNetAdapterSettingsView adoNetAdapterSettingsView)
		{
			if ((object)adoNetAdapterConfiguration == null)
				throw new ArgumentNullException("adoNetAdapterConfiguration");

			if ((object)adoNetAdapterSettingsView == null)
				throw new ArgumentNullException("adoNetAdapterSettingsView");

			adoNetAdapterSettingsView.ConnectionString = adoNetAdapterConfiguration.ConnectionString;
			adoNetAdapterSettingsView.ConnectionType = adoNetAdapterConfiguration.GetConnectionType();

			adoNetAdapterSettingsView.PreExecuteCommandText = adoNetAdapterConfiguration.PreExecuteCommandText;
			adoNetAdapterSettingsView.PreExecuteCommandType = adoNetAdapterConfiguration.PreExecuteCommandType;

			adoNetAdapterSettingsView.ExecuteCommandText = adoNetAdapterConfiguration.ExecuteCommandText;
			adoNetAdapterSettingsView.ExecuteCommandType = adoNetAdapterConfiguration.ExecuteCommandType;

			adoNetAdapterSettingsView.PostExecuteCommandText = adoNetAdapterConfiguration.PostExecuteCommandText;
			adoNetAdapterSettingsView.PostExecuteCommandType = adoNetAdapterConfiguration.PostExecuteCommandType;
		}

		private static void _ApplyDocumentToViewDelimitedText(DelimitedTextAdapterConfiguration delimitedTextAdapterConfiguration, IDelTextAdapterSettingsView delTextAdapterSettingsView)
		{
			if ((object)delimitedTextAdapterConfiguration == null)
				throw new ArgumentNullException("delimitedTextAdapterConfiguration");

			if ((object)delTextAdapterSettingsView == null)
				throw new ArgumentNullException("delTextAdapterSettingsView");

			delTextAdapterSettingsView.TextFilePath = delimitedTextAdapterConfiguration.DelimitedTextFilePath;

			if ((object)delimitedTextAdapterConfiguration.DelimitedTextSpec != null)
			{
				delTextAdapterSettingsView.IsFirstRowHeaders = delimitedTextAdapterConfiguration.DelimitedTextSpec.FirstRecordIsHeader;
				delTextAdapterSettingsView.QuoteValue = EscapeValue(delimitedTextAdapterConfiguration.DelimitedTextSpec.QuoteValue);
				delTextAdapterSettingsView.RecordDelimiter = EscapeValue(delimitedTextAdapterConfiguration.DelimitedTextSpec.RecordDelimiter);
				delTextAdapterSettingsView.FieldDelimiter = EscapeValue(delimitedTextAdapterConfiguration.DelimitedTextSpec.FieldDelimiter);

				delTextAdapterSettingsView.ClearHeaderSpecViews();

				if ((object)delimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs != null)
				{
					foreach (HeaderSpec headerSpec in delimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs)
						delTextAdapterSettingsView.AddHeaderSpecView(headerSpec.HeaderName, headerSpec.FieldType);
				}
			}
		}

		private void ApplyDocumentToView(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			//this.View.ConfigurationVersion = obfuscationConfiguration.ConfigurationVersion;

			this.ApplyDocumentToViewAvalanche(obfuscationConfiguration);
			this.ApplyDocumentToViewDictionary(obfuscationConfiguration);
			this.ApplyDocumentToViewSource(obfuscationConfiguration);
			this.ApplyDocumentToViewMetadata(obfuscationConfiguration);
			this.ApplyDocumentToViewDestination(obfuscationConfiguration);
		}

		private void ApplyDocumentToViewAdoNetDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration != null &&
				(object)obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration != null)
				_ApplyDocumentToViewAdoNet(obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration, this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView);
		}

		private void ApplyDocumentToViewAdoNetDictionary(DictionaryConfiguration dictionaryConfiguration, IAdapterSettingsView adapterSettingsView)
		{
			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)adapterSettingsView == null)
				throw new ArgumentNullException("adapterSettingsView");

			if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration != null &&
				(object)dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration != null)
				_ApplyDocumentToViewAdoNet(dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration, adapterSettingsView.AdoNetAdapterSettingsView);
		}

		private void ApplyDocumentToViewAdoNetSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration != null &&
				(object)obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration != null)
				_ApplyDocumentToViewAdoNet(obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration, this.View.SourceAdapterSettings.AdoNetAdapterSettingsView);
		}

		private void ApplyDocumentToViewAvalanche(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.HashConfiguration != null)
			{
				this.View.AvalancheSettings.HashMultiplier = obfuscationConfiguration.HashConfiguration.Multiplier;
				this.View.AvalancheSettings.HashSeed = obfuscationConfiguration.HashConfiguration.Seed;
			}
		}

		private void ApplyDocumentToViewDelimitedTextDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration != null &&
				(object)obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration != null)
				_ApplyDocumentToViewDelimitedText(obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration, this.View.DestinationAdapterSettings.DelTextAdapterSettingsView);
		}

		private void ApplyDocumentToViewDelimitedTextDictionary(DictionaryConfiguration dictionaryConfiguration, IAdapterSettingsView adapterSettingsView)
		{
			if ((object)dictionaryConfiguration == null)
				throw new ArgumentNullException("dictionaryConfiguration");

			if ((object)adapterSettingsView == null)
				throw new ArgumentNullException("adapterSettingsView");

			if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration != null &&
				(object)dictionaryConfiguration.DictionaryAdapterConfiguration.DelimitedTextAdapterConfiguration != null)
				_ApplyDocumentToViewDelimitedText(dictionaryConfiguration.DictionaryAdapterConfiguration.DelimitedTextAdapterConfiguration, adapterSettingsView.DelTextAdapterSettingsView);
		}

		private void ApplyDocumentToViewDelimitedTextSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration != null &&
				(object)obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration != null)
				_ApplyDocumentToViewDelimitedText(obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration, this.View.SourceAdapterSettings.DelTextAdapterSettingsView);
		}

		private void ApplyDocumentToViewDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			Type type = null;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration != null)
			{
				if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(obfuscationConfiguration.DestinationAdapterConfiguration.AdapterAqtn))
				{
					type = obfuscationConfiguration.DestinationAdapterConfiguration.GetAdapterType();
					this.View.DestinationAdapterSettings.SelectedAdapterType = type;
				}

				if ((object)type == null)
				{
					// do nothing
				}
				else if (type == typeof(DelimitedTextDestinationAdapter))
					this.ApplyDocumentToViewDelimitedTextDestination(obfuscationConfiguration);
				else if (type == typeof(SqlBulkCopyAdoNetDestinationAdapter) ||
						type == typeof(RecordCommandAdoNetDestinationAdapter))
					this.ApplyDocumentToViewAdoNetDestination(obfuscationConfiguration);
				else
				{
					// do nothing
				}
			}
		}

		private void ApplyDocumentToViewDictionary(ObfuscationConfiguration obfuscationConfiguration)
		{
			IDictionarySpecView dictionarySpecView;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.DictionaryConfigurations != null)
			{
				foreach (DictionaryConfiguration dictionaryConfiguration in obfuscationConfiguration.DictionaryConfigurations)
				{
					Type type = null;

					dictionarySpecView = this.View.DictionarySettings.AddDictionarySpecView(dictionaryConfiguration.DictionaryId, dictionaryConfiguration.PreloadEnabled, dictionaryConfiguration.RecordCount, null);

					_InitializeDictionaryAdapterView(dictionarySpecView);

					if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration != null)
					{
						if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(dictionaryConfiguration.DictionaryAdapterConfiguration.AdapterAqtn))
						{
							type = dictionaryConfiguration.DictionaryAdapterConfiguration.GetAdapterType();
							dictionarySpecView.DictionaryAdapterSettings.SelectedAdapterType = type;
						}

						if ((object)type == null)
						{
							// do nothing
						}
						else if (type == typeof(DelimitedTextDictionaryAdapter))
							this.ApplyDocumentToViewDelimitedTextDictionary(dictionaryConfiguration, dictionarySpecView.DictionaryAdapterSettings);
						else if (type == typeof(AdoNetDictionaryAdapter))
							this.ApplyDocumentToViewAdoNetDictionary(dictionaryConfiguration, dictionarySpecView.DictionaryAdapterSettings);
						else
						{
							// do nothing
						}
					}
				}
			}
		}

		private void ApplyDocumentToViewMetadata(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.TableConfiguration != null &&
				(object)obfuscationConfiguration.TableConfiguration.ColumnConfigurations != null)
			{
				foreach (ColumnConfiguration columnConfiguration in obfuscationConfiguration.TableConfiguration.ColumnConfigurations)
					this.View.MetadataSettings.AddMetaColumnSpecView(columnConfiguration.ColumnName, columnConfiguration.ObfuscationStrategyAqtn);
			}
		}

		private void ApplyDocumentToViewSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			Type type = null;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration != null)
			{
				if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(obfuscationConfiguration.SourceAdapterConfiguration.AdapterAqtn))
				{
					type = obfuscationConfiguration.SourceAdapterConfiguration.GetAdapterType();
					this.View.SourceAdapterSettings.SelectedAdapterType = type;
				}

				if ((object)type == null)
				{
					// do nothing
				}
				else if (type == typeof(DelimitedTextSourceAdapter))
					this.ApplyDocumentToViewDelimitedTextSource(obfuscationConfiguration);
				else if (type == typeof(AdoNetSourceAdapter))
					this.ApplyDocumentToViewAdoNetSource(obfuscationConfiguration);
				else
				{
					// do nothing
				}
			}
		}

		#endregion
	}
}