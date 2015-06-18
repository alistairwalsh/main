using System.Windows.Forms;

namespace _2ndAsset.Utilities.DataObfu.WindowsTool.Controls
{
	partial class AvalancheSettingsUserControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AvalancheSettingsUserControl));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnRegenerateHashValues = new System.Windows.Forms.Button();
			this.tmTableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tmLabel5 = new System.Windows.Forms.Label();
			this.tmLabel6 = new System.Windows.Forms.Label();
			this.txtBxHashSeed = new System.Windows.Forms.TextBox();
			this.txtBxHashMultiplier = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.tmTableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.btnRegenerateHashValues);
			this.groupBox1.Controls.Add(this.tmTableLayoutPanel1);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(451, 108);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Hash Settings";
			// 
			// btnRegenerateHashValues
			// 
			this.btnRegenerateHashValues.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRegenerateHashValues.ForeColor = System.Drawing.Color.Red;
			this.btnRegenerateHashValues.Location = new System.Drawing.Point(6, 74);
			this.btnRegenerateHashValues.Name = "btnRegenerateHashValues";
			this.btnRegenerateHashValues.Size = new System.Drawing.Size(200, 23);
			this.btnRegenerateHashValues.TabIndex = 3;
			this.btnRegenerateHashValues.Text = "Regenerate Hash Values";
			this.btnRegenerateHashValues.UseVisualStyleBackColor = true;
			this.btnRegenerateHashValues.Click += new System.EventHandler(this.btnRegenerateHashValues_Click);
			// 
			// tmTableLayoutPanel1
			// 
			this.tmTableLayoutPanel1.ColumnCount = 2;
			this.tmTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.tmTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tmTableLayoutPanel1.Controls.Add(this.tmLabel5, 0, 0);
			this.tmTableLayoutPanel1.Controls.Add(this.tmLabel6, 0, 1);
			this.tmTableLayoutPanel1.Controls.Add(this.txtBxHashSeed, 1, 1);
			this.tmTableLayoutPanel1.Controls.Add(this.txtBxHashMultiplier, 1, 0);
			this.tmTableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
			this.tmTableLayoutPanel1.Name = "tmTableLayoutPanel1";
			this.tmTableLayoutPanel1.RowCount = 2;
			this.tmTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tmTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tmTableLayoutPanel1.Size = new System.Drawing.Size(200, 53);
			this.tmTableLayoutPanel1.TabIndex = 0;
			// 
			// tmLabel5
			// 
			this.tmLabel5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tmLabel5.AutoSize = true;
			this.tmLabel5.Location = new System.Drawing.Point(3, 6);
			this.tmLabel5.Name = "tmLabel5";
			this.tmLabel5.Size = new System.Drawing.Size(79, 13);
			this.tmLabel5.TabIndex = 0;
			this.tmLabel5.Text = "Hash Multiplier:";
			// 
			// tmLabel6
			// 
			this.tmLabel6.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.tmLabel6.AutoSize = true;
			this.tmLabel6.Location = new System.Drawing.Point(3, 33);
			this.tmLabel6.Name = "tmLabel6";
			this.tmLabel6.Size = new System.Drawing.Size(63, 13);
			this.tmLabel6.TabIndex = 2;
			this.tmLabel6.Text = "Hash Seed:";
			// 
			// txtBxHashSeed
			// 
			this.txtBxHashSeed.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtBxHashSeed.Location = new System.Drawing.Point(123, 29);
			this.txtBxHashSeed.Name = "txtBxHashSeed";
			this.txtBxHashSeed.Size = new System.Drawing.Size(73, 20);
			this.txtBxHashSeed.TabIndex = 3;
			this.txtBxHashSeed.Validating += new System.ComponentModel.CancelEventHandler(this._all_txtBx_Validating);
			// 
			// txtBxHashMultiplier
			// 
			this.txtBxHashMultiplier.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.txtBxHashMultiplier.Location = new System.Drawing.Point(123, 3);
			this.txtBxHashMultiplier.Name = "txtBxHashMultiplier";
			this.txtBxHashMultiplier.Size = new System.Drawing.Size(73, 20);
			this.txtBxHashMultiplier.TabIndex = 1;
			this.txtBxHashMultiplier.Validating += new System.ComponentModel.CancelEventHandler(this._all_txtBx_Validating);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(212, 19);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(233, 78);
			this.label5.TabIndex = 2;
			this.label5.Text = resources.GetString("label5.Text");
			// 
			// AvalancheSettingsUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.MinimumSize = new System.Drawing.Size(460, 313);
			this.Name = "AvalancheSettingsUserControl";
			this.Size = new System.Drawing.Size(460, 313);
			this.groupBox1.ResumeLayout(false);
			this.tmTableLayoutPanel1.ResumeLayout(false);
			this.tmTableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private GroupBox groupBox1;
		private Button btnRegenerateHashValues;
		private TableLayoutPanel tmTableLayoutPanel1;
		private Label tmLabel5;
		private Label tmLabel6;
		private TextBox txtBxHashSeed;
		private TextBox txtBxHashMultiplier;
		private Label label5;







	}
}
