/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;
using System.ComponentModel;

using _2ndAsset.Common.WinForms.Forms;
using _2ndAsset.Common.WinForms.Presentation.Views;

namespace _2ndAsset.Common.WinForms.Controls
{
	public class BasePartialViewUserControl : BaseUserControl, IPartialView
	{
		#region Constructors/Destructors

		public BasePartialViewUserControl()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected IPartialView _
		{
			get
			{
				return this;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		IFullView IPartialView.FullView
		{
			get
			{
				return ((object)this.CoreGetParent<BaseFullViewForm>()) as IFullView;
			}
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		IBaseView IBaseView.ParentView
		{
			get
			{
				return ((object)this.CoreGetParent<BasePartialViewUserControl>()) as IBaseView ??
						this._.FullView;
			}
		}

		#endregion
	}
}