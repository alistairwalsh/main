/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

namespace _2ndAsset.Common.WinForms.Forms
{
	public partial class BaseForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.ttMain = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// ttMain
			// 
			this.ttMain.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
			// 
			// _2ndAssetForm
			// 
			this.ClientSize = new System.Drawing.Size(292, 273);
			this.Name = "BaseForm";
			this.ResumeLayout(false);

		}

		private System.Windows.Forms.ToolTip ttMain;

		#endregion
	}
}