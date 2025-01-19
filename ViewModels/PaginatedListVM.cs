﻿namespace ITstudyv4.ViewModels
{
    public class PaginatedListVM<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public int extra { get; set; }
    }
}
