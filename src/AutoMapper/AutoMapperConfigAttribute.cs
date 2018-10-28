using System;

namespace hydrogen.AutoMapper
{
    /// <summary>
    ///		Specifies that a type has AutoMapper configuration
    /// </summary>
    /// <remarks>
    ///		Any type that is decorated by this attribute, should have a public, static method called "ConfigureAutoMapper"
    ///		that doesn't take any arguments, and should return void. This method will be called during AutoMapper configuration,
    ///		by AutoMapperConfigurator, prior to validation of mappings.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public class AutoMapperConfigAttribute : Attribute
    {
    }
}