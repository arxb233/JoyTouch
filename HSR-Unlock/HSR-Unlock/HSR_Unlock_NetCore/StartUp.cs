using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace HSR_Unlock_NetCore
{
	internal class StartUp
	{
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
