using System.Windows.Forms;

using _2ndAsset.Common.WinForms.Controls;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Forms
{
	partial class SplashForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashForm));
			this.pbAppLogo = new PictureBox();
			this.btnOK = new Button();
			this.pnlMain = new Panel();
			this.pbMain = new ProgressBar();
			this.tmrMain = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pbAppLogo)).BeginInit();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// pbAppLogo
			// 
			this.pbAppLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbAppLogo.BackColor = System.Drawing.Color.Transparent;
			this.pbAppLogo.Location = new System.Drawing.Point(12, 12);
			this.pbAppLogo.Name = "pbAppLogo";
			this.pbAppLogo.Size = new System.Drawing.Size(376, 363);
			this.pbAppLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbAppLogo.TabIndex = 12;
			this.pbAppLogo.TabStop = false;
			this.pbAppLogo.Click += new System.EventHandler(this.closeFormBy_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOK.Location = new System.Drawing.Point(313, 381);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.closeFormBy_Click);
			// 
			// pnlMain
			// 
			this.pnlMain.BackColor = System.Drawing.SystemColors.Control;
			this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlMain.Controls.Add(this.pbMain);
			this.pnlMain.Controls.Add(this.pbAppLogo);
			this.pnlMain.Controls.Add(this.btnOK);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 0);
			this.pnlMain.Margin = new System.Windows.Forms.Padding(0);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(403, 414);
			this.pnlMain.TabIndex = 0;
			this.pnlMain.Click += new System.EventHandler(this.closeFormBy_Click);
			// 
			// pbMain
			// 
			this.pbMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pbMain.Location = new System.Drawing.Point(12, 380);
			this.pbMain.Maximum = 1000;
			this.pbMain.Name = "pbMain";
			this.pbMain.Size = new System.Drawing.Size(295, 23);
			this.pbMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.pbMain.TabIndex = 0;
			this.pbMain.Click += new System.EventHandler(this.closeFormBy_Click);
			// 
			// tmrMain
			// 
			this.tmrMain.Enabled = true;
			this.tmrMain.Interval = 150;
			this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
			// 
			// SplashForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(403, 414);
			this.Controls.Add(this.pnlMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SplashForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Click += new System.EventHandler(this.closeFormBy_Click);
			((System.ComponentModel.ISupportInitialize)(this.pbAppLogo)).EndInit();
			this.pnlMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private PictureBox pbAppLogo;
		private Button btnOK;
		private Panel pnlMain;
		private ProgressBar pbMain;
		private System.Windows.Forms.Timer tmrMain;
	}
}
