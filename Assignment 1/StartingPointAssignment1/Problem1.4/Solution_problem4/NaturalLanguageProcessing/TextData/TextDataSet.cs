using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaturalLanguageProcessing.Dictionaries;

namespace NaturalLanguageProcessing.TextData
{
    public class TextDataSet
    {
        private List<Sentence> sentenceList;
        private Dictionary dictionary;

        public TextDataSet()
        {
            sentenceList = new List<Sentence>();
        }

        public void Tokenize()
        {
            foreach (Sentence sentence in sentenceList)
            { 
                sentence.Tokenize();
            }
        }

        // Note: On a data set of the size used here, it takes a while to run
        // (a few minutes, in Release mode, more in Debug mode).
        public void MakeDictionaryAndIndex()
        {
            dictionary = new Dictionary();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            dictionary.Build(this); 

            stopWatch.Stop();
            TimeSpan timeTaken = stopWatch.Elapsed;
            string time = "Creating dictionary: " + timeTaken.ToString(@"m\:ss\.fff");
            Console.WriteLine(time);

            // Run through all tokens in all sentences, and find the corresponding
            // index (in the dictionary) of each token, and then add it to the
            // tokenIndexList (of the sentence in question). Note that, for
            // *this* problem, we don't really need the tokenIndexList (in the
            // sentences), but it's a good exercise to learn how to make it.

            // Assigning indices to the sequential tokens in the data set based on the dicitonaries sorted alphabetically
            DictionaryItemComparer comparer = new DictionaryItemComparer();
            dictionary.ItemList.Sort(comparer);

            foreach (Sentence sentence in sentenceList)
            {
                foreach (string token in sentence.TokenList)
                {
                    DictionaryItem tokenDictionaryItem = new DictionaryItem(token);
                    int tokenIndex = dictionary.ItemList.BinarySearch(tokenDictionaryItem, comparer);
                    sentence.TokenIndexList.Add(tokenIndex);
                }
            }
        }

        public List<Sentence> SentenceList
        {
            get { return sentenceList; }
        }

        public Dictionary Dictionary
        {
            get { return dictionary; }
        }
    }
}
