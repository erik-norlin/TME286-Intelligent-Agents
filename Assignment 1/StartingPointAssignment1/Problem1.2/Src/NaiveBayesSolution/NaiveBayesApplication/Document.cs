using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NaiveBayesApplication
{
    public class Document
    {
        private int label;
        private string rawData;
        private List<string> tokenList;
        private int inferredLabel = -1;
        private List<double> classLogProbabilityList;

        public Document()
        {
            tokenList = new List<string>();
            inferredLabel = -1;
            classLogProbabilityList = new List<double>();
        }

        public static Document Merge(List<Document> documentList)
        {
            Document mergedDocument = new Document();
            mergedDocument.Label = documentList[0].Label;
            for (int ii = 0; ii<  documentList.Count; ii++)
            {
                for (int jj = 0; jj < documentList[ii].TokenList.Count;jj++)
                {
                    mergedDocument.TokenList.Add(documentList[ii].TokenList[jj]);
                }
            }
            return mergedDocument;
        }

        public void Clean()
        {
            // Cleaning data
            rawData = rawData.ToLower();

            char[] replaceChars = new Char[] { ':', '\"', '/', '&', '(', ')', '[', ']', '{', '}', '<', '>', '_', '-', '*' };
            foreach (char replaceChar in replaceChars)
            {
                rawData = rawData.Replace(replaceChar, ' ');
            }
            // OBS: Removing appostrophes is done in tokenization method
        }

        public void Tokenize()
        {
            Char[] splitList = new char[] { ' ', ',', ';', '.', '!', '?' };
            string[] tokenTempList = rawData.Split(splitList);
            string appostrophesPattern = @"^'.'$";

            foreach (String tokenTemp in tokenTempList)
            {
                string token = tokenTemp.Trim(' ');
                bool removeAppostrophes = Regex.IsMatch(token, appostrophesPattern);

                if (removeAppostrophes)
                {
                    token = token.Trim('\'');
                }

                tokenList.Add(token);
            }
            tokenList.RemoveAll(emptyString => emptyString == "");
        }

        public void RemoveStopWords(List<string> stopWordList)
        {
            foreach (string stopWord in stopWordList)
            {
                tokenList.RemoveAll(removeStopWord => removeStopWord == stopWord);
            }
        }


        // This method returns the document as a single string (used for visualization in the MainForm)
        // If the document has not been tokenized, the method simply returns the raw data,
        // otherwise is returns the concatenated list of tokens.
        public string AsString()
        {
            if (tokenList.Count == 0)
            {
                return  label.ToString() + " " + rawData;
            }
            else
            {
                string documentAsString = label.ToString() + " ";
                foreach (string token in tokenList) { documentAsString += token + " "; };
                documentAsString = documentAsString.TrimEnd(' ');
                return documentAsString;
            }
        }
        
        public int Label
        {
            get { return label; }
            set { label = value; }
        }

        public string RawData
        {
            get { return rawData; }
            set { rawData = value; }
        }

        public int InferredLabel
        {
            get { return inferredLabel; }
            set { inferredLabel = value; }
        }

        public List<string> TokenList
        {
            get { return tokenList; }
            set { tokenList = value; }
        }

        public List<double> ClassLogProbabilityList
        {
            get { return classLogProbabilityList; }
            set { classLogProbabilityList = value; }
        }
    }
}
