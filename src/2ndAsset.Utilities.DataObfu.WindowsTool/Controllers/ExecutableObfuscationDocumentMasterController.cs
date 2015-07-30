/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Solder.Framework;

using _2ndAsset.Common.WinForms;
using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Destination;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Dictionary;
using _2ndAsset.ObfuscationEngine.Core.Adapter.Source;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Hosting.Tool;
using _2ndAsset.ObfuscationEngine.UI;
using _2ndAsset.ObfuscationEngine.UI.Controllers;
using _2ndAsset.ObfuscationEngine.UI.Views;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers
{
	public sealed class ExecutableObfuscationDocumentMasterController : ObfuscationDocumentMasterController<IExecutableObfuscationDocumentView>
	{
		#region Constructors/Destructors

		public ExecutableObfuscationDocumentMasterController()
		{
		}

		#endregion

		#region Methods/Operators

		[DispatchActionUri(Uri = Constants.URI_EXECUTE_OBFUSCATION_EVENT)]
		public void ExecuteObfuscation(IPartialView sourceView, object actionContext)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;

			this.View.ExecutionPartialView.RecordCount = null;
			this.View.ExecutionPartialView.IsComplete = null;
			this.View.ExecutionPartialView.DurationSeconds = null;

			obfuscationConfiguration = new ObfuscationConfiguration()
										{
											ConfigurationVersion = ObfuscationConfiguration.CurrentConfigurationVersion,
											EngineVersion = ObfuscationConfiguration.CurrentEngineVersion
										};
			this.ApplyViewToDocument(obfuscationConfiguration);

			messages = obfuscationConfiguration.Validate();

			if ((object)messages != null && messages.Any())
			{
				this.View.ExecutionPartialView.IsComplete = true;
				this.View.ShowMessages(messages, "Configuration validation on execution", false);

				return;
			}

			using (IToolHost toolHost = new ToolHost())
			{
				toolHost.Host(obfuscationConfiguration, (count, complete, duration) =>
														{
															this.View.ExecutionPartialView.RecordCount = count;
															this.View.ExecutionPartialView.IsComplete = complete;
															this.View.ExecutionPartialView.DurationSeconds = duration;
														});
			}

			this.View.ShowAlert("Done.");
		}

		protected override void InitializeDictionaryAdapterView(IDictionarySpecListView view)
		{
			IList<IListItem<Type>> typeListItems;

			if ((object)view == null)
				throw new ArgumentNullException("view");

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(DelimitedTextDictionaryAdapter), "Delimited Text File (dictionary)"));
			typeListItems.Add(new ListItem<Type>(typeof(AdoNetDictionaryAdapter), "ADO.NET DB Provider (dictionary)"));

			view.DictionaryAdapterSettingsPartialView.AdapterTypes = typeListItems;
			view.DictionaryAdapterSettingsPartialView.SelectedAdapterType = null;
		}

		public override void InitializeView(IExecutableObfuscationDocumentView view)
		{
			IList<IListItem<Type>> typeListItems;

			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(NullSourceAdapter), "Null/Nop (source)"));
			typeListItems.Add(new ListItem<Type>(typeof(DelimitedTextSourceAdapter), "Delimited Text File (source)"));
			typeListItems.Add(new ListItem<Type>(typeof(AdoNetSourceAdapter), "ADO.NET DB Provider (source)"));

			this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.AdapterTypes = typeListItems;
			this.View.ObfuscationPartialView.SourceAdapterSettingsPartialView.SelectedAdapterType = null;

			typeListItems = new List<IListItem<Type>>();
			typeListItems.Add(new ListItem<Type>(typeof(NullDestinationAdapter), "Null/Nop (destination)"));
			typeListItems.Add(new ListItem<Type>(typeof(DelimitedTextDestinationAdapter), "Delimited Text File (destination)"));
			typeListItems.Add(new ListItem<Type>(typeof(SqlBulkCopyAdoNetDestinationAdapter), "SQL Server Bulk Provider (destination)"));

			this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.AdapterTypes = typeListItems;
			this.View.ObfuscationPartialView.DestinationAdapterSettingsPartialView.SelectedAdapterType = null;

			this.View.StatusText = "Ready";
		}

		#endregion
	}
}