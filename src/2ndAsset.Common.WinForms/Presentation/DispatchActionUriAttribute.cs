/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

namespace _2ndAsset.Common.WinForms.Presentation
{
	public class DispatchActionUriAttribute : Attribute
	{
		#region Constructors/Destructors

		public DispatchActionUriAttribute()
		{
		}

		#endregion

		#region Fields/Constants

		private string uri;

		#endregion

		#region Properties/Indexers/Events

		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		#endregion
	}
}