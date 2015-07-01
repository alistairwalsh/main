using System.Windows.Forms;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	partial class DictionarySettingsUserControl
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
			this.components = new System.ComponentModel.Container();
			this.grpBxDictionarySpecs = new System.Windows.Forms.GroupBox();
			this.btnClearDictionarySpecs = new System.Windows.Forms.Button();
			this.btnAddDictionarySpec = new System.Windows.Forms.Button();
			this.btnMoveUpDictionarySpec = new System.Windows.Forms.Button();
			this.btnMoveDnDictionarySpec = new System.Windows.Forms.Button();
			this.btnRemoveDictionarySpec = new System.Windows.Forms.Button();
			this.lvDictionarySpecs = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiAdapterSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.grpBxDictionarySpecs.SuspendLayout();
			this.cmsMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpBxDictionarySpecs
			// 
			this.grpBxDictionarySpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpBxDictionarySpecs.Controls.Add(this.btnClearDictionarySpecs);
			this.grpBxDictionarySpecs.Controls.Add(this.btnAddDictionarySpec);
			this.grpBxDictionarySpecs.Controls.Add(this.btnMoveUpDictionarySpec);
			this.grpBxDictionarySpecs.Controls.Add(this.btnMoveDnDictionarySpec);
			this.grpBxDictionarySpecs.Controls.Add(this.btnRemoveDictionarySpec);
			this.grpBxDictionarySpecs.Controls.Add(this.lvDictionarySpecs);
			this.grpBxDictionarySpecs.Location = new System.Drawing.Point(3, 3);
			this.grpBxDictionarySpecs.Name = "grpBxDictionarySpecs";
			this.grpBxDictionarySpecs.Size = new System.Drawing.Size(451, 307);
			this.grpBxDictionarySpecs.TabIndex = 10;
			this.grpBxDictionarySpecs.TabStop = false;
			this.grpBxDictionarySpecs.Text = "Dictionary Specifications";
			// 
			// btnClearDictionarySpecs
			// 
			this.btnClearDictionarySpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClearDictionarySpecs.Location = new System.Drawing.Point(13, 278);
			this.btnClearDictionarySpecs.Name = "btnClearDictionarySpecs";
			this.btnClearDictionarySpecs.Size = new System.Drawing.Size(75, 23);
			this.btnClearDictionarySpecs.TabIndex = 5;
			this.btnClearDictionarySpecs.Text = "Clear";
			this.btnClearDictionarySpecs.UseVisualStyleBackColor = true;
			this.btnClearDictionarySpecs.Click += new System.EventHandler(this.btnClearDictionarySpecs_Click);
			// 
			// btnAddDictionarySpec
			// 
			this.btnAddDictionarySpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddDictionarySpec.Location = new System.Drawing.Point(208, 278);
			this.btnAddDictionarySpec.Name = "btnAddDictionarySpec";
			this.btnAddDictionarySpec.Size = new System.Drawing.Size(75, 23);
			this.btnAddDictionarySpec.TabIndex = 6;
			this.btnAddDictionarySpec.Text = "Add";
			this.btnAddDictionarySpec.UseVisualStyleBackColor = true;
			this.btnAddDictionarySpec.Click += new System.EventHandler(this.btnAddDictionarySpec_Click);
			// 
			// btnMoveUpDictionarySpec
			// 
			this.btnMoveUpDictionarySpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveUpDictionarySpec.Location = new System.Drawing.Point(370, 220);
			this.btnMoveUpDictionarySpec.Name = "btnMoveUpDictionarySpec";
			this.btnMoveUpDictionarySpec.Size = new System.Drawing.Size(75, 23);
			this.btnMoveUpDictionarySpec.TabIndex = 3;
			this.btnMoveUpDictionarySpec.Text = "Move Up";
			this.btnMoveUpDictionarySpec.UseVisualStyleBackColor = true;
			this.btnMoveUpDictionarySpec.Click += new System.EventHandler(this.btnMoveUpDictionarySpec_Click);
			// 
			// btnMoveDnDictionarySpec
			// 
			this.btnMoveDnDictionarySpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveDnDictionarySpec.Location = new System.Drawing.Point(370, 249);
			this.btnMoveDnDictionarySpec.Name = "btnMoveDnDictionarySpec";
			this.btnMoveDnDictionarySpec.Size = new System.Drawing.Size(75, 23);
			this.btnMoveDnDictionarySpec.TabIndex = 4;
			this.btnMoveDnDictionarySpec.Text = "Move Dn";
			this.btnMoveDnDictionarySpec.UseVisualStyleBackColor = true;
			this.btnMoveDnDictionarySpec.Click += new System.EventHandler(this.btnMoveDnDictionarySpec_Click);
			// 
			// btnRemoveDictionarySpec
			// 
			this.btnRemoveDictionarySpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveDictionarySpec.Location = new System.Drawing.Point(289, 278);
			this.btnRemoveDictionarySpec.Name = "btnRemoveDictionarySpec";
			this.btnRemoveDictionarySpec.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveDictionarySpec.TabIndex = 7;
			this.btnRemoveDictionarySpec.Text = "Remove";
			this.btnRemoveDictionarySpec.UseVisualStyleBackColor = true;
			this.btnRemoveDictionarySpec.Click += new System.EventHandler(this.btnRemoveDictionarySpec_Click);
			// 
			// lvDictionarySpecs
			// 
			this.lvDictionarySpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvDictionarySpecs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader8,
            this.columnHeader3});
			this.lvDictionarySpecs.ContextMenuStrip = this.cmsMain;
			this.lvDictionarySpecs.FullRowSelect = true;
			this.lvDictionarySpecs.HideSelection = false;
			this.lvDictionarySpecs.Location = new System.Drawing.Point(13, 19);
			this.lvDictionarySpecs.MultiSelect = false;
			this.lvDictionarySpecs.Name = "lvDictionarySpecs";
			this.lvDictionarySpecs.Size = new System.Drawing.Size(351, 253);
			this.lvDictionarySpecs.TabIndex = 1;
			this.lvDictionarySpecs.UseCompatibleStateImageBehavior = false;
			this.lvDictionarySpecs.View = System.Windows.Forms.View.Details;
			this.lvDictionarySpecs.SelectedIndexChanged += new System.EventHandler(this.lvDictionarySpecs_SelectedIndexChanged);
			this.lvDictionarySpecs.DoubleClick += new System.EventHandler(this.lvDictionarySpecs_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Dictionary ID";
			this.columnHeader1.Width = 150;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Preload Enabled";
			this.columnHeader2.Width = 100;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Record Count";
			this.columnHeader8.Width = 100;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Adapter Settings";
			this.columnHeader3.Width = 150;
			// 
			// cmsMain
			// 
			this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdapterSettings});
			this.cmsMain.Name = "cmsMain";
			this.cmsMain.Size = new System.Drawing.Size(171, 26);
			// 
			// tsmiAdapterSettings
			// 
			this.tsmiAdapterSettings.Name = "tsmiAdapterSettings";
			this.tsmiAdapterSettings.Size = new System.Drawing.Size(170, 22);
			this.tsmiAdapterSettings.Text = "Adapter Settings...";
			this.tsmiAdapterSettings.Click += new System.EventHandler(this.tsmiAdapterSettings_Click);
			// 
			// DictionarySettingsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpBxDictionarySpecs);
			this.MinimumSize = new System.Drawing.Size(460, 313);
			this.Name = "DictionarySettingsUserControl";
			this.Size = new System.Drawing.Size(460, 313);
			this.grpBxDictionarySpecs.ResumeLayout(false);
			this.cmsMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private GroupBox grpBxDictionarySpecs;
		private Button btnClearDictionarySpecs;
		private Button btnAddDictionarySpec;
		private Button btnMoveUpDictionarySpec;
		private Button btnMoveDnDictionarySpec;
		private ListView lvDictionarySpecs;
		private ColumnHeader columnHeader1;
		private ColumnHeader columnHeader2;
		private ColumnHeader columnHeader8;
		private Button btnRemoveDictionarySpec;
		private ContextMenuStrip cmsMain;
		private ToolStripMenuItem tsmiAdapterSettings;
		private ColumnHeader columnHeader3;





	}
}
