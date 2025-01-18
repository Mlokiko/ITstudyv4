namespace ITstudyv4.ViewModels
{
    public class PaginatedThreadsVM<T>
    {
        public IEnumerable<T> Threads { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalThreads { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalThreads / PageSize);

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
