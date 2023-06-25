using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            Console.WriteLine(Text);

            Text.ToLower();
            Text = Text.Trim(' ');
            char[] splitChars = new Char[] { ':', ';', '!', '\"', '&', '?', '(', ')', '[', ']', '{', '}', '_', ' ' };
            string[] tokenTempList = Text.Split(splitChars); //, StringSplitOptions.RemoveEmptyEntries);

            // Checking if last token is an abbreviation or number with decimals
            String abbreviationPattern = @"\.\w+\.";
            String decimalPattern = @"\.\d+";
            int lastIndex = tokenTempList.Length;
            string lastToken = tokenTempList[lastIndex];
            bool lastTokenIsAbbreviation = Regex.IsMatch(lastToken, abbreviationPattern);
            bool lastTokenIsDecimalNumber = Regex.IsMatch(lastToken, decimalPattern);

            if (!lastTokenIsAbbreviation && !lastTokenIsDecimalNumber)
            {
                tokenTempList[lastIndex] = lastToken.Trim('.');
            }

            // Checking if each token ends with comma, if not, it could be a decimal number
            foreach (String tokenTemp in tokenTempList)
            {
                string token = tokenTemp.Trim(splitChars);
                string endWithCommaPattern = @",$";
                bool endWithComma = Regex.IsMatch(token, endWithCommaPattern);

                if (!endWithComma)
                {
                    token = token.Trim(',');
                }

                tokenList.Add(token);
                //Console.WriteLine(tokenTemp);
                //Console.WriteLine(token);
            }
            tokenList.RemoveAll(emptyString => emptyString == "");
            foreach (String a in tokenList)
            {
                Console.WriteLine(a);
            }
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
