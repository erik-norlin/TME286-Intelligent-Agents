using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    public class TextPreprocessing
    {

        public TextPreprocessing()
        {
        }

        public static string Clean(string rawData)
        {
            rawData = rawData.ToLower().Trim();
            char[] replaceChars = new Char[] { '.', ',', ':', ';', '!', '?', '\"', '/', '&', '(', ')', '[', ']', '{', '}', '<', '>', '_', '-', '=', '*', '\t', '\n', '\r' };
            foreach (char replaceChar in replaceChars)
            {
                rawData = rawData.Replace(replaceChar, ' ');
            }
            string cleanData = rawData;
            return cleanData;
        }

        public static List<string> Tokenize(string cleanData)
        {
            List<string> tokenList = new List<string>();
            Char[] splitList = new char[] { ' ' };
            string[] tokenTempList = cleanData.Split(splitList, StringSplitOptions.RemoveEmptyEntries);
            string appostrophePattern = @"('.*')|(^')";

            foreach (String tokenTemp in tokenTempList)
            {
                string token = tokenTemp.Trim();

                // Some appostrohes could be contractions, others not
                bool removeAppostrophe = Regex.IsMatch(token, appostrophePattern);
                if (removeAppostrophe)
                {
                    token = token.Trim('\'').Trim();
                }
                tokenList.Add(token);
            }

            tokenList.RemoveAll(emptyString => emptyString == "");
            return tokenList;
        }
    }
}
