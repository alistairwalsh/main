using System.Windows.Forms;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controls
{
	partial class MetadataSettingsUserControl
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
			if (disposing && (components != null))
			{
				components.Dispose();
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
			this.grpBxMetaColumnSpecs = new System.Windows.Forms.GroupBox();
			this.btnRefreshMetaColumnSpecs = new System.Windows.Forms.Button();
			this.btnClearMetaColumnSpecs = new System.Windows.Forms.Button();
			this.btnAddMetaColumnSpec = new System.Windows.Forms.Button();
			this.btnMoveUpMetaColumnSpec = new System.Windows.Forms.Button();
			this.btnMoveDnMetaColumnSpec = new System.Windows.Forms.Button();
			this.btnRemoveMetaColumnSpec = new System.Windows.Forms.Button();
			this.lvMetaColumnSpecs = new System.Windows.Forms.ListView();
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.grpBxMetaColumnSpecs.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpBxMetaColumnSpecs
			// 
			this.grpBxMetaColumnSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpBxMetaColumnSpecs.Controls.Add(this.btnRefreshMetaColumnSpecs);
			this.grpBxMetaColumnSpecs.Controls.Add(this.btnClearMetaColumnSpecs);
			this.grpBxMetaColumnSpecs.Controls.Add(this.btnAddMetaColumnSpec);
			this.grpBxMetaColumnSpecs.Controls.Add(this.btnMoveUpMetaColumnSpec);
			this.grpBxMetaColumnSpecs.Controls.Add(this.btnMoveDnMetaColumnSpec);
			this.grpBxMetaColumnSpecs.Controls.Add(this.btnRemoveMetaColumnSpec);
			this.grpBxMetaColumnSpecs.Controls.Add(this.lvMetaColumnSpecs);
			this.grpBxMetaColumnSpecs.Location = new System.Drawing.Point(3, 3);
			this.grpBxMetaColumnSpecs.Name = "grpBxMetaColumnSpecs";
			this.grpBxMetaColumnSpecs.Size = new System.Drawing.Size(451, 307);
			this.grpBxMetaColumnSpecs.TabIndex = 0;
			this.grpBxMetaColumnSpecs.TabStop = false;
			this.grpBxMetaColumnSpecs.Text = "MetaColumn Specifications";
			// 
			// btnRefreshMetaColumnSpecs
			// 
			this.btnRefreshMetaColumnSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefreshMetaColumnSpecs.Location = new System.Drawing.Point(370, 19);
			this.btnRefreshMetaColumnSpecs.Name = "btnRefreshMetaColumnSpecs";
			this.btnRefreshMetaColumnSpecs.Size = new System.Drawing.Size(75, 23);
			this.btnRefreshMetaColumnSpecs.TabIndex = 1;
			this.btnRefreshMetaColumnSpecs.Text = "Refresh";
			this.btnRefreshMetaColumnSpecs.UseVisualStyleBackColor = true;
			this.btnRefreshMetaColumnSpecs.Click += new System.EventHandler(this.btnRefreshMetaColumnSpecs_Click);
			// 
			// btnClearMetaColumnSpecs
			// 
			this.btnClearMetaColumnSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClearMetaColumnSpecs.Location = new System.Drawing.Point(13, 278);
			this.btnClearMetaColumnSpecs.Name = "btnClearMetaColumnSpecs";
			this.btnClearMetaColumnSpecs.Size = new System.Drawing.Size(75, 23);
			this.btnClearMetaColumnSpecs.TabIndex = 6;
			this.btnClearMetaColumnSpecs.Text = "Clear";
			this.btnClearMetaColumnSpecs.UseVisualStyleBackColor = true;
			this.btnClearMetaColumnSpecs.Click += new System.EventHandler(this.btnClearMetaColumnSpecs_Click);
			// 
			// btnAddMetaColumnSpec
			// 
			this.btnAddMetaColumnSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddMetaColumnSpec.Location = new System.Drawing.Point(208, 278);
			this.btnAddMetaColumnSpec.Name = "btnAddMetaColumnSpec";
			this.btnAddMetaColumnSpec.Size = new System.Drawing.Size(75, 23);
			this.btnAddMetaColumnSpec.TabIndex = 5;
			this.btnAddMetaColumnSpec.Text = "Add";
			this.btnAddMetaColumnSpec.UseVisualStyleBackColor = true;
			this.btnAddMetaColumnSpec.Click += new System.EventHandler(this.btnAddMetaColumnSpec_Click);
			// 
			// btnMoveUpMetaColumnSpec
			// 
			this.btnMoveUpMetaColumnSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveUpMetaColumnSpec.Location = new System.Drawing.Point(370, 220);
			this.btnMoveUpMetaColumnSpec.Name = "btnMoveUpMetaColumnSpec";
			this.btnMoveUpMetaColumnSpec.Size = new System.Drawing.Size(75, 23);
			this.btnMoveUpMetaColumnSpec.TabIndex = 2;
			this.btnMoveUpMetaColumnSpec.Text = "Move Up";
			this.btnMoveUpMetaColumnSpec.UseVisualStyleBackColor = true;
			this.btnMoveUpMetaColumnSpec.Click += new System.EventHandler(this.btnMoveUpMetaColumnSpec_Click);
			// 
			// btnMoveDnMetaColumnSpec
			// 
			this.btnMoveDnMetaColumnSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveDnMetaColumnSpec.Location = new System.Drawing.Point(370, 249);
			this.btnMoveDnMetaColumnSpec.Name = "btnMoveDnMetaColumnSpec";
			this.btnMoveDnMetaColumnSpec.Size = new System.Drawing.Size(75, 23);
			this.btnMoveDnMetaColumnSpec.TabIndex = 3;
			this.btnMoveDnMetaColumnSpec.Text = "Move Dn";
			this.btnMoveDnMetaColumnSpec.UseVisualStyleBackColor = true;
			this.btnMoveDnMetaColumnSpec.Click += new System.EventHandler(this.btnMoveDnMetaColumnSpec_Click);
			// 
			// btnRemoveMetaColumnSpec
			// 
			this.btnRemoveMetaColumnSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveMetaColumnSpec.Location = new System.Drawing.Point(289, 278);
			this.btnRemoveMetaColumnSpec.Name = "btnRemoveMetaColumnSpec";
			this.btnRemoveMetaColumnSpec.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveMetaColumnSpec.TabIndex = 4;
			this.btnRemoveMetaColumnSpec.Text = "Remove";
			this.btnRemoveMetaColumnSpec.UseVisualStyleBackColor = true;
			this.btnRemoveMetaColumnSpec.Click += new System.EventHandler(this.btnRemoveMetaColumnSpec_Click);
			// 
			// lvMetaColumnSpecs
			// 
			this.lvMetaColumnSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvMetaColumnSpecs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader9,
            this.columnHeader10});
			this.lvMetaColumnSpecs.FullRowSelect = true;
			this.lvMetaColumnSpecs.HideSelection = false;
			this.lvMetaColumnSpecs.Location = new System.Drawing.Point(13, 19);
			this.lvMetaColumnSpecs.MultiSelect = false;
			this.lvMetaColumnSpecs.Name = "lvMetaColumnSpecs";
			this.lvMetaColumnSpecs.Size = new System.Drawing.Size(351, 253);
			this.lvMetaColumnSpecs.TabIndex = 0;
			this.lvMetaColumnSpecs.UseCompatibleStateImageBehavior = false;
			this.lvMetaColumnSpecs.View = System.Windows.Forms.View.Details;
			this.lvMetaColumnSpecs.SelectedIndexChanged += new System.EventHandler(this.lvMetaColumnSpecs_SelectedIndexChanged);
			this.lvMetaColumnSpecs.DoubleClick += new System.EventHandler(this.lvMetaColumnSpecs_DoubleClick);
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Column Name";
			this.columnHeader4.Width = 150;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Is Column Nullable";
			this.columnHeader5.Width = 150;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Obfuscation Strategy";
			this.columnHeader6.Width = 100;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Dictionary Reference";
			this.columnHeader9.Width = 100;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Extent Value";
			this.columnHeader10.Width = 100;
			// 
			// MetadataSettingsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpBxMetaColumnSpecs);
			this.MinimumSize = new System.Drawing.Size(460, 313);
			this.Name = "MetadataSettingsUserControl";
			this.Size = new System.Drawing.Size(460, 313);
			this.grpBxMetaColumnSpecs.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private GroupBox grpBxMetaColumnSpecs;
		private Button btnClearMetaColumnSpecs;
		private Button btnAddMetaColumnSpec;
		private Button btnMoveUpMetaColumnSpec;
		private Button btnMoveDnMetaColumnSpec;
		private ListView lvMetaColumnSpecs;
		private Button btnRemoveMetaColumnSpec;
		private Button btnRefreshMetaColumnSpecs;
		private ColumnHeader columnHeader4;
		private ColumnHeader columnHeader5;
		private ColumnHeader columnHeader6;
		private ColumnHeader columnHeader9;
		private ColumnHeader columnHeader10;





	}
}
