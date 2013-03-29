namespace Useful
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;

	/// <summary>
	/// A lock construct that is based on a SemaphoreSlim instance. Supports asyncronous and multi-threaded use.
	/// Each "lock" is one decrement/increment pair for a semaphore. The idea is to allow semaphores to be used as async-safe locks.
	/// </summary>
	public sealed class SemaphoreLock : IDisposable
	{
		public static SemaphoreLock Take(SemaphoreSlim semaphore)
		{
			Argument.ValidateIsNotNull(semaphore, "semaphore");

			semaphore.Wait();

			return new SemaphoreLock(semaphore);
		}

		public static async Task<SemaphoreLock> TakeAsync(SemaphoreSlim semaphore)
		{
			Argument.ValidateIsNotNull(semaphore, "semaphore");

			await semaphore.WaitAsync();

			return new SemaphoreLock(semaphore);
		}

		public static SemaphoreLock TryTake(SemaphoreSlim semaphore, TimeSpan timeout)
		{
			Argument.ValidateIsNotNull(semaphore, "semaphore");

			if (semaphore.Wait(timeout))
				return new SemaphoreLock(semaphore);
			else
				return null;
		}

		public static async Task<SemaphoreLock> TryTakeAsync(SemaphoreSlim semaphore, TimeSpan timeout)
		{
			Argument.ValidateIsNotNull(semaphore, "semaphore");

			if (await semaphore.WaitAsync(timeout))
				return new SemaphoreLock(semaphore);
			else
				return null;
		}

		private readonly SemaphoreSlim _semaphore;

		private SemaphoreLock(SemaphoreSlim semaphore)
		{
			_semaphore = semaphore;
		}

		public void Dispose()
		{
			_semaphore.Release();
		}
	}
}