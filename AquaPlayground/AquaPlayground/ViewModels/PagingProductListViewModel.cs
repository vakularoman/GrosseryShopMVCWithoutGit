namespace AquaPlayground.ViewModels
{
    public class PagingListViewModel<T>
    {
        public List<T> Elements { get; set; }

        public int CurrentPageNumber { get; set; }

        public int RecordsPerPageCount { get; set; }

        public int TotalPagesCount { get; set; }
    }
}
