using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HSR_Unlock_NetCore
{
	// Token: 0x0200000C RID: 12
	internal class Native
	{
		// Token: 0x0600003A RID: 58
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr CreateMutex(IntPtr lpMutexAttributes, bool bInitialOwner, string lpName);

		// Token: 0x0600003B RID: 59
		[DllImport("kernel32.dll")]
		public static extern int GetLastError();

		// Token: 0x0600003C RID: 60
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr LoadLibrary(string lpFileName);

		// Token: 0x0600003D RID: 61
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		// Token: 0x0600003E RID: 62
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern void FreeLibrary(IntPtr handle);

		// Token: 0x0600003F RID: 63
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

		// Token: 0x06000040 RID: 64
		[DllImport("kernel32.dll")]
		public static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, out string lpBuffer, uint nSize, IntPtr[] Arguments);

		// Token: 0x06000041 RID: 65
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

		// Token: 0x06000042 RID: 66
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x06000043 RID: 67
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint ResumeThread(IntPtr hThread);

		// Token: 0x06000044 RID: 68
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesWritten);

		// Token: 0x06000045 RID: 69
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

		// Token: 0x06000046 RID: 70
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

		// Token: 0x06000047 RID: 71
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

		// Token: 0x06000048 RID: 72
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

		// Token: 0x0400003C RID: 60
		public const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096U;

		// Token: 0x0400003D RID: 61
		public const uint FORMAT_MESSAGE_ALLOCATE_BUFFER = 256U;

		// Token: 0x0400003E RID: 62
		public const uint CREATE_SUSPENDED = 4U;

		// Token: 0x0400003F RID: 63
		public const uint MEM_COMMIT = 4096U;

		// Token: 0x04000040 RID: 64
		public const uint MEM_RESERVE = 8192U;

		// Token: 0x04000041 RID: 65
		public const uint PAGE_READWRITE = 4U;

		// Token: 0x04000042 RID: 66
		public const uint PAGE_EXECUTE_READWRITE = 64U;

		// Token: 0x04000043 RID: 67
		public const uint MEM_RELEASE = 32768U;
	}
}
