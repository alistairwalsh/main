/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.ObfuscationEngine.UI.Views;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Controllers;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controls
{
	public partial class ExecutionUserControl : _ExecutionUserControl, IExecutionPartialView
	{
		#region Constructors/Destructors

		public ExecutionUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		double? IExecutionPartialView.DurationSeconds
		{
			set
			{
				this.txtBxDurationSeconds.CoreSetValue<double?>(value, "###,###,##0.##");
			}
		}

		bool? IExecutionPartialView.IsComplete
		{
			set
			{
				this.txtBxIsComplete.CoreSetValue<bool?>(value, null);
			}
		}

		long? IExecutionPartialView.RecordCount
		{
			set
			{
				this.txtBxTotalRecords.CoreSetValue<long?>(value, "###,###,##0");
			}
		}

		#endregion

		#region Methods/Operators

		private void btnExecute_Click(object sender, EventArgs e)
		{
			this.Controller.Execute();
		}

		#endregion
	}

	public class _ExecutionUserControl : BasePartialViewUserControl<IExecutionPartialView, ExecutionController>
	{
		#region Constructors/Destructors

		public _ExecutionUserControl()
		{
		}

		#endregion
	}
}