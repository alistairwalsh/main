/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Linq;
using System.Windows.Forms;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.ObfuscationEngine.UI.Controllers;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	public partial class ObfuscationUserControl : _ObfuscationUserControl, IObfuscationPartialView
	{
		#region Constructors/Destructors

		public ObfuscationUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Fields/Constants

		private string configurationVersion;

		#endregion

		#region Properties/Indexers/Events

		IAvalancheSettingsPartialView IObfuscationPartialView.AvalancheSettingsPartialView
		{
			get
			{
				return this.avalancheSettingsUc;
			}
		}

		IAdapterSettingsPartialView IObfuscationPartialView.DestinationAdapterSettingsPartialView
		{
			get
			{
				return this.destinationAdapterSettingsUc;
			}
		}

		IDictionarySettingsPartialView IObfuscationPartialView.DictionarySettingsPartialView
		{
			get
			{
				return this.dictionarySettingsUc;
			}
		}

		IMetadataSettingsPartialView IObfuscationPartialView.MetadataSettingsPartialView
		{
			get
			{
				return this.metadataSettingsUc;
			}
		}

		IAdapterSettingsPartialView IObfuscationPartialView.SourceAdapterSettingsPartialView
		{
			get
			{
				return this.sourceAdapterSettingsUc;
			}
		}

		string IObfuscationPartialView.ConfigurationVersion
		{
			get
			{
				return this.configurationVersion;
			}
			set
			{
				this.configurationVersion = value;
			}
		}

		#endregion

		#region Methods/Operators

		private void ObfuscationUserControl_Load(object sender, EventArgs e)
		{
			foreach (TabPage tabPage in this.tabMain.TabPages.Cast<TabPage>().Reverse())
				tabPage.Show();
		}

		#endregion
	}

	public class _ObfuscationUserControl : BaseUserControl<IObfuscationPartialView, ObfuscationSlaveController>
	{
		#region Constructors/Destructors

		public _ObfuscationUserControl()
		{
		}

		#endregion
	}
}