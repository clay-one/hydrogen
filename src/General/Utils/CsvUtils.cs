using System;
using System.Collections.Generic;
using System.Linq;

namespace hydrogen.General.Utils
{
    public static class CsvUtils
    {
        public static string ToCsvString<T>(IEnumerable<T> enumerable, Func<T, string> toString = null)
        {
            if (enumerable == null)
                return string.Empty;

            toString = toString ?? (t => t.ToString());
            return string.Join(",", enumerable.Select(toString));
        }

        public static IEnumerable<int> ParseInt32Enumerable(string csv)
        {
            if (string.IsNullOrWhiteSpace(csv))
                return Enumerable.Empty<int>();

            return csv.Split(',').Where(v => !string.IsNullOrWhiteSpace(v)).Select(int.Parse);
        }

        public static IEnumerable<long> ParseInt64Enumerable(string csv)
        {
            if (string.IsNullOrWhiteSpace(csv))
                return Enumerable.Empty<long>();

            return csv.Split(',').Where(v => !string.IsNullOrWhiteSpace(v)).Select(long.Parse);
        }

        public static IEnumerable<T> ParseEnumArray<T>(string csv)
        {
            if (string.IsNullOrWhiteSpace(csv))
                return Enumerable.Empty<T>();

            return csv.Split(',').Where(p => !string.IsNullOrWhiteSpace(p)).Select(p => (T) Enum.ToObject(typeof (T), int.Parse(p)));
        }
    }
}