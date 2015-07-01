using System.Windows.Forms;

using _2ndAsset.ObfuscationEngine.UI.Controls;

namespace _2ndAsset.ObfuscationEngine.UI.Forms
{
	partial class ObfuscationForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObfuscationForm));
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
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tpWelcome = new System.Windows.Forms.TabPage();
			this.tpAvalanche = new System.Windows.Forms.TabPage();
			this.avalancheSettingsUc = new AvalancheSettingsUserControl();
			this.tpSource = new System.Windows.Forms.TabPage();
			this.sourceAdapterSettingsUc = new AdapterSettingsUserControl();
			this.tpDictionaries = new System.Windows.Forms.TabPage();
			this.dictionarySettingsUc = new DictionarySettingsUserControl();
			this.tpMetadata = new System.Windows.Forms.TabPage();
			this.tpDestination = new System.Windows.Forms.TabPage();
			this.destinationAdapterSettingsUc = new AdapterSettingsUserControl();
			this.tpExecution = new System.Windows.Forms.TabPage();
			this.executionUc = new ExecutionUserControl();
			this.metadataSettingsUc = new MetadataSettingsUserControl();
			this.msMain.SuspendLayout();
			this.ssMain.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tpAvalanche.SuspendLayout();
			this.tpSource.SuspendLayout();
			this.tpDictionaries.SuspendLayout();
			this.tpMetadata.SuspendLayout();
			this.tpDestination.SuspendLayout();
			this.tpExecution.SuspendLayout();
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
			this.pnlMain.Controls.Add(this.tabMain);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.Location = new System.Drawing.Point(0, 24);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(883, 387);
			this.pnlMain.TabIndex = 1;
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.tpWelcome);
			this.tabMain.Controls.Add(this.tpAvalanche);
			this.tabMain.Controls.Add(this.tpSource);
			this.tabMain.Controls.Add(this.tpDictionaries);
			this.tabMain.Controls.Add(this.tpMetadata);
			this.tabMain.Controls.Add(this.tpDestination);
			this.tabMain.Controls.Add(this.tpExecution);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Location = new System.Drawing.Point(0, 0);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(883, 387);
			this.tabMain.TabIndex = 5;
			// 
			// tpWelcome
			// 
			this.tpWelcome.Location = new System.Drawing.Point(4, 22);
			this.tpWelcome.Name = "tpWelcome";
			this.tpWelcome.Size = new System.Drawing.Size(875, 361);
			this.tpWelcome.TabIndex = 6;
			this.tpWelcome.Text = "Welcome";
			this.tpWelcome.UseVisualStyleBackColor = true;
			// 
			// tpAvalanche
			// 
			this.tpAvalanche.Controls.Add(this.avalancheSettingsUc);
			this.tpAvalanche.Location = new System.Drawing.Point(4, 22);
			this.tpAvalanche.Name = "tpAvalanche";
			this.tpAvalanche.Size = new System.Drawing.Size(875, 361);
			this.tpAvalanche.TabIndex = 5;
			this.tpAvalanche.Text = "Avalanche";
			this.tpAvalanche.UseVisualStyleBackColor = true;
			// 
			// avalancheSettingsUc
			// 
			this.avalancheSettingsUc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.avalancheSettingsUc.Location = new System.Drawing.Point(0, 0);
			this.avalancheSettingsUc.MinimumSize = new System.Drawing.Size(460, 313);
			this.avalancheSettingsUc.Name = "avalancheSettingsUc";
			this.avalancheSettingsUc.Size = new System.Drawing.Size(875, 361);
			this.avalancheSettingsUc.TabIndex = 0;
			// 
			// tpSource
			// 
			this.tpSource.Controls.Add(this.sourceAdapterSettingsUc);
			this.tpSource.Location = new System.Drawing.Point(4, 22);
			this.tpSource.Name = "tpSource";
			this.tpSource.Padding = new System.Windows.Forms.Padding(3);
			this.tpSource.Size = new System.Drawing.Size(875, 361);
			this.tpSource.TabIndex = 0;
			this.tpSource.Text = "Source";
			this.tpSource.UseVisualStyleBackColor = true;
			// 
			// sourceAdapterSettingsUc
			// 
			this.sourceAdapterSettingsUc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sourceAdapterSettingsUc.Location = new System.Drawing.Point(3, 3);
			this.sourceAdapterSettingsUc.MinimumSize = new System.Drawing.Size(460, 313);
			this.sourceAdapterSettingsUc.Name = "sourceAdapterSettingsUc";
			this.sourceAdapterSettingsUc.Size = new System.Drawing.Size(869, 355);
			this.sourceAdapterSettingsUc.TabIndex = 0;
			// 
			// tpDictionaries
			// 
			this.tpDictionaries.Controls.Add(this.dictionarySettingsUc);
			this.tpDictionaries.Location = new System.Drawing.Point(4, 22);
			this.tpDictionaries.Name = "tpDictionaries";
			this.tpDictionaries.Size = new System.Drawing.Size(875, 361);
			this.tpDictionaries.TabIndex = 4;
			this.tpDictionaries.Text = "Dictionaries";
			this.tpDictionaries.UseVisualStyleBackColor = true;
			// 
			// dictionarySettingsUc
			// 
			this.dictionarySettingsUc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dictionarySettingsUc.Location = new System.Drawing.Point(0, 0);
			this.dictionarySettingsUc.MinimumSize = new System.Drawing.Size(460, 313);
			this.dictionarySettingsUc.Name = "dictionarySettingsUc";
			this.dictionarySettingsUc.Size = new System.Drawing.Size(875, 361);
			this.dictionarySettingsUc.TabIndex = 0;
			// 
			// tpMetadata
			// 
			this.tpMetadata.Controls.Add(this.metadataSettingsUc);
			this.tpMetadata.Location = new System.Drawing.Point(4, 22);
			this.tpMetadata.Name = "tpMetadata";
			this.tpMetadata.Size = new System.Drawing.Size(875, 361);
			this.tpMetadata.TabIndex = 3;
			this.tpMetadata.Text = "Metadata";
			this.tpMetadata.UseVisualStyleBackColor = true;
			// 
			// tpDestination
			// 
			this.tpDestination.Controls.Add(this.destinationAdapterSettingsUc);
			this.tpDestination.Location = new System.Drawing.Point(4, 22);
			this.tpDestination.Name = "tpDestination";
			this.tpDestination.Padding = new System.Windows.Forms.Padding(3);
			this.tpDestination.Size = new System.Drawing.Size(875, 361);
			this.tpDestination.TabIndex = 1;
			this.tpDestination.Text = "Destination";
			this.tpDestination.UseVisualStyleBackColor = true;
			// 
			// destinationAdapterSettingsUc
			// 
			this.destinationAdapterSettingsUc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.destinationAdapterSettingsUc.Location = new System.Drawing.Point(3, 3);
			this.destinationAdapterSettingsUc.MinimumSize = new System.Drawing.Size(460, 313);
			this.destinationAdapterSettingsUc.Name = "destinationAdapterSettingsUc";
			this.destinationAdapterSettingsUc.Size = new System.Drawing.Size(869, 355);
			this.destinationAdapterSettingsUc.TabIndex = 1;
			// 
			// tpExecution
			// 
			this.tpExecution.Controls.Add(this.executionUc);
			this.tpExecution.Location = new System.Drawing.Point(4, 22);
			this.tpExecution.Name = "tpExecution";
			this.tpExecution.Size = new System.Drawing.Size(875, 361);
			this.tpExecution.TabIndex = 2;
			this.tpExecution.Text = "Execution";
			this.tpExecution.UseVisualStyleBackColor = true;
			// 
			// executionUc
			// 
			this.executionUc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.executionUc.Location = new System.Drawing.Point(0, 0);
			this.executionUc.MinimumSize = new System.Drawing.Size(460, 313);
			this.executionUc.Name = "executionUc";
			this.executionUc.Size = new System.Drawing.Size(875, 361);
			this.executionUc.TabIndex = 0;
			// 
			// metadataSettingsUc
			// 
			this.metadataSettingsUc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.metadataSettingsUc.Location = new System.Drawing.Point(0, 0);
			this.metadataSettingsUc.MinimumSize = new System.Drawing.Size(460, 313);
			this.metadataSettingsUc.Name = "metadataSettingsUc";
			this.metadataSettingsUc.Size = new System.Drawing.Size(875, 361);
			this.metadataSettingsUc.TabIndex = 0;
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
			this.Name = "ObfuscationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.msMain.ResumeLayout(false);
			this.msMain.PerformLayout();
			this.ssMain.ResumeLayout(false);
			this.ssMain.PerformLayout();
			this.pnlMain.ResumeLayout(false);
			this.tabMain.ResumeLayout(false);
			this.tpAvalanche.ResumeLayout(false);
			this.tpSource.ResumeLayout(false);
			this.tpDictionaries.ResumeLayout(false);
			this.tpMetadata.ResumeLayout(false);
			this.tpDestination.ResumeLayout(false);
			this.tpExecution.ResumeLayout(false);
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
		private TabControl tabMain;
		private TabPage tpSource;
		private Controls.AdapterSettingsUserControl sourceAdapterSettingsUc;
		private TabPage tpMetadata;
		private TabPage tpDestination;
		private Controls.AdapterSettingsUserControl destinationAdapterSettingsUc;
		private TabPage tpExecution;
		private Controls.ExecutionUserControl executionUc;
		private ToolStripMenuItem tsmiExecute;
		private ToolStripSeparator toolStripSeparator1;
		private TabPage tpDictionaries;
		private Controls.DictionarySettingsUserControl dictionarySettingsUc;
		private TabPage tpWelcome;
		private TabPage tpAvalanche;
		private Controls.AvalancheSettingsUserControl avalancheSettingsUc;
		private Controls.MetadataSettingsUserControl metadataSettingsUc;

	}
}