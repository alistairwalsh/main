/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;

using Solder.Framework;

using _2ndAsset.Common.WinForms.Presentation;
using _2ndAsset.ObfuscationEngine.Core.Config;
using _2ndAsset.ObfuscationEngine.Core.Hosting.Tool;
using _2ndAsset.ObfuscationEngine.UI.Controllers;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers
{
	public sealed class ExecutableObfuscationDocumentController : ObfuscationDocumentController<IExecutableObfuscationDocumentView>
	{
		#region Constructors/Destructors

		public ExecutableObfuscationDocumentController()
		{
		}

		#endregion

		#region Methods/Operators

		[DispatchActionUri(Uri = "action://obfuscation/execute")]
		public void ExecuteObfuscation(IPartialView partialView, object context)
		{
			ObfuscationConfiguration obfuscationConfiguration;
			IEnumerable<Message> messages;

			this.View.ExecutionPartialView.RecordCount = null;
			this.View.ExecutionPartialView.IsComplete = null;
			this.View.ExecutionPartialView.DurationSeconds = null;

			obfuscationConfiguration = this.ApplyViewToDocument();

			messages = obfuscationConfiguration.Validate();

			if ((object)messages != null && messages.Any())
			{
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

		public override void InitializeView(IExecutableObfuscationDocumentView view)
		{
			// do nothing
			base.InitializeView(view);
		}

		#endregion
	}
}