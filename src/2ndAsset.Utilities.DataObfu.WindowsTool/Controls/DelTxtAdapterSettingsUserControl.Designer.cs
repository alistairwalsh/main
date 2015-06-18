namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controls
{
	partial class DelTxtAdapterSettingsUserControl
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
			this.grpBxDelimitedTextSpec = new System.Windows.Forms.GroupBox();
			this.btnClearHeaderSpecs = new System.Windows.Forms.Button();
			this.btnAddHeaderSpec = new System.Windows.Forms.Button();
			this.btnMoveUpHeaderSpec = new System.Windows.Forms.Button();
			this.btnMoveDnHeaderSpec = new System.Windows.Forms.Button();
			this.btnRefreshFieldSpecs = new System.Windows.Forms.Button();
			this.btnRemoveHeaderSpec = new System.Windows.Forms.Button();
			this.lvFieldSpecs = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tmTableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tmLabel5 = new System.Windows.Forms.Label();
			this.tmLabel6 = new System.Windows.Forms.Label();
			this.tmLabel7 = new System.Windows.Forms.Label();
			this.tmLabel8 = new System.Windows.Forms.Label();
			this.chkBxFirstRowIsHeaders = new System.Windows.Forms.CheckBox();
			this.txtBxQuoteValue = new System.Windows.Forms.TextBox();
			this.txtBxFieldDelimiter = new System.Windows.Forms.TextBox();
			this.txtBxRecordDelimiter = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtBxTextFilePath = new System.Windows.Forms.TextBox();
			this.lblTextFilePath = new System.Windows.Forms.Label();
			this.grpBxDelimitedTextSpec.SuspendLayout();
			this.tmTableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpBxDelimitedTextSpec
			// 
			this.grpBxDelimitedTextSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpBxDelimitedTextSpec.Controls.Add(this.btnClearHeaderSpecs);
			this.grpBxDelimitedTextSpec.Controls.Add(this.btnAddHeaderSpec);
			this.grpBxDelimitedTextSpec.Controls.Add(this.btnMoveUpHeaderSpec);
			this.grpBxDelimitedTextSpec.Controls.Add(this.btnMoveDnHeaderSpec);
			this.grpBxDelimitedTextSpec.Controls.Add(this.btnRefreshFieldSpecs);
			this.grpBxDelimitedTextSpec.Controls.Add(this.btnRemoveHeaderSpec);
			this.grpBxDelimitedTextSpec.Controls.Add(this.lvFieldSpecs);
			this.grpBxDelimitedTextSpec.Controls.Add(this.tmTableLayoutPanel1);
			this.grpBxDelimitedTextSpec.Location = new System.Drawing.Point(3, 32);
			this.grpBxDelimitedTextSpec.Name = "grpBxDelimitedTextSpec";
			this.grpBxDelimitedTextSpec.Size = new System.Drawing.Size(414, 250);
			this.grpBxDelimitedTextSpec.TabIndex = 9;
			this.grpBxDelimitedTextSpec.TabStop = false;
			this.grpBxDelimitedTextSpec.Text = "Delimited Text Specification";
			// 
			// btnClearHeaderSpecs
			// 
			this.btnClearHeaderSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClearHeaderSpecs.Location = new System.Drawing.Point(13, 221);
			this.btnClearHeaderSpecs.Name = "btnClearHeaderSpecs";
			this.btnClearHeaderSpecs.Size = new System.Drawing.Size(75, 23);
			this.btnClearHeaderSpecs.TabIndex = 5;
			this.btnClearHeaderSpecs.Text = "Clear";
			this.btnClearHeaderSpecs.UseVisualStyleBackColor = true;
			this.btnClearHeaderSpecs.Click += new System.EventHandler(this.btnClearHeaderSpecs_Click);
			// 
			// btnAddHeaderSpec
			// 
			this.btnAddHeaderSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddHeaderSpec.Location = new System.Drawing.Point(171, 221);
			this.btnAddHeaderSpec.Name = "btnAddHeaderSpec";
			this.btnAddHeaderSpec.Size = new System.Drawing.Size(75, 23);
			this.btnAddHeaderSpec.TabIndex = 6;
			this.btnAddHeaderSpec.Text = "Add";
			this.btnAddHeaderSpec.UseVisualStyleBackColor = true;
			this.btnAddHeaderSpec.Click += new System.EventHandler(this.btnAddHeaderSpec_Click);
			// 
			// btnMoveUpHeaderSpec
			// 
			this.btnMoveUpHeaderSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveUpHeaderSpec.Location = new System.Drawing.Point(333, 163);
			this.btnMoveUpHeaderSpec.Name = "btnMoveUpHeaderSpec";
			this.btnMoveUpHeaderSpec.Size = new System.Drawing.Size(75, 23);
			this.btnMoveUpHeaderSpec.TabIndex = 3;
			this.btnMoveUpHeaderSpec.Text = "Move Up";
			this.btnMoveUpHeaderSpec.UseVisualStyleBackColor = true;
			this.btnMoveUpHeaderSpec.Click += new System.EventHandler(this.btnMoveUpHeaderSpec_Click);
			// 
			// btnMoveDnHeaderSpec
			// 
			this.btnMoveDnHeaderSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnMoveDnHeaderSpec.Location = new System.Drawing.Point(333, 192);
			this.btnMoveDnHeaderSpec.Name = "btnMoveDnHeaderSpec";
			this.btnMoveDnHeaderSpec.Size = new System.Drawing.Size(75, 23);
			this.btnMoveDnHeaderSpec.TabIndex = 4;
			this.btnMoveDnHeaderSpec.Text = "Move Dn";
			this.btnMoveDnHeaderSpec.UseVisualStyleBackColor = true;
			this.btnMoveDnHeaderSpec.Click += new System.EventHandler(this.btnMoveDnHeaderSpec_Click);
			// 
			// btnRefreshFieldSpecs
			// 
			this.btnRefreshFieldSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefreshFieldSpecs.Location = new System.Drawing.Point(333, 83);
			this.btnRefreshFieldSpecs.Name = "btnRefreshFieldSpecs";
			this.btnRefreshFieldSpecs.Size = new System.Drawing.Size(75, 23);
			this.btnRefreshFieldSpecs.TabIndex = 2;
			this.btnRefreshFieldSpecs.Text = "Refresh";
			this.btnRefreshFieldSpecs.UseVisualStyleBackColor = true;
			this.btnRefreshFieldSpecs.Click += new System.EventHandler(this.btnRefreshFieldSpecs_Click);
			// 
			// btnRemoveHeaderSpec
			// 
			this.btnRemoveHeaderSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRemoveHeaderSpec.Location = new System.Drawing.Point(252, 221);
			this.btnRemoveHeaderSpec.Name = "btnRemoveHeaderSpec";
			this.btnRemoveHeaderSpec.Size = new System.Drawing.Size(75, 23);
			this.btnRemoveHeaderSpec.TabIndex = 7;
			this.btnRemoveHeaderSpec.Text = "Remove";
			this.btnRemoveHeaderSpec.UseVisualStyleBackColor = true;
			this.btnRemoveHeaderSpec.Click += new System.EventHandler(this.btnRemoveHeaderSpec_Click);
			// 
			// lvFieldSpecs
			// 
			this.lvFieldSpecs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lvFieldSpecs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.lvFieldSpecs.FullRowSelect = true;
			this.lvFieldSpecs.HideSelection = false;
			this.lvFieldSpecs.Location = new System.Drawing.Point(13, 83);
			this.lvFieldSpecs.MultiSelect = false;
			this.lvFieldSpecs.Name = "lvFieldSpecs";
			this.lvFieldSpecs.Size = new System.Drawing.Size(314, 132);
			this.lvFieldSpecs.TabIndex = 1;
			this.lvFieldSpecs.UseCompatibleStateImageBehavior = false;
			this.lvFieldSpecs.View = System.Windows.Forms.View.Details;
			this.lvFieldSpecs.SelectedIndexChanged += new System.EventHandler(this.lvFieldSpecs_SelectedIndexChanged);
			this.lvFieldSpecs.DoubleClick += new System.EventHandler(this.lvFieldSpecs_DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Field Name";
			this.columnHeader1.Width = 150;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Field Type";
			this.columnHeader2.Width = 100;
			// 
			// tmTableLayoutPanel1
			// 
			this.tmTableLayoutPanel1.ColumnCount = 4;
			this.tmTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tmTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tmTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tmTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tmTableLayoutPanel1.Controls.Add(this.tmLabel5, 0, 0);
			this.tmTableLayoutPanel1.Controls.Add(this.tmLabel6, 0, 1);
			this.tmTableLayoutPanel1.Controls.Add(this.tmLabel7, 2, 0);
			this.tmTableLayoutPanel1.Controls.Add(this.tmLabel8, 2, 1);
			this.tmTableLayoutPanel1.Controls.Add(this.chkBxFirstRowIsHeaders, 1, 0);
			this.tmTableLayoutPanel1.Controls.Add(this.txtBxQuoteValue, 1, 1);
			this.tmTableLayoutPanel1.Controls.Add(this.txtBxFieldDelimiter, 3, 1);
			this.tmTableLayoutPanel1.Controls.Add(this.txtBxRecordDelimiter, 3, 0);
			this.tmTableLayoutPanel1.Location = new System.Drawing.Point(13, 19);
			this.tmTableLayoutPanel1.Name = "tmTableLayoutPanel1";
			this.tmTableLayoutPanel1.RowCount = 2;
			this.tmTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tmTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tmTableLayoutPanel1.Size = new System.Drawing.Size(395, 57);
			this.tmTableLayoutPanel1.TabIndex = 0;
			// 
			// tmLabel5
			// 
			this.tmLabel5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tmLabel5.AutoSize = true;
			this.tmLabel5.Location = new System.Drawing.Point(3, 7);
			this.tmLabel5.Name = "tmLabel5";
			this.tmLabel5.Size = new System.Drawing.Size(110, 13);
			this.tmLabel5.TabIndex = 0;
			this.tmLabel5.Text = "1st Record is Header:";
			// 
			// tmLabel6
			// 
			this.tmLabel6.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tmLabel6.AutoSize = true;
			this.tmLabel6.Location = new System.Drawing.Point(3, 36);
			this.tmLabel6.Name = "tmLabel6";
			this.tmLabel6.Size = new System.Drawing.Size(69, 13);
			this.tmLabel6.TabIndex = 4;
			this.tmLabel6.Text = "Quote Value:";
			// 
			// tmLabel7
			// 
			this.tmLabel7.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tmLabel7.AutoSize = true;
			this.tmLabel7.Location = new System.Drawing.Point(200, 7);
			this.tmLabel7.Name = "tmLabel7";
			this.tmLabel7.Size = new System.Drawing.Size(88, 13);
			this.tmLabel7.TabIndex = 2;
			this.tmLabel7.Text = "Record Delimiter:";
			// 
			// tmLabel8
			// 
			this.tmLabel8.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tmLabel8.AutoSize = true;
			this.tmLabel8.Location = new System.Drawing.Point(200, 36);
			this.tmLabel8.Name = "tmLabel8";
			this.tmLabel8.Size = new System.Drawing.Size(75, 13);
			this.tmLabel8.TabIndex = 6;
			this.tmLabel8.Text = "Field Delimiter:";
			// 
			// chkBxFirstRowIsHeaders
			// 
			this.chkBxFirstRowIsHeaders.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.chkBxFirstRowIsHeaders.AutoSize = true;
			this.chkBxFirstRowIsHeaders.Location = new System.Drawing.Point(150, 7);
			this.chkBxFirstRowIsHeaders.Name = "chkBxFirstRowIsHeaders";
			this.chkBxFirstRowIsHeaders.Size = new System.Drawing.Size(15, 14);
			this.chkBxFirstRowIsHeaders.TabIndex = 1;
			this.chkBxFirstRowIsHeaders.UseVisualStyleBackColor = true;
			// 
			// txtBxQuoteValue
			// 
			this.txtBxQuoteValue.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtBxQuoteValue.Location = new System.Drawing.Point(121, 32);
			this.txtBxQuoteValue.Name = "txtBxQuoteValue";
			this.txtBxQuoteValue.Size = new System.Drawing.Size(73, 20);
			this.txtBxQuoteValue.TabIndex = 5;
			// 
			// txtBxFieldDelimiter
			// 
			this.txtBxFieldDelimiter.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtBxFieldDelimiter.Location = new System.Drawing.Point(308, 32);
			this.txtBxFieldDelimiter.Name = "txtBxFieldDelimiter";
			this.txtBxFieldDelimiter.Size = new System.Drawing.Size(73, 20);
			this.txtBxFieldDelimiter.TabIndex = 7;
			// 
			// txtBxRecordDelimiter
			// 
			this.txtBxRecordDelimiter.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtBxRecordDelimiter.Location = new System.Drawing.Point(308, 4);
			this.txtBxRecordDelimiter.Name = "txtBxRecordDelimiter";
			this.txtBxRecordDelimiter.Size = new System.Drawing.Size(73, 20);
			this.txtBxRecordDelimiter.TabIndex = 3;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(387, 3);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(30, 23);
			this.btnBrowse.TabIndex = 8;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtBxTextFilePath
			// 
			this.txtBxTextFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBxTextFilePath.Location = new System.Drawing.Point(83, 3);
			this.txtBxTextFilePath.Name = "txtBxTextFilePath";
			this.txtBxTextFilePath.Size = new System.Drawing.Size(302, 20);
			this.txtBxTextFilePath.TabIndex = 7;
			// 
			// lblTextFilePath
			// 
			this.lblTextFilePath.AutoSize = true;
			this.lblTextFilePath.Location = new System.Drawing.Point(2, 6);
			this.lblTextFilePath.Name = "lblTextFilePath";
			this.lblTextFilePath.Size = new System.Drawing.Size(75, 13);
			this.lblTextFilePath.TabIndex = 6;
			this.lblTextFilePath.Text = "Text File Path:";
			// 
			// DelTxtAdapterSettingsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpBxDelimitedTextSpec);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtBxTextFilePath);
			this.Controls.Add(this.lblTextFilePath);
			this.MinimumSize = new System.Drawing.Size(424, 287);
			this.Name = "DelTxtAdapterSettingsUserControl";
			this.Size = new System.Drawing.Size(424, 287);
			this.grpBxDelimitedTextSpec.ResumeLayout(false);
			this.tmTableLayoutPanel1.ResumeLayout(false);
			this.tmTableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox grpBxDelimitedTextSpec;
		private System.Windows.Forms.Button btnClearHeaderSpecs;
		private System.Windows.Forms.Button btnAddHeaderSpec;
		private System.Windows.Forms.Button btnMoveUpHeaderSpec;
		private System.Windows.Forms.Button btnMoveDnHeaderSpec;
		private System.Windows.Forms.Button btnRefreshFieldSpecs;
		private System.Windows.Forms.Button btnRemoveHeaderSpec;
		private System.Windows.Forms.ListView lvFieldSpecs;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.TableLayoutPanel tmTableLayoutPanel1;
		private System.Windows.Forms.Label tmLabel5;
		private System.Windows.Forms.Label tmLabel6;
		private System.Windows.Forms.Label tmLabel7;
		private System.Windows.Forms.Label tmLabel8;
		private System.Windows.Forms.CheckBox chkBxFirstRowIsHeaders;
		private System.Windows.Forms.TextBox txtBxQuoteValue;
		private System.Windows.Forms.TextBox txtBxFieldDelimiter;
		private System.Windows.Forms.TextBox txtBxRecordDelimiter;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtBxTextFilePath;
		private System.Windows.Forms.Label lblTextFilePath;
	}
}
