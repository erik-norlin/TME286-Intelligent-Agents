using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    // This class stores the cosine similarity between the user input and a dialouge corpus query. It also stores the the corpus index.
    public class CosineSimilarityItem
    {
        private double cosineSimilarity;
        private int corpusIndex;

        public double CosineSimilarity
        {
            get { return cosineSimilarity; }
            set { cosineSimilarity = value; }
        }

        public int CorpusIndex
        {
            get { return corpusIndex; }
            set { corpusIndex = value; }
        }
    }
}
