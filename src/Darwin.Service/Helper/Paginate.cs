namespace Darwin.Service.Helper;

public class Paginate<T> : List<T>
{
    public List<T> Items { get; set; }
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public static Paginate<T> ToPagedList(IQueryable<T> source, int currentPage, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((currentPage - 1) * pageSize)
            .Take(pageSize).ToList();
        var totalPages=(int)Math.Ceiling(count/(double)pageSize);
        return new Paginate<T> { Items = items, CurrentPage = currentPage, TotalCount = count, PageSize = pageSize, TotalPages = totalPages };
    }
}
