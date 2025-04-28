using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace HSR_Unlock_NetCore
{
	public class TokenManager
	{
		public CancellationToken Token
		{
			get
			{
				return this._cts.Token;
			}
		}

		public void AddTask(Task task)
		{
			this._tasks.Add(task);
		}

		public void Cancel()
		{
			this._cts.Cancel();
			Task.WaitAll(this._tasks.ToArray());
		}

		private CancellationTokenSource _cts = new CancellationTokenSource();

		private List<Task> _tasks = new List<Task>();
	}
}
