using System.Collections.Generic;

namespace WordFinder
{
    public class VerticalFinder : IFinderBehaviour
    {
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
            var vectorList = Matrix;
            var count = 0;
            var firstRound = true;
            foreach (string horizontalVector in vectorList)
            {
                foreach (var item in horizontalVector)
                {
                    if (firstRound)
                    {
                        var verticalList = new List<string>();
                        verticalList.Insert(0, horizontalVector[count].ToString());
                        verticalListOfList.Add(verticalList);
                    }
                    else
                    {
                        var listToInsert = verticalListOfList[count];
                        listToInsert.Add(horizontalVector[count].ToString());
                    }
                    count++;
                }
                count = 0;
                firstRound = false;
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
