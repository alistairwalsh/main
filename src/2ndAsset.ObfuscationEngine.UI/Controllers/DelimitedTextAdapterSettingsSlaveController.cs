/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Solder.Framework;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.Core;
using _2ndAsset.ObfuscationEngine.Core.Adapter;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Config.Adapters;
using _2ndAsset.ObfuscationEngine.Core.Hosting.Tool;
using _2ndAsset.ObfuscationEngine.Core.Support.DelimitedText;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public sealed class DelimitedTextAdapterSettingsSlaveController : SlaveController<IDelimitedTextAdapterSettingsPartialView>
	{
		#region Constructors/Destructors

		public DelimitedTextAdapterSettingsSlaveController()
		{
		}

		#endregion

		#region Fields/Constants

		private const string CR = "{CR}";
		private const string CRLF = "{CRLF}";
		private const string LF = "{LF}";
		private const string TAB = "{TAB}";

		#endregion

		#region Methods/Operators

		private static void _ApplyDocumentToViewDelimitedText(DelimitedTextAdapterConfiguration delimitedTextAdapterConfiguration, IDelimitedTextAdapterSettingsPartialView delimitedTextAdapterSettingsPartialView)
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

		private ObfuscationConfiguration ApplyViewToDocument()
		{
			return new ObfuscationConfiguration(); // TODO
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

		public void BrowseFileSystemLocation()
		{
			string filePath;

			filePath = this.View.TextFilePath;

			switch (this.View.CoreGetParent<IAdapterSettingsPartialView>().AdapterDirection)
			{
				case AdapterDirection.Source:
				case AdapterDirection.Dictionary:
					if (!this.View.CoreGetParent<IObfuscationDocumentView>().TryGetOpenFilePath(out filePath))
						return;
					break;
				case AdapterDirection.Destination:
					if (!this.View.CoreGetParent<IObfuscationDocumentView>().TryGetSaveFilePath(out filePath))
						return;
					break;
				default:
					break;
			}

			this.View.TextFilePath = filePath;
		}

		public override void InitializeView(IDelimitedTextAdapterSettingsPartialView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);

			this.View.IsActiveSettings = false;
		}

		public void RefreshDelimitedTextFieldSpecs()
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;
			bool? result;

			bool succeeded;
			IEnumerable<IMetaColumn> metaColumns;

			obfuscationConfiguration = this.ApplyViewToDocument();

			messages = obfuscationConfiguration.Validate();

			if ((object)messages != null && messages.Any())
			{
				result = this.View.CoreGetParent<IFullView>().ShowMessages(messages, "Configuration validation on refresh delimited text field spec", true);

				if (!(result ?? false))
				{
					//this.View.CloseView(null);
					return;
				}
			}
			
			using (IToolHost toolHost = new ToolHost())
				succeeded = toolHost.TryGetUpstreamMetadata(obfuscationConfiguration, out metaColumns);

			if (succeeded && (object)metaColumns != null)
			{
				this.View.ClearHeaderSpecViews();

				foreach (IMetaColumn metaColumn in metaColumns)
				{
					var headerSpec = (HeaderSpec)metaColumn.TagContext;
					this.View.AddHeaderSpecView(headerSpec.HeaderName, headerSpec.FieldType);
				}
			}
		}

		#endregion
	}
}