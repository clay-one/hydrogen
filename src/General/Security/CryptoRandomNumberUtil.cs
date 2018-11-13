using System;
using System.Security.Cryptography;
using System.Text;

namespace Hydrogen.General.Security
{
    public class CryptoRandomNumberUtil
    {
        private static readonly RandomNumberGenerator RandomNumberGenerator = new RNGCryptoServiceProvider();

		public static int GetInt32()
		{
			byte[] resultBytes = GetBytes(4);
			return BitConverter.ToInt32(resultBytes, 0);
		}

		public static int GetInt32(int min, int max)
		{
			if (max < min)
				throw new ArgumentException("Invalid range");

			return (Math.Abs(GetInt32()%(max - min + 1))) + min;
		}

        public static byte[] GetBytes(int length, bool nonZeroOnly = false)
        {
            var randomBytes = new byte[length];
            if (nonZeroOnly)
                RandomNumberGenerator.GetNonZeroBytes(randomBytes);
            else
                RandomNumberGenerator.GetBytes(randomBytes);

            return randomBytes;
        }

        public static string GetNumericString(int length)
        {
            var randomBytes = GetBytes(length);
            var result = new StringBuilder();

            foreach (byte b in randomBytes)
                result.Append(b%10);

            return result.ToString();
        }

        public static string GetAlphaNumericString(int length)
        {
            var randomBytes = GetBytes(length);
            var result = new StringBuilder();

            foreach (byte b in randomBytes)
            {
                int i = b%36;
                
                if (i < 10)
                    result.Append(i);
                else
                    result.Append((char)('a' + (i - 10)));
            }

            return result.ToString();
        }
    }
}