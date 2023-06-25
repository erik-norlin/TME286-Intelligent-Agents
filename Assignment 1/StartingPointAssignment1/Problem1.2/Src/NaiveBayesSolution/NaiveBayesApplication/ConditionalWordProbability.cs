using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesApplication
{
    // This class stores the conditional word probabilities, i.e. P(w_i|c_j) for
    // a given word w_i. Thus, the number of elements in the conditionalProbabilityList
    // should be equal to the number of classes (in this case, two, but keep the code general!)
    public class ConditionalWordProbability
    {
        private string word;
        private List<double> conditionalProbabilityList;

        public ConditionalWordProbability()
        {
            conditionalProbabilityList = new List<double>();
        }

        public string Word
        {
            get { return word; }
            set { word = value; }
        }

        public List<double> ConditionalProbabilityList
        {
            get { return conditionalProbabilityList; }
            set { conditionalProbabilityList = value; }
        }
    }
}
