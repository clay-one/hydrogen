using System.Collections.Generic;

namespace hydrogen.General.Collections
{
	public static class QueueExtensions
	{
		public static void EnqueueAll<T>(this Queue<T> queue, IEnumerable<T> elements)
		{
			if (elements == null)
				return;

			foreach (var element in elements)
			{
				queue.Enqueue(element);
			}
		}
	}
}