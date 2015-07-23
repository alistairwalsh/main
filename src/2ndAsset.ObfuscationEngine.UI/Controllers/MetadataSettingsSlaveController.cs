/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public sealed class MetadataSettingsSlaveController : SlaveController<IMetadataSettingsPartialView>
	{
		#region Constructors/Destructors

		public MetadataSettingsSlaveController()
		{
		}

		#endregion

		#region Methods/Operators

		public override void InitializeView(IMetadataSettingsPartialView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);
		}

		public void RefreshUpstream()
		{
			this.DispatchPresentationEvent(Constants.RefreshUpstreamMetadataColumnsEventUri, null);
		}

		#endregion
	}
}