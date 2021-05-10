using System;
using System.Collections.Generic;
using System.Text;

namespace WordFinder
{
    public interface IWordFinder
    {
        IEnumerable<string> Find(IEnumerable<string> wordstream);
    }
}
