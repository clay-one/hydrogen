using System.Text.RegularExpressions;

namespace Hydrogen.General.Text
{
    public static class EmailUtils
    {
        private const string EmailRegexString = @"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?";

        private static readonly Regex EmailRegex = new Regex(EmailRegexString);
        private static readonly Regex WholeEmailRegex = new Regex(RegexUtils.CreateWholeInputRegex(EmailRegexString));

        public static bool IsValidEmail(string email, bool allowPartial)
        {
            return (allowPartial ? EmailRegex : WholeEmailRegex).IsMatch(email);
        }

        public static bool IsValidEmail(string email)
        {
            return IsValidEmail(email, false);
        }
    }
}