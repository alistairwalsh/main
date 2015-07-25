/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public sealed class DictionarySettingsSlaveController : SlaveController<IDictionarySettingsPartialView>
	{
		#region Constructors/Destructors

		public DictionarySettingsSlaveController()
		{
		}

		#endregion

		#region Methods/Operators

		public void ApplyDocumentToView(ObfuscationConfiguration obfuscationConfiguration)
		{
			IDictionarySpecListView dictionarySpecListView;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			if ((object)obfuscationConfiguration.DictionaryConfigurations != null)
			{
				foreach (DictionaryConfiguration dictionaryConfiguration in obfuscationConfiguration.DictionaryConfigurations)
				{
					Type type = null;

					dictionarySpecListView = this.View.AddDictionarySpecView(dictionaryConfiguration.DictionaryId, dictionaryConfiguration.PreloadEnabled, dictionaryConfiguration.RecordCount, null);

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

		public void ApplyViewToDocument(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			foreach (IDictionarySpecListView dictionarySpecView in this.View.DictionarySpecListViews)
			{
				//DictionaryConfiguration dictionaryConfiguration;

				//if ((object)dictionarySpecView.DictionaryAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
				//dictionarySpecView.DictionaryAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyViewToDocument(obfuscationConfiguration);

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

		protected void InitializeDictionaryAdapterView(IDictionarySpecListView view)
		{
			// ???
		}

		public override void InitializeView(IDictionarySettingsPartialView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);
		}

		#endregion
	}
}