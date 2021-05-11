using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinder
{
    public class WordFinder
    {
        public IFinderBehaviour FinderBehaviour { get; set; }
        public IEnumerable<string> Matrix { get; private set; }
        public List<string> HorizontalListOfArray { get; private set; }
        public List<string> VerticalListOfArray { get; private set; }
        public List<string> WordsFound { get; private set; }
        public WordFinder(IEnumerable<string> matrix)
        {
            if (ValidateWordStream(matrix))
            {
                Matrix = matrix;
            }
            else
            {
                throw new InvalidOperationException("Matrix can not exceed 64 values");
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            if (ValidateWordStream(wordstream))
            {
                FinderBehaviour.Matrix = Matrix;
                var wordsFound = FinderBehaviour.Find(wordstream);
                return CleanRepeatedWords(wordsFound);
            }
            else
            {
                throw new InvalidOperationException("Words to be found should have less than 64 characters");
            } 
        }

        private IEnumerable<T> CleanRepeatedWords<T>(IEnumerable<T> input)
        {
            var cleanList = new List<T>();
            foreach (T item in input)
            {
                if (!cleanList.Contains(item))
                {
                    cleanList.Add(item);
                }
            }
            return cleanList;
        }

        private static bool ValidateWordStream(IEnumerable<string> wordstream)
        {
            foreach (var word in wordstream)
            {
                if (word.Count() > 64)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
