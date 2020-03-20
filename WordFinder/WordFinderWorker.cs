using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinder
{
    public class WordFinderWorker
    {
        public IEnumerable<string> Dictionary { get; set; }
        public List<string> HorizontalListOfArray { get; set; }
        public List<string> VerticalListOfArray { get; set; }
        public List<string> WordsFound { get; set; }
        public WordFinderWorker(IEnumerable<string> dictionary)
        {
            if (dictionary.Count() <= 2048)
            {
                this.Dictionary = dictionary;
            }
            else
            {
                throw new System.InvalidOperationException("Dictionary can not exceed 2048 words.");
            }
        }


        public IList<string> Find(IEnumerable<string> source)
        {
            if (source.Count() <= 64)
            {
                //Convert source to vertical and horizontal vectors.
                MapToVectorList(source);

                //Loop looking for the word in the array string.
                MatchWordsInArrays();

                cleanRepeated();
            }
            else
            {
                throw new System.InvalidOperationException("Matrix should have less than 64 items");
            }

            return this.WordsFound;
        }

        private void MatchWordsInArrays()
        {
            this.WordsFound = new List<string>();
            foreach (var word in this.Dictionary)
            {
                foreach (var item in this.HorizontalListOfArray)
                {
                    if (item.Contains(word))
                    {
                        this.WordsFound.Add(word);
                    }
                }
                foreach (var item in this.VerticalListOfArray)
                {
                    if (item.Contains(word))
                    {
                        this.WordsFound.Add(word);
                    }
                }
            }
        }

        private void cleanRepeated()
        {
            this.WordsFound = this.WordsFound.Distinct().ToList();
        }

        private void MapToVectorList(IEnumerable<string> source)
        {
            var verticalListOfList = new List<List<string>>();
            this.HorizontalListOfArray = new List<string>();
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
                this.HorizontalListOfArray.Add(horizontalVector);
            }
            //concatenate list of charactes in list of strings
            this.VerticalListOfArray = new List<string>();
            foreach (var list in verticalListOfList)
            {
                VerticalListOfArray.Add(string.Join("", list.ToArray()));
            }
        }
    }
}
