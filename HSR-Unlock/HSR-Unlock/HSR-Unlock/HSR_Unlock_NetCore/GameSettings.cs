using System;
using System.Runtime.InteropServices;

namespace HSR_Unlock_NetCore
{
	// Token: 0x0200000D RID: 13
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GameSettings
	{
		// Token: 0x04000044 RID: 68
		public int TargetFramerate;

		// Token: 0x04000045 RID: 69
		public float FovDelta;
	}
}
