using System;
using System.Collections.Generic;

namespace hydrogen.General.Localization
{
	public static class TimeSpanLocalizationUtils
	{
		public static string BuildRelativeText(TimeSpan? timeSpan, int maxNumberOfParts, string zeroDurationString = null)
		{
			if (!timeSpan.HasValue)
				return TimeSpanLocalizationResources.Unknown;

			return BuildRelativeText(timeSpan.Value, maxNumberOfParts, zeroDurationString);
		}

		public static string BuildDurationText(TimeSpan? timeSpan, int maxNumberOfParts, string zeroDurationString = null)
		{
			if (!timeSpan.HasValue)
				return TimeSpanLocalizationResources.Unknown;

			return BuildDurationText(timeSpan.Value, maxNumberOfParts, zeroDurationString);
		}

		public static string BuildRelativeText(TimeSpan timeSpan, int maxNumberOfParts = 2, string zeroDurationString = null)
		{
			if (Math.Abs(timeSpan.TotalSeconds) < 1)
				return zeroDurationString ?? TimeSpanLocalizationResources.SameMoment;

			string result = BuildDurationText(timeSpan, maxNumberOfParts);
			return string.Format(timeSpan.Ticks < 0 ? TimeSpanLocalizationResources.Ago : TimeSpanLocalizationResources.Later, result);
		}

		public static string BuildDurationText(TimeSpan timeSpan, int maxNumberOfParts = 2, string zeroDurationString = null)
		{
			if (maxNumberOfParts > 7 || maxNumberOfParts < 1)
				throw new ArgumentException("maxNumberOfParts should be between 1 and 7");

			if (timeSpan.Ticks < 0)
				timeSpan = timeSpan.Duration();

			var days = timeSpan.Days;
			int years = days/365;
			days %= 365;
			int months = days/30;
			days %= 30;
			int weeks = days/7;
			days %= 7;

			var resultParts = BuildTextArray(years, months, weeks, days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, maxNumberOfParts);
			if (resultParts == null || resultParts.Count < 1)
				return zeroDurationString ?? TimeSpanLocalizationResources.ZeroDuration;

			return string.Join(TimeSpanLocalizationResources.Joiner, resultParts);
		}

		private static IList<string> BuildTextArray(int years, int months, int weeks, int days, int hours, int minutes, int seconds, int maxNumberOfParts)
		{
			var result = new List<string>(maxNumberOfParts);
			var remainingNumberOfParts = maxNumberOfParts;

			if (years != 0)
			{
				result.Add(years == 1 ? TimeSpanLocalizationResources.OneYear : string.Format(TimeSpanLocalizationResources.Years, years));
				remainingNumberOfParts--;
			}

			if (months != 0 && remainingNumberOfParts > 0)
			{
				result.Add(months == 1 ? TimeSpanLocalizationResources.OneMonth : string.Format(TimeSpanLocalizationResources.Months, months));
				remainingNumberOfParts--;
			}

			if (weeks != 0 && remainingNumberOfParts > 0)
			{
				result.Add(weeks == 1 ? TimeSpanLocalizationResources.OneWeek : string.Format(TimeSpanLocalizationResources.Weeks, weeks));
				remainingNumberOfParts--;
			}

			if (days != 0 && remainingNumberOfParts > 0)
			{
				result.Add(days == 1 ? TimeSpanLocalizationResources.OneDay : string.Format(TimeSpanLocalizationResources.Days, days));
				remainingNumberOfParts--;
			}

			if (hours != 0 && remainingNumberOfParts > 0)
			{
				result.Add(hours == 1 ? TimeSpanLocalizationResources.OneHour : string.Format(TimeSpanLocalizationResources.Hours, hours));
				remainingNumberOfParts--;
			}

			if (minutes != 0 && remainingNumberOfParts > 0)
			{
				result.Add(minutes == 1 ? TimeSpanLocalizationResources.OneMinute : string.Format(TimeSpanLocalizationResources.Minutes, minutes));
				remainingNumberOfParts--;
			}

			if (seconds != 0 && remainingNumberOfParts > 0)
			{
				result.Add(seconds == 1 ? TimeSpanLocalizationResources.OneSecod : string.Format(TimeSpanLocalizationResources.Seconds, seconds));
			}

			return result;
		}
	}
}