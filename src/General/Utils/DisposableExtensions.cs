using System;

namespace Hydrogen.General.Utils
{
    public static class DisposableExtensions
	{
		public static void Use<TDisposable>(this TDisposable disposable, Action<TDisposable> a) where TDisposable : IDisposable
		{
			using (disposable)
			{
				a(disposable);
			}
		}

		public static TResult Use<TDisposable, TResult>(this TDisposable disposable, Func<TDisposable, TResult> f) where TDisposable : IDisposable
		{
			using (disposable)
			{
				return f(disposable);
			}
		}
	}
}