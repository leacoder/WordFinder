using System;

namespace WordFinder
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dictionary = new string[] { "chill", "wind", "snow", "cold"};
            var src = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            var result = new WordFinderWorker(dictionary).Find(src);
            Console.WriteLine(result);
        }


        
    }
}
