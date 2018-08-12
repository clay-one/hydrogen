using System;

namespace hydrogen.General.Localization
{
    public static class DateTimeLocalizationExtensions
	{
		public static string ToLocalizedDateString(this DateTime dateTime)
		{
			return DateTimeLocalizationUtils.ToLocalizedDateString(dateTime);
		}

		public static string ToLocalizedDateString(this DateTime? dateTime)
		{
			return DateTimeLocalizationUtils.ToLocalizedDateString(dateTime);
		}

		public static string ToLocalizedTimeString(this DateTime dateTime)
		{
			return DateTimeLocalizationUtils.ToLocalizedTimeString(dateTime);
		}

		public static string ToLocalizedTimeString(this DateTime? dateTime)
		{
			return DateTimeLocalizationUtils.ToLocalizedTimeString(dateTime);
		}
	}
}