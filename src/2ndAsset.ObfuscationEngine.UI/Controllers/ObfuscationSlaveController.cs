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
	public sealed class ObfuscationSlaveController : SlaveController<IObfuscationPartialView>
	{
		#region Constructors/Destructors

		public ObfuscationSlaveController()
		{
		}

		#endregion

		#region Methods/Operators

		protected void ApplyDocumentToView(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			this.View.ConfigurationVersion = obfuscationConfiguration.ConfigurationVersion.SafeToString();

			//this.ApplyDocumentToViewAvalanche(obfuscationConfiguration);
			//this.ApplyDocumentToViewDictionary(obfuscationConfiguration);
			//this.ApplyDocumentToViewSource(obfuscationConfiguration);
			//this.ApplyDocumentToViewMetadata(obfuscationConfiguration);
			//this.ApplyDocumentToViewDestination(obfuscationConfiguration);
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
					this.View.DestinationAdapterSettingsPartialView.SelectedAdapterType = type;
				}

				//if ((object)this.View.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
				//this.View.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyDocumentToView(obfuscationConfiguration);
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
					this.View.SourceAdapterSettingsPartialView.SelectedAdapterType = type;
				}

				//if ((object)this.View.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
				//this.View.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyDocumentToView(obfuscationConfiguration);
			}
		}

		public void ApplyViewToDocument(ObfuscationConfiguration obfuscationConfiguration)
		{
			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.ConfigurationVersion = ObfuscationConfiguration.CurrentConfigurationVersion;
			obfuscationConfiguration.EngineVersion = ObfuscationConfiguration.CurrentEngineVersion;

			//this.ApplyViewToDocumentAvalanche(obfuscationConfiguration);
			//this.ApplyViewToDocumentDictionary(obfuscationConfiguration);
			//this.ApplyViewToDocumentSource(obfuscationConfiguration);
			//this.ApplyViewToDocumentMetadata(obfuscationConfiguration);
			//this.ApplyViewToDocumentDestination(obfuscationConfiguration);
		}

		private void ApplyViewToDocumentDestination(ObfuscationConfiguration obfuscationConfiguration)
		{
			Type type = null;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.DestinationAdapterConfiguration = new AdapterConfiguration();

			type = this.View.DestinationAdapterSettingsPartialView.SelectedAdapterType;

			if ((object)type == null)
			{
				// do nothing
			}
			else
			{
				obfuscationConfiguration.DestinationAdapterConfiguration.AdapterAqtn = this.View.DestinationAdapterSettingsPartialView.SelectedAdapterType.AssemblyQualifiedName;

				//if ((object)this.View.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
				//this.View.DestinationAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyViewToDocument(obfuscationConfiguration);
			}
		}

		private void ApplyViewToDocumentSource(ObfuscationConfiguration obfuscationConfiguration)
		{
			Type type = null;

			if ((object)obfuscationConfiguration == null)
				throw new ArgumentNullException("obfuscationConfiguration");

			obfuscationConfiguration.SourceAdapterConfiguration = new AdapterConfiguration();

			type = this.View.SourceAdapterSettingsPartialView.SelectedAdapterType;

			if ((object)type == null)
			{
				// do nothing
			}
			else
			{
				obfuscationConfiguration.SourceAdapterConfiguration.AdapterAqtn = this.View.SourceAdapterSettingsPartialView.SelectedAdapterType.AssemblyQualifiedName;

				//if ((object)this.View.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView != null)
				//this.View.SourceAdapterSettingsPartialView.CurrentAdapterSpecificSettingsPartialView.ApplyViewToDocument(obfuscationConfiguration);
			}
		}

		public override void InitializeView(IObfuscationPartialView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);

			this.View.ConfigurationVersion = ObfuscationConfiguration.CurrentConfigurationVersion.ToString();
		}

		#endregion
	}
}