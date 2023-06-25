using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    public class CosineSimilarityComparer : IComparer<CosineSimilarityItem>
    {
        public int Compare(CosineSimilarityItem item1, CosineSimilarityItem item2)
        {
            return item2.CosineSimilarity.CompareTo(item1.CosineSimilarity);
        }
    }
}
