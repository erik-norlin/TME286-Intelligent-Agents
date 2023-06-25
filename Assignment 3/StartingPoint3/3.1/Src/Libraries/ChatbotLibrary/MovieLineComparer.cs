using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{   
    public class MoveLineComparer : IComparer<MovieLineItem>
    {
        public int Compare(MovieLineItem item1, MovieLineItem item2)
        {
            return item1.MovieLine.CompareTo(item2.MovieLine);
        }
    }
}
