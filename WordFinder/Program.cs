using System;
using System.Collections.Generic;

namespace WordFinder
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dictionary = new string[] { "chill", "wind", "snow", "cold"};
            var src = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            var result = new WordFinderWorker(dictionary).Find(src) as List<string>;
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
