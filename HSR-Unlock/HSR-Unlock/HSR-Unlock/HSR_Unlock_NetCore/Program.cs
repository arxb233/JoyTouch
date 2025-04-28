using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace HSR_Unlock_NetCore
{
	// Token: 0x0200000F RID: 15
	internal static class Program
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000367A File Offset: 0x0000187A
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00003681 File Offset: 0x00001881
		public static IServiceProvider ServiceProvider { get; private set; }

		// Token: 0x06000055 RID: 85 RVA: 0x0000368C File Offset: 0x0000188C
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

		// Token: 0x04000048 RID: 72
		private static IntPtr MutexHandle = IntPtr.Zero;

		// Token: 0x0400004A RID: 74
		public static int Build = 10001;
	}
}
