using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace hydrogen.General.Localization
{
    public class DateTimeLocalizationUtils
	{
		private static readonly PersianCalendar PersianCalendar = new PersianCalendar();
		private static readonly Regex ShamsiDateRegex = new Regex(@"^(1[34][0-9][0-9])/(0?[1-9]|10|11|12)/(0?[1-9]|[12][0-9]|30|31)$");

		#region Public methods

		public static string ToLocalizedDateString(DateTime dateTime)
		{
			// TODO: Make the conversion locale-sensitive
			return PersianCalendar.GetYear(dateTime).ToString("0000") + "/" +
			       PersianCalendar.GetMonth(dateTime).ToString("00") + "/" +
			       PersianCalendar.GetDayOfMonth(dateTime).ToString("00");
		}

		public static string ToLocalizedTimeString(DateTime dateTime)
		{
			return dateTime.Hour.ToString("00") + ":" + dateTime.Minute.ToString("00");
		}

		public static string ToLocalizedDateString(DateTime? dateTime)
		{
			return dateTime?.ToLocalizedDateString();
		}

		public static string ToLocalizedTimeString(DateTime? dateTime)
		{
			return dateTime?.ToLocalizedTimeString();
		}

		public static DateTime? FromLocalizedDateString(string input)
		{
			var shamsiMatch = ShamsiDateRegex.Match(input);
			DateTime? result;

			if (shamsiMatch.Success)
				result = TryParseShamsiDate(shamsiMatch.Groups[1].Value, shamsiMatch.Groups[2].Value, shamsiMatch.Groups[3].Value);
			else
				result = TryParseGregorianDate(input);

			return result;

		}

		#endregion

		#region Private helper methods

		private static DateTime? TryParseGregorianDate(string stringValue)
		{
			DateTime result;
			if (!DateTime.TryParse(stringValue, out result))
				return null;

			return result;
		}

		private static DateTime? TryParseShamsiDate(string yearString, string monthString, string dayString)
		{
			int year;
			int month;
			int day;

			if (!int.TryParse(yearString, out year)) return null;
			if (!int.TryParse(monthString, out month)) return null;
			if (!int.TryParse(dayString, out day)) return null;

			var calendar = new PersianCalendar();
			try
			{
				return calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
			}
			catch (ArgumentOutOfRangeException)
			{
				return null;
			}
		}

		#endregion
	}
}