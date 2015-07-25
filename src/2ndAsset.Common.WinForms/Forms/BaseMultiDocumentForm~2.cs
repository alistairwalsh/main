/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Forms
{
	public class BaseMultiDocumentForm<TMultiDocumentView, TMasterController> : BaseFullViewForm<TMultiDocumentView, TMasterController>, IMultiDocumentView
		where TMultiDocumentView : class, IMultiDocumentView
		where TMasterController : MasterController<TMultiDocumentView>, new()
	{
		#region Constructors/Destructors

		public BaseMultiDocumentForm()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly IList<BaseFullViewForm> documentForms = new List<BaseFullViewForm>();

		#endregion

		#region Properties/Indexers/Events

		protected IList<BaseFullViewForm> DocumentForms
		{
			get
			{
				return this.documentForms;
			}
		}

		IList<IDocumentView> IMultiDocumentView.DocumentViews
		{
			get
			{
				return this.DocumentForms.Cast<IDocumentView>().ToList();
			}
		}

		protected bool HasAnyDocuments
		{
			get
			{
				return this.DocumentForms.Count > 0;
			}
		}

		#endregion

		#region Methods/Operators

		protected virtual void CoreDocumentFormClosed(BaseForm form)
		{
			if ((object)form == null)
				throw new ArgumentNullException("form");

			// do nothing
		}

		protected virtual void CoreDocumentFormLoaded(BaseForm form)
		{
			if ((object)form == null)
				throw new ArgumentNullException("form");

			// do nothing
		}

		protected virtual void CoreDocumentFormTextChanged(BaseForm form)
		{
			if ((object)form == null)
				throw new ArgumentNullException("form");

			// do nothing
		}

		IDocumentView IMultiDocumentView.CreateDocumentView(Uri viewUri, string documentFilePath)
		{
			BaseFullViewForm form;
			IDocumentView documentView;
			Type controlType;

			if ((object)viewUri == null)
				throw new ArgumentNullException("viewUri");

			if (!this.UriToControlTypes.TryGetValue(viewUri, out controlType))
				throw new InvalidOperationException(string.Format("{0}", viewUri));

			form = (BaseFullViewForm)Activator.CreateInstance(controlType);
			form.HandleCreated += this.documentForm_HandleCreated;
			form.Load += this.documentForm_Load;
			form.TextChanged += this.documentForm_TextChanged;
			form.Closed += this.documentForm_Closed;

			documentView = (IDocumentView)form;
			documentView.FilePath = documentFilePath;

			form.Show();

			return documentView;
		}

		private void documentForm_Closed(object sender, EventArgs e)
		{
			BaseFullViewForm form;

			form = (BaseFullViewForm)sender;
			form.Closed -= this.documentForm_Closed;
			form.TextChanged -= this.documentForm_TextChanged;
			form.Load -= this.documentForm_Load;
			form.HandleCreated -= this.documentForm_HandleCreated;

			this.CoreDocumentFormClosed(form);

			this.DocumentForms.Remove(form);
			form.Dispose();

			this.CoreRefreshControlState();
			this.Activate();
		}

		private void documentForm_HandleCreated(object sender, EventArgs e)
		{
			BaseFullViewForm form;

			form = (BaseFullViewForm)sender;

			this.DocumentForms.Add(form);

			this.CoreDocumentFormLoaded(form);
			this.CoreRefreshControlState();
		}

		private void documentForm_Load(object sender, EventArgs e)
		{
		}

		private void documentForm_TextChanged(object sender, EventArgs e)
		{
			BaseFullViewForm form;

			form = (BaseFullViewForm)sender;

			this.CoreDocumentFormTextChanged(form);
			this.CoreRefreshControlState();
		}

		#endregion
	}
}