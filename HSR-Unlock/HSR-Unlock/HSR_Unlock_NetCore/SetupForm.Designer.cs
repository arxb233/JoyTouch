namespace HSR_Unlock_NetCore
{
	public partial class SetupForm : global::System.Windows.Forms.Form
	{
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::HSR_Unlock_NetCore.SetupForm));
			this.LabelGamePath = new global::System.Windows.Forms.Label();
			this.BtnBrowse = new global::System.Windows.Forms.Button();
			this.TTGamePath = new global::System.Windows.Forms.ToolTip(this.components);
			base.SuspendLayout();
			this.LabelGamePath.AutoSize = true;
			this.LabelGamePath.Location = new global::System.Drawing.Point(12, 9);
			this.LabelGamePath.Name = "LabelGamePath";
			this.LabelGamePath.Size = new global::System.Drawing.Size(72, 15);
			this.LabelGamePath.TabIndex = 0;
			this.LabelGamePath.Text = "游戏路径：";
			this.BtnBrowse.Location = new global::System.Drawing.Point(12, 46);
			this.BtnBrowse.Name = "BtnBrowse";
			this.BtnBrowse.Size = new global::System.Drawing.Size(75, 23);
			this.BtnBrowse.TabIndex = 1;
			this.BtnBrowse.Text = "浏览";
			this.BtnBrowse.UseVisualStyleBackColor = true;
			this.BtnBrowse.Click += new global::System.EventHandler(this.BtnBrowse_Click);
			this.TTGamePath.AutoPopDelay = 5000;
			this.TTGamePath.InitialDelay = 100;
			this.TTGamePath.ReshowDelay = 100;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 15f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(334, 81);
			base.Controls.Add(this.BtnBrowse);
			base.Controls.Add(this.LabelGamePath);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SetupForm";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "路径设置";
			base.Load += new global::System.EventHandler(this.SetupForm_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private global::System.ComponentModel.IContainer components;

		private global::System.Windows.Forms.Label LabelGamePath;

		private global::System.Windows.Forms.Button BtnBrowse;

		private global::System.Windows.Forms.ToolTip TTGamePath;
	}
}
