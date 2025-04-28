using System;
using System.Runtime.InteropServices;

namespace HSR_Unlock_NetCore
{
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GameSettings
	{
		public int TargetFramerate;

		public float FovDelta;
	}
}
