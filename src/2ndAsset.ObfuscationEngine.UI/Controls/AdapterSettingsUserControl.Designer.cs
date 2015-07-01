using System.Windows.Forms;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	partial class AdapterSettingsUserControl
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
			this.lblAdapterType = new System.Windows.Forms.Label();
			this.ddlType = new System.Windows.Forms.ComboBox();
			this.pnlAdapterConfigHost = new System.Windows.Forms.Panel();
			this.adoNetAdapterSettingsUc = new AdoNetAdapterSettingsUserControl();
			this.delTxtAdapterSettingsUc = new DelTxtAdapterSettingsUserControl();
			this.pnlAdapterConfigHost.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblAdapterType
			// 
			this.lblAdapterType.AutoSize = true;
			this.lblAdapterType.Location = new System.Drawing.Point(3, 6);
			this.lblAdapterType.Name = "lblAdapterType";
			this.lblAdapterType.Size = new System.Drawing.Size(74, 13);
			this.lblAdapterType.TabIndex = 0;
			this.lblAdapterType.Text = "Adapter Type:";
			// 
			// ddlType
			// 
			this.ddlType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ddlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlType.FormattingEnabled = true;
			this.ddlType.Location = new System.Drawing.Point(84, 3);
			this.ddlType.Name = "ddlType";
			this.ddlType.Size = new System.Drawing.Size(370, 21);
			this.ddlType.TabIndex = 1;
			this.ddlType.SelectedIndexChanged += new System.EventHandler(this.ddlType_SelectedIndexChanged);
			// 
			// pnlAdapterConfigHost
			// 
			this.pnlAdapterConfigHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlAdapterConfigHost.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlAdapterConfigHost.Controls.Add(this.adoNetAdapterSettingsUc);
			this.pnlAdapterConfigHost.Controls.Add(this.delTxtAdapterSettingsUc);
			this.pnlAdapterConfigHost.Location = new System.Drawing.Point(6, 30);
			this.pnlAdapterConfigHost.Name = "pnlAdapterConfigHost";
			this.pnlAdapterConfigHost.Size = new System.Drawing.Size(448, 280);
			this.pnlAdapterConfigHost.TabIndex = 2;
			this.pnlAdapterConfigHost.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlAdapterConfigHost_Paint);
			// 
			// adoNetAdapterSettingsUc
			// 
			this.adoNetAdapterSettingsUc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.adoNetAdapterSettingsUc.Location = new System.Drawing.Point(0, 0);
			this.adoNetAdapterSettingsUc.MinimumSize = new System.Drawing.Size(424, 287);
			this.adoNetAdapterSettingsUc.Name = "adoNetAdapterSettingsUc";
			this.adoNetAdapterSettingsUc.Size = new System.Drawing.Size(444, 287);
			this.adoNetAdapterSettingsUc.TabIndex = 3;
			// 
			// delTxtAdapterSettingsUc
			// 
			this.delTxtAdapterSettingsUc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.delTxtAdapterSettingsUc.Location = new System.Drawing.Point(0, 0);
			this.delTxtAdapterSettingsUc.MinimumSize = new System.Drawing.Size(424, 287);
			this.delTxtAdapterSettingsUc.Name = "delTxtAdapterSettingsUc";
			this.delTxtAdapterSettingsUc.Size = new System.Drawing.Size(444, 287);
			this.delTxtAdapterSettingsUc.TabIndex = 0;
			// 
			// AdapterSettingsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pnlAdapterConfigHost);
			this.Controls.Add(this.lblAdapterType);
			this.Controls.Add(this.ddlType);
			this.MinimumSize = new System.Drawing.Size(460, 313);
			this.Name = "AdapterSettingsUserControl";
			this.Size = new System.Drawing.Size(460, 313);
			this.pnlAdapterConfigHost.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label lblAdapterType;
		private ComboBox ddlType;
		private Panel pnlAdapterConfigHost;
		private DelTxtAdapterSettingsUserControl delTxtAdapterSettingsUc;
		private AdoNetAdapterSettingsUserControl adoNetAdapterSettingsUc;




	}
}
