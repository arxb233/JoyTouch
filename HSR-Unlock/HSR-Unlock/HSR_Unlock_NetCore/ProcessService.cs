using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace HSR_Unlock_NetCore
{
	public class ProcessService
	{
		public ProcessService(TokenManager tokenManager, ConfigLoader configLoader)
		{
			this._tokenManager = tokenManager;
			this._configLoader = configLoader;
		}

		public void Start()
		{
			this._tokenManager.AddTask(Task.Run(new Func<Task>(this.Worker)));
		}

		private async Task Worker()
		{
			MemoryMappedFile memoryMappedFile = this.OpenMapping();
			if (memoryMappedFile == null)
			{
				MessageBox.Show("操作失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				MemoryMappedViewAccessor accessor = memoryMappedFile.CreateViewAccessor();
				GameSettings gameSettings = default(GameSettings);
				if (this.StartGameAndInject())
				{
					await Task.Delay(1000);
					Process[] processes = Process.GetProcessesByName("StarRail");
					if (processes.Length == 0)
					{
						MessageBox.Show("无法找到游戏进程", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						Task task = Task.Run(() => this.ProcessMonitor(processes[0]));
						this._tokenManager.AddTask(task);
						Config config = this._configLoader.config;
						while (!this._tokenManager.Token.IsCancellationRequested)
						{
							await Task.Delay(16);
							gameSettings.TargetFramerate = config.TargetFramerate;
							gameSettings.FovDelta = config.FovDelta;
							accessor.Write<GameSettings>(0L, ref gameSettings);
						}
					}
				}
			}
		}

		private async Task ProcessMonitor(Process process)
		{
			while (!this._tokenManager.Token.IsCancellationRequested)
			{
				await Task.Delay(1000);
				if (process.HasExited)
				{
					await Task.Run(delegate
					{
						Task.Delay(2000);
						Application.Exit();
					});
					return;
				}
			}
		}

		private MemoryMappedFile OpenMapping()
		{
			MemoryMappedFile memoryMappedFile;
			try
			{
				memoryMappedFile = MemoryMappedFile.CreateOrOpen("SRUnlocker", 4096L);
			}
			catch (Exception)
			{
				memoryMappedFile = null;
			}
			return memoryMappedFile;
		}

		private string GetCommandLine()
		{
			Config config = this._configLoader.config;
			if (!config.UseMobileUI)
			{
				return string.Empty;
			}
			string text3;
			try
			{
				string text = ((from x in File.ReadAllLines(Path.Combine(Path.GetDirectoryName(config.ProcessPath), "config.ini")).ToList<string>()
					select x.Split('=', StringSplitOptions.None) into x
					where x.Length == 2
					select x).ToDictionary((string[] x) => x[0], (string[] x) => x[1])["uapc"].Contains("hkrpg_global") ? "SOFTWARE\\Cognosphere\\Star Rail" : "SOFTWARE\\miHoYo\\崩坏：星穹铁道");
				string text2 = "GraphicsSettings_Model_h2986158309";
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(text))
				{
					if (registryKey == null)
					{
						text3 = string.Empty;
					}
					else
					{
						byte[] array = registryKey.GetValue(text2, Array.Empty<byte>()) as byte[];
						if (array == null)
						{
							text3 = string.Empty;
						}
						else
						{
							string text4 = Convert.ToBase64String(array);
							text3 = "-platform_type CLOUD_WEB_TOUCH -graphics_setting " + text4;
						}
					}
				}
			}
			catch (Exception)
			{
				text3 = string.Empty;
			}
			return text3;
		}

		private bool StartGameAndInject()
		{
			Config config = this._configLoader.config;
			Process[] processesByName = Process.GetProcessesByName("StarRail");
			if (processesByName.Length != 0)
			{
				foreach (Process process in processesByName)
				{
					process.CloseMainWindow();
					process.Kill();
				}
			}
			try
			{
				string text = Assembly.GetExecutingAssembly().GetManifestResourceNames().Single((string str) => str.EndsWith("dinner.bin"));
				Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(text);
				byte[] array2 = new byte[manifestResourceStream.Length];
				manifestResourceStream.Read(array2, 0, array2.Length);
				if (!this.CompareUnlockerBinary(array2))
				{
					File.WriteAllBytes("unlocker.dll", array2);
				}
				if (!File.Exists("unlocker.dll"))
				{
					MessageBox.Show("未找到unlocker.dll，请检查是否被杀软处理了", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return false;
				}
			}
			catch (Exception)
			{
			}
			string processPath = config.ProcessPath;
			string directoryName = Path.GetDirectoryName(processPath);
			STARTUPINFO startupinfo = default(STARTUPINFO);
			PROCESS_INFORMATION process_INFORMATION;
			if (!Native.CreateProcess(processPath, this.GetCommandLine(), IntPtr.Zero, IntPtr.Zero, false, 4U, IntPtr.Zero, directoryName, ref startupinfo, out process_INFORMATION))
			{
				int lastError = Native.GetLastError();
				MessageBox.Show(this.FormatWinErrorMessage(lastError), "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return false;
			}
			string fullPath = Path.GetFullPath("unlocker.dll");
			byte[] bytes = Encoding.Unicode.GetBytes(fullPath);
			IntPtr hProcess = process_INFORMATION.hProcess;
			IntPtr hThread = process_INFORMATION.hThread;
			IntPtr intPtr = Native.VirtualAllocEx(hProcess, IntPtr.Zero, (uint)bytes.Length, 12288U, 4U);
			if (intPtr == IntPtr.Zero)
			{
				int lastError2 = Native.GetLastError();
				MessageBox.Show(this.FormatWinErrorMessage(lastError2), "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				int num = 0;
				Native.WriteProcessMemory(hProcess, intPtr, bytes, bytes.Length, out num);
				IntPtr procAddress = Native.GetProcAddress(Native.LoadLibrary("kernel32.dll"), "LoadLibraryW");
				uint num2 = 0U;
				IntPtr intPtr2 = Native.CreateRemoteThread(hProcess, IntPtr.Zero, 0U, procAddress, intPtr, 0U, out num2);
				if (intPtr2 == IntPtr.Zero)
				{
					int lastError3 = Native.GetLastError();
					MessageBox.Show(this.FormatWinErrorMessage(lastError3), "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					if (!config.UseMobileUI)
					{
						Native.ResumeThread(hThread);
					}
					Native.WaitForSingleObject(intPtr2, uint.MaxValue);
					Native.CloseHandle(intPtr2);
				}
			}
			if (intPtr != IntPtr.Zero)
			{
				Native.VirtualFreeEx(hProcess, intPtr, 0U, 32768U);
			}
			Native.CloseHandle(hProcess);
			Native.CloseHandle(hThread);
			return true;
		}

		private bool CompareUnlockerBinary(byte[] resourceBuffer)
		{
			if (!File.Exists("unlocker.dll"))
			{
				return false;
			}
			byte[] array = File.ReadAllBytes("unlocker.dll");
			return array.Length == resourceBuffer.Length && StructuralComparisons.StructuralEqualityComparer.Equals(array, resourceBuffer);
		}

		private string FormatWinErrorMessage(int errorCode)
		{
			IntPtr moduleHandle = Native.GetModuleHandle(null);
			string text;
			if (Native.FormatMessage(4352U, moduleHandle, (uint)errorCode, 0U, out text, 0U, null) == 0U)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new(7, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Error: ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(errorCode);
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
			return text.TrimEnd(new char[] { '\r', '\n' });
		}

		private TokenManager _tokenManager;

		private ConfigLoader _configLoader;
	}
}
