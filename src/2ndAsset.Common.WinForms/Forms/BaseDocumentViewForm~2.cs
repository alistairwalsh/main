/*
	Copyright �2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Forms
{
	public class BaseDocumentViewForm<TDocumentView, TMasterController> : BaseFullViewForm<TDocumentView, TMasterController>, IDocumentFullView
		where TDocumentView : class, IDocumentFullView
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

		string IDocumentFullView.FilePath
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