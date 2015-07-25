/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Forms
{
	public class BaseDocumentViewForm<TDocumentView, TMasterController> : BaseFullViewForm<TDocumentView, TMasterController>, IDocumentView
		where TDocumentView : class, IDocumentView
		where TMasterController : MasterController<TDocumentView>, new()
	{
		#region Constructors/Destructors

		public BaseDocumentViewForm()
		{
		}

		#endregion

		#region Fields/Constants

		private string filePath;

		#endregion

		#region Properties/Indexers/Events

		string IDocumentView.FilePath
		{
			get
			{
				return this.filePath;
			}
			set
			{
				this.filePath = value;
			}
		}

		#endregion
	}
}