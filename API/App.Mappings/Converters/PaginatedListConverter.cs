using App.ServiceLayer.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace App.Mappings.Converters
{
    public class PaginatedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<TSource>, PaginatedList<TDestination>> where TSource : class where TDestination : class
    {
        public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
        {
            var items = context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source.Items);

            return new PaginatedList<TDestination>
            {
                PageIndex = source.PageIndex,
                TotalPages = source.TotalPages,
                TotalCount = source.TotalCount,
                Items = items.ToList()
            };
        }
    }
}
