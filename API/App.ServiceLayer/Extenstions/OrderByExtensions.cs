using System;
using System.Linq;
using System.Linq.Expressions;

namespace App.ServiceLayer.Extenstions
{
    public static class OrderByExtensions
    {
        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, string orderByDirection)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return source;
            }

            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), GetSortOrder(orderByDirection), new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        private static string GetSortOrder(string orderByDirection) => string.IsNullOrEmpty(orderByDirection) || orderByDirection.Equals("asc") ? "OrderBy" : "OrderByDescending";
    }
}
