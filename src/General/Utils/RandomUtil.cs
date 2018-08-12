using System;
using System.Threading;

namespace hydrogen.General.Utils
{
    /// <summary>
    /// Provides thread-based random number generators, and guarantees unique seeds for their initialization.
    /// Code copied from Jon Skeet's website, from the following URL:
    /// http://csharpindepth.com/Articles/Chapter12/Random.aspx
    /// </summary>
    public static class RandomProvider
	{
		private static int _seed = Environment.TickCount;

		private static readonly ThreadLocal<Random> RandomWrapper = new ThreadLocal<Random>(() =>
			new Random(Interlocked.Increment(ref _seed))
		);

		public static Random GetThreadRandom()
		{
			return RandomWrapper.Value;
		}
	}
}