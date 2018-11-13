using System.Text;

namespace Hydrogen.General.Text
{
    public static class DigitLocalizationUtils
    {
        public static string ToEnglish(string input)
        {
            var result = new StringBuilder(input);
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] >= 0x0660 && result[i] <= 0x669)
                    result[i] = (char) (result[i] + '0' - 0x0660);

                if (result[i] >= 0x06F0 && result[i] <= 0x6F9)
                    result[i] = (char) (result[i] + '0' - 0x06F0);
            }

            return result.ToString();
        }

        public static string ToPersian(string input)
        {
            var result = new StringBuilder(input);
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] >= '0' && result[i] <= '9')
                    result[i] = (char) (result[i] + 0x06F0 - '0');

                if (result[i] >= 0x0660 && result[i] <= 0x669)
                    result[i] = (char) (result[i] + 0x06F0 - 0x0660);
            }

            return result.ToString();
        }
    }
}