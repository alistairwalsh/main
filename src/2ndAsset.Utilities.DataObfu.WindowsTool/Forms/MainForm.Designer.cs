using System.Windows.Forms;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Forms
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.ssMain = new System.Windows.Forms.StatusStrip();
			this.tsslMain = new System.Windows.Forms.ToolStripStatusLabel();
			this.msMain = new System.Windows.Forms.MenuStrip();
			this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNew = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSaveAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDocumentWindows = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiTopics = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCloseAll = new System.Windows.Forms.ToolStripMenuItem();
			this.ssMain.SuspendLayout();
			this.msMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// ssMain
			// 
			this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslMain});
			this.ssMain.Location = new System.Drawing.Point(0, 393);
			this.ssMain.Name = "ssMain";
			this.ssMain.Size = new System.Drawing.Size(703, 22);
			this.ssMain.TabIndex = 3;
			this.ssMain.Text = "statusStrip1";
			// 
			// tsslMain
			// 
			this.tsslMain.Name = "tsslMain";
			this.tsslMain.Size = new System.Drawing.Size(54, 17);
			this.tsslMain.Text = "%TEXT%";
			// 
			// msMain
			// 
			this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiDocumentWindows,
            this.tsmiHelp});
			this.msMain.Location = new System.Drawing.Point(0, 0);
			this.msMain.Name = "msMain";
			this.msMain.Size = new System.Drawing.Size(703, 24);
			this.msMain.TabIndex = 0;
			this.msMain.Text = "menuStrip1";
			// 
			// tsmiFile
			// 
			this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNew,
            this.tsmiOpen,
            this.toolStripSeparator3,
            this.tsmiSaveAll,
            this.toolStripSeparator1,
            this.tsmiExit});
			this.tsmiFile.Name = "tsmiFile";
			this.tsmiFile.Size = new System.Drawing.Size(37, 20);
			this.tsmiFile.Text = "&File";
			// 
			// tsmiNew
			// 
			this.tsmiNew.Name = "tsmiNew";
			this.tsmiNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.tsmiNew.Size = new System.Drawing.Size(187, 22);
			this.tsmiNew.Text = "&New";
			this.tsmiNew.Click += new System.EventHandler(this.tsmiNew_Click);
			// 
			// tsmiOpen
			// 
			this.tsmiOpen.Name = "tsmiOpen";
			this.tsmiOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.tsmiOpen.Size = new System.Drawing.Size(187, 22);
			this.tsmiOpen.Text = "&Open...";
			this.tsmiOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(184, 6);
			// 
			// tsmiSaveAll
			// 
			this.tsmiSaveAll.Name = "tsmiSaveAll";
			this.tsmiSaveAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
			this.tsmiSaveAll.Size = new System.Drawing.Size(187, 22);
			this.tsmiSaveAll.Text = "Save All";
			this.tsmiSaveAll.Click += new System.EventHandler(this.tsmiSaveAll_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
			// 
			// tsmiExit
			// 
			this.tsmiExit.Name = "tsmiExit";
			this.tsmiExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.tsmiExit.Size = new System.Drawing.Size(187, 22);
			this.tsmiExit.Text = "E&xit";
			this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
			// 
			// tsmiDocumentWindows
			// 
			this.tsmiDocumentWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCloseAll,
            this.toolStripSeparator4});
			this.tsmiDocumentWindows.Name = "tsmiDocumentWindows";
			this.tsmiDocumentWindows.Size = new System.Drawing.Size(63, 20);
			this.tsmiDocumentWindows.Text = "&Window";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(194, 6);
			// 
			// tsmiHelp
			// 
			this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTopics,
            this.tsmiAbout});
			this.tsmiHelp.Name = "tsmiHelp";
			this.tsmiHelp.Size = new System.Drawing.Size(44, 20);
			this.tsmiHelp.Text = "&Help";
			// 
			// tsmiTopics
			// 
			this.tsmiTopics.Name = "tsmiTopics";
			this.tsmiTopics.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.tsmiTopics.Size = new System.Drawing.Size(132, 22);
			this.tsmiTopics.Text = "&Topics";
			this.tsmiTopics.Click += new System.EventHandler(this.tsmiTopics_Click);
			// 
			// tsmiAbout
			// 
			this.tsmiAbout.Name = "tsmiAbout";
			this.tsmiAbout.ShortcutKeys = System.Windows.Forms.Keys.F12;
			this.tsmiAbout.Size = new System.Drawing.Size(132, 22);
			this.tsmiAbout.Text = "&About";
			this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
			// 
			// tsmiCloseAll
			// 
			this.tsmiCloseAll.Name = "tsmiCloseAll";
			this.tsmiCloseAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
			this.tsmiCloseAll.Size = new System.Drawing.Size(197, 22);
			this.tsmiCloseAll.Text = "Close All";
			this.tsmiCloseAll.Click += new System.EventHandler(this.tsmiCloseAll_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(703, 415);
			this.Controls.Add(this.ssMain);
			this.Controls.Add(this.msMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.msMain;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ssMain.ResumeLayout(false);
			this.ssMain.PerformLayout();
			this.msMain.ResumeLayout(false);
			this.msMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip ssMain;
		private System.Windows.Forms.MenuStrip msMain;
		private System.Windows.Forms.ToolStripMenuItem tsmiFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiNew;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiExit;
		private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
		private System.Windows.Forms.ToolStripMenuItem tsmiTopics;
		private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
		private System.Windows.Forms.ToolStripStatusLabel tsslMain;
		private System.Windows.Forms.ToolStripMenuItem tsmiSaveAll;
		private ToolStripSeparator toolStripSeparator3;
		private ToolStripMenuItem tsmiDocumentWindows;
		private ToolStripSeparator toolStripSeparator4;
		private ToolStripMenuItem tsmiCloseAll;
	}
}

