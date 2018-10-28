using System;
using System.Globalization;

namespace hydrogen.General.Text
{
	public static class NumericStringUtils
	{
		private const string CardinalTextualNumberResourcePrefix = "CardinalTextual";
		private const string OrdinalTextualNumberResourcePrefix = "OrdinalTextual";

		public static string ShortNumericString(decimal? input)
		{
			if (!input.HasValue)
				return null;

			if (input == 0)
				return NumericStringUtilsResources.CardinalTextual0;

			decimal number = input.Value;
			string format = NumericStringUtilsResources.NumericOnes;

			if (number >= 1000)
			{
				number /= 1000;
				format = NumericStringUtilsResources.NumericThousands;
			}

			if (number >= 1000)
			{
				number /= 1000;
				format = NumericStringUtilsResources.NumericMillions;
			}

			if (number >= 1000)
			{
				number /= 1000;
				format = NumericStringUtilsResources.NumericBillions;
			}

			number = Math.Round(number, Math.Max(3 - CountIntegralDigits(number), 0));
			return string.Format(format, number);
		}

		public static string FullyTextualNumber(decimal? input)
		{
			if (!input.HasValue)
				return null;

			bool negative = input.Value < 0;
			input = Math.Abs(input.Value);

			// Fractions are not supported for now.
			long integerPart = Convert.ToInt32(input);

			// Only implementing numbers under 1000 for now
			if (input > 1000)
				return ShortNumericString(input);

			string result = FullyTextualNumber3Digits(integerPart, false);
			return negative ? string.Format(NumericStringUtilsResources.TextualNegativeFormat, result) : result;
		}

		public static string FullyTextualOrdinalNumber(long? input)
		{
			if (!input.HasValue)
				return null;

			bool negative = input.Value < 0;
			input = Math.Abs(input.Value);

			// Only implementing numbers under 1000 for now
			string result;

			if (input.Value == 1)
				result = NumericStringUtilsResources.First;
			else if (input > 1000)
				result = string.Format(NumericStringUtilsResources.GenericOrdinalFormat, ShortNumericString(input));
			else
				result = FullyTextualNumber3Digits(input.Value, true);

			return negative ? string.Format(NumericStringUtilsResources.TextualNegativeFormat, result) : result;
		}

		public static String BytesToString(long byteCount)
		{
			// Copied from the following SO question:
			// http://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net

			string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
			if (byteCount == 0)
				return "0 " + suf[0];

			long bytes = Math.Abs(byteCount);
			int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
			double num = Math.Round(bytes / Math.Pow(1024, place), 1);
			return (Math.Sign(byteCount) * num).ToString(CultureInfo.InvariantCulture) + " " + suf[place];
		}

		#region Private helper methods

		private static int CountIntegralDigits(decimal input)
		{
			int result = 1;
			while (input >= 10)
			{
				result++;
				input /= 10;
			}

			return result;
		}

		private static string FullyTextualNumber2Digits(long input, bool ordinal)
		{
			if (input <= 20)
				return NumericStringUtilsResources.ResourceManager.GetString(GetTextualNumberResourcePrefix(ordinal) + input);

			long ones = input%10;
			long tens = input - ones;

			if (ones == 0)
				return NumericStringUtilsResources.ResourceManager.GetString(GetTextualNumberResourcePrefix(ordinal) + tens);

			return NumericStringUtilsResources.ResourceManager.GetString(GetTextualNumberResourcePrefix(false) + tens) +
			       NumericStringUtilsResources.TextualJointer +
			       NumericStringUtilsResources.ResourceManager.GetString(GetTextualNumberResourcePrefix(ordinal) + ones);
		}

		private static string FullyTextualNumber3Digits(long input, bool ordinal)
		{
			if (input < 100)
				return FullyTextualNumber2Digits(input, ordinal);

			long ones = input%100;
			long hundreds = input - ones;

			if (ones == 0)
				return NumericStringUtilsResources.ResourceManager.GetString(GetTextualNumberResourcePrefix(ordinal) + hundreds);

			return NumericStringUtilsResources.ResourceManager.GetString(GetTextualNumberResourcePrefix(false) + hundreds) +
			       NumericStringUtilsResources.TextualJointer +
			       FullyTextualNumber2Digits(ones, ordinal);
		}

		private static string GetTextualNumberResourcePrefix(bool ordinal)
		{
			return ordinal ? OrdinalTextualNumberResourcePrefix : CardinalTextualNumberResourcePrefix;
		}

		#endregion
	}
}