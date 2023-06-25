using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.TextData
{
    public class TextDataSet
    {
        private List<Sentence> sentenceList;

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

        public List<Sentence> SentenceList
        {
            get { return sentenceList; }
        }
    }
}
