using System.Collections.Generic;

namespace hydrogen.General.Collections
{
    public static class CollectionExtensions
    {
        public static void AddAll<T>(this ICollection<T> target, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                target.Add(item);
            }
        }

        public static void AddAll<T>(this ISet<T> target, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                target.Add(item);
            }
        }

	    public static void AddIfNotContains<T>(this ICollection<T> target, T item)
	    {
		    if (!target.Contains(item))
				target.Add(item);
	    }

	    public static void SetItemExistance<T>(this ICollection<T> target, T item, bool existance)
	    {
		    if (existance && !target.Contains(item))
			    target.Add(item);
		    else
			    target.Remove(item);
	    }
    }
}