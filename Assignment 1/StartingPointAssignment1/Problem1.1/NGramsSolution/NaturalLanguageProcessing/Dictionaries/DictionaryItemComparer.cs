using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.Dictionaries
{
    public class DictionaryItemComparer : IComparer<DictionaryItem>
    {
        public int Compare(DictionaryItem item1, DictionaryItem item2)
        {
            return item1.Token.CompareTo(item2.Token);
        }
    }
}

