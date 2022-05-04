using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace App.ServiceLayer.Extenstions
{
    public static class QuerySearchExtensions
    {
        public static IQueryable<T> WhereBoolPropertyEquals<T>(this IQueryable<T> source, Func<T, bool> property, bool? propertyValue)
        {
            if (!propertyValue.HasValue)
            {
                return source;
            }
            return source.Where(x => property(x) == propertyValue.Value);
        }

        public static IQueryable<T> WhereStringPropertyContains<T>(this IQueryable<T> source , Func<T, string> property, string propertyValue)
        {
            if (string.IsNullOrEmpty(propertyValue))
            {
                return source;
            }
            return source.Where(x => property(x).ToLower().Contains(propertyValue.ToLower()));
        }

        public static IQueryable<T> WhereDatetimePropertyGreaterOrEqual<T>(this IQueryable<T> source, Func<T, DateTime> property, DateTime? propertyValue)
        {
            if (!propertyValue.HasValue)
            {
                return source;
            }
            return source.Where(x => property(x) >= propertyValue.Value);
        }

        public static IQueryable<T> WhereDateTimeDayPropertyGreaterOrEqual<T>(this IQueryable<T> source, Func<T, DateTime> property, DateTime? propertyValue)
        {
            if (!propertyValue.HasValue)
            {
                return source;
            }
            return source.Where(x => property(x).Date >= propertyValue.Value.Date);
        }

        public static IQueryable<T> WhereDatetimePropertyLessOrEqual<T>(this IQueryable<T> source, Func<T, DateTime> property, DateTime? propertyValue)
        {
            if (!propertyValue.HasValue)
            {
                return source;
            }
            return source.Where(x => property(x) <= propertyValue.Value);
        }

        public static IQueryable<T> WhereDateTimeDayPropertyLessOrEqual<T>(this IQueryable<T> source, Func<T, DateTime> property, DateTime? propertyValue)
        {
            if (!propertyValue.HasValue)
            {
                return source;
            }
            return source.Where(x => property(x).Date <= propertyValue.Value.Date);
        }

        public static IQueryable<T> WhereGuidPropertyEquals<T>(this IQueryable<T> source, Func<T, Guid> property, Guid? propertyValue)
        {
            if (!propertyValue.HasValue)
            {
                return source;
            }
            return source.Where(x => property(x) == propertyValue.Value);
        }

        public static IQueryable<T> WhereIntPropertyEquals<T>(this IQueryable<T> source, Func<T, int> property, int? propertyValue)
        {
            if (!propertyValue.HasValue)
            {
                return source;
            }
            return source.Where(x => property(x) == propertyValue.Value);
        }

        public static IQueryable<T> WhereCollectionContainsPropertyValue<T, TCollectionType>(this IQueryable<T> source, Func<T, TCollectionType> property, IEnumerable<TCollectionType> collection)
        {
            if (!collection.Any())
            {
                return source;
            }
            return source.Where(x => collection.Contains(property(x)));
        }
    }
}
