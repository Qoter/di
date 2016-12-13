using System;
using System.Collections.Generic;

namespace TagCloud.Core.Infrastructure
{
    public static class EnumerableExtension
    {
        public static T MinBy<T, T2>(this IEnumerable<T> source, Func<T, T2> getKey) where T2 : IComparable<T2>
        {
            using (var enumerator = source.GetEnumerator())
            {
                enumerator.MoveNext();
                var currentMin = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    if (getKey(currentMin).CompareTo(getKey(enumerator.Current)) > 0)
                    {
                        currentMin = enumerator.Current;
                    }
                }
                return currentMin;
            }
        }
    }
}
