
using System.Windows.Forms;

using _2ndAsset.Common.WinForms.Controls;

using _2ndAsset.Common.WinForms.Controls;

namespace _2ndAsset.Ssis.Components.UI.Forms
{
	partial class ObfuConfMainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObfuConfMainForm));
			this.pgRoot = new PropertyGrid();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.btnUpdateMetadata = new Button();
			this.btnImport = new Button();
			this.btnExport = new Button();
			this.SuspendLayout();
			// 
			// pgRoot
			// 
			this.pgRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pgRoot.Location = new System.Drawing.Point(1, 1);
			this.pgRoot.Name = "pgRoot";
			this.pgRoot.Size = new System.Drawing.Size(595, 322);
			this.pgRoot.TabIndex = 1;
			this.pgRoot.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgRoot_PropertyValueChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(511, 334);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(430, 334);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnUpdateMetadata
			// 
			this.btnUpdateMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnUpdateMetadata.Location = new System.Drawing.Point(12, 334);
			this.btnUpdateMetadata.Name = "btnUpdateMetadata";
			this.btnUpdateMetadata.Size = new System.Drawing.Size(75, 23);
			this.btnUpdateMetadata.TabIndex = 3;
			this.btnUpdateMetadata.Text = "Update";
			this.btnUpdateMetadata.UseVisualStyleBackColor = true;
			this.btnUpdateMetadata.Click += new System.EventHandler(this.btnUpdateMetadata_Click);
			// 
			// btnImport
			// 
			this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnImport.Location = new System.Drawing.Point(93, 334);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(75, 23);
			this.btnImport.TabIndex = 3;
			this.btnImport.Text = "Import...";
			this.btnImport.UseVisualStyleBackColor = true;
			this.btnImport.Visible = false;
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnExport
			// 
			this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnExport.Location = new System.Drawing.Point(174, 334);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(75, 23);
			this.btnExport.TabIndex = 3;
			this.btnExport.Text = "Export...";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Visible = false;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// ObfuConfMainForm
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(597, 365);
			this.Controls.Add(this.btnExport);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.btnUpdateMetadata);
			this.Controls.Add(this.pgRoot);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ObfuConfMainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);

		}

		#endregion

		private Button btnCancel;
		private Button btnOK;
		private PropertyGrid pgRoot;
		private Button btnUpdateMetadata;
		private Button btnImport;
		private Button btnExport;

	}
}