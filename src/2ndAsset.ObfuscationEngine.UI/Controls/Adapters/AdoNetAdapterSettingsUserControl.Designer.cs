namespace _2ndAsset.ObfuscationEngine.UI.Controls.Adapters
{
	partial class AdoNetAdapterSettingsUserControl
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
			this.lblConnectionString = new System.Windows.Forms.Label();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.txtBxConnectionString = new System.Windows.Forms.TextBox();
			this.lblConnectionType = new System.Windows.Forms.Label();
			this.ddlConnectionType = new System.Windows.Forms.ComboBox();
			this.tabCommands = new System.Windows.Forms.TabControl();
			this.tpPreProcess = new System.Windows.Forms.TabPage();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.ddlPreExecuteCommandType = new System.Windows.Forms.ComboBox();
			this.txtBxPreExecuteCommandText = new System.Windows.Forms.TextBox();
			this.tpProcess = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.ddlExecuteCommandType = new System.Windows.Forms.ComboBox();
			this.txtBxExecuteCommandText = new System.Windows.Forms.TextBox();
			this.tpPostProcess = new System.Windows.Forms.TabPage();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.ddlPostExecuteCommandType = new System.Windows.Forms.ComboBox();
			this.txtBxPostExecuteCommandText = new System.Windows.Forms.TextBox();
			this.tabCommands.SuspendLayout();
			this.tpPreProcess.SuspendLayout();
			this.tpProcess.SuspendLayout();
			this.tpPostProcess.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblConnectionString
			// 
			this.lblConnectionString.AutoSize = true;
			this.lblConnectionString.Location = new System.Drawing.Point(5, 7);
			this.lblConnectionString.Name = "lblConnectionString";
			this.lblConnectionString.Size = new System.Drawing.Size(94, 13);
			this.lblConnectionString.TabIndex = 0;
			this.lblConnectionString.Text = "Connection String:";
			// 
			// btnBrowse
			// 
			this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowse.Location = new System.Drawing.Point(390, 4);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(30, 23);
			this.btnBrowse.TabIndex = 2;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// txtBxConnectionString
			// 
			this.txtBxConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBxConnectionString.Location = new System.Drawing.Point(105, 4);
			this.txtBxConnectionString.Name = "txtBxConnectionString";
			this.txtBxConnectionString.Size = new System.Drawing.Size(283, 20);
			this.txtBxConnectionString.TabIndex = 1;
			// 
			// lblConnectionType
			// 
			this.lblConnectionType.AutoSize = true;
			this.lblConnectionType.Location = new System.Drawing.Point(5, 36);
			this.lblConnectionType.Name = "lblConnectionType";
			this.lblConnectionType.Size = new System.Drawing.Size(91, 13);
			this.lblConnectionType.TabIndex = 3;
			this.lblConnectionType.Text = "Connection Type:";
			// 
			// ddlConnectionType
			// 
			this.ddlConnectionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ddlConnectionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlConnectionType.FormattingEnabled = true;
			this.ddlConnectionType.Location = new System.Drawing.Point(105, 33);
			this.ddlConnectionType.Name = "ddlConnectionType";
			this.ddlConnectionType.Size = new System.Drawing.Size(315, 21);
			this.ddlConnectionType.TabIndex = 4;
			this.ddlConnectionType.SelectedIndexChanged += new System.EventHandler(this.ddlType_SelectedIndexChanged);
			// 
			// tabCommands
			// 
			this.tabCommands.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabCommands.Controls.Add(this.tpPreProcess);
			this.tabCommands.Controls.Add(this.tpProcess);
			this.tabCommands.Controls.Add(this.tpPostProcess);
			this.tabCommands.Location = new System.Drawing.Point(3, 60);
			this.tabCommands.Name = "tabCommands";
			this.tabCommands.SelectedIndex = 0;
			this.tabCommands.Size = new System.Drawing.Size(417, 224);
			this.tabCommands.TabIndex = 9;
			// 
			// tpPreProcess
			// 
			this.tpPreProcess.Controls.Add(this.label3);
			this.tpPreProcess.Controls.Add(this.label4);
			this.tpPreProcess.Controls.Add(this.ddlPreExecuteCommandType);
			this.tpPreProcess.Controls.Add(this.txtBxPreExecuteCommandText);
			this.tpPreProcess.Location = new System.Drawing.Point(4, 4);
			this.tpPreProcess.Name = "tpPreProcess";
			this.tpPreProcess.Padding = new System.Windows.Forms.Padding(3);
			this.tpPreProcess.Size = new System.Drawing.Size(409, 198);
			this.tpPreProcess.TabIndex = 0;
			this.tpPreProcess.Text = "Pre-Execute";
			this.tpPreProcess.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(1, 10);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(90, 13);
			this.label3.TabIndex = 13;
			this.label3.Text = "Command Text";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(122, 10);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 13);
			this.label4.TabIndex = 14;
			this.label4.Text = "Command Type:";
			// 
			// ddlPreExecuteCommandType
			// 
			this.ddlPreExecuteCommandType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ddlPreExecuteCommandType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlPreExecuteCommandType.FormattingEnabled = true;
			this.ddlPreExecuteCommandType.Location = new System.Drawing.Point(212, 5);
			this.ddlPreExecuteCommandType.Name = "ddlPreExecuteCommandType";
			this.ddlPreExecuteCommandType.Size = new System.Drawing.Size(195, 21);
			this.ddlPreExecuteCommandType.TabIndex = 15;
			this.ddlPreExecuteCommandType.SelectedIndexChanged += new System.EventHandler(this.ddlType_SelectedIndexChanged);
			// 
			// txtBxPreExecuteCommandText
			// 
			this.txtBxPreExecuteCommandText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBxPreExecuteCommandText.Location = new System.Drawing.Point(4, 31);
			this.txtBxPreExecuteCommandText.Multiline = true;
			this.txtBxPreExecuteCommandText.Name = "txtBxPreExecuteCommandText";
			this.txtBxPreExecuteCommandText.Size = new System.Drawing.Size(403, 163);
			this.txtBxPreExecuteCommandText.TabIndex = 16;
			// 
			// tpProcess
			// 
			this.tpProcess.Controls.Add(this.label1);
			this.tpProcess.Controls.Add(this.label2);
			this.tpProcess.Controls.Add(this.ddlExecuteCommandType);
			this.tpProcess.Controls.Add(this.txtBxExecuteCommandText);
			this.tpProcess.Location = new System.Drawing.Point(4, 4);
			this.tpProcess.Name = "tpProcess";
			this.tpProcess.Size = new System.Drawing.Size(409, 198);
			this.tpProcess.TabIndex = 2;
			this.tpProcess.Text = "Execute";
			this.tpProcess.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(1, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Command Text";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(122, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Command Type:";
			// 
			// ddlExecuteCommandType
			// 
			this.ddlExecuteCommandType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ddlExecuteCommandType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlExecuteCommandType.FormattingEnabled = true;
			this.ddlExecuteCommandType.Location = new System.Drawing.Point(212, 5);
			this.ddlExecuteCommandType.Name = "ddlExecuteCommandType";
			this.ddlExecuteCommandType.Size = new System.Drawing.Size(195, 21);
			this.ddlExecuteCommandType.TabIndex = 11;
			// 
			// txtBxExecuteCommandText
			// 
			this.txtBxExecuteCommandText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBxExecuteCommandText.Location = new System.Drawing.Point(4, 31);
			this.txtBxExecuteCommandText.Multiline = true;
			this.txtBxExecuteCommandText.Name = "txtBxExecuteCommandText";
			this.txtBxExecuteCommandText.Size = new System.Drawing.Size(403, 163);
			this.txtBxExecuteCommandText.TabIndex = 12;
			// 
			// tpPostProcess
			// 
			this.tpPostProcess.Controls.Add(this.label5);
			this.tpPostProcess.Controls.Add(this.label6);
			this.tpPostProcess.Controls.Add(this.ddlPostExecuteCommandType);
			this.tpPostProcess.Controls.Add(this.txtBxPostExecuteCommandText);
			this.tpPostProcess.Location = new System.Drawing.Point(4, 4);
			this.tpPostProcess.Name = "tpPostProcess";
			this.tpPostProcess.Padding = new System.Windows.Forms.Padding(3);
			this.tpPostProcess.Size = new System.Drawing.Size(409, 198);
			this.tpPostProcess.TabIndex = 1;
			this.tpPostProcess.Text = "Post-Execute";
			this.tpPostProcess.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(1, 10);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(90, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Command Text";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(122, 10);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(84, 13);
			this.label6.TabIndex = 14;
			this.label6.Text = "Command Type:";
			// 
			// ddlPostExecuteCommandType
			// 
			this.ddlPostExecuteCommandType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ddlPostExecuteCommandType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlPostExecuteCommandType.FormattingEnabled = true;
			this.ddlPostExecuteCommandType.Location = new System.Drawing.Point(212, 5);
			this.ddlPostExecuteCommandType.Name = "ddlPostExecuteCommandType";
			this.ddlPostExecuteCommandType.Size = new System.Drawing.Size(195, 21);
			this.ddlPostExecuteCommandType.TabIndex = 15;
			this.ddlPostExecuteCommandType.SelectedIndexChanged += new System.EventHandler(this.ddlType_SelectedIndexChanged);
			// 
			// txtBxPostExecuteCommandText
			// 
			this.txtBxPostExecuteCommandText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBxPostExecuteCommandText.Location = new System.Drawing.Point(4, 31);
			this.txtBxPostExecuteCommandText.Multiline = true;
			this.txtBxPostExecuteCommandText.Name = "txtBxPostExecuteCommandText";
			this.txtBxPostExecuteCommandText.Size = new System.Drawing.Size(403, 163);
			this.txtBxPostExecuteCommandText.TabIndex = 16;
			// 
			// AdoNetAdapterSettingsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabCommands);
			this.Controls.Add(this.lblConnectionType);
			this.Controls.Add(this.ddlConnectionType);
			this.Controls.Add(this.lblConnectionString);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtBxConnectionString);
			this.MinimumSize = new System.Drawing.Size(424, 287);
			this.Name = "AdoNetAdapterSettingsUserControl";
			this.Size = new System.Drawing.Size(424, 287);
			this.tabCommands.ResumeLayout(false);
			this.tpPreProcess.ResumeLayout(false);
			this.tpPreProcess.PerformLayout();
			this.tpProcess.ResumeLayout(false);
			this.tpProcess.PerformLayout();
			this.tpPostProcess.ResumeLayout(false);
			this.tpPostProcess.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblConnectionString;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TextBox txtBxConnectionString;
		private System.Windows.Forms.Label lblConnectionType;
		private System.Windows.Forms.ComboBox ddlConnectionType;
		private System.Windows.Forms.TabControl tabCommands;
		private System.Windows.Forms.TabPage tpPreProcess;
		private System.Windows.Forms.TabPage tpProcess;
		private System.Windows.Forms.TabPage tpPostProcess;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox ddlExecuteCommandType;
		private System.Windows.Forms.TextBox txtBxExecuteCommandText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox ddlPreExecuteCommandType;
		private System.Windows.Forms.TextBox txtBxPreExecuteCommandText;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox ddlPostExecuteCommandType;
		private System.Windows.Forms.TextBox txtBxPostExecuteCommandText;
	}
}
