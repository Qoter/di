using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagCloud.Console
{
    internal static class MatchCollectionExtension
    {
        public static IEnumerable<string> EnumerateMatches(this MatchCollection matchCollection)
        {
            for (var i = 0; i < matchCollection.Count; i++)
            {
                yield return matchCollection[i].Value;
            }
        }
    }
}
