/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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
			this.EmitPresentationEvent(Constants.RefreshUpstreamMetadataColumnsEventUri, null);
		}

		#endregion
	}
}