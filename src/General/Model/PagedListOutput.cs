using System.Collections.Generic;

namespace hydrogen.General.Model
{
    public class PagedListOutput<T>
    {
        public List<T> PageItems { get; set; }
        public int PageNumber { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int TotalNumberOfItems { get; set; }
    }
}