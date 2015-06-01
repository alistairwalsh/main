using System.Windows.Forms;

using _2ndAsset.Common.WinForms.Controls;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Forms
{
	partial class AboutForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
			this.pbAppLogo = new PictureBox();
			this.lblProduct = new Label();
			this.lblVersion = new Label();
			this.lblCopyright = new Label();
			this.lblCompany = new Label();
			this.txtBxDescription = new TextBox();
			this.btnOK = new Button();
			this.lblWin32FileVersion = new Label();
			this.lblTrademark = new Label();
			this.lblTitle = new Label();
			this.lblInformationalVersion = new Label();
			this.lblConfiguration = new Label();
			this.tableLayoutPanel1 = new TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.pbAppLogo)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pbAppLogo
			// 
			this.pbAppLogo.BackColor = System.Drawing.Color.Transparent;
			this.pbAppLogo.Location = new System.Drawing.Point(12, 12);
			this.pbAppLogo.Name = "pbAppLogo";
			this.pbAppLogo.Size = new System.Drawing.Size(300, 300);
			this.pbAppLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbAppLogo.TabIndex = 12;
			this.pbAppLogo.TabStop = false;
			// 
			// lblProduct
			// 
			this.lblProduct.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblProduct.Location = new System.Drawing.Point(0, 0);
			this.lblProduct.Margin = new System.Windows.Forms.Padding(0);
			this.lblProduct.MaximumSize = new System.Drawing.Size(0, 17);
			this.lblProduct.Name = "lblProduct";
			this.lblProduct.Size = new System.Drawing.Size(371, 17);
			this.lblProduct.TabIndex = 0;
			this.lblProduct.Text = "%TEXT%";
			this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblVersion
			// 
			this.lblVersion.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblVersion.Location = new System.Drawing.Point(0, 22);
			this.lblVersion.Margin = new System.Windows.Forms.Padding(0);
			this.lblVersion.MaximumSize = new System.Drawing.Size(0, 17);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(371, 17);
			this.lblVersion.TabIndex = 1;
			this.lblVersion.Text = "%TEXT%";
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCopyright
			// 
			this.lblCopyright.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblCopyright.Location = new System.Drawing.Point(0, 44);
			this.lblCopyright.Margin = new System.Windows.Forms.Padding(0);
			this.lblCopyright.MaximumSize = new System.Drawing.Size(0, 17);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(371, 17);
			this.lblCopyright.TabIndex = 2;
			this.lblCopyright.Text = "%TEXT%";
			this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCompany
			// 
			this.lblCompany.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblCompany.Location = new System.Drawing.Point(0, 66);
			this.lblCompany.Margin = new System.Windows.Forms.Padding(0);
			this.lblCompany.MaximumSize = new System.Drawing.Size(0, 17);
			this.lblCompany.Name = "lblCompany";
			this.lblCompany.Size = new System.Drawing.Size(371, 17);
			this.lblCompany.TabIndex = 3;
			this.lblCompany.Text = "%TEXT%";
			this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtBxDescription
			// 
			this.txtBxDescription.Location = new System.Drawing.Point(318, 216);
			this.txtBxDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
			this.txtBxDescription.Multiline = true;
			this.txtBxDescription.Name = "txtBxDescription";
			this.txtBxDescription.ReadOnly = true;
			this.txtBxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtBxDescription.Size = new System.Drawing.Size(371, 96);
			this.txtBxDescription.TabIndex = 2;
			this.txtBxDescription.TabStop = false;
			this.txtBxDescription.Text = "%TEXT%";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOK.Location = new System.Drawing.Point(615, 318);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblWin32FileVersion
			// 
			this.lblWin32FileVersion.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblWin32FileVersion.Location = new System.Drawing.Point(0, 88);
			this.lblWin32FileVersion.Margin = new System.Windows.Forms.Padding(0);
			this.lblWin32FileVersion.MaximumSize = new System.Drawing.Size(0, 17);
			this.lblWin32FileVersion.Name = "lblWin32FileVersion";
			this.lblWin32FileVersion.Size = new System.Drawing.Size(371, 17);
			this.lblWin32FileVersion.TabIndex = 4;
			this.lblWin32FileVersion.Text = "%TEXT%";
			this.lblWin32FileVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTrademark
			// 
			this.lblTrademark.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblTrademark.Location = new System.Drawing.Point(0, 110);
			this.lblTrademark.Margin = new System.Windows.Forms.Padding(0);
			this.lblTrademark.MaximumSize = new System.Drawing.Size(0, 17);
			this.lblTrademark.Name = "lblTrademark";
			this.lblTrademark.Size = new System.Drawing.Size(371, 17);
			this.lblTrademark.TabIndex = 5;
			this.lblTrademark.Text = "%TEXT%";
			this.lblTrademark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTitle
			// 
			this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblTitle.Location = new System.Drawing.Point(0, 132);
			this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
			this.lblTitle.MaximumSize = new System.Drawing.Size(0, 17);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(371, 17);
			this.lblTitle.TabIndex = 6;
			this.lblTitle.Text = "%TEXT%";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblInformationalVersion
			// 
			this.lblInformationalVersion.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblInformationalVersion.Location = new System.Drawing.Point(0, 154);
			this.lblInformationalVersion.Margin = new System.Windows.Forms.Padding(0);
			this.lblInformationalVersion.MaximumSize = new System.Drawing.Size(0, 17);
			this.lblInformationalVersion.Name = "lblInformationalVersion";
			this.lblInformationalVersion.Size = new System.Drawing.Size(371, 17);
			this.lblInformationalVersion.TabIndex = 7;
			this.lblInformationalVersion.Text = "%TEXT%";
			this.lblInformationalVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblConfiguration
			// 
			this.lblConfiguration.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblConfiguration.Location = new System.Drawing.Point(0, 176);
			this.lblConfiguration.Margin = new System.Windows.Forms.Padding(0);
			this.lblConfiguration.MaximumSize = new System.Drawing.Size(0, 17);
			this.lblConfiguration.Name = "lblConfiguration";
			this.lblConfiguration.Size = new System.Drawing.Size(371, 17);
			this.lblConfiguration.TabIndex = 8;
			this.lblConfiguration.Text = "%TEXT%";
			this.lblConfiguration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.lblProduct, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblConfiguration, 0, 8);
			this.tableLayoutPanel1.Controls.Add(this.lblVersion, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.lblInformationalVersion, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.lblTitle, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.lblTrademark, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.lblWin32FileVersion, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.lblCopyright, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.lblCompany, 0, 3);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(318, 12);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 9;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(371, 198);
			this.tableLayoutPanel1.TabIndex = 1;
			// 
			// AboutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(702, 348);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.pbAppLogo);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.txtBxDescription);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.Padding = new System.Windows.Forms.Padding(9);
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			((System.ComponentModel.ISupportInitialize)(this.pbAppLogo)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private PictureBox pbAppLogo;
		private Label lblProduct;
		private Label lblVersion;
		private Label lblCopyright;
		private Label lblCompany;
		private TextBox txtBxDescription;
		private Button btnOK;
		private Label lblWin32FileVersion;
		private Label lblTrademark;
		private Label lblTitle;
		private Label lblInformationalVersion;
		private Label lblConfiguration;
		private TableLayoutPanel tableLayoutPanel1;
	}
}
