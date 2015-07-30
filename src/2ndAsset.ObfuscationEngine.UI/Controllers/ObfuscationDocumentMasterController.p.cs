/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using Solder.Framework.Utilities;

using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public abstract partial class ObfuscationDocumentMasterController<TObfuscationDocumentView>
		where TObfuscationDocumentView : class, IObfuscationDocumentView
	{
		#region Methods/Operators

		private void ApplyDocumentToView_AvalancheSettings(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.HashConfiguration != null)
			{
				this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashMultiplier = obfuscationConfiguration.HashConfiguration.Multiplier;
				this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashSeed = obfuscationConfiguration.HashConfiguration.Seed;
			}
		}

		private void ApplyDocumentToView_DestinationAdapterSettings(ObfuscationConfiguration obfuscationConfiguration)
		{
			Type type;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration != null)
			{
				if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(obfuscationConfiguration.DestinationAdapterConfiguration.AdapterAqtn))
				{
					type = obfuscationConfiguration.DestinationAdapterConfiguration.GetAdapterType();
					this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType = type;
				}

				//if ((object)this.View.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
				//this.View.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyDocumentToView(obfuscationConfiguration);
			}
		}

		private void ApplyDocumentToView_DictionarySettings(ObfuscationConfiguration obfuscationConfiguration)
		{
			IDictionarySpecListView dictionarySpecListView;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.DictionaryConfigurations != null)
			{
				foreach (DictionaryConfiguration dictionaryConfiguration in obfuscationConfiguration.DictionaryConfigurations)
				{
					Type type;

					dictionarySpecListView = this.View.ObfuscationPartialView.DictionarySettingsPartialView.AddDictionarySpecView(dictionaryConfiguration.DictionaryId, dictionaryConfiguration.PreloadEnabled, dictionaryConfiguration.RecordCount, null);

					this.InitializeDictionaryAdapterView(dictionarySpecListView);

					if ((object)dictionaryConfiguration.DictionaryAdapterConfiguration != null)
					{
						if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(dictionaryConfiguration.DictionaryAdapterConfiguration.AdapterAqtn))
						{
							type = dictionaryConfiguration.DictionaryAdapterConfiguration.GetAdapterType();
							dictionarySpecListView.DictionaryAdapterSettingsPartialView.SelectedAdapterType = type;
						}

						//if ((object)dictionarySpecListView.DictionaryAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
						//dictionarySpecListView.DictionaryAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyDocumentToView(obfuscationConfiguration);
					}
				}
			}
		}

		private void ApplyDocumentToView_MetadataSettings(ObfuscationConfiguration obfuscationConfiguration)
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

		private void ApplyDocumentToView_ObfuscationSettings(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.ConfigurationVersion != null)
			{
				this.View.ObfuscationPartialView.ConfigurationVersion = obfuscationConfiguration.ConfigurationVersion.SafeToString();
				//this.View.ObfuscationPartialView.EngineVersion = obfuscationConfiguration.EngineVersion.SafeToString();
			}

			this.ApplyDocumentToView_AvalancheSettings(obfuscationConfiguration);
			this.ApplyDocumentToView_MetadataSettings(obfuscationConfiguration);
			this.ApplyDocumentToView_DictionarySettings(obfuscationConfiguration);
			this.ApplyDocumentToView_SourceAdapterSettings(obfuscationConfiguration);
			this.ApplyDocumentToView_DestinationAdapterSettings(obfuscationConfiguration);
		}

		private void ApplyDocumentToView_SourceAdapterSettings(ObfuscationConfiguration obfuscationConfiguration)
		{
			Type type;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration != null)
			{
				if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(obfuscationConfiguration.SourceAdapterConfiguration.AdapterAqtn))
				{
					type = obfuscationConfiguration.SourceAdapterConfiguration.GetAdapterType();
					this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType = type;
				}

				//if ((object)this.View.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
				//this.View.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyDocumentToView(obfuscationConfiguration);
			}
		}

		private void ApplyViewToDocument_AvalancheSettings(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.HashConfiguration = obfuscationConfiguration.HashConfiguration ?? new HashConfiguration();
			obfuscationConfiguration.HashConfiguration.Multiplier = this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashMultiplier;
			obfuscationConfiguration.HashConfiguration.Seed = this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashSeed;
		}

		private void ApplyViewToDocument_DestinationAdapterSettings(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.DestinationAdapterConfiguration = obfuscationConfiguration.DestinationAdapterConfiguration ?? new AdapterConfiguration();

			if ((object)this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType != null)
			{
				obfuscationConfiguration.DestinationAdapterConfiguration.AdapterAqtn = this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType.AssemblyQualifiedName;

				//if ((object)this.View.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
				//this.View.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyViewToDocument(obfuscationConfiguration);
			}
		}

		private void ApplyViewToDocument_DictionarySettings(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			foreach (IDictionarySpecListView dictionarySpecView in this.View.ObfuscationPartialView.DictionarySettingsPartialView.DictionarySpecListViews)
			{
				DictionaryConfiguration dictionaryConfiguration;

				//if ((object)dictionarySpecView.DictionaryAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
				//dictionarySpecView.DictionaryAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyViewToDocument(obfuscationConfiguration);

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

		private void ApplyViewToDocument_MetadataSettings(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.TableConfiguration = obfuscationConfiguration.TableConfiguration ?? new TableConfiguration();

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

		private void ApplyViewToDocument_ObfuscationSettings(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.ConfigurationVersion = obfuscationConfiguration.ConfigurationVersion ?? new Version(this.View.ObfuscationPartialView.ConfigurationVersion);
			//obfuscationConfiguration.EngineVersion = obfuscationConfiguration.EngineVersion ?? new Version(this.View.ObfuscationPartialView.EngineVersion);

			this.ApplyViewToDocument_AvalancheSettings(obfuscationConfiguration);
			this.ApplyViewToDocument_MetadataSettings(obfuscationConfiguration);
			this.ApplyViewToDocument_DictionarySettings(obfuscationConfiguration);
			this.ApplyViewToDocument_SourceAdapterSettings(obfuscationConfiguration);
			this.ApplyViewToDocument_DestinationAdapterSettings(obfuscationConfiguration);
		}

		private void ApplyViewToDocument_SourceAdapterSettings(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.SourceAdapterConfiguration = obfuscationConfiguration.SourceAdapterConfiguration ?? new AdapterConfiguration();

			if ((object)this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType != null)
			{
				obfuscationConfiguration.SourceAdapterConfiguration.AdapterAqtn = this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType.AssemblyQualifiedName;

				//if ((object)this.View.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
				//this.View.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyViewToDocument(obfuscationConfiguration);
			}
		}

		protected abstract void InitializeDictionaryAdapterView(IDictionarySpecListView view);

		/*
		 * 		private static void _ApplyDocumentToViewDelimitedText(DelimitedTextAdapterConfiguration delimitedTextAdapterConfiguration, IDelimitedTextAdapterSettingsPartialView delimitedTextAdapterSettingsPartialView)
		{
			if ((object)delimitedTextAdapterConfiguration == null)
				throw new ArgumentNullException("delimitedTextAdapterConfiguration");

			if ((object)delimitedTextAdapterSettingsPartialView == null)
				throw new ArgumentNullException("delimitedTextAdapterSettingsPartialView");

			delimitedTextAdapterSettingsPartialView.TextFilePath = delimitedTextAdapterConfiguration.DelimitedTextFilePath;

			if ((object)delimitedTextAdapterConfiguration.DelimitedTextSpec != null)
			{
				delimitedTextAdapterSettingsPartialView.IsFirstRowHeaders = delimitedTextAdapterConfiguration.DelimitedTextSpec.FirstRecordIsHeader;
				delimitedTextAdapterSettingsPartialView.QuoteValue = EscapeValue(delimitedTextAdapterConfiguration.DelimitedTextSpec.QuoteValue);
				delimitedTextAdapterSettingsPartialView.RecordDelimiter = EscapeValue(delimitedTextAdapterConfiguration.DelimitedTextSpec.RecordDelimiter);
				delimitedTextAdapterSettingsPartialView.FieldDelimiter = EscapeValue(delimitedTextAdapterConfiguration.DelimitedTextSpec.FieldDelimiter);

				delimitedTextAdapterSettingsPartialView.ClearHeaderSpecViews();

				if ((object)delimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs != null)
				{
					foreach (HeaderSpec headerSpec in delimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs)
						delimitedTextAdapterSettingsPartialView.AddHeaderSpecView(headerSpec.HeaderName, headerSpec.FieldType);
				}
			}
		}

		private static void _ApplyViewToDocumentDelimitedText(IDelimitedTextAdapterSettingsPartialView delimitedTextAdapterSettingsPartialView, DelimitedTextAdapterConfiguration delimitedTextAdapterConfiguration)
		{
			if ((object)delimitedTextAdapterSettingsPartialView == null)
				throw new ArgumentNullException("delimitedTextAdapterSettingsPartialView");

			if ((object)delimitedTextAdapterConfiguration == null)
				throw new ArgumentNullException("delimitedTextAdapterConfiguration");

			delimitedTextAdapterConfiguration.DelimitedTextFilePath = delimitedTextAdapterSettingsPartialView.TextFilePath;
			delimitedTextAdapterConfiguration.DelimitedTextSpec.FirstRecordIsHeader = delimitedTextAdapterSettingsPartialView.IsFirstRowHeaders;
			delimitedTextAdapterConfiguration.DelimitedTextSpec.QuoteValue = UnescapeValue(delimitedTextAdapterSettingsPartialView.QuoteValue);
			delimitedTextAdapterConfiguration.DelimitedTextSpec.RecordDelimiter = UnescapeValue(delimitedTextAdapterSettingsPartialView.RecordDelimiter);
			delimitedTextAdapterConfiguration.DelimitedTextSpec.FieldDelimiter = UnescapeValue(delimitedTextAdapterSettingsPartialView.FieldDelimiter);

			foreach (IHeaderSpecListView headerSpecView in delimitedTextAdapterSettingsPartialView.HeaderSpecViews)
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


		



		




			


		*/

		#endregion
	}
}