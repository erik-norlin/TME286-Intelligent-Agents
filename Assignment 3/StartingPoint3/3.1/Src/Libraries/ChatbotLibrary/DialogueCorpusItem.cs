using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    [DataContract]
    public class DialogueCorpusItem
    {
        // This class stores a sentence pair as well as the (normalized) TF-IDF embedding for the query sentence (S_1)

        private string query; // = S_1 in the assignment (used for computing cosine similarity)
        private string response; // = S_2 in the assignment
        private List<double> tfIdfVector; 
        private List<int> tfIdfIndex;
        private List<int> queryTokenIndexList;
        private List<int> responseTokenIndexList;

        public string AsString()
        {
            string itemAsString = query + " \t " + response;
            return itemAsString;
        }

        public DialogueCorpusItem(string query, string response)
        {
            this.query = query;
            this.response = response;
        }

        public void ComputeTFIDFVector(Vocabulary vocabulary)
        {
            tfIdfVector = new List<double>();
            tfIdfIndex = new List<int>();

            var groupedTokenIndices = queryTokenIndexList.GroupBy(i => i);

            // Only storing the values in the TF-IDF vector that aren't 0 to avoid storing too many zeros (sparse array)
            foreach (var groupedTokenIndex in groupedTokenIndices)
            {
                int tokenIndex = groupedTokenIndex.Key;
                double tf = (double)groupedTokenIndex.Count();
                double idf = vocabulary.ItemList[tokenIndex].IDF;
                double tfidf = tf * idf;
                tfIdfVector.Add(tfidf);
                tfIdfIndex.Add(tokenIndex);
            }

            // Normalising vector to unit length
            double norm = 0;
            foreach (double tfidf in tfIdfVector)
            {
                norm += Math.Pow(tfidf, 2);
            }

            for (int i = 0; i < tfIdfVector.Count; i++) 
            {
                tfIdfVector[i] = tfIdfVector[i] / Math.Sqrt(norm);
            }
        }

        [DataMember]
        public List<double> TFIDFVector
        {
            get { return tfIdfVector; }
            set { tfIdfVector = value; }
        }

        [DataMember]
        public List<int> TFIDFIndex
        {
            get { return tfIdfIndex; }
            set { tfIdfIndex = value; }
        }

        [DataMember]
        public string Query
        {
            get { return query; }
            set { query = value; }
        }

        [DataMember]
        public string Response
        {
            get { return response; }
            set { response = value; }
        }

        public List<int> QueryTokenIndexList
        {
            get { return queryTokenIndexList; }
            set { queryTokenIndexList = value; }
        }

        public List<int> ResponseTokenIndexList
        {
            get { return responseTokenIndexList; }
            set { responseTokenIndexList = value; }
        }
    }
}
