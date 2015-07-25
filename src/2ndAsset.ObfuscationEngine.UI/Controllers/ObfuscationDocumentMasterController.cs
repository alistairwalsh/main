/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
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

		public void ApplyDocumentToView(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			//this.View.ObfuscationPartialView.ApplyDocumentToView(obfuscationConfiguration);
		}

		public void ApplyViewToDocument(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");
		}

		public void CloseDocument()
		{
			this.View.CloseView(null);
		}

		public override void InitializeView(TObfuscationDocumentView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);
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
		public void RefreshMetaColumnSpecs(IMetadataSettingsPartialView sourceView, object actionContext)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;
			bool? result;
			bool succeeded;
			IEnumerable<IMetaColumn> metaColumns;

			if ((object)sourceView == null)
				throw new ArgumentNullException("sourceView");

			if ((object)sourceView != (object)this.View.ObfuscationPartialView.MetadataSettingsPartialView)
				throw new ArgumentOutOfRangeException("sourceView");

			obfuscationConfiguration = new ObfuscationConfiguration();
			this.ApplyViewToDocument(obfuscationConfiguration);

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

			obfuscationConfiguration = new ObfuscationConfiguration();
			this.ApplyViewToDocument(obfuscationConfiguration);

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
		public void UpdateAdapter(IAdapterSettingsPartialView sourceView, object actionContext)
		{
			if ((object)sourceView == null)
				throw new ArgumentNullException("sourceView");

			Debug.WriteLine(Constants.URI_ADAPTER_UPDATE_EVENT);
		}

		#endregion
	}
}