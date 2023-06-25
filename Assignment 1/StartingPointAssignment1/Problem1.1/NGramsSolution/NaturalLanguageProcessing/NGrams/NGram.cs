using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.NGrams
{
    public class NGram
    {
        private const int INSTANCE_FORMAT_WIDTH = 8;

        private List<string> tokenList;
        private string tokenString; // Concatenation (with spaces) of the tokens - can be used in binary sorting ...
        private int numberOfInstances;

        // Generates a new NGram
        // The tokenString is the concatenation of the individual tokens, with " " in between.
        // For example, the 3-gram generated from the following list of tokens {"the","dog","ate"}
        // would have a tokenstring of the form "the dog ate", and so on.
        // The tokenString is used in binary search; see also the NGramSet class.
        public NGram(List<string> tokenList)
        {
            this.tokenList = new List<string>();
            tokenString = "";
            foreach (string token in tokenList)
            {
                this.tokenList.Add(token);
                tokenString += token + " ";
            }
            tokenString = tokenString.TrimEnd(' ');
            numberOfInstances = 1;
        }

        public string AsString()
        {
            string nGramAsString = numberOfInstances.ToString().PadLeft(INSTANCE_FORMAT_WIDTH) + " " + tokenString; 
            return nGramAsString;
        }

        public List<string> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }

        public string TokenString
        {
            get { return tokenString; }
            set { tokenString = value; }
        }

        public int NumberOfInstances
        {
            get { return numberOfInstances; }
            set { numberOfInstances = value; }
        }
    }
}
