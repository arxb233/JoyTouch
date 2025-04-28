using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace HSR_Unlock_NetCore
{
	// Token: 0x02000008 RID: 8
	public class ConfigLoader
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002148 File Offset: 0x00000348
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002150 File Offset: 0x00000350
		public Config config { get; set; } = new Config();

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002159 File Offset: 0x00000359
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002161 File Offset: 0x00000361
		public int ScaledFov { get; set; }

		// Token: 0x0600001C RID: 28 RVA: 0x0000216C File Offset: 0x0000036C
		public ConfigLoader(TokenManager tokenManager)
		{
			this._tokenManager = tokenManager;
			this.LoadSettings();
			this.config.FovDelta = Math.Clamp(this.config.FovDelta, 0f, 50f);
			this.ScaledFov = Math.Clamp(this.ScaledFov, 0, 5000);
			this._tokenManager.AddTask(Task.Run(new Func<Task>(this.Worker)));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000021F0 File Offset: 0x000003F0
		private void LoadSettings()
		{
			if (!File.Exists(ConfigLoader.ConfigPath))
			{
				return;
			}
			try
			{
				string text = File.ReadAllText(ConfigLoader.ConfigPath);
				this.config = JsonConvert.DeserializeObject<Config>(text) ?? new Config();
				this.ScaledFov = (int)(this.config.FovDelta * 100f);
			}
			catch (Exception ex)
			{
				MessageBox.Show("配置加载失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002278 File Offset: 0x00000478
		public void SaveSettings()
		{
			string text = JsonConvert.SerializeObject(this.config, Formatting.Indented);
			File.WriteAllText(ConfigLoader.ConfigPath, text);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022A0 File Offset: 0x000004A0
		private async Task Worker()
		{
			try
			{
				while (!this._tokenManager.Token.IsCancellationRequested)
				{
					await Task.Delay(5000, this._tokenManager.Token);
					this.SaveSettings();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022E4 File Offset: 0x000004E4
		public void SetupBindings(MainForm form)
		{
			this._mainForm = form;
			Binding binding = new Binding("Value", this.config, "TargetFramerate");
			binding.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
			form.FPSSlider.DataBindings.Add(binding);
			binding = new Binding("Value", this.config, "TargetFramerate");
			binding.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
			form.FPSInput.DataBindings.Add(binding);
			form.FOVInput.Text = this.config.FovDelta.ToString("F1");
			form.FOVSlider.Value = this.ScaledFov;
			Binding binding2 = new Binding("Checked", this.config, "AutoStart");
			binding2.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
			form.AutoStart.DataBindings.Add(binding2);
			binding2 = new Binding("Checked", this.config, "StartMinimized");
			binding2.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
			form.StartMinimized.DataBindings.Add(binding2);
			binding2 = new Binding("Checked", this.config, "UseMobileUI");
			binding2.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
			form.UseMobileUI.DataBindings.Add(binding2);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002412 File Offset: 0x00000612
		public void UpdateFov(float value, bool scaled)
		{
			if (scaled)
			{
				this.ScaledFov = (int)value;
				this.config.FovDelta = value / 100f;
				return;
			}
			this.ScaledFov = (int)(value * 100f);
			this.config.FovDelta = value;
		}

		// Token: 0x0400000C RID: 12
		private static readonly string ConfigPath = "hsr_config.json";

		// Token: 0x0400000D RID: 13
		private MainForm _mainForm;

		// Token: 0x0400000E RID: 14
		private TokenManager _tokenManager;
	}
}
