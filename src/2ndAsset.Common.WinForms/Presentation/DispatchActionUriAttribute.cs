/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
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