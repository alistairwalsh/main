using System.Windows.Forms;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	partial class ObfuscationUserControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tpWelcome = new System.Windows.Forms.TabPage();
			this.tpAvalanche = new System.Windows.Forms.TabPage();
			this.avalancheSettingsUc = new _2ndAsset.ObfuscationEngine.UI.Controls.AvalancheSettingsUserControl();
			this.tpSource = new System.Windows.Forms.TabPage();
			this.sourceAdapterSettingsUc = new _2ndAsset.ObfuscationEngine.UI.Controls.AdapterSettingsUserControl();
			this.tpDictionaries = new System.Windows.Forms.TabPage();
			this.dictionarySettingsUc = new _2ndAsset.ObfuscationEngine.UI.Controls.DictionarySettingsUserControl();
			this.tpMetadata = new System.Windows.Forms.TabPage();
			this.metadataSettingsUc = new _2ndAsset.ObfuscationEngine.UI.Controls.MetadataSettingsUserControl();
			this.tpDestination = new System.Windows.Forms.TabPage();
			this.destinationAdapterSettingsUc = new _2ndAsset.ObfuscationEngine.UI.Controls.AdapterSettingsUserControl();
			this.tabMain.SuspendLayout();
			this.tpAvalanche.SuspendLayout();
			this.tpSource.SuspendLayout();
			this.tpDictionaries.SuspendLayout();
			this.tpMetadata.SuspendLayout();
			this.tpDestination.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.tpWelcome);
			this.tabMain.Controls.Add(this.tpAvalanche);
			this.tabMain.Controls.Add(this.tpSource);
			this.tabMain.Controls.Add(this.tpDictionaries);
			this.tabMain.Controls.Add(this.tpMetadata);
			this.tabMain.Controls.Add(this.tpDestination);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Location = new System.Drawing.Point(0, 0);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(883, 387);
			this.tabMain.TabIndex = 6;
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
			this.dictionarySettingsUc.SelectedDictionarySpecListView = null;
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
			// metadataSettingsUc
			// 
			this.metadataSettingsUc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.metadataSettingsUc.Location = new System.Drawing.Point(0, 0);
			this.metadataSettingsUc.MinimumSize = new System.Drawing.Size(460, 313);
			this.metadataSettingsUc.Name = "metadataSettingsUc";
			this.metadataSettingsUc.SelectedMetaColumnSpecListView = null;
			this.metadataSettingsUc.Size = new System.Drawing.Size(875, 361);
			this.metadataSettingsUc.TabIndex = 0;
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
			// ObfuscationUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabMain);
			this.MinimumSize = new System.Drawing.Size(460, 313);
			this.Name = "ObfuscationUserControl";
			this.Size = new System.Drawing.Size(883, 387);
			this.tabMain.ResumeLayout(false);
			this.tpAvalanche.ResumeLayout(false);
			this.tpSource.ResumeLayout(false);
			this.tpDictionaries.ResumeLayout(false);
			this.tpMetadata.ResumeLayout(false);
			this.tpDestination.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private TabControl tabMain;
		private TabPage tpWelcome;
		private TabPage tpAvalanche;
		private AvalancheSettingsUserControl avalancheSettingsUc;
		private TabPage tpSource;
		private AdapterSettingsUserControl sourceAdapterSettingsUc;
		private TabPage tpDictionaries;
		private DictionarySettingsUserControl dictionarySettingsUc;
		private TabPage tpMetadata;
		private MetadataSettingsUserControl metadataSettingsUc;
		private TabPage tpDestination;
		private AdapterSettingsUserControl destinationAdapterSettingsUc;







	}
}
