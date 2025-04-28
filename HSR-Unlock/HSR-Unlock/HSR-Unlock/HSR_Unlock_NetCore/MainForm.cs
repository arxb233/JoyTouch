using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using HSR_Unlock;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace HSR_Unlock_NetCore
{
	// Token: 0x02000009 RID: 9
	public partial class MainForm : Form
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002458 File Offset: 0x00000658
		public TrackBar FPSSlider
		{
			get
			{
				return this.SliderFPS;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002460 File Offset: 0x00000660
		public TrackBar FOVSlider
		{
			get
			{
				return this.SliderFOV;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002468 File Offset: 0x00000668
		public NumericUpDown FPSInput
		{
			get
			{
				return this.InputFPS;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002470 File Offset: 0x00000670
		public TextBox FOVInput
		{
			get
			{
				return this.InputFOV;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002478 File Offset: 0x00000678
		public CheckBox AutoStart
		{
			get
			{
				return this.CBAutoStart;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002480 File Offset: 0x00000680
		public CheckBox StartMinimized
		{
			get
			{
				return this.CBStartMinimized;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002488 File Offset: 0x00000688
		public CheckBox UseMobileUI
		{
			get
			{
				return this.CBMobileUI;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002490 File Offset: 0x00000690
		public MainForm(ConfigLoader configLoader, TokenManager tokenManager, ProcessService processService)
		{
			this.InitializeComponent();
			this._processService = processService;
			this._tokenManager = tokenManager;
			this._configLoader = configLoader;
			this._config = configLoader.config;
			this._configLoader.SetupBindings(this);
			this.TTAutoStart.SetToolTip(this.CBAutoStart, "自动启动游戏（下次开启解锁器时生效）");
			this.TTStartMinimized.SetToolTip(this.CBStartMinimized, "启动解锁器时自动最小化到托盘");
			base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002514 File Offset: 0x00000714
		private void MainForm_FormClosing(object sender, EventArgs e)
		{
			this.notifyIcon.Visible = false;
			this._tokenManager.Cancel();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002530 File Offset: 0x00000730
		private void InputFOV_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = (TextBox)sender;
			try
			{
				float num = float.Parse(textBox.Text);
				num = Math.Clamp(num, 0f, 50f);
				this._configLoader.UpdateFov(num, false);
				this.SliderFOV.Scroll -= this.SliderFOV_Scroll;
				this.SliderFOV.Value = this._configLoader.ScaledFov;
				this.SliderFOV.Scroll += this.SliderFOV_Scroll;
				textBox.ForeColor = Color.Black;
			}
			catch (Exception)
			{
				textBox.ForeColor = Color.Red;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025E0 File Offset: 0x000007E0
		private void SliderFOV_Scroll(object sender, EventArgs e)
		{
			this._configLoader.UpdateFov((float)this.SliderFOV.Value, true);
			this.InputFOV.Text = this._config.FovDelta.ToString("F1");
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002628 File Offset: 0x00000828
		private void BtnStartGame_Click(object sender, EventArgs e)
		{
			if (!this.CheckProcessPath())
			{
				return;
			}
			this._processService.Start();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000263E File Offset: 0x0000083E
		private void BtnEditPath_Click(object sender, EventArgs e)
		{
			this.StartSetupWindow();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002646 File Offset: 0x00000846
		private bool CheckProcessPath()
		{
			if (File.Exists(this._config.ProcessPath))
			{
				return true;
			}
			MessageBox.Show("当前游戏路径无效，请重新设置", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			this.StartSetupWindow();
			return false;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002676 File Offset: 0x00000876
		private void StartSetupWindow()
		{
			Program.ServiceProvider.GetRequiredService<SetupForm>().ShowDialog();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002688 File Offset: 0x00000888
		private void MenuExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000268F File Offset: 0x0000088F
		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			base.Show();
			base.Activate();
			base.WindowState = FormWindowState.Normal;
			base.ShowInTaskbar = true;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000026AB File Offset: 0x000008AB
		private void MainForm_SizeChanged(object sender, EventArgs e)
		{
			if (base.WindowState == FormWindowState.Minimized)
			{
				this.TryHideWindow();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000026BC File Offset: 0x000008BC
		private void TryHideWindow()
		{
			base.WindowState = FormWindowState.Minimized;
			this.notifyIcon.Visible = true;
			base.ShowInTaskbar = false;
			this.notifyIcon.ShowBalloonTip(1000);
			base.Hide();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000026F0 File Offset: 0x000008F0
		private void MainForm_Load(object sender, EventArgs e)
		{
			//Task.Run(new Func<Task>(this.CheckVersion));
			if (string.IsNullOrEmpty(this._config.ProcessPath))
			{
				this.StartSetupWindow();
			}
			if (this._config.AutoStart)
			{
				this._processService.Start();
			}
			if (this._config.StartMinimized)
			{
				this.TryHideWindow();
			}
		}

        // Token: 0x04000011 RID: 17
        private ConfigLoader _configLoader;

		// Token: 0x04000012 RID: 18
		private TokenManager _tokenManager;

		// Token: 0x04000013 RID: 19
		private ProcessService _processService;

		// Token: 0x04000014 RID: 20
		private Config _config;
	}
}
