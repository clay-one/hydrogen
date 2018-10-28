using System.Resources;

namespace hydrogen.General.Localization
{
	public static class EnumExtensions
	{
		public static string Label<TEnum>(this TEnum enumObject, ResourceManager resourceManager = null)
			where TEnum : struct
		{
			return resourceManager == null
				? enumObject.ToString()
				: resourceManager.GetString(typeof (TEnum).Name + "_" + enumObject) ??
				  resourceManager.GetString(typeof (TEnum).Name) ??
				  enumObject.ToString();
		}

		public static string Label<TEnum>(this TEnum? enumObject, ResourceManager resourceManager = null)
			where TEnum : struct
		{
			return enumObject?.Label(resourceManager);
		}
	}
}