using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hydrogen.General.Collections
{
	public class PagedList<T> : IEnumerable<T>
	{
		private PaginationStats _paginationStats;

		public PaginationStats Stats => _paginationStats;

	    public int PageSize => _paginationStats.PageSize;

	    public int PageNumber => _paginationStats.PageNumber;

	    public int TotalNumberOfPages => _paginationStats.TotalNumberOfPages;

	    public int TotalNumberOfItems => _paginationStats.TotalNumberOfItems;

	    public int FirstItemIndex => _paginationStats.FirstItemIndex;

	    public int LastItemIndex => _paginationStats.FirstItemIndex + (PageItems?.Count ?? 0) - 1;

	    public List<T> PageItems { get; set; }

		#region Initialization

		[Obsolete("Use static builder methods instead")]
		public PagedList(int totalNumberOfItems, int pageSize, int pageNumber)
		{
			_paginationStats = PaginationStats.FromPageNumber(totalNumberOfItems, pageSize, pageNumber);
		}

		[Obsolete("Use static builder methods instead")]
		public PagedList(List<T> allItems)
		{
			PageItems = allItems;
			_paginationStats = PaginationStats.SinglePage(allItems.Count);
		}

		private PagedList()
		{
		}

		public static PagedList<T> BuildFromCompleteList(List<T> allItems)
		{
		    var result = new PagedList<T>
		    {
		        PageItems = allItems,
		        _paginationStats = PaginationStats.SinglePage(allItems.Count)
		    };

		    return result;
		}

		public static PagedList<T> BuildUsingPageNumber(int totalNumberOfItems, int pageSize, int pageNumber)
		{
		    var result = new PagedList<T>
		    {
		        _paginationStats = PaginationStats.FromPageNumber(totalNumberOfItems, pageSize, pageNumber)
		    };

		    return result;
		}

		public static PagedList<T> BuildUsingStartIndex(int totalNumberOfItems, int pageSize, int startIndex)
		{
		    var result = new PagedList<T>
		    {
		        _paginationStats = PaginationStats.FromStartIndex(totalNumberOfItems, pageSize, startIndex)
		    };

		    return result;
		}

	    public static PagedList<T> BuildUsingPagedList<TSource>(PagedList<TSource> source)
	    {
	        var result = new PagedList<T>
	        {
	            _paginationStats = PaginationStats.FromPaginationStats(source._paginationStats)
	        };

	        return result;
	    }

	    public static PagedList<T> BuildUsingPagedList<TSource>(PagedList<TSource> source, Func<TSource, T> mapping)
	    {
	        var result = BuildUsingPagedList(source);
	        result.PageItems = source.PageItems.Select(mapping).ToList();

	        return result;
	    }

		#endregion

		#region Implementation of IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			if (PageItems != null)
				return PageItems.GetEnumerator();

			throw new InvalidOperationException("List does not have a value.");
		}

		#endregion

		#region Implementation of IEnumerable<out T>

		public IEnumerator<T> GetEnumerator()
		{
			if (PageItems != null)
				return PageItems.GetEnumerator();

			throw new InvalidOperationException("List does not have a value.");
		}

		#endregion

		#region Public methods

		public void FillFrom(IQueryable<T> query)
		{
			PageItems = query.Skip(FirstItemIndex - 1).Take(PageSize).ToList();
		}

		public void FillFrom(IEnumerable<T> items)
		{
			PageItems = items.Skip(FirstItemIndex - 1).Take(PageSize).ToList();
		}

        public void FillFromNoSkip(IEnumerable<T> items)
		{
			PageItems = items.Take(PageSize).ToList();
		}

		#endregion
	}
}