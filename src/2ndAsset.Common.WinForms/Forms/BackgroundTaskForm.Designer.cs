/*
	Copyright ©2014-2015 2ndAsset.com (dpbullington@gmail.com) - D. P. Bullington
	Distributed under the MIT license: http://www.opensource.org/licenses/mit-license.php
*/

using System.Windows.Forms;

using _2ndAsset.Common.WinForms.Controls;

namespace _2ndAsset.Common.WinForms.Forms
{
	partial class BackgroundTaskForm
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

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackgroundTaskForm));
			this.pbarMain = new ProgressBar();
			this.tmrMain = new System.Windows.Forms.Timer(this.components);
			this.btnCancel = new Button();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.lblCaption = new Label();
			this.SuspendLayout();
			// 
			// pbarMain
			// 
			this.pbarMain.Location = new System.Drawing.Point(13, 98);
			this.pbarMain.Name = "pbarMain";
			this.pbarMain.Size = new System.Drawing.Size(256, 20);
			this.pbarMain.Step = 1;
			this.pbarMain.TabIndex = 1;
			// 
			// tmrMain
			// 
			this.tmrMain.Interval = 250;
			this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(194, 132);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			// 
			// lblCaption
			// 
			this.lblCaption.Location = new System.Drawing.Point(13, 13);
			this.lblCaption.Name = "lblCaption";
			this.lblCaption.Size = new System.Drawing.Size(256, 70);
			this.lblCaption.TabIndex = 0;
			this.lblCaption.Text = "%CAPTION%";
			// 
			// BackgroundTaskForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(279, 165);
			this.Controls.Add(this.lblCaption);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.pbarMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BackgroundTaskForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}

		#endregion

		private ProgressBar pbarMain;
		private System.Windows.Forms.Timer tmrMain;
		private Button btnCancel;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private Label lblCaption;
	}
}