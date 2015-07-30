/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Windows.Forms;

namespace _2ndAsset.Common.WinForms
{
	public sealed class SplashApplicationContext : ApplicationContext
	{
		#region Constructors/Destructors

		public SplashApplicationContext(Form applicationForm, Form splashForm)
			: base(splashForm)
		{
			if ((object)applicationForm == null)
				throw new ArgumentNullException("applicationForm");

			if ((object)splashForm == null)
				throw new ArgumentNullException("splashForm");

			this.applicationForm = applicationForm;
			this.splashForm = splashForm;
		}

		#endregion

		#region Fields/Constants

		private Form applicationForm;
		private Form splashForm;

		#endregion

		#region Properties/Indexers/Events

		private Form ApplicationForm
		{
			get
			{
				return this.applicationForm;
			}
			set
			{
				this.applicationForm = value;
			}
		}

		private Form SplashForm
		{
			get
			{
				return this.splashForm;
			}
			set
			{
				this.splashForm = value;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void OnMainFormClosed(object sender, EventArgs e)
		{
			if ((object)sender == (object)this.ApplicationForm)
				base.OnMainFormClosed(sender, e);
			else if ((object)sender == (object)this.SplashForm)
			{
				this.SplashForm.Dispose();
				this.SplashForm = null;

				this.MainForm = this.ApplicationForm;
				this.MainForm.Show();
			}
		}

		#endregion
	}
}