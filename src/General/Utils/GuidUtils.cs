using System;

namespace Hydrogen.General.Utils
{
    public static class GuidUtils
    {
        public static string ToUrlFriendly(this Guid guid)
        {
            string enc = Convert.ToBase64String(guid.ToByteArray());
            enc = enc.Replace("/", "_");
            enc = enc.Replace("+", "-");
            return enc.Substring(0, 22);
        }

        public static Guid ParseUrlFriendlyGuid(string urlFriendlyGuid)
        {
            urlFriendlyGuid = urlFriendlyGuid.Replace("_", "/");
            urlFriendlyGuid = urlFriendlyGuid.Replace("-", "+");
            byte[] buffer = Convert.FromBase64String(urlFriendlyGuid + "==");
            return new Guid(buffer);
        }
    }
}