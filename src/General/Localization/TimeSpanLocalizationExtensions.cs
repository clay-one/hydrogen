using System;

namespace hydrogen.General.Localization
{
    public static class TimeSpanLocalizationExtensions
	{
		public static string ToLocalizedDurationString(this TimeSpan timeSpan, int maxNumberOfParts = 2, string zeroDurationString = null)
		{
			return TimeSpanLocalizationUtils.BuildDurationText(timeSpan, maxNumberOfParts, zeroDurationString);
		}

		public static string ToLocalizedRelativeString(this TimeSpan timeSpan, int maxNumberOfParts = 2, string zeroDurationString = null)
		{
			return TimeSpanLocalizationUtils.BuildRelativeText(timeSpan, maxNumberOfParts, zeroDurationString);
		}

		public static string ToLocalizedDurationString(this TimeSpan? timeSpan, int maxNumberOfParts = 2, string zeroDurationString = null)
		{
			return TimeSpanLocalizationUtils.BuildDurationText(timeSpan, maxNumberOfParts, zeroDurationString);
		}

		public static string ToLocalizedRelativeString(this TimeSpan? timeSpan, int maxNumberOfParts = 2, string zeroDurationString = null)
		{
			return TimeSpanLocalizationUtils.BuildRelativeText(timeSpan, maxNumberOfParts, zeroDurationString);
		}
	}
}