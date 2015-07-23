/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	public class AdapterSpecificConfigurationUserControl<TAdapterSpecificConfigurationPartialView, TAdapterSpecificConfigurationSlaveController> : BaseUserControl<TAdapterSpecificConfigurationPartialView, TAdapterSpecificConfigurationSlaveController>, IAdapterSpecificSettingsPartialView
		where TAdapterSpecificConfigurationPartialView : class, IAdapterSpecificSettingsPartialView
		where TAdapterSpecificConfigurationSlaveController : SlaveController<TAdapterSpecificConfigurationPartialView>, new()
	{
		#region Constructors/Destructors

		public AdapterSpecificConfigurationUserControl()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		bool IAdapterSpecificSettingsPartialView.IsActiveSettings
		{
			get
			{
				return this.Visible;
			}
			set
			{
				this.Visible = value;
			}
		}

		#endregion

		#region Methods/Operators

		void IAdapterSpecificSettingsPartialView.ApplyDocumentToView(ObfuscationConfiguration obfuscationConfiguration)
		{
			throw new NotImplementedException();
		}

		void IAdapterSpecificSettingsPartialView.ApplyViewToDocument(ObfuscationConfiguration obfuscationConfiguration)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}