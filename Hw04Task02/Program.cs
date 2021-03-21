/*
 * 2. Дана коллекция List<T>, требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
 * а) для целых чисел;
 * в) **используя Linq. 
 * 
 * 3.  Дан фрагмент программы, cвернуть обращение к OrderBy с использованием лямбда-выражения =>. * 
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Hw04Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            //задание 2а
            List<int> ints = new() { 1, 2, 3, 4, 5, 4, 4, 4, 3, 3, 2, 2, 2, 1, 1, 1 };
            var freqInts = GetFrequency(ints);
            foreach (var entry in freqInts)
                Console.WriteLine($"[{entry.Key}] : {entry.Value}");

            //задание 2в через обобщенный метод-расширение
            List<string> strings = new() { "apple", "lemon", "pineapple", "melon", "melon", "melon", "pineapple", "lemon" };
            foreach (var entry in strings.GetFrequency())
                Console.WriteLine($"[{entry.Key}] : {entry.Value}");


            // задание 3

            Console.WriteLine($"{Environment.NewLine} OrderBy example");
            Dictionary<string, int> dict = new()
            {
                { "four", 4 },
                { "two", 2 },
                { "one", 1 },
                { "three", 3 }
            };

            // var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; })
            var d = dict.OrderBy(p => p.Value);

            foreach (var pair in d)
                Console.WriteLine("{0} - {1}", pair.Key, pair.Value);
        }

        static Dictionary<T, int> GetFrequency<T>(List<T> list) where T : IEquatable<T>
        {
            Dictionary<T, int> dict = new();
            foreach (T entry in list)
                if (dict.TryAdd(entry, 1) is false)
                    dict[entry]++;
            return dict;
        }
    }
}
