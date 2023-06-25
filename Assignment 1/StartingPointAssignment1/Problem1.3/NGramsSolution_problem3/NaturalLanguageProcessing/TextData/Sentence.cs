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

        // Tokenizing one sentence at a time
        public void Tokenize()
        {
            text = text.ToLower();
            text = text.Trim(' ');
            char[] splitChars = new Char[] { ':', ';', '!', '\"', '/', '&', '?', '(', ')', '[', ']', '{', '}', '_', ' ', '-' };
            string[] tokenTempList = text.Split(splitChars);

            // Checking if last token is an abbreviation or number with decimals
            String abbreviationPattern = @"\.\w+\.";
            String decimalPattern = @"\.\d+";
            int lastIndex = tokenTempList.Length - 1;
            string lastToken = tokenTempList[lastIndex];
            bool lastTokenIsAbbreviation = Regex.IsMatch(lastToken, abbreviationPattern);
            bool lastTokenIsDecimalNumber = Regex.IsMatch(lastToken, decimalPattern);

            if (!lastTokenIsAbbreviation && !lastTokenIsDecimalNumber)
            {
                tokenTempList[lastIndex] = lastToken.Trim('.');
            }

            // Checking if each token ends with comma, if not, it could be a number
            string appostrophesPattern = @"^'.'$";
            string endWithCommaPattern = @",$";

            foreach (String tokenTemp in tokenTempList)
            {
                string token = tokenTemp.Trim(splitChars);
                bool endWithComma = Regex.IsMatch(token, endWithCommaPattern);

                if (endWithComma)
                {
                    token = token.Trim(',');
                }

                bool removeAppostrophes = Regex.IsMatch(token, appostrophesPattern);
                if (removeAppostrophes)
                {
                    token = token.Trim('\'');
                }

                tokenList.Add(token);
            }
            tokenList.RemoveAll(emptyString => emptyString == "");
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
