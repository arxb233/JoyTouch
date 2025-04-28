using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HSR_Unlock_NetCore
{
	[Obfuscation(Feature = "renaming", Exclude = true)]
	public class Config
	{
		public string ProcessPath { get; set; } = "";

		public int TargetFramerate { get; set; } = 120;

		public float FovDelta { get; set; }

		public bool AutoStart { get; set; }

		public bool StartMinimized { get; set; }

		public bool UseMobileUI { get; set; }
	}
}
