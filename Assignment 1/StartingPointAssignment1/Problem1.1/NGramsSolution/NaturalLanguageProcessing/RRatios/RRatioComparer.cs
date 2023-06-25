using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.RRatios
{
    // Compares two items based on their r-ratios.
    public class RRatioComparer : IComparer<RRatio>
    {
        public int Compare(RRatio item1, RRatio item2)
        {
            return item1.Ratio.CompareTo(item2.Ratio);
        }
    }
}
