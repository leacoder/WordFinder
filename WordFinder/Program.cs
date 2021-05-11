using System;
using System.Collections.Generic;
using System.Linq;
using WordFinder.FinderStrategy;

namespace WordFinder
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to 'Word Finder'\n\n");
            Console.WriteLine("This a little example. \nJust take a minute to find some words\n\n");
            var wordStream = new string[] { "chill", "wind", "snow", "cold" };
            var matrix = new string[] { "abcdc", "fgwio", "chill", "pqnsd", "uvdxy" };
            PrintArray(matrix);
            Console.WriteLine("\nPress enter to know the results\n");
            Console.ReadLine();

            var wordsFinder = new WordFinder(matrix);

            wordsFinder.FinderBehaviour = new HorizontalVerticalFinder();
            var wordsFinded = wordsFinder.Find(wordStream);
            Console.WriteLine("This are the Vertical and Horizontal words found:");
            PrintWordsFinded(wordsFinded);

            Console.WriteLine("This are the Vertical words found:");
            wordsFinder.FinderBehaviour = new VerticalFinder();
            wordsFinded = wordsFinder.Find(wordStream);
            PrintWordsFinded(wordsFinded);

            wordsFinder.FinderBehaviour = new HorizontalFinder();
            wordsFinded = wordsFinder.Find(wordStream);
            Console.WriteLine("This are the Horizontal words found:");
            PrintWordsFinded(wordsFinded);
        }

        private static void PrintArray(string[] wordArray)
        {
            foreach (var word in wordArray)
            {
                Console.WriteLine("         " + word.ToString());
            }
        }

        private static void PrintWordsFinded(IEnumerable<string> wordsFinded)
        {
            foreach (var word in wordsFinded)
            {
                Console.WriteLine(word.ToString());
            }
            Console.WriteLine("\n");
        }
    }
}
