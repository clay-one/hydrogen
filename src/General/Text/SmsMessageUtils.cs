namespace hydrogen.General.Text
{
    public static class SmsMessageUtils
	{
		public const int MaxUnicodeFirstSegmentLength = 70;
		public const int MaxUnicodeSegmentLength = 67;

		public static int CalculateNumberOfSegments(string messageText)
		{
			if (messageText == null)
				return 0;

			if (messageText.Length <= MaxUnicodeFirstSegmentLength)
				return 1;

			return (messageText.Length + MaxUnicodeSegmentLength - 1)/MaxUnicodeSegmentLength;
		}
	}
}