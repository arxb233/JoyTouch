using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HSR_Unlock_NetCore
{
	// Token: 0x02000007 RID: 7
	[Obfuscation(Feature = "renaming", Exclude = true)]
	public class Config
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020C7 File Offset: 0x000002C7
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020CF File Offset: 0x000002CF
		public string ProcessPath { get; set; } = "";

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020D8 File Offset: 0x000002D8
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020E0 File Offset: 0x000002E0
		public int TargetFramerate { get; set; } = 120;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020E9 File Offset: 0x000002E9
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000020F1 File Offset: 0x000002F1
		public float FovDelta { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020FA File Offset: 0x000002FA
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002102 File Offset: 0x00000302
		public bool AutoStart { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000210B File Offset: 0x0000030B
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002113 File Offset: 0x00000313
		public bool StartMinimized { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000211C File Offset: 0x0000031C
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002124 File Offset: 0x00000324
		public bool UseMobileUI { get; set; }
	}
}
