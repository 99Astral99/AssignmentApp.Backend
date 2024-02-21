using Microsoft.EntityFrameworkCore;

namespace AssignmentApp.Application.Common.Pagination
{
    public class PaginatedList<T>
    {
        public List<T> Results { get; set; }
        public PaginationMetadata Metadata { get; set; }

        public PaginatedList(List<T> results, int count, int pageIndex, int pageSize)
        {
            Metadata = new PaginationMetadata()
            {
                PageIndex = pageIndex,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            Results = results;
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var results = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(results, count, pageIndex, pageSize);
        }
    }
}
