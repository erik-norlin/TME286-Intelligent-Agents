using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using NaturalLanguageProcessing.TextData;
using static System.Net.Mime.MediaTypeNames;

namespace NaturalLanguageProcessing.Dictionaries
{
    public class Dictionary
    {
        private List<DictionaryItem> itemList;

        public Dictionary()
        {
            itemList = new List<DictionaryItem>();  
        }

        // This method builds the dictionary (from the text data set) AND counts
        // the number of instances of each token.
        public void Build(TextDataSet dataSet)
        {

            // Creating a list of all tokens (including dublicates)
            List<string> tokenList = new List<string>();
            foreach (Sentence sentence in dataSet.SentenceList)
            {
                foreach (string token in sentence.TokenList)
                {
                    tokenList.Add(token);
                }
            }

            tokenList.Sort();
            var groupedTokenList = tokenList.GroupBy(i => i);

            // Counting occurrences of each token and assigning them to corresponding dictionary item
            foreach (var group in groupedTokenList)
            {
                DictionaryItem dictionaryItem = new DictionaryItem(group.Key);
                dictionaryItem.Count = group.Count();
                itemList.Add(dictionaryItem);
            }
            Console.WriteLine("No. dictionary items: " + dataSet.Dictionary.ItemList.Count);
        }

        public List<DictionaryItem> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
    }
}
