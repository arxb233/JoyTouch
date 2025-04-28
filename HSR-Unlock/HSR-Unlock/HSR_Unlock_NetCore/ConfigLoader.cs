using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace HSR_Unlock_NetCore
{
	public class ConfigLoader
	{
		public Config config { get; set; } = new Config();

		public int ScaledFov { get; set; }

		public ConfigLoader(TokenManager tokenManager)
		{
			this._tokenManager = tokenManager;
			this.LoadSettings();
			this.config.FovDelta = Math.Clamp(this.config.FovDelta, 0f, 50f);
			this.ScaledFov = Math.Clamp(this.ScaledFov, 0, 5000);
			this._tokenManager.AddTask(Task.Run(new Func<Task>(this.Worker)));
		}

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

		public void SaveSettings()
		{
			string text = JsonConvert.SerializeObject(this.config, Formatting.Indented);
			File.WriteAllText(ConfigLoader.ConfigPath, text);
		}

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

		private static readonly string ConfigPath = "hsr_config.json";

		private MainForm _mainForm;

		private TokenManager _tokenManager;
	}
}
