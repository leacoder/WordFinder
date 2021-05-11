using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinder
{
    public class WordFinderWorker : IWordFinder
    {
        public IEnumerable<string> Dictionary { get; private set; }
        public List<string> HorizontalListOfArray { get; private set; }
        public List<string> VerticalListOfArray { get; private set; }
        public List<string> WordsFound { get; private set; }
        public WordFinderWorker(IEnumerable<string> dictionary)
        {
            if (dictionary.Count() <= 2048)
            {
                Dictionary = dictionary;
            }
            else
            {
                throw new InvalidOperationException("Dictionary can not exceed 2048 words.");
            }
        }


        public IEnumerable<string> Find(IEnumerable<string> source)
        {
            if (source.Count() <= 64)
            {
                //Convert source to vertical and horizontal vectors.
                MapToVectorList(source);

                //Loop looking for the word in the array string.
                MatchWordsInArrays();

                CleanRepeated();
            }
            else
            {
                throw new InvalidOperationException("Matrix should have less than 64 items");
            }

            return WordsFound;
        }

        private void MatchWordsInArrays()
        {
            WordsFound = new List<string>();
            foreach (var word in Dictionary)
            {
                foreach (var item in HorizontalListOfArray)
                {
                    if (item.Contains(word))
                    {
                        WordsFound.Add(word);
                    }
                }
                foreach (var item in VerticalListOfArray)
                {
                    if (item.Contains(word))
                    {
                        WordsFound.Add(word);
                    }
                }
            }
        }

        private void CleanRepeated()
        {
            WordsFound = WordsFound.Distinct().ToList();
        }

        private void MapToVectorList(IEnumerable<string> source)
        {
            var verticalListOfList = new List<List<string>>();
            HorizontalListOfArray = new List<string>();
            var vectorList = source.ToArray();
            var count = 0;
            var firstRound = true;
            foreach (string horizontalVector in vectorList)
            {
                foreach (var item in horizontalVector)
                {
                    if (firstRound)
                    {
                        var verticalList = new List<string>();
                        verticalList.Insert(0, horizontalVector.ElementAt(count).ToString());
                        verticalListOfList.Add(verticalList);
                    }
                    else
                    {
                        var listToInsert = verticalListOfList[count];
                        listToInsert.Add(horizontalVector.ElementAt(count).ToString());
                    }
                    count++;
                }
                count = 0;
                firstRound = false;
                HorizontalListOfArray.Add(horizontalVector);
            }
            //concatenate list of charactes in list of strings
            VerticalListOfArray = new List<string>();
            foreach (var list in verticalListOfList)
            {
                VerticalListOfArray.Add(string.Join("", list.ToArray()));
            }
        }
    }
}
