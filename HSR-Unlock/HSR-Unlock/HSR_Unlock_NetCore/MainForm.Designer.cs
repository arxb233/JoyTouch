namespace HSR_Unlock_NetCore
{
	public partial class MainForm : global::System.Windows.Forms.Form
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
            components = new System.ComponentModel.Container();
            LabelFPS = new System.Windows.Forms.Label();
            InputFPS = new System.Windows.Forms.NumericUpDown();
            SliderFPS = new System.Windows.Forms.TrackBar();
            LabelFOV = new System.Windows.Forms.Label();
            InputFOV = new System.Windows.Forms.TextBox();
            SliderFOV = new System.Windows.Forms.TrackBar();
            CBStartMinimized = new System.Windows.Forms.CheckBox();
            CBAutoStart = new System.Windows.Forms.CheckBox();
            BtnStartGame = new System.Windows.Forms.Button();
            BtnEditPath = new System.Windows.Forms.Button();
            TTAutoStart = new System.Windows.Forms.ToolTip(components);
            TTStartMinimized = new System.Windows.Forms.ToolTip(components);
            notifyIcon = new System.Windows.Forms.NotifyIcon(components);
            NotifyMenu = new System.Windows.Forms.ContextMenuStrip(components);
            ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            CBMobileUI = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)InputFPS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SliderFPS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SliderFOV).BeginInit();
            NotifyMenu.SuspendLayout();
            SuspendLayout();
            // 
            // LabelFPS
            // 
            LabelFPS.AutoSize = true;
            LabelFPS.Location = new System.Drawing.Point(12, 10);
            LabelFPS.Name = "LabelFPS";
            LabelFPS.Size = new System.Drawing.Size(44, 17);
            LabelFPS.TabIndex = 0;
            LabelFPS.Text = "帧数：";
            // 
            // InputFPS
            // 
            InputFPS.Location = new System.Drawing.Point(51, 8);
            InputFPS.Maximum = new decimal(new int[] { 420, 0, 0, 0 });
            InputFPS.Name = "InputFPS";
            InputFPS.Size = new System.Drawing.Size(231, 23);
            InputFPS.TabIndex = 1;
            // 
            // SliderFPS
            // 
            SliderFPS.Location = new System.Drawing.Point(12, 41);
            SliderFPS.Maximum = 420;
            SliderFPS.Minimum = 10;
            SliderFPS.Name = "SliderFPS";
            SliderFPS.Size = new System.Drawing.Size(270, 45);
            SliderFPS.TabIndex = 2;
            SliderFPS.TickStyle = System.Windows.Forms.TickStyle.None;
            SliderFPS.Value = 60;
            // 
            // LabelFOV
            // 
            LabelFOV.AutoSize = true;
            LabelFOV.Location = new System.Drawing.Point(12, 75);
            LabelFOV.Name = "LabelFOV";
            LabelFOV.Size = new System.Drawing.Size(44, 17);
            LabelFOV.TabIndex = 3;
            LabelFOV.Text = "视野：";
            // 
            // InputFOV
            // 
            InputFOV.Location = new System.Drawing.Point(51, 71);
            InputFOV.Name = "InputFOV";
            InputFOV.Size = new System.Drawing.Size(231, 23);
            InputFOV.TabIndex = 4;
            InputFOV.TextChanged += InputFOV_TextChanged;
            // 
            // SliderFOV
            // 
            SliderFOV.LargeChange = 10;
            SliderFOV.Location = new System.Drawing.Point(12, 104);
            SliderFOV.Maximum = 5000;
            SliderFOV.Name = "SliderFOV";
            SliderFOV.Size = new System.Drawing.Size(270, 45);
            SliderFOV.TabIndex = 5;
            SliderFOV.TickStyle = System.Windows.Forms.TickStyle.None;
            SliderFOV.Scroll += SliderFOV_Scroll;
            // 
            // CBStartMinimized
            // 
            CBStartMinimized.AutoSize = true;
            CBStartMinimized.Location = new System.Drawing.Point(12, 177);
            CBStartMinimized.Name = "CBStartMinimized";
            CBStartMinimized.Size = new System.Drawing.Size(87, 21);
            CBStartMinimized.TabIndex = 6;
            CBStartMinimized.Text = "启动至托盘";
            CBStartMinimized.UseVisualStyleBackColor = true;
            // 
            // CBAutoStart
            // 
            CBAutoStart.AutoSize = true;
            CBAutoStart.Location = new System.Drawing.Point(12, 148);
            CBAutoStart.Name = "CBAutoStart";
            CBAutoStart.Size = new System.Drawing.Size(75, 21);
            CBAutoStart.TabIndex = 7;
            CBAutoStart.Text = "游戏自启";
            CBAutoStart.UseVisualStyleBackColor = true;
            // 
            // BtnStartGame
            // 
            BtnStartGame.Location = new System.Drawing.Point(207, 173);
            BtnStartGame.Name = "BtnStartGame";
            BtnStartGame.Size = new System.Drawing.Size(75, 26);
            BtnStartGame.TabIndex = 8;
            BtnStartGame.Text = "启动游戏";
            BtnStartGame.UseVisualStyleBackColor = true;
            BtnStartGame.Click += BtnStartGame_Click;
            // 
            // BtnEditPath
            // 
            BtnEditPath.Location = new System.Drawing.Point(126, 173);
            BtnEditPath.Name = "BtnEditPath";
            BtnEditPath.Size = new System.Drawing.Size(75, 26);
            BtnEditPath.TabIndex = 9;
            BtnEditPath.Text = "修改路径";
            BtnEditPath.UseVisualStyleBackColor = true;
            BtnEditPath.Click += BtnEditPath_Click;
            // 
            // TTAutoStart
            // 
            TTAutoStart.AutoPopDelay = 5000;
            TTAutoStart.InitialDelay = 100;
            TTAutoStart.ReshowDelay = 100;
            // 
            // TTStartMinimized
            // 
            TTStartMinimized.AutoPopDelay = 5000;
            TTStartMinimized.InitialDelay = 100;
            TTStartMinimized.ReshowDelay = 100;
            // 
            // notifyIcon
            // 
            notifyIcon.BalloonTipText = "已最小化至托盘";
            notifyIcon.BalloonTipTitle = "星铁FPS解锁";
            notifyIcon.ContextMenuStrip = NotifyMenu;
            notifyIcon.Text = "星铁FPS解锁";
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            // 
            // NotifyMenu
            // 
            NotifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { ExitMenuItem });
            NotifyMenu.Name = "NotifyMenu";
            NotifyMenu.Size = new System.Drawing.Size(101, 26);
            // 
            // ExitMenuItem
            // 
            ExitMenuItem.Name = "ExitMenuItem";
            ExitMenuItem.Size = new System.Drawing.Size(100, 22);
            ExitMenuItem.Text = "退出";
            ExitMenuItem.Click += MenuExit_Click;
            // 
            // CBMobileUI
            // 
            CBMobileUI.AutoSize = true;
            CBMobileUI.Location = new System.Drawing.Point(126, 148);
            CBMobileUI.Name = "CBMobileUI";
            CBMobileUI.Size = new System.Drawing.Size(76, 21);
            CBMobileUI.TabIndex = 10;
            CBMobileUI.Text = "移动端UI";
            CBMobileUI.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(294, 212);
            Controls.Add(CBMobileUI);
            Controls.Add(BtnEditPath);
            Controls.Add(BtnStartGame);
            Controls.Add(CBAutoStart);
            Controls.Add(CBStartMinimized);
            Controls.Add(SliderFOV);
            Controls.Add(InputFOV);
            Controls.Add(LabelFOV);
            Controls.Add(SliderFPS);
            Controls.Add(InputFPS);
            Controls.Add(LabelFPS);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "星铁FPS解锁 by: Ex_M";
            Load += MainForm_Load;
            SizeChanged += MainForm_SizeChanged;
            ((System.ComponentModel.ISupportInitialize)InputFPS).EndInit();
            ((System.ComponentModel.ISupportInitialize)SliderFPS).EndInit();
            ((System.ComponentModel.ISupportInitialize)SliderFOV).EndInit();
            NotifyMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }


        private global::System.ComponentModel.IContainer components;

		
		private global::System.Windows.Forms.Label LabelFPS;

		
		private global::System.Windows.Forms.NumericUpDown InputFPS;

		
		private global::System.Windows.Forms.TrackBar SliderFPS;

		
		private global::System.Windows.Forms.Label LabelFOV;

		
		private global::System.Windows.Forms.TextBox InputFOV;

		
		private global::System.Windows.Forms.TrackBar SliderFOV;

		
		private global::System.Windows.Forms.CheckBox CBStartMinimized;

		
		private global::System.Windows.Forms.CheckBox CBAutoStart;

		
		private global::System.Windows.Forms.Button BtnStartGame;

		
		private global::System.Windows.Forms.Button BtnEditPath;

		
		private global::System.Windows.Forms.ToolTip TTAutoStart;

		
		private global::System.Windows.Forms.ToolTip TTStartMinimized;

		
		private global::System.Windows.Forms.NotifyIcon notifyIcon;

		
		private global::System.Windows.Forms.ContextMenuStrip NotifyMenu;

		
		private global::System.Windows.Forms.ToolStripMenuItem ExitMenuItem;

		
		private global::System.Windows.Forms.CheckBox CBMobileUI;
	}
}
