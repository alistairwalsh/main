/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.Collections.Generic;
using System.Linq;

using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.Common.WinForms.Forms
{
	public class _2ndAssetMultiDocumentForm<TMultiDocumentView, TController> : _2ndAssetForm<TMultiDocumentView, TController>, IMultiDocumentView
		where TMultiDocumentView : class, IMultiDocumentView
		where TController : class, IBaseController<TMultiDocumentView>, new()
	{
		#region Constructors/Destructors

		public _2ndAssetMultiDocumentForm()
		{
		}

		#endregion

		#region Fields/Constants

		private readonly IList<_2ndAssetForm> documentForms = new List<_2ndAssetForm>();

		#endregion

		#region Properties/Indexers/Events

		protected IList<_2ndAssetForm> DocumentForms
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

		protected virtual void CoreDocumentFormClosed(_2ndAssetForm form)
		{
			if ((object)form == null)
				throw new ArgumentNullException("form");

			// do nothing
		}

		protected virtual void CoreDocumentFormLoaded(_2ndAssetForm form)
		{
			if ((object)form == null)
				throw new ArgumentNullException("form");

			// do nothing
		}

		protected virtual void CoreDocumentFormTextChanged(_2ndAssetForm form)
		{
			if ((object)form == null)
				throw new ArgumentNullException("form");

			// do nothing
		}

		IDocumentView IMultiDocumentView.CreateDocumentView(Uri viewUri, string documentFilePath)
		{
			_2ndAssetForm form;
			IDocumentView documentView;
			Type controlType;

			if ((object)viewUri == null)
				throw new ArgumentNullException("viewUri");

			if (!this.UriToControlTypes.TryGetValue(viewUri, out controlType))
				throw new InvalidOperationException(string.Format("{0}", viewUri));

			form = (_2ndAssetForm)Activator.CreateInstance(controlType);
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
			_2ndAssetForm form;

			form = (_2ndAssetForm)sender;
			form.Closed -= this.documentForm_Closed;
			form.Load -= this.documentForm_Load;
			form.TextChanged -= this.documentForm_TextChanged;

			this.CoreDocumentFormClosed(form);

			this.DocumentForms.Remove(form);
			form.Dispose();

			this.CoreRefreshControlState();
			this.Activate();
		}

		private void documentForm_Load(object sender, EventArgs e)
		{
			_2ndAssetForm form;

			form = (_2ndAssetForm)sender;

			this.DocumentForms.Add(form);

			this.CoreDocumentFormLoaded(form);
			this.CoreRefreshControlState();
		}

		private void documentForm_TextChanged(object sender, EventArgs e)
		{
			_2ndAssetForm form;

			form = (_2ndAssetForm)sender;

			this.CoreDocumentFormTextChanged(form);
			this.CoreRefreshControlState();
		}

		#endregion
	}
}