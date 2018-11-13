using System.Linq;

namespace Hydrogen.General.Text
{
    public static class StringUtils
	{
		public static string JoinNonEmpty(string separator, params string[] strings)
		{
			return string.Join(separator, strings.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray());
		}

		public static string PrependIfNotEmpty(string prefix, string content)
		{
			if (string.IsNullOrWhiteSpace(content))
				return string.Empty;

			return prefix + content;
		}
	}
}