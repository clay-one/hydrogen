using System;

namespace hydrogen.General.Text
{
    /// <summary>
    /// Source code copied from PersianNormalizer class within Lucene.Net
    /// </summary>
    public class PersianCharacterNormalizer
	{
		public const char Yeh = 'ي';
		public const char FarsiYeh = 'ی';
		public const char YehBarree = 'ے';
		public const char Keheh = 'ک';
		public const char Kaf = 'ك';
		public const char HamzaAbove = 'ٔ';
		public const char HehYeh = 'ۀ';
		public const char HehGoal = 'ہ';
		public const char Heh = 'ه';

		public static int Normalize(char[] s, int len)
		{
			for (int pos = 0; pos < len; ++pos)
			{
				switch (s[pos])
				{
					case 'ۀ':
					case 'ہ':
						s[pos] = 'ه';
						break;
					case 'ی':
					case 'ے':
						s[pos] = 'ي';
						break;
					case 'ٔ':
						len = Delete(s, pos, len);
						--pos;
						break;
					case 'ک':
						s[pos] = 'ك';
						break;
				}
			}
			return len;
		}

		private static int Delete(char[] s, int pos, int len)
		{
			if (pos < len)
				Array.Copy(s, pos + 1, s, pos, len - pos - 1);
			return len - 1;
		}
	}
}