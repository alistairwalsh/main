/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using TextMetal.Middleware.Common;
using TextMetal.Middleware.Common.Utilities;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.ObfuscationEngine.Core;
using _2ndAsset.ObfuscationEngine.Core.Adapter;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Support;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

using Message = TextMetal.Middleware.Common.Message;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers
{
	public sealed class ObfuscationController : BaseController<IObfuscationView>
	{
		#region Constructors/Destructors

		public ObfuscationController()
		{
		}

		#endregion

		#region Fields/Constants

		private const string CR = "{CR}";
		private const string CRLF = "{CRLF}";
		private const string CURRENT_CONFIGURATION_NAMESPACE = "http://www.2ndAsset.com/cfg/v0.1.0";
		private const string LF = "{LF}";
		private const string TAB = "{TAB}";

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

		private static IEnumerable<IDictionary<string, object>> WrapRecordCounter(IEnumerable<IDictionary<string, object>> records, Action<long, bool, double> recordProcessCallback)
		{
			long recordCount = 0;
			DateTime startUtc;

			startUtc = DateTime.UtcNow;

			if ((object)records == null)
				throw new ArgumentNullException("records");

			foreach (IDictionary<string, object> record in records)
			{
				recordCount++;

				if ((recordCount % 1000) == 0)
				{
					//Thread.Sleep(250);
					if ((object)recordProcessCallback != null)
						recordProcessCallback(recordCount, false, (DateTime.UtcNow - startUtc).TotalSeconds);
					Application.DoEvents();
				}

				yield return record;
			}

			if ((object)recordProcessCallback != null)
				recordProcessCallback(recordCount, true, (DateTime.UtcNow - startUtc).TotalSeconds);
		}

		private void ApplyDocumentToView(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			this.View.ConfigurationNamespace = obfuscationConfiguration.ConfigurationNamespace ?? CURRENT_CONFIGURATION_NAMESPACE;

			this.ApplyDocumentToViewSource(obfuscationConfiguration);
			this.ApplyDocumentToViewDestination(obfuscationConfiguration);

			if ((object)obfuscationConfiguration.HashConfiguration != null)
			{
				this.View.AvalancheSettings.HashMultiplier = obfuscationConfiguration.HashConfiguration.Multiplier;
				this.View.AvalancheSettings.HashSeed = obfuscationConfiguration.HashConfiguration.Seed;
			}

			if ((object)obfuscationConfiguration.TableConfiguration != null &&
				(object)obfuscationConfiguration.TableConfiguration.ColumnConfigurations != null)
			{
				foreach (ColumnConfiguration columnConfiguration in obfuscationConfiguration.TableConfiguration.ColumnConfigurations)
					this.View.MetadataSettings.AddMetaColumnSpecView(columnConfiguration.ColumnName, columnConfiguration.IsColumnNullable, columnConfiguration.ObfuscationStrategy, columnConfiguration.DictionaryReference, columnConfiguration.ExtentValue);
			}

			if ((object)obfuscationConfiguration.DictionaryConfigurations != null)
			{
				foreach (DictionaryConfiguration dictionaryConfiguration in obfuscationConfiguration.DictionaryConfigurations)
					this.View.DictionarySettings.AddDictionarySpecView(dictionaryConfiguration.DictionaryId, dictionaryConfiguration.PreloadEnabled, dictionaryConfiguration.RecordCount);
			}
		}

		private void ApplyDocumentToViewAdoNetDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration != null)
			{
				this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ConnectionString = obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ConnectionString;
				this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ConnectionType = obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.GetConnectionType();

				this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.PreExecuteCommandText = obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandText;
				this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.PreExecuteCommandType = obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandType;

				this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandText = obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText;
				this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandType = obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType;

				this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.PostExecuteCommandText = obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandText;
				this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.PostExecuteCommandType = obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandType;
			}
		}

		private void ApplyDocumentToViewAdoNetSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration != null)
			{
				this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ConnectionString = obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ConnectionString;
				this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ConnectionType = obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.GetConnectionType();

				this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.PreExecuteCommandText = obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandText;
				this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.PreExecuteCommandType = obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandType;

				this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandText = obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText;
				this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandType = obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType;

				this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.PostExecuteCommandText = obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandText;
				this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.PostExecuteCommandType = obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandType;
			}
		}

		private void ApplyDocumentToViewDelimitedTextDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration != null)
			{
				this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.TextFilePath = obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath;

				if ((object)obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec != null)
				{
					this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.IsFirstRowHeaders = obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.FirstRecordIsHeader;
					this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.QuoteValue = EscapeValue(obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.QuoteValue);
					this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.RecordDelimiter = EscapeValue(obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.RecordDelimiter);
					this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.FieldDelimiter = EscapeValue(obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.FieldDelimiter);

					this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.ClearHeaderSpecViews();

					if ((object)obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs != null)
					{
						foreach (HeaderSpec headerSpec in obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs)
							this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.AddHeaderSpecView(headerSpec.HeaderName, headerSpec.FieldType);
					}
				}
			}
		}

		private void ApplyDocumentToViewDelimitedTextSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration != null)
			{
				this.View.SourceAdapterSettings.DelTextAdapterSettingsView.TextFilePath = obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath;

				if ((object)obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec != null)
				{
					this.View.SourceAdapterSettings.DelTextAdapterSettingsView.IsFirstRowHeaders = obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.FirstRecordIsHeader;
					this.View.SourceAdapterSettings.DelTextAdapterSettingsView.QuoteValue = EscapeValue(obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.QuoteValue);
					this.View.SourceAdapterSettings.DelTextAdapterSettingsView.RecordDelimiter = EscapeValue(obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.RecordDelimiter);
					this.View.SourceAdapterSettings.DelTextAdapterSettingsView.FieldDelimiter = EscapeValue(obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.FieldDelimiter);

					this.View.SourceAdapterSettings.DelTextAdapterSettingsView.ClearHeaderSpecViews();

					if ((object)obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs != null)
					{
						foreach (HeaderSpec headerSpec in obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs)
							this.View.SourceAdapterSettings.DelTextAdapterSettingsView.AddHeaderSpecView(headerSpec.HeaderName, headerSpec.FieldType);
					}
				}
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
					this.View.DestinationAdapterSettings.SelectedAdapterType = type;
				}

				if ((object)type == null)
				{
					// do nothing
				}
				else if (type == typeof(DelimitedTextDestinationAdapter))
					this.ApplyDocumentToViewDelimitedTextDestination(obfuscationConfiguration);
				else if (type == typeof(SqlServerBulkAdoNetDestinationAdapter) ||
						type == typeof(UpdateAdoNetDestinationAdapter))
					this.ApplyDocumentToViewAdoNetDestination(obfuscationConfiguration);
				else
				{
					// do nothing
				}
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

		private ObfuscationConfiguration ApplyViewToDocument()
		{
			ObfuscationConfiguration obfuscationConfiguration;

			obfuscationConfiguration = new ObfuscationConfiguration();
			obfuscationConfiguration.ConfigurationNamespace = CURRENT_CONFIGURATION_NAMESPACE;

			this.ApplyViewToDocumentSource(obfuscationConfiguration);
			this.ApplyViewToDocumentDestination(obfuscationConfiguration);

			obfuscationConfiguration.HashConfiguration = new HashConfiguration();
			obfuscationConfiguration.HashConfiguration.Multiplier = this.View.AvalancheSettings.HashMultiplier;
			obfuscationConfiguration.HashConfiguration.Seed = this.View.AvalancheSettings.HashSeed;

			obfuscationConfiguration.TableConfiguration = new TableConfiguration();

			foreach (IMetaColumnSpecView metaColumnSpecView in this.View.MetadataSettings.MetaColumnSpecViews)
			{
				ColumnConfiguration columnConfiguration;

				columnConfiguration = new ColumnConfiguration()
									{
										ColumnName = metaColumnSpecView.ColumnName ?? string.Empty,
										ObfuscationStrategy = metaColumnSpecView.ObfuscationStrategy ?? ObfuscationStrategy.None,
										DictionaryReference = metaColumnSpecView.DictionaryRef ?? string.Empty,
										ExtentValue = metaColumnSpecView.ExtentValue
									};

				obfuscationConfiguration.TableConfiguration.ColumnConfigurations.Add(columnConfiguration);
			}

			foreach (IDictionarySpecView dictionarySpecView in this.View.DictionarySettings.DictionarySpecViews)
			{
				DictionaryConfiguration dictionaryConfiguration;

				dictionaryConfiguration = new DictionaryConfiguration()
										{
											DictionaryId = dictionarySpecView.DictionaryId,
											PreloadEnabled = dictionarySpecView.PreloadEnabled,
											RecordCount = dictionarySpecView.RecordCount,
											DictionaryAdapterConfiguration = new AdapterConfiguration()
																			{
																			}
										};

				obfuscationConfiguration.DictionaryConfigurations.Add(dictionaryConfiguration);
			}

			return obfuscationConfiguration;
		}

		private void ApplyViewToDocumentAdoNetDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration = new AdoNetAdapterConfiguration();

			if ((object)this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ConnectionType != null)
				obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ConnectionAqtn = this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ConnectionType.AssemblyQualifiedName;

			obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ConnectionString = this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ConnectionString;

			obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandText = this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.PreExecuteCommandText;
			obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandType = this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.PreExecuteCommandType;

			obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText = this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandText;
			obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType = this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandType;

			obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandText = this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.PostExecuteCommandText;
			obfuscationConfiguration.DestinationAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandType = this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.PostExecuteCommandType;
		}

		private void ApplyViewToDocumentAdoNetSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration = new AdoNetAdapterConfiguration();

			if ((object)this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ConnectionType != null)
				obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ConnectionAqtn = this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ConnectionType.AssemblyQualifiedName;

			obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ConnectionString = this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ConnectionString;

			obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandText = this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.PreExecuteCommandText;
			obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.PreExecuteCommandType = this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.PreExecuteCommandType;

			obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandText = this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandText;
			obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.ExecuteCommandType = this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandType;

			obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandText = this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.PostExecuteCommandText;
			obfuscationConfiguration.SourceAdapterConfiguration.AdoNetAdapterConfiguration.PostExecuteCommandType = this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.PostExecuteCommandType;
		}

		private void ApplyViewToDocumentDelimitedTextDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration = new DelimitedTextAdapterConfiguration();
			obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath = this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.TextFilePath;
			obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec = new DelimitedTextSpec();
			obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.FirstRecordIsHeader = this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.IsFirstRowHeaders;
			obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.QuoteValue = UnescapeValue(this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.QuoteValue);
			obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.RecordDelimiter = UnescapeValue(this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.RecordDelimiter);
			obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.FieldDelimiter = UnescapeValue(this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.FieldDelimiter);

			foreach (IHeaderSpecView headerSpecView in this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.HeaderSpecViews)
			{
				HeaderSpec headerSpec;

				headerSpec = new HeaderSpec()
							{
								FieldType = headerSpecView.FieldType.GetValueOrDefault(),
								HeaderName = headerSpecView.HeaderName
							};

				obfuscationConfiguration.DestinationAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs.Add(headerSpec);
			}
		}

		private void ApplyViewToDocumentDelimitedTextSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration = new DelimitedTextAdapterConfiguration();
			obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextFilePath = this.View.SourceAdapterSettings.DelTextAdapterSettingsView.TextFilePath;
			obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec = new DelimitedTextSpec();
			obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.FirstRecordIsHeader = this.View.SourceAdapterSettings.DelTextAdapterSettingsView.IsFirstRowHeaders;
			obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.QuoteValue = UnescapeValue(this.View.SourceAdapterSettings.DelTextAdapterSettingsView.QuoteValue);
			obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.RecordDelimiter = UnescapeValue(this.View.SourceAdapterSettings.DelTextAdapterSettingsView.RecordDelimiter);
			obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.FieldDelimiter = UnescapeValue(this.View.SourceAdapterSettings.DelTextAdapterSettingsView.FieldDelimiter);

			foreach (IHeaderSpecView headerSpecView in this.View.SourceAdapterSettings.DelTextAdapterSettingsView.HeaderSpecViews)
			{
				HeaderSpec headerSpec;

				headerSpec = new HeaderSpec()
							{
								FieldType = headerSpecView.FieldType.GetValueOrDefault(),
								HeaderName = headerSpecView.HeaderName
							};

				obfuscationConfiguration.SourceAdapterConfiguration.DelimitedTextAdapterConfiguration.DelimitedTextSpec.HeaderSpecs.Add(headerSpec);
			}
		}

		private void ApplyViewToDocumentDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			Type type = null;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.DestinationAdapterConfiguration = new AdapterConfiguration();

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
				else if (type == typeof(SqlServerBulkAdoNetDestinationAdapter) ||
						type == typeof(UpdateAdoNetDestinationAdapter))
					this.ApplyViewToDocumentAdoNetDestination(obfuscationConfiguration);
				else
				{
					// do nothing
				}
			}
		}

		private void ApplyViewToDocumentSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			Type type = null;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.SourceAdapterConfiguration = new AdapterConfiguration();

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

		[DispatchActionUri(Uri = "action://obfuscation/adapter-settings/ado-net/browse-database-connection")]
		public void BrowseDatabaseConnection(IAdoNetAdapterSettingsView partialView, object context)
		{
			Type connectionType;
			string connectionString;

			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			connectionType = partialView.ConnectionType;
			connectionString = partialView.ConnectionString;

			if ((object)partialView == this.View.SourceAdapterSettings.AdoNetAdapterSettingsView)
			{
				if (!this.View.TryGetDatabaseConnection(ref connectionType, ref connectionString))
					return;
			}
			else if ((object)partialView == this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView)
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
		public void BrowseFileSystemLocation(IDelTextAdapterSettingsView partialView, object context)
		{
			string filePath;

			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)partialView == this.View.SourceAdapterSettings.DelTextAdapterSettingsView)
			{
				if (!this.View.TryGetOpenFilePath(out filePath))
					return;
			}
			else if ((object)partialView == this.View.DestinationAdapterSettings.DelTextAdapterSettingsView)
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

		[DispatchActionUri(Uri = "action://obfuscation/execute")]
		public void ExecuteObfuscation(IPartialView partialView, object context)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<IDictionary<string, object>> sourceDataEnumerable;
			IEnumerable<Message> messages;

			this.View.Execution.RecordCount = null;
			this.View.Execution.IsComplete = null;
			this.View.Execution.DurationSeconds = null;

			obfuscationConfiguration = this.ApplyViewToDocument();

			messages = obfuscationConfiguration.Validate();

			if ((object)messages != null && messages.Any())
			{
				this.View.ShowMessages(messages, "Configuration validation on execution", false);

				return;
			}

			using (ISourceAdapter sourceAdapter = (ISourceAdapter)Activator.CreateInstance(obfuscationConfiguration.SourceAdapterConfiguration.GetAdapterType()))
			{
				sourceAdapter.Initialize(obfuscationConfiguration);

				using (IDestinationAdapter destinationAdapter = (IDestinationAdapter)Activator.CreateInstance(obfuscationConfiguration.DestinationAdapterConfiguration.GetAdapterType()))
				{
					destinationAdapter.Initialize(obfuscationConfiguration);
					destinationAdapter.UpstreamMetadata = sourceAdapter.UpstreamMetadata;

					sourceDataEnumerable = WrapRecordCounter(sourceAdapter.PullData(obfuscationConfiguration.TableConfiguration), (count, complete, duration) =>
																																{
																																	this.View.Execution.RecordCount = count;
																																	this.View.Execution.IsComplete = complete;
																																	this.View.Execution.DurationSeconds = duration;
																																});
					destinationAdapter.PushData(obfuscationConfiguration.TableConfiguration, sourceDataEnumerable);
				}
			}

			this.View.ShowAlert("Done.");
		}

		public override void InitializeView(IObfuscationView view)
		{
			IList<IListItem<Type>> typeListItems;
			IList<IListItem<CommandType?>> commandTypeListItems;

			//if ((object)view == null)
			//throw new ArgumentNullException("view");

			base.InitializeView(view);

			this.View.ViewText = string.Format("{0}", this.View.FilePath.SafeToString(null, "<new>"));

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(DelimitedTextSourceAdapter), "Delimited Text File (source)"));
			typeListItems.Add(new ListItem<Type>(typeof(AdoNetSourceAdapter), "ADO.NET DB Provider (source)"));

			this.View.SourceAdapterSettings.AdapterTypes = typeListItems;
			this.View.SourceAdapterSettings.SelectedAdapterType = null;
			this.View.SourceAdapterSettings.DelTextAdapterSettingsView.IsActiveSettings = false;
			this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.IsActiveSettings = false;

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(OleDbConnection), "OleDbConnection (source)"));
			typeListItems.Add(new ListItem<Type>(typeof(OdbcConnection), "OdbcConnection (source)"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlConnection), "SqlConnection (source)"));

			this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ConnectionTypes = typeListItems;
			this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ConnectionType = null;

			commandTypeListItems = new List<IListItem<CommandType?>>();
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.Text, "Text (source)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.StoredProcedure, "Stored Procedure (source)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.TableDirect, "Table Direct (source)"));

			this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.CommandTypes = commandTypeListItems;
			this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandType = null;

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(DelimitedTextDestinationAdapter), "Delimited Text File (destination)"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlServerBulkAdoNetDestinationAdapter), "SQL Server Bulk Provider (destination)"));

			this.View.DestinationAdapterSettings.AdapterTypes = typeListItems;
			this.View.DestinationAdapterSettings.SelectedAdapterType = null;
			this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.IsActiveSettings = false;
			this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.IsActiveSettings = false;

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(OleDbConnection), "OleDbConnection (destination)"));
			typeListItems.Add(new ListItem<Type>(typeof(OdbcConnection), "OdbcConnection (destination)"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlConnection), "SqlConnection (destination)"));

			this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ConnectionTypes = typeListItems;
			this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ConnectionType = null;

			commandTypeListItems = new List<IListItem<CommandType?>>();
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.Text, "Text (destination)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.StoredProcedure, "Stored Procedure (destination)"));
			commandTypeListItems.Add(new ListItem<CommandType?>(CommandType.TableDirect, "Table Direct (destination)"));

			this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.CommandTypes = commandTypeListItems;
			this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandType = null;

			this.View.ConfigurationNamespace = CURRENT_CONFIGURATION_NAMESPACE;

			this.View.StatusText = "Ready";
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
		public void RefreshDelimitedTextFieldSpecs(IDelTextAdapterSettingsView partialView, object context)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;
			bool? result;

			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)partialView == this.View.SourceAdapterSettings.DelTextAdapterSettingsView)
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

				using (ISourceAdapter sourceAdapter = (ISourceAdapter)Activator.CreateInstance(obfuscationConfiguration.SourceAdapterConfiguration.GetAdapterType()))
				{
					sourceAdapter.Initialize(obfuscationConfiguration);

					if ((object)sourceAdapter.UpstreamMetadata != null)
					{
						this.View.SourceAdapterSettings.DelTextAdapterSettingsView.ClearHeaderSpecViews();

						foreach (MetaColumn metaColumn in sourceAdapter.UpstreamMetadata)
						{
							var headerSpec = (HeaderSpec)metaColumn.Tag;
							this.View.SourceAdapterSettings.DelTextAdapterSettingsView.AddHeaderSpecView(headerSpec.HeaderName, headerSpec.FieldType);
						}
					}
				}
			}
			else if ((object)partialView == this.View.DestinationAdapterSettings.DelTextAdapterSettingsView)
				this.View.ShowAlert("Not implemented in this release.");
			else
				throw new InvalidOperationException(string.Format(partialView.GetType().FullName));
		}

		[DispatchActionUri(Uri = "action://obfuscation/metadata-settings/refresh-meta-column-specs")]
		public void RefreshMetaColumnSpecs(IMetadataSettingsView partialView, object context)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;
			bool? result;

			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)partialView == this.View.MetadataSettings)
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

				using (ISourceAdapter sourceAdapter = (ISourceAdapter)Activator.CreateInstance(obfuscationConfiguration.SourceAdapterConfiguration.GetAdapterType()))
				{
					sourceAdapter.Initialize(obfuscationConfiguration);

					if ((object)sourceAdapter.UpstreamMetadata != null)
					{
						this.View.MetadataSettings.ClearMetaColumnSpecViews();

						foreach (MetaColumn metaColumn in sourceAdapter.UpstreamMetadata)
							this.View.MetadataSettings.AddMetaColumnSpecView(metaColumn.ColumnName, metaColumn.IsNullable, ObfuscationStrategy.None, string.Empty, null);
					}
				}
			}
			else
				throw new InvalidOperationException(string.Format(partialView.GetType().FullName));
		}

		[DispatchActionUri(Uri = "action://obfuscation/hash-settings/regenerate-all")]
		public void RegenerateHashSettings(IAvalancheSettingsView partialView, object context)
		{
			bool result;
			const long HASH_MULTIPLIER = 33;
			const long HASH_SEED = 5381;

			result = this.View.ShowConfirm("Proceed with hash regeneration?", Severity.Information);

			if (!result)
				return;

			this.View.AvalancheSettings.HashMultiplier = HASH_MULTIPLIER;
			this.View.AvalancheSettings.HashSeed = HASH_SEED;
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

			if ( /*!DataTypeFascade.Instance.IsNullOrWhiteSpace(this.View.FilePath) &&*/
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
				obfuscationConfiguration = ObfuscationMixIn.FromJsonFile<ObfuscationConfiguration>(documentFilePath);
			}
			else
			{
				// just create new
				obfuscationConfiguration = new ObfuscationConfiguration()
											{
												//SourceAdapterAqtn = this.View.SourceAdapterSettings.SelectedAdapterType.FullName,
												//DestinationAdapterAqtn = this.View.DestinationAdapterSettings.SelectedAdapterType.FullName
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
				ObfuscationMixIn.ToJsonFile<ObfuscationConfiguration>(obfuscationConfiguration, documentFilePath);
				return true;
			}

			return false;
		}

		[DispatchActionUri(Uri = "action://obfuscation/adapter/update")]
		public void UpdateAdapter(IPartialView partialView, object context)
		{
			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");

			if ((object)partialView == this.View.SourceAdapterSettings)
			{
				this.View.SourceAdapterSettings.DelTextAdapterSettingsView.IsActiveSettings = false;
				this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.IsActiveSettings = false;
				this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.IsConnectionTypeReadOnly = false;
				this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.IsCommandTypeReadOnly = false;

				if ((object)this.View.SourceAdapterSettings.SelectedAdapterType == null)
				{
					// do nothing
				}
				else if (this.View.SourceAdapterSettings.SelectedAdapterType == typeof(DelimitedTextSourceAdapter))
					this.View.SourceAdapterSettings.DelTextAdapterSettingsView.IsActiveSettings = true;
				else if (this.View.SourceAdapterSettings.SelectedAdapterType == typeof(AdoNetSourceAdapter))
					this.View.SourceAdapterSettings.AdoNetAdapterSettingsView.IsActiveSettings = true;
				else
					throw new InvalidOperationException(string.Format("Unrecognized source adapter UI-view AQTN '{0}'.", partialView.GetType().FullName));
			}
			else if ((object)partialView == this.View.DestinationAdapterSettings)
			{
				this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.IsActiveSettings = false;
				this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.IsActiveSettings = false;
				this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.IsConnectionTypeReadOnly = false;
				this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.IsCommandTypeReadOnly = false;

				if ((object)this.View.DestinationAdapterSettings.SelectedAdapterType == null)
				{
					// do nothing
				}
				else if (this.View.DestinationAdapterSettings.SelectedAdapterType == typeof(DelimitedTextDestinationAdapter))
					this.View.DestinationAdapterSettings.DelTextAdapterSettingsView.IsActiveSettings = true;
				else if (this.View.DestinationAdapterSettings.SelectedAdapterType == typeof(SqlServerBulkAdoNetDestinationAdapter))
				{
					this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.IsActiveSettings = true;
					this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ConnectionType = typeof(SqlConnection);
					this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.IsConnectionTypeReadOnly = true;
					this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.ExecuteCommandType = CommandType.TableDirect;
					this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.IsCommandTypeReadOnly = true;
				}
				else if (this.View.DestinationAdapterSettings.SelectedAdapterType == typeof(UpdateAdoNetDestinationAdapter))
					this.View.DestinationAdapterSettings.AdoNetAdapterSettingsView.IsActiveSettings = true;
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