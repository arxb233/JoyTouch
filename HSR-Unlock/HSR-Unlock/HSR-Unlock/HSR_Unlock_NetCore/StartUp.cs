using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace HSR_Unlock_NetCore
{
	// Token: 0x02000011 RID: 17
	internal class StartUp
	{
		// Token: 0x0600005C RID: 92 RVA: 0x00003A69 File Offset: 0x00001C69
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<MainForm>();
			services.AddTransient<SetupForm>();
			services.AddSingleton<ProcessService>();
			services.AddSingleton<ConfigLoader>();
			services.AddSingleton<TokenManager>();
		}
	}
}
