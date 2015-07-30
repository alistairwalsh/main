using System.Windows.Forms;

using _2ndAsset.ObfuscationEngine.UI.Controls;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Forms
{
	partial class ExecutableObfuscationDocumentForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExecutableObfuscationDocumentForm));
			this.msMain = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSave = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiExecute = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiClose = new System.Windows.Forms.ToolStripMenuItem();
			this.ssMain = new System.Windows.Forms.StatusStrip();
			this.tsslMain = new System.Windows.Forms.ToolStripStatusLabel();
			this.pnlMain = new System.Windows.Forms.Panel();
			this.obfuscationUc = new _2ndAsset.ObfuscationEngine.UI.Controls.ObfuscationUserControl();
			this.msMain.SuspendLayout();
			this.ssMain.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// msMain
			// 
			this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile});
			this.msMain.Location = new System.Drawing.Point(0, 0);
			this.msMain.Name = "msMain";
			this.msMain.Size = new System.Drawing.Size(883, 24);
			this.msMain.TabIndex = 0;
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSave,
            this.tsmiSaveAs,
            this.toolStripSeparator3,
            this.tsmiExecute,
            this.toolStripSeparator1,
            this.tsmiClose});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Size = new System.Drawing.Size(37, 20);
			this.tsmiFile.Text = "&File";
			// 
			// tsmiSave
			// 
			this.tsmiSave.Name = "tsmiSave";
			this.tsmiSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.tsmiSave.Size = new System.Drawing.Size(195, 22);
			this.tsmiSave.Text = "&Save";
			this.tsmiSave.Click += new System.EventHandler(this.tsmiSave_Click);
			// 
			// tsmiSaveAs
			// 
			this.tsmiSaveAs.Name = "tsmiSaveAs";
			this.tsmiSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.tsmiSaveAs.Size = new System.Drawing.Size(195, 22);
			this.tsmiSaveAs.Text = "Save &As...";
			this.tsmiSaveAs.Click += new System.EventHandler(this.tsmiSaveAs_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(192, 6);
			// 
			// tsmiExecute
			// 
			this.tsmiExecute.Name = "tsmiExecute";
			this.tsmiExecute.ShortcutKeys = System.Windows.Forms.Keys.F5;
			this.tsmiExecute.Size = new System.Drawing.Size(195, 22);
			this.tsmiExecute.Text = "Execute";
			this.tsmiExecute.Click += new System.EventHandler(this.tsmiExecute_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
			// 
			// tsmiClose
			// 
			this.tsmiClose.Name = "tsmiClose";
			this.tsmiClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.tsmiClose.Size = new System.Drawing.Size(195, 22);
			this.tsmiClose.Text = "&Close";
			this.tsmiClose.Click += new System.EventHandler(this.tsmiClose_Click);
			// 
			// ssMain
			// 
			this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslMain});
			this.ssMain.Location = new System.Drawing.Point(0, 411);
			this.ssMain.Name = "ssMain";
			this.ssMain.Size = new System.Drawing.Size(883, 22);
			this.ssMain.TabIndex = 2;
			this.ssMain.Text = "statusStrip1";
			// 
			// tsslMain
			// 
			this.tsslMain.Name = "tsslMain";
			this.tsslMain.Size = new System.Drawing.Size(54, 17);
			this.tsslMain.Text = "%TEXT%";
			// 
			// pnlMain
			// 
			this.pnlMain.AutoScroll = true;
			this.pnlMain.Controls.Add(this.obfuscationUc);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 24);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(883, 387);
			this.pnlMain.TabIndex = 1;
			// 
			// obfuscationUc
			// 
			this.obfuscationUc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.obfuscationUc.Location = new System.Drawing.Point(0, 0);
			this.obfuscationUc.MinimumSize = new System.Drawing.Size(460, 313);
			this.obfuscationUc.Name = "obfuscationUc";
			this.obfuscationUc.Size = new System.Drawing.Size(883, 387);
			this.obfuscationUc.TabIndex = 0;
			// 
			// ObfuscationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(883, 433);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.msMain);
			this.Controls.Add(this.ssMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.msMain;
			this.Name = "ExecutableObfuscationDocumentForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.msMain.ResumeLayout(false);
			this.msMain.PerformLayout();
			this.ssMain.ResumeLayout(false);
			this.ssMain.PerformLayout();
			this.pnlMain.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MenuStrip msMain;
		private StatusStrip ssMain;
		private Panel pnlMain;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiSave;
		private System.Windows.Forms.ToolStripMenuItem tsmiSaveAs;
		private System.Windows.Forms.ToolStripMenuItem tsmiClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripStatusLabel tsslMain;
		private ToolStripMenuItem tsmiExecute;
		private ToolStripSeparator toolStripSeparator1;
		private ObfuscationUserControl obfuscationUc;

	}
}