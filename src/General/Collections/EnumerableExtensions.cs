using System;
using System.Collections.Generic;
using System.Linq;

namespace hydrogen.General.Collections
{
	public static class EnumerableExtensions
	{
		public static bool IsNullOrEmptyEnumerable<TSource>(this IEnumerable<TSource> source)
		{
			return !source.SafeAny();
		}

		public static IEnumerable<TSource> EmptyIfNull<TSource>(this IEnumerable<TSource> source)
		{
			return source ?? Enumerable.Empty<TSource>();
		}

		public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource> source) where TSource : class
		{
			return source.Where(i => i != null);
		}

		public static IEnumerable<TSource> WhereHasValue<TSource>(this IEnumerable<TSource?> source) where TSource : struct
		{
			return source.Where(i => i.HasValue).Select(i => i.Value);
		}

		public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			foreach (var item in source)
			{
				action(item);
			}
		}

		public static bool SafeAny<TSource>(this IEnumerable<TSource> source, bool nullResult = false)
		{
			return source?.Any() ?? nullResult;
		}

		public static bool SafeAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, bool nullResult = false)
		{
			return source?.Any(predicate) ?? nullResult;
		}

		public static List<TSource> SafeToList<TSource>(this IEnumerable<TSource> source, List<TSource> nullResult = null)
		{
			return source?.ToList() ?? nullResult;
		}

		public static TSource[] SafeToArray<TSource>(this IEnumerable<TSource> source, TSource[] nullResult = null)
		{
			return source?.ToArray() ?? nullResult;
		}

		public static IEnumerable<TSource> SafeWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, IEnumerable<TSource> nullResult = null)
		{
			return source?.Where(predicate) ?? nullResult;
		}

		public static IEnumerable<TResult> SafeSelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, IEnumerable<TResult> nullResult = null)
		{
			return source?.Select(selector) ?? nullResult;
		}

		public static void SafeForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			if (source != null && action != null)
				source.ForEach(action);
		}
	}
}