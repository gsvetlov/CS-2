using System;
using System.Collections.Generic;
using System.Linq;

namespace Hw04Task02
{
    static class ListExtensionExample
    {
        internal static Dictionary<T, int> GetFrequency<T>(this List<T> list) where T : IEquatable<T> =>
            list.GroupBy(item => item, entry => entry, (item, entry) => (key: item, value: entry.Count()))
                .ToDictionary(e => e.key, e => e.value);
    }
}
