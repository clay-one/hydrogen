using System.Collections.Generic;

namespace hydrogen.General.Collections
{
	public static class StackExtensions
	{
		public static void PushAll<T>(this Stack<T> stack, IEnumerable<T> elements)
		{
			if (elements == null)
				return;

			foreach (var element in elements)
			{
				stack.Push(element);
			}
		}
	}
}