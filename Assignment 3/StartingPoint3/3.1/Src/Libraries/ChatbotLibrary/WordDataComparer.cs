using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    public class WordDataComparer : IComparer<WordData>
    {
        public int Compare(WordData item1, WordData item2)
        {
            return item1.Word.CompareTo(item2.Word);
        }
    }
}
