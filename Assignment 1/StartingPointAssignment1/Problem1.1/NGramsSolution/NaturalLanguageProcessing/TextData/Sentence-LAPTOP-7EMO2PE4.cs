using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.TextData
{
    public class Sentence
    {
        private string text;
        private List<string> tokenList;
        private List<int> tokenIndexList;

        public Sentence()
        {
            tokenList = new List<string>();
            tokenIndexList = new List<int>();   
        }

        // Write this method:
        //
        // First, make the text lower-case (ToLower()...)
        // Remember to handle (remove) end-of-sentence markers, as well as "," and
        // quotation marks (if any). Also, make sure *not* to split abbreviations and contractions.
        //
        // Spend some effort on this method: It should be more than just a few lines - there are
        // many special cases to deal with!
        public void Tokenize()
        {

            // Add code here


        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public List<string> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }

        public List<int> TokenIndexList
        {
            get { return tokenIndexList; }
            set { tokenIndexList = value; }
        }
    }
}
