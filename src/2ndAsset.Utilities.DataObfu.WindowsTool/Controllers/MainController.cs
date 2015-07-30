/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Linq;

using Solder.Framework;
using Solder.Framework.Utilities;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers
{
	public sealed class MainController : MasterController<IMainView>
	{
		#region Constructors/Destructors

		public MainController()
		{
		}

		#endregion

		#region Fields/Constants

		private static readonly Uri aboutBoxViewUri = new Uri("view://main/about");
		private static readonly Uri documentViewUri = new Uri("view://main/document");

		#endregion

		#region Properties/Indexers/Events

		public static Uri AboutBoxViewUri
		{
			get
			{
				return aboutBoxViewUri;
			}
		}

		public static Uri DocumentViewUri
		{
			get
			{
				return documentViewUri;
			}
		}

		#endregion

		#region Methods/Operators

		public void AboutBox()
		{
			this.View.ShowView(AboutBoxViewUri);
		}

		public bool CloseAllDocuments(string verb)
		{
			bool? result;
			int dirtyCount;

			dirtyCount = this.View.DocumentViews.Count(v => v.IsViewDirty);
			result = false;

			if (dirtyCount > 0)
			{
				result = this.View.ShowQuestion(string.Format("Do you to save changes to {0} document(s)?", dirtyCount), true, Severity.Warning);

				if ((object)result == null)
					return true; // cancel
			}

			var documentViews = this.View.DocumentViews.Where(v => !v.IsViewDirty || result == true);

			foreach (IDocumentFullView documentView in documentViews)
			{
				//if (documentView.IsViewDirty)
				//documentView.SaveDocument(false);

				documentView.CloseView(null);
			}

			return false;
		}

		public void HelpTopics()
		{
			this.View.ShowAlert("Help is not available in this release.", severity: Severity.Warning);
		}

		public override void InitializeView(IMainView view)
		{
			base.InitializeView(view);

			this.View.ViewText = string.Format("{0} Studio", ExecutableApplicationFascade.Current.AssemblyInformationFascade.Product);
			this.View.StatusText = "Ready";
		}

		public void NewDocument()
		{
			this.View.StatusText = "Document create started...";
			this.ShowDocument(null);
			this.View.StatusText = "Document create completed successfully.";
		}

		public void OpenDocument()
		{
			string filePath;

			this.View.StatusText = "Document open started...";

			if (!this.View.TryGetOpenFilePath(out filePath))
			{
				this.View.StatusText = "Document open canceled.";
				return;
			}

			this.ShowDocument(filePath);
			this.View.StatusText = "Document open completed successfully.";
		}

		public void QuitNow()
		{
			this.View.CloseView(null);
		}

		public void SaveAllDocuments()
		{
		}

		private void ShowDocument(string documentFilePath)
		{
			IDocumentFullView documentView;

			documentView = this.View.CreateDocumentView(DocumentViewUri, documentFilePath);
		}

		#endregion
	}
}