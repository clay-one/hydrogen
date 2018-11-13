using System;
using System.Resources;
using Hydrogen.General.Text;
using Hydrogen.General.Utils;

namespace Hydrogen.General.Localization
{
    public static class EnumLocalizationUtils
    {
        public static string ToLocalizedString(string fullName, ResourceManager resourceManager = null)
        {
            return resourceManager?.GetString(fullName);
        }

        public static string ToLocalizedString(string enumTypeName, string enumItemName,
            ResourceManager resourceManager = null)
        {
            if (enumItemName.IsNullOrWhitespace())
                return null;

            if (enumTypeName.IsNullOrWhitespace())
                return ToLocalizedString(enumItemName, resourceManager);

            return 
                ToLocalizedString(enumTypeName + "_" + enumItemName, resourceManager) ??
                ToLocalizedString(enumItemName, resourceManager) ??
                enumItemName;
        }

        public static string ToLocalizedString(Type enumType, object enumObject, ResourceManager resourceManager = null)
        {
            return ToLocalizedString(
                enumType.IfNotNull(t => t.Name), 
                enumObject.IfNotNull(o => o.ToString()),
                resourceManager);
        }

        public static string ToLocalizedString<TEnum>(this TEnum enumObject, ResourceManager resourceManager = null)
            where TEnum : struct
        {
            return ToLocalizedString(typeof(TEnum), enumObject, resourceManager);
        }

        public static string ToLocalizedString<TEnum>(this TEnum? enumObject, ResourceManager resourceManager = null)
            where TEnum : struct
        {
            return enumObject?.ToLocalizedString(resourceManager);
        }
    }
}