
using System.Windows.Forms;

using _2ndAsset.Common.WinForms.Controls;

namespace _2ndAsset.Ssis.Components.UI.Forms
{
	partial class DictConfMainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DictConfMainForm));
			this.pgRoot = new PropertyGrid();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.lvMain = new ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnAdd = new Button();
			this.btnRemove = new Button();
			this.btnClear = new Button();
			this.SuspendLayout();
			// 
			// pgRoot
			// 
			this.pgRoot.Location = new System.Drawing.Point(302, 1);
			this.pgRoot.Name = "pgRoot";
			this.pgRoot.Size = new System.Drawing.Size(285, 316);
			this.pgRoot.TabIndex = 1;
			this.pgRoot.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgRoot_PropertyValueChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(500, 328);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(419, 328);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lvMain
			// 
			this.lvMain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.lvMain.FullRowSelect = true;
			this.lvMain.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvMain.HideSelection = false;
			this.lvMain.Location = new System.Drawing.Point(0, 1);
			this.lvMain.MultiSelect = false;
			this.lvMain.Name = "lvMain";
			this.lvMain.Size = new System.Drawing.Size(285, 316);
			this.lvMain.TabIndex = 0;
			this.lvMain.UseCompatibleStateImageBehavior = false;
			this.lvMain.View = System.Windows.Forms.View.Details;
			this.lvMain.SelectedIndexChanged += new System.EventHandler(this.lvMain_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "DictionaryId";
			this.columnHeader1.Width = 75;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "RecordCount";
			this.columnHeader2.Width = 75;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "CommandText";
			this.columnHeader3.Width = 125;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(12, 328);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(75, 23);
			this.btnAdd.TabIndex = 2;
			this.btnAdd.Text = "Add";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(93, 328);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(75, 23);
			this.btnRemove.TabIndex = 3;
			this.btnRemove.Text = "Remove";
			this.btnRemove.UseVisualStyleBackColor = true;
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(210, 328);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 4;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// DictConfMainForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(586, 359);
			this.Controls.Add(this.lvMain);
			this.Controls.Add(this.pgRoot);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DictConfMainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}

		#endregion

		private Button btnCancel;
		private Button btnOK;
		private PropertyGrid pgRoot;
		private ListView lvMain;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private Button btnAdd;
		private Button btnRemove;
		private Button btnClear;

	}
}