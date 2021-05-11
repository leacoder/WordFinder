using System.Collections.Generic;
using System.Linq;

namespace WordFinder
{
    public class HorizontalFinder : IFinderBehaviour
    {
        public List<string> HorizontalListOfArray { get; private set; }
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
            }
            return wordsFound;
        }

        private void MapToVectorList()
        {
            HorizontalListOfArray = new List<string>();
            foreach (string horizontalVector in Matrix)
            {
                HorizontalListOfArray.Add(horizontalVector);
            }
        }
    }
}
