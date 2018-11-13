using System.Text.RegularExpressions;

namespace Hydrogen.General.Text
{
    public static class RegexExtensions
	{
		public static bool IsMatchWhole(this Regex regex, string input)
		{
			return regex.Match(input).SuccessWholeInput(input);
		}

		public static bool IsMatchStart(this Regex regex, string input)
		{
			return regex.Match(input).SuccessInputStart();
		}

		public static bool SuccessWholeInput(this Match match, string input)
		{
			return match.Success && match.Index == 0 && match.Length == input.Length;
		}

		public static bool SuccessInputStart(this Match match)
		{
			return match.Success && match.Index == 0;
		}
	}
}
