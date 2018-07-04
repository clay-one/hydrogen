using System;
using System.Collections.Generic;

namespace hydrogen.General.Utils
{
    /// <summary>
    /// Contains most generic extension methods that can be applied to any object type
    /// </summary>
	public static class ObjectExtensions
	{
		public static TResult IfNotNull<TSource, TResult>(this TSource source, Func<TSource, TResult> f) where TSource: class
		{
			return source == null ? default(TResult) : f(source);
		}

		public static TResult IfHasValue<TSource, TResult>(this TSource? source, Func<TSource, TResult> f) where TSource: struct
		{
			return source.HasValue ? f(source.Value) : default(TResult);
		}

		public static TResult IfNotNull<TSource, TResult>(this TSource source, Func<TSource, TResult> f, TResult nullValue) where TSource: class
		{
			return source == null ? nullValue : f(source);
		}

		public static TResult IfHasValue<TSource, TResult>(this TSource? source, Func<TSource, TResult> f, TResult nullValue) where TSource : struct
		{
			return source.HasValue ? f(source.Value) : nullValue;
		}

	    public static string SafeToString<T>(this T source, string nullValue = null) where T : class
	    {
		    return source?.ToString() ?? nullValue;
	    }

	    public static string SafeToString<T>(this T? source, string nullValue = null) where T : struct
	    {
		    return source?.ToString() ?? nullValue;
	    }

        /// <summary>
        /// Wraps this object instance into an IEnumerable&lt;T&gt;
        /// consisting of a single item.
        /// </summary>
        /// <typeparam name="T"> Type of the wrapped object.</typeparam>
        /// <param name="item"> The object to wrap.</param>
        /// <returns>
        /// An IEnumerable&lt;T&gt; consisting of a single item.
        /// </returns>
        /// <remarks>
        /// Copied from:
        /// http://stackoverflow.com/questions/1577822/passing-a-single-item-as-ienumerablet
        /// </remarks>
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
	}
}