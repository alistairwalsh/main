using System.Windows.Forms;

namespace _2ndAsset.ObfuscationEngine.UI.Controls
{
	partial class ExecutionUserControl
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
			this.btnExecute = new System.Windows.Forms.Button();
			this.grpBxExecutionProgress = new System.Windows.Forms.GroupBox();
			this.tmTableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tmLabel5 = new System.Windows.Forms.Label();
			this.tmLabel6 = new System.Windows.Forms.Label();
			this.txtBxIsComplete = new System.Windows.Forms.TextBox();
			this.txtBxTotalRecords = new System.Windows.Forms.TextBox();
			this.txtBxDurationSeconds = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.grpBxExecutionProgress.SuspendLayout();
			this.tmTableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnExecute
			// 
			this.btnExecute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnExecute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnExecute.Location = new System.Drawing.Point(3, 117);
			this.btnExecute.Name = "btnExecute";
			this.btnExecute.Size = new System.Drawing.Size(451, 23);
			this.btnExecute.TabIndex = 1;
			this.btnExecute.Text = "Execute >>";
			this.btnExecute.UseVisualStyleBackColor = true;
			this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
			// 
			// grpBxExecutionProgress
			// 
			this.grpBxExecutionProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpBxExecutionProgress.Controls.Add(this.tmTableLayoutPanel1);
			this.grpBxExecutionProgress.Location = new System.Drawing.Point(3, 3);
			this.grpBxExecutionProgress.Name = "grpBxExecutionProgress";
			this.grpBxExecutionProgress.Size = new System.Drawing.Size(451, 108);
			this.grpBxExecutionProgress.TabIndex = 0;
			this.grpBxExecutionProgress.TabStop = false;
			this.grpBxExecutionProgress.Text = "Execution Progress";
			// 
			// tmTableLayoutPanel1
			// 
			this.tmTableLayoutPanel1.ColumnCount = 2;
			this.tmTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tmTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tmTableLayoutPanel1.Controls.Add(this.tmLabel5, 0, 0);
			this.tmTableLayoutPanel1.Controls.Add(this.tmLabel6, 0, 1);
			this.tmTableLayoutPanel1.Controls.Add(this.txtBxIsComplete, 1, 1);
			this.tmTableLayoutPanel1.Controls.Add(this.txtBxTotalRecords, 1, 0);
			this.tmTableLayoutPanel1.Controls.Add(this.txtBxDurationSeconds, 1, 2);
			this.tmTableLayoutPanel1.Controls.Add(this.label1, 0, 2);
			this.tmTableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
			this.tmTableLayoutPanel1.Name = "tmTableLayoutPanel1";
			this.tmTableLayoutPanel1.RowCount = 3;
			this.tmTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tmTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tmTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tmTableLayoutPanel1.Size = new System.Drawing.Size(200, 77);
			this.tmTableLayoutPanel1.TabIndex = 0;
			// 
			// tmLabel5
			// 
			this.tmLabel5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tmLabel5.AutoSize = true;
			this.tmLabel5.Location = new System.Drawing.Point(3, 6);
			this.tmLabel5.Name = "tmLabel5";
			this.tmLabel5.Size = new System.Drawing.Size(72, 13);
			this.tmLabel5.TabIndex = 0;
			this.tmLabel5.Text = "Total records:";
			// 
			// tmLabel6
			// 
			this.tmLabel6.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tmLabel6.AutoSize = true;
			this.tmLabel6.Location = new System.Drawing.Point(3, 32);
			this.tmLabel6.Name = "tmLabel6";
			this.tmLabel6.Size = new System.Drawing.Size(64, 13);
			this.tmLabel6.TabIndex = 2;
			this.tmLabel6.Text = "Is complete:";
			// 
			// txtBxIsComplete
			// 
			this.txtBxIsComplete.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtBxIsComplete.Location = new System.Drawing.Point(123, 29);
			this.txtBxIsComplete.Name = "txtBxIsComplete";
			this.txtBxIsComplete.ReadOnly = true;
			this.txtBxIsComplete.Size = new System.Drawing.Size(73, 20);
			this.txtBxIsComplete.TabIndex = 3;
			// 
			// txtBxTotalRecords
			// 
			this.txtBxTotalRecords.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtBxTotalRecords.Location = new System.Drawing.Point(123, 3);
			this.txtBxTotalRecords.Name = "txtBxTotalRecords";
			this.txtBxTotalRecords.ReadOnly = true;
			this.txtBxTotalRecords.Size = new System.Drawing.Size(73, 20);
			this.txtBxTotalRecords.TabIndex = 1;
			// 
			// txtBxDurationSeconds
			// 
			this.txtBxDurationSeconds.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtBxDurationSeconds.Location = new System.Drawing.Point(123, 55);
			this.txtBxDurationSeconds.Name = "txtBxDurationSeconds";
			this.txtBxDurationSeconds.ReadOnly = true;
			this.txtBxDurationSeconds.Size = new System.Drawing.Size(73, 20);
			this.txtBxDurationSeconds.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Duration (seconds):";
			// 
			// ExecutionUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpBxExecutionProgress);
			this.Controls.Add(this.btnExecute);
			this.MinimumSize = new System.Drawing.Size(460, 313);
			this.Name = "ExecutionUserControl";
			this.Size = new System.Drawing.Size(460, 313);
			this.grpBxExecutionProgress.ResumeLayout(false);
			this.tmTableLayoutPanel1.ResumeLayout(false);
			this.tmTableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private Button btnExecute;
		private GroupBox grpBxExecutionProgress;
		private TableLayoutPanel tmTableLayoutPanel1;
		private Label tmLabel5;
		private Label tmLabel6;
		private TextBox txtBxIsComplete;
		private TextBox txtBxTotalRecords;
		private TextBox txtBxDurationSeconds;
		private Label label1;






	}
}
