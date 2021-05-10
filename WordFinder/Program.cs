using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinder
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to 'Word Finder'\n\n");
            Console.WriteLine("This a little example. \nJust take a minute to find some words\n\n");
            var wordsToFind = new string[] { "chill", "wind", "snow", "cold"};
            var matrixOfChars = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            matrixOfChars.ToList().ForEach(i => Console.WriteLine("         "+i.ToString()));
            Console.WriteLine("\nPress a key to know the results");
            Console.ReadLine();

            var result = new WordFinderWorker(wordsToFind).Find(matrixOfChars);
            foreach (var item in result)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
