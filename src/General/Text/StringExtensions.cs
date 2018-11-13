using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hydrogen.General.Text
{
    public static class StringExtensions
	{
		private static readonly Regex WhitespaceRegex = new Regex("\\s");

		public static string FormatIfNotNull(this string format, params object[] formatParams)
		{
			if (formatParams.Any(fp => fp == null))
				return null;

			return String.Format(format, formatParams);
		}

		public static string FormatIfNotEmpty(this string format, params string[] formatParams)
		{
			if (formatParams.Any(String.IsNullOrWhiteSpace))
				return null;

			return String.Format(format, formatParams.Cast<object>().ToArray());
		}

		public static string If(this string str, bool condition)
		{
			if (condition)
				return str;

			return null;
		}

		public static string If(this string str, bool? condition)
		{
			if (condition.HasValue && condition.Value)
				return str;

			return null;
		}

		public static TResult IfNotEmpty<TResult>(this string str, Func<string, TResult> f, TResult emptyValue)
		{
			return String.IsNullOrWhiteSpace(str) ? emptyValue : f(str);
		}

		public static TResult IfNotEmpty<TResult>(this string str, Func<string, TResult> f)
		{
			return String.IsNullOrWhiteSpace(str) ? default(TResult) : f(str);
		}

		public static string Truncate(this string str, int maxLength)
		{
			if (str == null)
				return null;

			if (str.Length <= maxLength)
				return str;

			return str.Substring(0, maxLength - 3) + "...";
		}

		public static string RemoveWhitespace(this string str)
		{
			if (str == null)
				return null;

			return WhitespaceRegex.Replace(str, "");
		}

		public static string[] SplitByWhitespace(this string str)
		{
			if (str == null)
				return null;

			return WhitespaceRegex.Split(str);
		}

		public static bool IsNullOrWhitespace(this string str)
		{
			return String.IsNullOrWhiteSpace(str);
		}

		public static bool HasText(this string str)
		{
			return !String.IsNullOrWhiteSpace(str);
		}

		public static IEnumerable<string> WhereNotNullOrEmpty(this IEnumerable<string> source)
		{
			return source?.Where(s => !String.IsNullOrEmpty(s));
		}

		public static IEnumerable<string> WhereNotNullOrWhitespace(this IEnumerable<string> source)
		{
			return source?.Where(s => !String.IsNullOrWhiteSpace(s));
		}
	}
}