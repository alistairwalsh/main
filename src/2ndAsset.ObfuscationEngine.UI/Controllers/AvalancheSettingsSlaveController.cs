﻿/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using Solder.Framework;

using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.UI.Views;

namespace _2ndAsset.ObfuscationEngine.UI.Controllers
{
	public sealed class AvalancheSettingsSlaveController : SlaveController<IAvalancheSettingsPartialView>
	{
		#region Constructors/Destructors

		public AvalancheSettingsSlaveController()
		{
		}

		#endregion

		#region Methods/Operators

		public override void InitializeView(IAvalancheSettingsPartialView view)
		{
			if ((object)view == null)
				throw new ArgumentNullException("view");

			base.InitializeView(view);

			this.RegenerateHashSettings(false);
		}

		public void RegenerateHashSettings()
		{
			this.RegenerateHashSettings(true);
		}

		private void RegenerateHashSettings(bool interactive)
		{
			bool result;
			const long HASH_MULTIPLIER = 33;
			const long HASH_SEED = 5381;

			if (interactive)
			{
				result = this.View.FullView.ShowConfirm("Proceed with hash regeneration?", Severity.Information);

				if (!result)
					return;
			}

			this.View.HashMultiplier = HASH_MULTIPLIER;
			this.View.HashSeed = HASH_SEED;
		}

		#endregion
	}
}