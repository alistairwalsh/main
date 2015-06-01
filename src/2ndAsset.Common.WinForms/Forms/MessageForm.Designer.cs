/*
	Copyright ©2014-2015 2ndAsset.com (info@2ndasset.com) - D. P. Bullington
	CLOSED SOURCE, COMMERCIAL PRODUCT - THIS IS NOT OPEN SOURCE
*/

using System.Windows.Forms;

using _2ndAsset.Common.WinForms.Controls;

namespace _2ndAsset.Common.WinForms.Forms
{
	partial class MessageForm
	{
		//Form overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components;
		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageForm));
			this.lblMessage = new System.Windows.Forms.Label();
			this.ilMain = new System.Windows.Forms.ImageList(this.components);
			this.btnOkay = new System.Windows.Forms.Button();
			this.tvMessages = new System.Windows.Forms.TreeView();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point(12, 9);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(75, 13);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "%MESSAGE%";
			// 
			// ilMain
			// 
			this.ilMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMain.ImageStream")));
			this.ilMain.TransparentColor = System.Drawing.Color.Transparent;
			this.ilMain.Images.SetKeyName(0, "folder.ico");
			this.ilMain.Images.SetKeyName(1, "info.ico");
			this.ilMain.Images.SetKeyName(2, "warning.ico");
			this.ilMain.Images.SetKeyName(3, "error.ico");
			// 
			// btnOkay
			// 
			this.btnOkay.Location = new System.Drawing.Point(241, 223);
			this.btnOkay.Name = "btnOkay";
			this.btnOkay.Size = new System.Drawing.Size(75, 23);
			this.btnOkay.TabIndex = 3;
			this.btnOkay.Text = "OK";
			this.btnOkay.UseVisualStyleBackColor = true;
			this.btnOkay.Click += new System.EventHandler(this.btnOkay_Click);
			// 
			// tvMessages
			// 
			this.tvMessages.ImageIndex = 0;
			this.tvMessages.ImageList = this.ilMain;
			this.tvMessages.Indent = 19;
			this.tvMessages.Location = new System.Drawing.Point(13, 26);
			this.tvMessages.Name = "tvMessages";
			this.tvMessages.SelectedImageIndex = 0;
			this.tvMessages.Size = new System.Drawing.Size(384, 177);
			this.tvMessages.TabIndex = 1;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(322, 223);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// MessageForm
			// 
			this.AcceptButton = this.btnOkay;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(409, 258);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.tvMessages);
			this.Controls.Add(this.btnOkay);
			this.Controls.Add(this.lblMessage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MessageForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private Label lblMessage;
		private Button btnOkay;
		private ImageList ilMain;
		private TreeView tvMessages;
		private Button btnCancel;
	}
}