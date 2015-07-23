/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Solder.Framework;
using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.ObfuscationEngine.Core;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Hosting.Tool;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public abstract class ObfuscationDocumentMasterController<TObfuscationDocumentView> : MasterController<TObfuscationDocumentView>
		where TObfuscationDocumentView : class, IObfuscationDocumentView
	{
		#region Constructors/Destructors

		protected ObfuscationDocumentMasterController()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly Uri adapterSettingsViewUri = new Uri("view://obfuscation/adapter-settings");

		#endregion

		#region Properties/Indexers/Events

		public static Uri AdapterSettingsViewUri
		{
			get
			{
				return adapterSettingsViewUri;
			}
		}

		#endregion

		#region Methods/Operators

		protected void ApplyDocumentToView(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			this.View.ObfuscationPartialView.ConfigurationVersion = obfuscationConfiguration.ConfigurationVersion.SafeToString();

			this.ApplyDocumentToViewAvalanche(obfuscationConfiguration);
			this.ApplyDocumentToViewDictionary(obfuscationConfiguration);
			this.ApplyDocumentToViewSource(obfuscationConfiguration);
			this.ApplyDocumentToViewMetadata(obfuscationConfiguration);
			this.ApplyDocumentToViewDestination(obfuscationConfiguration);
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

				if ((object)this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
					this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyDocumentToView(obfuscationConfiguration);
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

						if ((object)dictionarySpecListView.DictionaryAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
							dictionarySpecListView.DictionaryAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyDocumentToView(obfuscationConfiguration);
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

				if ((object)this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
					this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyDocumentToView(obfuscationConfiguration);
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

		private void ApplyViewToDocumentAvalanche(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.HashConfiguration = new HashConfiguration();
			obfuscationConfiguration.HashConfiguration.Multiplier = this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashMultiplier;
			obfuscationConfiguration.HashConfiguration.Seed = this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashSeed;
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

				if ((object)this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
					this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyViewToDocument(obfuscationConfiguration);
			}
		}

		private void ApplyViewToDocumentDictionary(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			foreach (IDictionarySpecListView dictionarySpecView in this.View.ObfuscationPartialView.DictionarySettingsPartialView.DictionarySpecListViews)
			{
				//DictionaryConfiguration dictionaryConfiguration;

				if ((object)dictionarySpecView.DictionaryAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
					dictionarySpecView.DictionaryAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyViewToDocument(obfuscationConfiguration);

				//dictionaryConfiguration = new DictionaryConfiguration()
				//						{
				//							DictionaryId = dictionarySpecView.DictionaryId,
				//							PreloadEnabled = dictionarySpecView.PreloadEnabled,
				//							RecordCount = dictionarySpecView.RecordCount,
				//							DictionaryAdapterConfiguration = new AdapterConfiguration()
				//						};

				//obfuscationConfiguration.DictionaryConfigurations.Add(dictionaryConfiguration);
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

				if ((object)this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
					this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyViewToDocument(obfuscationConfiguration);
			}
		}

		public void CloseDocument()
		{
			this.View.CloseView(null);
		}

		protected abstract void InitializeDictionaryAdapterView(IDictionarySpecListView view);

		public override void InitializeView(TObfuscationDocumentView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);

			this.View.ObfuscationPartialView.ConfigurationVersion = ObfuscationConfiguration.CurrentConfigurationVersion.ToString();
		}

		public override void ReadyView()
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;
			bool? result;

			base.ReadyView();

			obfuscationConfiguration = this.View.ShowAsync("Loading document...", this.ThreadSafeLoadCurrentDocument, null);

			if ((object)obfuscationConfiguration != null)
			{
				messages = obfuscationConfiguration.Validate();

				if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(this.View.FilePath) &&
					messages.Any())
				{
					result = this.View.ShowMessages(messages, "Obfuscation configuration load validation failed.", true);

					if (!(result ?? false))
					{
						this.View.StatusText = "Document load canceled.";
						//this.View.CloseView(null);
						return;
					}
				}

				this.ApplyDocumentToView(obfuscationConfiguration);
			}
		}

		[DispatchActionUri(Uri = Constants.URI_REFRESH_UPSTREAM_METADATA_COLUMNS_EVENT)]
		public void RefreshMetaColumnSpecs(IMetadataSettingsPartialView partialView, object context)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;
			bool? result;
			bool succeeded;
			IEnumerable<IMetaColumn> metaColumns;

			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)partialView != (object)this.View.ObfuscationPartialView.MetadataSettingsPartialView)
				throw new ArgumentOutOfRangeException("partialView");

			obfuscationConfiguration = this.ApplyViewToDocument();

			messages = obfuscationConfiguration.Validate();

			if ((object)messages != null && messages.Any())
			{
				result = this.View.ShowMessages(messages, "Configuration validation on refresh delimited text field spec", true);

				if (!(result ?? false))
					return;
			}

			using (IToolHost toolHost = new ToolHost())
				succeeded = toolHost.TryGetUpstreamMetadata(obfuscationConfiguration, out metaColumns);

			if (succeeded && (object)metaColumns != null)
			{
				this.View.ObfuscationPartialView.MetadataSettingsPartialView.ClearMetaColumnSpecViews();

				foreach (IMetaColumn metaColumn in metaColumns)
				{
					var headerSpec = (HeaderSpec)metaColumn.TagContext;
					this.View.ObfuscationPartialView.MetadataSettingsPartialView.AddMetaColumnSpecView(metaColumn.ColumnName, string.Empty);
				}
			}
		}

		public bool SaveDocument(bool asCopy)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;
			string filePath;
			bool? result;

			this.View.StatusText = "Document validation started...";

			obfuscationConfiguration = this.ApplyViewToDocument();

			if ((object)obfuscationConfiguration == null)
				throw new InvalidOperationException(string.Format("Invalid document from view encountered."));

			messages = obfuscationConfiguration.Validate();

			if ( /*!DataTypeFascade.Instance.IsNullOrWhiteSpace(this.View.ObfuscationPartialView.FilePath) &&*/
				messages.Any())
			{
				result = this.View.ShowMessages(messages, "Obfuscation configuration save validation failed.", true);

				if (!(result ?? false))
				{
					this.View.StatusText = "Document save canceled.";
					//this.View.CloseView(null);
					return false;
				}
			}

			this.View.StatusText = "Document validation completed successfully.";

			this.View.StatusText = "Document save started...";

			if (asCopy && !DataTypeFascade.Instance.IsNullOrWhiteSpace(this.View.FilePath))
			{
				if (!this.View.ShowConfirm("Do you want to save a copy of the current document?", Severity.Information))
				{
					this.View.StatusText = "Document save canceled.";
					return false;
				}
			}

			if (asCopy)
			{
				// get new file path
				if (!this.View.TryGetSaveFilePath(out filePath))
				{
					this.View.StatusText = "Document save canceled.";
					return false;
				}

				this.View.FilePath = filePath;
			}
			else
			{
				if (DataTypeFascade.Instance.IsNullOrWhiteSpace(this.View.FilePath))
				{
					if (!this.View.TryGetSaveFilePath(out filePath))
					{
						this.View.StatusText = "Document save canceled.";
						return false;
					}

					this.View.FilePath = filePath;
				}
			}

			result = this.View.ShowAsync<bool>("Saving document...", this.ThreadSafeSaveCurrentDocument, obfuscationConfiguration);

			if (!(bool)result)
			{
				this.View.StatusText = "Document save canceled.";
				return false;
			}

			this.View.ViewText = string.Format("{0}", this.View.FilePath.SafeToString(null, "<new>"));
			this.View.StatusText = "Document save completed successfully.";

			return true;
		}

		private ObfuscationConfiguration ThreadSafeLoadCurrentDocument(object parameter)
		{
			string documentFilePath;
			ObfuscationConfiguration obfuscationConfiguration;

			documentFilePath = this.View.FilePath;

			if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(documentFilePath))
			{
				documentFilePath = Path.GetFullPath(documentFilePath);
				obfuscationConfiguration = OxymoronEngine.FromJsonFile<ObfuscationConfiguration>(documentFilePath);
			}
			else
			{
				// just create new
				obfuscationConfiguration = new ObfuscationConfiguration();
			}

			return obfuscationConfiguration;
		}

		private bool ThreadSafeSaveCurrentDocument(object parameter)
		{
			string documentFilePath;
			ObfuscationConfiguration obfuscationConfiguration;

			if ((object)parameter == null)
				throw new ArgumentNullException("parameter");

			obfuscationConfiguration = (ObfuscationConfiguration)parameter;
			documentFilePath = this.View.FilePath;

			if (!DataTypeFascade.Instance.IsNullOrWhiteSpace(documentFilePath))
			{
				documentFilePath = Path.GetFullPath(documentFilePath);
				OxymoronEngine.ToJsonFile<ObfuscationConfiguration>(obfuscationConfiguration, documentFilePath);
				return true;
			}

			return false;
		}

		[DispatchActionUri(Uri = Constants.URI_ADAPTER_UPDATE_EVENT)]
		public void UpdateAdapter(IAdapterSettingsPartialView partialView, object context)
		{
			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			this.View.RefreshView();
		}

		#endregion
	}
}