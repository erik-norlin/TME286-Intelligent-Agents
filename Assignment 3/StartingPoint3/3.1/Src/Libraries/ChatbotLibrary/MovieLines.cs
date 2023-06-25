using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    // This class stores all movie line items before they are paired
    public class MovieLines
    {
        private List<MovieLineItem> itemList;

        public MovieLines()
        {
            itemList = new List<MovieLineItem>();
        }

        public List<MovieLineItem> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
    }
}
