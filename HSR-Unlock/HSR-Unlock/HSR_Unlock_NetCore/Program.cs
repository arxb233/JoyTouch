using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace HSR_Unlock_NetCore
{
	internal static class Program
	{
		public static IServiceProvider ServiceProvider { get; private set; }

		[STAThread]
		private static void Main()
		{
			Program.MutexHandle = Native.CreateMutex(IntPtr.Zero, true, "HSRUnlocker");
			if (Marshal.GetLastWin32Error() == 183)
			{
				MessageBox.Show("已经有一个解锁器在运行", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			ServiceCollection serviceCollection = new ServiceCollection();
			serviceCollection.AddTransient<MainForm>();
			serviceCollection.AddTransient<SetupForm>();
			serviceCollection.AddSingleton<ProcessService>();
			serviceCollection.AddSingleton<ConfigLoader>();
			serviceCollection.AddSingleton<TokenManager>();
			Program.ServiceProvider = serviceCollection.BuildServiceProvider();
            ApplicationConfiguration.Initialize();
			Application.Run(Program.ServiceProvider.GetRequiredService<MainForm>());
		}

		private static IntPtr MutexHandle = IntPtr.Zero;

		public static int Build = 10001;
	}
}
