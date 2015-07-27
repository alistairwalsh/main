/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Diagnostics;

using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public sealed class AdapterSettingsSlaveController : SlaveController<IAdapterSettingsPartialView>
	{
		#region Constructors/Destructors

		public AdapterSettingsSlaveController()
		{
		}

		#endregion

		#region Methods/Operators

		public void ActivateAdapter()
		{
			this.EmitPresentationEvent(Constants.AdapterUpdateEventUri, this.View.SelectedAdapterType);
		}

		public override void InitializeView(IAdapterSettingsPartialView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);
		}

		[DispatchActionUri(Uri = Constants.URI_ADAPTER_UPDATE_EVENT)]
		public void UpdateAdapter(IAdapterSettingsPartialView sourceView, Type actionContext)
		{
			if ((object)sourceView == null)
				throw new ArgumentNullException("sourceView");

			Debug.WriteLine(actionContext);
		}

		#endregion
	}
}