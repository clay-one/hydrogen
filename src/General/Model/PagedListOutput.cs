using System.Collections.Generic;

namespace Hydrogen.General.Model
{
    public class PagedListOutput<T>
    {
        public List<T> PageItems { get; set; }
        public int PageNumber { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int TotalNumberOfItems { get; set; }
    }
}