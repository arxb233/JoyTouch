using System;
using System.Runtime.CompilerServices;

namespace HSR_Unlock_NetCore
{
	// Token: 0x0200000B RID: 11
	public struct STARTUPINFO
	{
		// Token: 0x0400002A RID: 42
		public int cb;

		// Token: 0x0400002B RID: 43
		public string lpReserved;

		// Token: 0x0400002C RID: 44
		public string lpDesktop;

		// Token: 0x0400002D RID: 45
		public string lpTitle;

		// Token: 0x0400002E RID: 46
		public int dwX;

		// Token: 0x0400002F RID: 47
		public int dwY;

		// Token: 0x04000030 RID: 48
		public int dwXSize;

		// Token: 0x04000031 RID: 49
		public int dwYSize;

		// Token: 0x04000032 RID: 50
		public int dwXCountChars;

		// Token: 0x04000033 RID: 51
		public int dwYCountChars;

		// Token: 0x04000034 RID: 52
		public int dwFillAttribute;

		// Token: 0x04000035 RID: 53
		public int dwFlags;

		// Token: 0x04000036 RID: 54
		public short wShowWindow;

		// Token: 0x04000037 RID: 55
		public short cbReserved2;

		// Token: 0x04000038 RID: 56
		public IntPtr lpReserved2;

		// Token: 0x04000039 RID: 57
		public IntPtr hStdInput;

		// Token: 0x0400003A RID: 58
		public IntPtr hStdOutput;

		// Token: 0x0400003B RID: 59
		public IntPtr hStdError;
	}
}
