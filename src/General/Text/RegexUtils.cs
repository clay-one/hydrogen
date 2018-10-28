namespace hydrogen.General.Text
{
    public static class RegexUtils
    {
        public static string CreateWholeInputRegex(string regex)
        {
            return "\\A(?:" + regex + ")\\z";
        }
    }
}