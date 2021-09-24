using App.ServiceLayer.Models;
using System.Linq;
using System.Threading.Tasks;

namespace App.ServiceLayer.Extenstions
{
    public static class PaginatedListExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);
    }
}
