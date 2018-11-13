using System.Text;

namespace Hydrogen.General.Text
{
    public static class StringBuilderExtensions
	{
		public static StringBuilder AppendSeparator(this StringBuilder builder, string separator)
		{
			if (builder.Length > 0)
				return builder.Append(separator);

			return builder;
		}

	    public static StringBuilder AppendIf(this StringBuilder builder, bool condition, string value)
	    {
	        if (condition)
	            builder.Append(value);

	        return builder;
	    }
	}
}