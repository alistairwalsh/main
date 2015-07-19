/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

using Solder.Framework;
using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.ObfuscationEngine.Core;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Destination;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Source;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Hosting.Tool;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public abstract partial class ObfuscationDocumentController<TObfuscationDocumentView> : BaseController<TObfuscationDocumentView>
		where TObfuscationDocumentView : class, IObfuscationDocumentView
	{
		#region Constructors/Destructors

		protected ObfuscationDocumentController()
		{
		}

		#endregion

		#region Fields/Constants

		private const string CR = "{CR}";
		private const string CRLF = "{CRLF}";
		private const string LF = "{LF}";
		private const string TAB = "{TAB}";
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

		private static string EscapeValue(string value)
		{
			if ((object)value == null)
				return null;

			value = value.Replace("\r\n", CRLF);
			value = value.Replace("\r", CR);
			value = value.Replace("\n", LF);
			value = value.Replace("\t", TAB);

			return value;
		}

		private static string UnescapeValue(string value)
		{
			if ((object)value == null)
				return null;

			value = value.Replace(CRLF, "\r\n");
			value = value.Replace(CR, "\r");
			value = value.Replace(LF, "\n");
			value = value.Replace(TAB, "\t");

			return value;
		}

		[DispatchActionUri(Uri = "action://obfuscation/adapter-settings/ado-net/browse-database-connection")]
		public void BrowseDatabaseConnection(IAdoNetAdapterSettingsPartialView partialView, object context)
		{
			Type connectionType;
			string connectionString;

			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			connectionType = partialView.ConnectionType;
			connectionString = partialView.ConnectionString;

			if ((object)partialView == this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView)
			{
				if (!this.View.TryGetDatabaseConnection(ref connectionType, ref connectionString))
					return;
			}
			else if ((object)partialView == this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView)
			{
				if (!this.View.TryGetDatabaseConnection(ref connectionType, ref connectionString))
					return;
			}
			else
				throw new InvalidOperationException(string.Format(partialView.GetType().FullName));

			partialView.ConnectionType = connectionType;
			partialView.ConnectionString = connectionString;
		}

		[DispatchActionUri(Uri = "action://obfuscation/adapter-settings/delimited-text/browse-file-system-location")]
		public void BrowseFileSystemLocation(IDelTextAdapterSettingsPartialView partialView, object context)
		{
			string filePath;

			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)partialView == this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.DelTextAdapterSettingsPartialView)
			{
				if (!this.View.TryGetOpenFilePath(out filePath))
					return;
			}
			else if ((object)partialView == this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.DelTextAdapterSettingsPartialView)
			{
				if (!this.View.TryGetSaveFilePath(out filePath))
					return;
			}
			else
				throw new InvalidOperationException(string.Format(partialView.GetType().FullName));

			partialView.TextFilePath = filePath;
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

		[DispatchActionUri(Uri = "action://obfuscation/adapter-settings/delimited-text/refresh-field-specs")]
		public void RefreshDelimitedTextFieldSpecs(IDelTextAdapterSettingsPartialView partialView, object context)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;
			bool? result;

			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)partialView == this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.DelTextAdapterSettingsPartialView)
			{
				obfuscationConfiguration = this.ApplyViewToDocument();

				messages = obfuscationConfiguration.Validate();

				if ((object)messages != null && messages.Any())
				{
					result = this.View.ShowMessages(messages, "Configuration validation on refresh delimited text field spec", true);

					if (!(result ?? false))
					{
						//this.View.CloseView(null);
						return;
					}
				}

				bool succeeded;
				IEnumerable<IMetaColumn> metaColumns;

				using (IToolHost toolHost = new ToolHost())
					succeeded = toolHost.TryGetUpstreamMetadata(obfuscationConfiguration, out metaColumns);

				if (succeeded && (object)metaColumns != null)
				{
					this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.DelTextAdapterSettingsPartialView.ClearHeaderSpecViews();

					foreach (IMetaColumn metaColumn in metaColumns)
					{
						var headerSpec = (HeaderSpec)metaColumn.TagContext;
						this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.DelTextAdapterSettingsPartialView.AddHeaderSpecView(headerSpec.HeaderName, headerSpec.FieldType);
					}
				}
			}
			else if ((object)partialView == this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.DelTextAdapterSettingsPartialView)
				this.View.ShowAlert("Not implemented in this release.");
			else
				throw new InvalidOperationException(string.Format(partialView.GetType().FullName));
		}

		[DispatchActionUri(Uri = "action://obfuscation/metadata-settings/refresh-meta-column-specs")]
		public void RefreshMetaColumnSpecs(IMetadataSettingsPartialView partialView, object context)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;
			bool? result;

			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)partialView == this.View.ObfuscationPartialView.MetadataSettingsPartialView)
			{
				obfuscationConfiguration = this.ApplyViewToDocument();

				messages = obfuscationConfiguration.Validate();

				if ((object)messages != null && messages.Any())
				{
					result = this.View.ShowMessages(messages, "Configuration validation on refresh delimited text field spec", true);

					if (!(result ?? false))
					{
						//this.View.ObfuscationPartialView.CloseView(null);
						return;
					}
				}

				bool succeeded;
				IEnumerable<IMetaColumn> metaColumns;

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
			else
				throw new InvalidOperationException(string.Format(partialView.GetType().FullName));
		}

		[DispatchActionUri(Uri = "action://obfuscation/hash-settings/regenerate-all")]
		public void RegenerateHashSettings(IAvalancheSettingsPartialView partialView, object context)
		{
			bool result;
			const long HASH_MULTIPLIER = 33;
			const long HASH_SEED = 5381;

			result = this.View.ShowConfirm("Proceed with hash regeneration?", Severity.Information);

			if (!result)
				return;

			this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashMultiplier = HASH_MULTIPLIER;
			this.View.ObfuscationPartialView.AvalancheSettingsPartialView.HashSeed = HASH_SEED;
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
				obfuscationConfiguration = new ObfuscationConfiguration()
											{
												//SourceAdapterAqtn = this.View.ObfuscationPartialView.SourceAdapterSettings.SelectedAdapterType.FullName,
												//DestinationAdapterAqtn = this.View.ObfuscationPartialView.DestinationAdapterSettings.SelectedAdapterType.FullName
											};
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

		[DispatchActionUri(Uri = "action://obfuscation/adapter/update")]
		public void UpdateAdapter(IPartialView partialView, object context)
		{
			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)partialView == this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView)
			{
				this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.DelTextAdapterSettingsPartialView.IsActiveSettings = false;
				this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsActiveSettings = false;
				this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsConnectionTypeReadOnly = false;
				this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsCommandTypeReadOnly = false;

				if ((object)this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType == null)
				{
					// do nothing
				}
				else if (this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType == typeof(NullSourceAdapter))
				{
					// do nothing
				}
				else if (this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType == typeof(DelimitedTextSourceAdapter))
					this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.DelTextAdapterSettingsPartialView.IsActiveSettings = true;
				else if (this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType == typeof(AdoNetSourceAdapter))
					this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsActiveSettings = true;
				else
					throw new InvalidOperationException(string.Format("Unrecognized source adapter UI-view AQTN '{0}'.", partialView.GetType().FullName));
			}
			else if ((object)partialView == this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView)
			{
				this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.DelTextAdapterSettingsPartialView.IsActiveSettings = false;
				this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsActiveSettings = false;
				this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsConnectionTypeReadOnly = false;
				this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsCommandTypeReadOnly = false;

				if ((object)this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType == null)
				{
					// do nothing
				}
				else if (this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType == typeof(NullDestinationAdapter))
				{
					// do nothing
				}
				else if (this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType == typeof(DelimitedTextDestinationAdapter))
					this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.DelTextAdapterSettingsPartialView.IsActiveSettings = true;
				else if (this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType == typeof(SqlBulkCopyAdoNetDestinationAdapter))
				{
					this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsActiveSettings = true;
					this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.ConnectionType = typeof(SqlConnection);
					this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsConnectionTypeReadOnly = true;
					this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.ExecuteCommandType = CommandType.TableDirect;
					this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsCommandTypeReadOnly = true;
				}
				else if (this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType == typeof(RecordCommandAdoNetDestinationAdapter))
					this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdoNetAdapterSettingsPartialView.IsActiveSettings = true;
				else
					throw new InvalidOperationException(string.Format("Unrecognized destination adapter UI-view AQTN '{0}'.", partialView.GetType().FullName));
			}
			else
				throw new InvalidOperationException(string.Format(partialView.GetType().FullName));

			this.View.RefreshView();
		}

		#endregion
	}
}