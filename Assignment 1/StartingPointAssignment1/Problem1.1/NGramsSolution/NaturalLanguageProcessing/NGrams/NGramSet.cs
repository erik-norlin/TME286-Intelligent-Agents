using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.NGrams
{
    public class NGramSet
    {
        private List<NGram> itemList;
        private NGramComparer comparer;

        public NGramSet()
        {
            itemList = new List<NGram>();
            comparer = new NGramComparer();
        }

        public void Append(List<string> tokenList)
        {
            const int INDEX_FOUND = 0;

            NGram nGram = new NGram(tokenList); // Creates 1 n-gram

            // Option 1: Just add 2-grams to the itemList. Then, in the end, sort them based
            // on the tokenString, then count (i.e. as in Method1 in the Dictionary.Build() method). 
            // I have not tried this method here, but it should be fine,
            // perhaps even faster than Option 1.

            // Option 2: Use binary search (on the tokenString of the nGram)
            // to find its index. If the index is negative, then this nGram is not
            // yet in the list. If so, insert it *in the appropriate location* to
            // keep the nGramList sorted (based on the tokenString). Try to figure
            // out how to do that (e.g. on StackOverflow) - if you cannot, then ask 
            // the examiner or the assistant.
            // Only a few lines of code are needed for this method... :)
            // However, it will take quite a while to run -- the binary search
            // becomes increasingly slower as the list grows.

            // Option 2:
            NGramComparer comparer = new NGramComparer();
            int nGramIndex = ItemList.BinarySearch(nGram, comparer);
            if (nGramIndex >= INDEX_FOUND)
            {
                itemList[nGramIndex].NumberOfInstances++;
            }
            else
            {
                int insertNGRamIndex = ~nGramIndex;
                itemList.Insert(insertNGRamIndex, nGram);
            }
        }

        public void SortOnFrequency()
        {
            itemList = itemList.OrderByDescending(n => n.NumberOfInstances).ToList();
        }

        public List<NGram> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
    }
}
