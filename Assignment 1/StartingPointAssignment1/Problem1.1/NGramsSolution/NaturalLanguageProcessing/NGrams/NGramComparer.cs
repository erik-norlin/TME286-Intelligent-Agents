using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.NGrams
{
    public class NGramComparer : IComparer<NGram>
    {
        public int Compare(NGram item1, NGram item2)
        {
            return item1.TokenString.CompareTo(item2.TokenString);
        }
    }
}
