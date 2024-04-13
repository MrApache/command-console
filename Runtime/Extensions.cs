using System.Collections.Generic;

namespace RB.Console
{
    public static class Extensions
    {
        public static void AddRange<T>(this HashSet<T> destination, IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                destination.Add(item);
            }
        }
    }
}