using System;
using System.Linq.Expressions;
using AutoMapper;

namespace Hydrogen.AutoMapper
{
    public static class AutoMapperConfigurationExtensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreAll<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;
        }

        public static IMappingExpression<TSource, TDestination> IgnoreUnmappedProperties<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var typeMap = Mapper.Configuration.FindTypeMapFor<TSource, TDestination>();
            if (typeMap != null)
            {
                foreach (var unmappedPropertyName in typeMap.GetUnmappedPropertyNames())
                {
                    expression.ForMember(unmappedPropertyName, opt => opt.Ignore());
                }
            }

            return expression;
        }

        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression, Expression<Func<TDestination, object>> destinationMember)
        {
            return expression.ForMember(destinationMember, opt => opt.Ignore());
        }

        public static IMappingExpression<TSource, TDestination> IgnoreSource<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression, Expression<Func<TSource, object>> sourceMember)
        {
            return expression.ForSourceMember(sourceMember, opt => opt.Ignore());
        }

    }
}