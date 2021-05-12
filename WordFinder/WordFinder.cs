using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinder
{
    public class WordFinder : IFinderBehaviour
    {
        public IFinderBehaviour FinderBehaviour { get; set; }
        public IEnumerable<string> Matrix { get; set; }
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
                return CleanRepeatedTopTenWords(wordsFound);
            }
            else
            {
                throw new InvalidOperationException("Words to be found should have less than 64 characters");
            } 
        }

        private IEnumerable<T> CleanRepeatedTopTenWords<T>(IEnumerable<T> input)
        {
            
            var dictionaryList = new Dictionary<T, int>();
            var cleanList = new List<T>();
            foreach (T item in input)
            {
                if (!dictionaryList.ContainsKey(item))
                {
                    dictionaryList.Add(item, 1);
                }
                else
                {
                    dictionaryList[item]++;
                }
            }

            var moreRepeatedWords = dictionaryList.OrderByDescending(x => x.Value);

            int wordsLimit = moreRepeatedWords.Count() < 10 ? moreRepeatedWords.Count() : 10;

            for (int i = 0; i < wordsLimit; i++)
            {
                cleanList.Add(moreRepeatedWords.ElementAt(i).Key);
            };

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
