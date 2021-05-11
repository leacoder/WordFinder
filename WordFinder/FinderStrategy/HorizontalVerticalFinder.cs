using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFinder.FinderStrategy
{
    public class HorizontalVerticalFinder : IFinderBehaviour
    {
        public List<string> HorizontalListOfArray { get; private set; }
        public List<string> VerticalListOfArray { get; private set; }
        public IEnumerable<string> Matrix { get; set; }
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            //Convert source to vertical and horizontal vectors.
            MapToVectorList();

            //Loop looking for the word in the array string.
            return MatchWordsInArrays(wordstream);
        }

        private List<string> MatchWordsInArrays(IEnumerable<string> wordStream)
        {
            var wordsFound = new List<string>();
            foreach (var word in wordStream)
            {
                foreach (var item in HorizontalListOfArray)
                {
                    if (item.Contains(word))
                    {
                        wordsFound.Add(word);
                    }
                }
                foreach (var item in VerticalListOfArray)
                {
                    if (item.Contains(word))
                    {
                        wordsFound.Add(word);
                    }
                }
            }
            return wordsFound;
        }

        private void MapToVectorList()
        {
            var verticalListOfList = new List<List<string>>();
            HorizontalListOfArray = new List<string>();
            var count = 0;
            var firstRound = true;
            foreach (string horizontalVector in Matrix)
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
