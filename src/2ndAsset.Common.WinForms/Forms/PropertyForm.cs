/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;
using System.Windows.Forms;

namespace _2ndAsset.Common.WinForms.Forms
{
	public partial class PropertyForm : BaseForm
	{
		#region Constructors/Destructors

		public PropertyForm(object target)
		{
			this.InitializeComponent();

			this.target = target;
		}

		#endregion

		#region Fields/Constants

		private readonly object target;

		#endregion

		#region Properties/Indexers/Events

		public event EventHandler PropertyUpdate;

		private object Target
		{
			get
			{
				return this.target;
			}
		}

		#endregion

		#region Methods/Operators

		protected override void CoreSetup()
		{
			base.CoreSetup();

			this.pgShape.SelectedObject = this.Target;
		}

		private void pgShape_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if ((object)this.PropertyUpdate != null)
				this.PropertyUpdate(null, null);
		}

		#endregion
	}
}