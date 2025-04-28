using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HSR_Unlock_NetCore
{
	// Token: 0x02000012 RID: 18
	public class TokenManager
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003A96 File Offset: 0x00001C96
		public CancellationToken Token
		{
			get
			{
				return this._cts.Token;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003AA3 File Offset: 0x00001CA3
		public void AddTask(Task task)
		{
			this._tasks.Add(task);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003AB1 File Offset: 0x00001CB1
		public void Cancel()
		{
			this._cts.Cancel();
			Task.WaitAll(this._tasks.ToArray());
		}

		// Token: 0x04000050 RID: 80
		private CancellationTokenSource _cts = new CancellationTokenSource();

		// Token: 0x04000051 RID: 81
		private List<Task> _tasks = new List<Task>();
	}
}
