


namespace Common.Utilities.Paging
{
    public static class PagingExtensions
    {
        public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, BasePaging pager)
        {
            return queryable.Skip(pager.SkipEntity).Take(pager.TakeEntity);
        }
        public static IEnumerable<T> Paging<T>(this IEnumerable<T> enumerable, BasePaging pager)
        {
            return enumerable.Skip(pager.SkipEntity).Take(pager.TakeEntity);
        }
    }
}
