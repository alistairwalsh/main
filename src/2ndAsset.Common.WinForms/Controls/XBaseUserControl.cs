/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.ComponentModel;

using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.Common.WinForms.Presentation.Controllers;
using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Controls
{
	public class XBaseUserControl : BaseUserControl, IPartialView
	{
		#region Constructors/Destructors

		public XBaseUserControl()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual IBaseController Controller
		{
			get
			{
				return this.CoreGetController();
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IFullView FullView
		{
			get
			{
				return ((object)this.CoreGetParent<XBaseForm>()) as IFullView;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IBaseView ParentView
		{
			get
			{
				return ((object)this.CoreGetParent<XBaseUserControl>()) as IBaseView ??
						this.FullView;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IPartialView PartialView
		{
			get
			{
				return this;
			}
		}

		#endregion

		#region Methods/Operators

		void IPartialView._()
		{
			throw new NotImplementedException();
		}

		protected virtual IBaseController CoreGetController()
		{
			return null;
		}

		#endregion
	}
}