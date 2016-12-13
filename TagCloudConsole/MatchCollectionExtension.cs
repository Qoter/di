using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TagCloudConsole
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
