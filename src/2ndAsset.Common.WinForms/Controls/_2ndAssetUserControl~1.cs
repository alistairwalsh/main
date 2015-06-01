/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using _2ndAsset.Common.WinForms.Presentation;

namespace _2ndAsset.Common.WinForms.Controls
{
	public class _2ndAssetUserControl<TPartialView> : _2ndAssetUserControl
		where TPartialView : class, IPartialView
	{
		#region Constructors/Destructors

		public _2ndAssetUserControl()
		{
		}

		#endregion

		#region Properties/Indexers/Events

		public IFullView FullView
		{
			get
			{
				return ((object)this.CoreGetParentForm()) as IFullView;
			}
		}

		public TPartialView PartialView
		{
			get
			{
				return ((object)this) as TPartialView;
			}
		}

		#endregion
	}
}