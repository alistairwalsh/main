/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System;

using _2ndAsset.Common.WinForms.Controls;
using _2ndAsset.Utilities.DataObfu.WindowsTool.Views;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controls
{
	public partial class ExecutionUserControl : _ExecutionUserControl, IExecutionView
	{
		#region Constructors/Destructors

		public ExecutionUserControl()
		{
			this.InitializeComponent();
		}

		#endregion

		#region Properties/Indexers/Events

		double? IExecutionView.DurationSeconds
		{
			set
			{
				this.txtBxDurationSeconds.CoreSetValue<double?>(value, "###,###,##0.##");
			}
		}

		bool? IExecutionView.IsComplete
		{
			set
			{
				this.txtBxIsComplete.CoreSetValue<bool?>(value, null);
			}
		}

		long? IExecutionView.RecordCount
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
			this.FullView.DispatchControllerAction(this, new Uri("action://obfuscation/execute"), null);
		}

		#endregion
	}

	public class _ExecutionUserControl : _2ndAssetUserControl<IExecutionView>
	{
		#region Constructors/Destructors

		public _ExecutionUserControl()
		{
		}

		#endregion
	}
}