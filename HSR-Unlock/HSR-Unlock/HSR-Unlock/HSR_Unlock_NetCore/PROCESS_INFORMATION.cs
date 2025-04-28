using System;

namespace HSR_Unlock_NetCore
{
	// Token: 0x0200000A RID: 10
	public struct PROCESS_INFORMATION
	{
		// Token: 0x04000026 RID: 38
		public IntPtr hProcess;

		// Token: 0x04000027 RID: 39
		public IntPtr hThread;

		// Token: 0x04000028 RID: 40
		public int dwProcessId;

		// Token: 0x04000029 RID: 41
		public int dwThreadId;
	}
}
