using System.IO;
using System.Reflection;

namespace hydrogen.General.Streams
{
	public static class ManifestResourceUtil
	{
		public static byte[] GetManifestResourceBytes(Assembly assembly, string manifestResourceName)
		{
			using (var resourceStream = assembly.GetManifestResourceStream(manifestResourceName))
			{
				if (resourceStream == null)
					return null;

				using (var memStream = new MemoryStream())
				{
					resourceStream.CopyTo(memStream);
					return memStream.ToArray();
				}
			}
		}
	}
}