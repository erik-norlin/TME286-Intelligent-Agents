using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    [DataContract]
    public class WordData
    {
        private string word;
        private double idf;

        public string AsString(string formatString)
        {
            string wordDataAsString = word.PadRight(20) + " (" + idf.ToString(formatString) + ")"; 
            return wordDataAsString;
        }

        [DataMember]
        public string Word
        {
            get { return word; }
            set { word = value; }
        }

        [DataMember]
        public double IDF
        {
            get { return idf; }
            set { idf = value; }
        }
    }
}
