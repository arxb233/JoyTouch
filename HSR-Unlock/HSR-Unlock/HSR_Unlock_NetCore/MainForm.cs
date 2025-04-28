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
	public partial class MainForm : Form
	{
		public TrackBar FPSSlider
		{
			get
			{
				return this.SliderFPS;
			}
		}
		public TrackBar FOVSlider
		{
			get
			{
				return this.SliderFOV;
			}
		}
		public NumericUpDown FPSInput
		{
			get
			{
				return this.InputFPS;
			}
		}

		public TextBox FOVInput
		{
			get
			{
				return this.InputFOV;
			}
		}

		public CheckBox AutoStart
		{
			get
			{
				return this.CBAutoStart;
			}
		}

		public CheckBox StartMinimized
		{
			get
			{
				return this.CBStartMinimized;
			}
		}

		public CheckBox UseMobileUI
		{
			get
			{
				return this.CBMobileUI;
			}
		}

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

		private void MainForm_FormClosing(object sender, EventArgs e)
		{
			this.notifyIcon.Visible = false;
			this._tokenManager.Cancel();
		}

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

		private void SliderFOV_Scroll(object sender, EventArgs e)
		{
			this._configLoader.UpdateFov((float)this.SliderFOV.Value, true);
			this.InputFOV.Text = this._config.FovDelta.ToString("F1");
		}

		private void BtnStartGame_Click(object sender, EventArgs e)
		{
			if (!this.CheckProcessPath())
			{
				return;
			}
			this._processService.Start();
		}

		private void BtnEditPath_Click(object sender, EventArgs e)
		{
			this.StartSetupWindow();
		}

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

		private void StartSetupWindow()
		{
			Program.ServiceProvider.GetRequiredService<SetupForm>().ShowDialog();
		}

		private void MenuExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			base.Show();
			base.Activate();
			base.WindowState = FormWindowState.Normal;
			base.ShowInTaskbar = true;
		}

		private void MainForm_SizeChanged(object sender, EventArgs e)
		{
			if (base.WindowState == FormWindowState.Minimized)
			{
				this.TryHideWindow();
			}
		}

		private void TryHideWindow()
		{
			base.WindowState = FormWindowState.Minimized;
			this.notifyIcon.Visible = true;
			base.ShowInTaskbar = false;
			this.notifyIcon.ShowBalloonTip(1000);
			base.Hide();
		}

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

        private ConfigLoader _configLoader;

		private TokenManager _tokenManager;

		private ProcessService _processService;

		private Config _config;
	}
}
