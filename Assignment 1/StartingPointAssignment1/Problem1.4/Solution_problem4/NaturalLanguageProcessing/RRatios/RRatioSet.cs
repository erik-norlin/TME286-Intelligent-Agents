using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.RRatios
{
    // This class contains a set of r-ratios, similar to NGramSet.
    public class RRatioSet
    {
        private List<RRatio> itemList;
        private RRatioComparer comparer;

        public RRatioSet()
        {
            itemList = new List<RRatio>();
            comparer = new RRatioComparer();
        }

        public void Append(RRatio ratioItem)
        {
              itemList.Add(ratioItem);
        }

        public void SortOnFrequencyDescending()
        {
            itemList = itemList.OrderByDescending(n => n.LogRatio).ToList();
        }

        public void SortOnFrequencyAscending()
        {
            itemList = itemList.OrderBy(n => n.LogRatio).ToList();
        }

        public List<RRatio> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
    }
}
