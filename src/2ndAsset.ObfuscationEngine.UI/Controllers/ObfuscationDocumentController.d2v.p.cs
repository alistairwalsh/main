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
	public partial class ObfuscationDocumentController<TObfuscationDocumentView>
		where TObfuscationDocumentView : class, IObfuscationDocumentView
	{
		#region Methods/Operators

		private static void _ApplyDocumentToViewAdoNet(AdoNetAdapterConfiguration adoNetAdapterConfiguration, IAdoNetAdapterSettingsPartialView adoNetAdapterSettingsPartialView)
		{
			if ((object)adoNetAdapterConfiguration == null)
				throw new ArgumentNullException("adoNetAdapterConfiguration");

			if ((object)adoNetAdapterSettingsPartialView == null)
				throw new ArgumentNullException("adoNetAdapterSettingsPartialView");

			adoNetAdapterSettingsPartialView.ConnectionString = adoNetAdapterConfiguration.ConnectionString;
			adoNetAdapterSettingsPartialView.ConnectionType = adoNetAdapterConfiguration.GetConnectionType();

			adoNetAdapterSettingsPartialView.PreExecuteCommandText = adoNetAdapterConfiguration.PreExecuteCommandText;
			adoNetAdapterSettingsPartialView.PreExecuteCommandType = adoNetAdapterConfiguration.PreExecuteCommandType;

			adoNetAdapterSettingsPartialView.ExecuteCommandText = adoNetAdapterConfiguration.ExecuteCommandText;
			adoNetAdapterSettingsPartialView.ExecuteCommandType = adoNetAdapterConfiguration.ExecuteCommandType;

			adoNetAdapterSettingsPartialView.PostExecuteCommandText = adoNetAdapterConfiguration.PostExecuteCommandText;
			adoNetAdapterSettingsPartialView.PostExecuteCommandType = adoNetAdapterConfiguration.PostExecuteCommandType;
		}

		private static void _ApplyDocumentToViewDelimitedText(DelimitedTextAdapterConfiguration delimitedTextAdapterConfiguration, IDelTextAdapterSettingsPartialView delTextAdapterSettingsPartialView)
		{
			if ((object)delimitedTextAdapterConfiguration == null)
				throw new ArgumentNullException("delimitedTextAdapterConfiguration");

			if ((object)delTextAdapterSettingsPartialView == null)
				throw new ArgumentNullException("delTextAdapterSettingsPartialView");

			delTextAdapterSettingsPartialView.TextFilePath = delimitedTextAdapterConfiguration.DelimitedTextFilePath;

			if ((object)delimitedTextAdapterConfiguration.DelimitedTextSpec != null)
			{
				delTextAdapterSettingsPartialView.IsFirstRowHeaders = delimitedTextAdapterConfiguration.DelimitedTextSpec.FirstRecordIsHeader;
				delTextAdapterSettingsPartialView.QuoteValue = EscapeValue(delimitedTextAdapterConfiguration.DelimitedTextSpec.QuoteValue);
				delTextAdapterSettingsPartialView.RecordDelimiter = EscapeValue(delimitedTextAdapterConfiguration.DelimitedTextSpec.RecordDelimiter);
				delTextAdapterSettingsPartialView.FieldDelimiter = EscapeValue(delimitedTextAdapterConfiguration.DelimitedTextSpec.FieldDelimiter);

				delTextAdapterSettingsPartialView.ClearHeaderSpecViews();

				if ((object)delimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs != null)
				{
					foreach (HeaderSpec headerSpec in delimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs)
						delTextAdapterSettingsPartialView.AddHeaderSpecView(headerSpec.HeaderName, headerSpec.FieldType);
				}
			}
		}

		protected void ApplyDocumentToView(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			//this.View.ObfuscationPartialView.ConfigurationVersion = obfuscationConfiguration.ConfigurationVersion;

			this.ApplyDocumentToViewAvalanche(obfuscationConfiguration);
			this.ApplyDocumentToViewDictionary(obfuscationConfiguration);
			this.ApplyDocumentToViewSource(obfuscationConfiguration);
			this.ApplyDocumentToViewMetadata(obfuscationConfiguration);
			this.ApplyDocumentToViewDestination(obfuscationConfiguration);
		}

		private void ApplyDocumentToViewAdoNetDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//if ((object)obfuscationConfiguration.DestinationAdapterConfiguration != null &&
			//	(object)obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration != null)
			//	_ApplyDocumentToViewAdoNet(obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration, this.View.ObfuscationPartialView.DestinationAdapterSettings.AdoNetAdapterSettingsView);
		}

		private void ApplyDocumentToViewAdoNetDictionary(DictionaryConfiguration dictionaryConfiguration, IAdapterSettingsPartialView adapterSettingsPartialView)
		{
			//if ((object)dictionaryConfiguration == null)
			//	throw new ArgumentNullException("dictionaryConfiguration");

			//if ((object)adapterSettingsView == null)
			//	throw new ArgumentNullException("adapterSettingsView");

			//if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration != null &&
			//	(object)dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration != null)
			//	_ApplyDocumentToViewAdoNet(dictionaryConfiguration.DictionaryAdapterConfiguration.AdoNetAdapterConfiguration, adapterSettingsView.AdoNetAdapterSettingsView);
		}

		private void ApplyDocumentToViewAdoNetSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//if ((object)obfuscationConfiguration.SourceAdapterConfiguration != null &&
			//	(object)obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration != null)
			//	_ApplyDocumentToViewAdoNet(obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration, this.View.ObfuscationPartialView.SourceAdapterSettings.AdoNetAdapterSettingsView);
		}

		private void ApplyDocumentToViewAvalanche(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.HashConfiguration != null)
			{
				this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashMultiplier = obfuscationConfiguration.HashConfiguration.Multiplier;
				this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashSeed = obfuscationConfiguration.HashConfiguration.Seed;
			}
		}

		private void ApplyDocumentToViewDelimitedTextDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//if ((object)obfuscationConfiguration.DestinationAdapterConfiguration != null &&
			//	(object)obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration != null)
			//	_ApplyDocumentToViewDelimitedText(obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration, this.View.ObfuscationPartialView.DestinationAdapterSettings.DelTextAdapterSettingsView);
		}

		private void ApplyDocumentToViewDelimitedTextDictionary(DictionaryConfiguration dictionaryConfiguration, IAdapterSettingsPartialView adapterSettingsPartialView)
		{
			//if ((object)dictionaryConfiguration == null)
			//	throw new ArgumentNullException("dictionaryConfiguration");

			//if ((object)adapterSettingsView == null)
			//	throw new ArgumentNullException("adapterSettingsView");

			//if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration != null &&
			//	(object)dictionaryConfiguration.DictionaryAdapterConfiguration.DelimitedTextAdapterConfiguration != null)
			//	_ApplyDocumentToViewDelimitedText(dictionaryConfiguration.DictionaryAdapterConfiguration.DelimitedTextAdapterConfiguration, adapterSettingsView.DelTextAdapterSettingsView);
		}

		private void ApplyDocumentToViewDelimitedTextSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			//if ((object)obfuscationConfiguration == null)
			//	throw new ArgumentNullException("obfuscationConfiguration");

			//if ((object)obfuscationConfiguration.SourceAdapterConfiguration != null &&
			//	(object)obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration != null)
			//	_ApplyDocumentToViewDelimitedText(obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration, this.View.ObfuscationPartialView.SourceAdapterSettings.DelTextAdapterSettingsView);
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
					this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType = type;
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
			IDictionarySpecListView dictionarySpecListView;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.DictionaryConfigurations != null)
			{
				foreach (DictionaryConfiguration dictionaryConfiguration in obfuscationConfiguration.DictionaryConfigurations)
				{
					Type type = null;

					dictionarySpecListView = this.View.ObfuscationPartialView.DictionarySettingsPartialView.AddDictionarySpecView(dictionaryConfiguration.DictionaryId, dictionaryConfiguration.PreloadEnabled, dictionaryConfiguration.RecordCount, null);

					this.InitializeDictionaryAdapterView(dictionarySpecListView);

					if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration != null)
					{
						if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(dictionaryConfiguration.DictionaryAdapterConfiguration.AdapterAqtn))
						{
							type = dictionaryConfiguration.DictionaryAdapterConfiguration.GetAdapterType();
							dictionarySpecListView.DictionaryAdapterSettingsPartialView.SelectedAdapterType = type;
						}

						if ((object)type == null)
						{
							// do nothing
						}
						else if (type == typeof(DelimitedTextDictionaryAdapter))
							this.ApplyDocumentToViewDelimitedTextDictionary(dictionaryConfiguration, dictionarySpecListView.DictionaryAdapterSettingsPartialView);
						else if (type == typeof(AdoNetDictionaryAdapter))
							this.ApplyDocumentToViewAdoNetDictionary(dictionaryConfiguration, dictionarySpecListView.DictionaryAdapterSettingsPartialView);
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
					this.View.ObfuscationPartialView.MetadataSettingsPartialView.AddMetaColumnSpecView(columnConfiguration.ColumnName, columnConfiguration.ObfuscationStrategyAqtn);
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
					this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType = type;
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