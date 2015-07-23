/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;

using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;
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
			IDictionary<string, object> context;
			IAdapterSpecificSettingsPartialView adapterSpecificSettingsPartialView;

			context = new Dictionary<string, object>();
			adapterSpecificSettingsPartialView = this.View.CurrentAdapterSpecificSettingsPartialView;
			context.Add(string.Empty, adapterSpecificSettingsPartialView);

			this.DispatchPresentationEvent(Constants.AdapterUpdateEventUri, context);
		}

		public override void InitializeView(IAdapterSettingsPartialView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);
		}

		[DispatchActionUri(Uri = Constants.URI_ADAPTER_UPDATE_EVENT)]
		public void UpdateAdapter(IPartialView partialView, object context)
		{
			if ((object)partialView == null)
				throw new ArgumentNullException("partialView");
		}

		#endregion
	}
}