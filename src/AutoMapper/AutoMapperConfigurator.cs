using System;
using System.Linq;
using System.Reflection;
using hydrogen.General.Collections;

namespace hydrogen.AutoMapper
{
    public static class AutoMapperConfigurator
    {
        public static void Scan(Assembly assembly)
        {
             assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(AutoMapperConfigAttribute)).Any())
                .OrderBy(GetDistanceFromObject)
                .ForEach(type =>
                {
                    var method = type.GetMethod("ConfigureAutoMapper", new Type[0]);
                    if (method == null || !method.IsStatic || method.ReturnType != typeof(void) || method.GetParameters().Any())
                        throw new InvalidOperationException(
                            "Type " + type.FullName + " is decorated with [AutoMapperConfigAttribute] but does not contain a public static method with the signature of 'void ConfigureAutoMapper()'");

                    method.Invoke(null, null); 
                });
        }

        private static int GetDistanceFromObject(Type t)
        {
            var result = 0;
            while (!(typeof(object) == t))
            {
                if (t == null)
                    return -1;

                result++;
                t = t.BaseType;
            }

            return result;
        }
    }
}