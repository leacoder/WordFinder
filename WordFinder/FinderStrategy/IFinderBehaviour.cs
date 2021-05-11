using System;
using System.Collections.Generic;
using System.Text;

namespace WordFinder
{
    public interface IFinderBehaviour
    {
        IEnumerable<string> Matrix { get; set; }
        IEnumerable<string> Find(IEnumerable<string> wordstream);
    }
}
