using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace NaiveBayesApplication
{
    public class BayesianDocumentClassifier
    {
        private List<double> priorProbabilitiesList;
        private List<ConditionalWordProbability> conditionalWordProbabilityList;

        public BayesianDocumentClassifier()
        {
            priorProbabilitiesList = new List<double>();
            conditionalWordProbabilityList = new List<ConditionalWordProbability>();
        }

        // The method takes a document as input, then computes
        // the log probability as in Eq. (4.26) (in the compendium).
        //
        // Among other things, this involves summing (for each class label)
        // over the tokens in the document, ignoring any
        // token for which a conditional word probability is not available
        // (i.e. for those (rare) words that did not appear anywhere in the training set)
        //
        // Then infer (and return) the class label; again see Eq. (4.26).
        //
        // (Note (final edit!), the returned int label is not really used here. You must
        // also assign the inferred label to the document (done below, row 42). The
        // returned label WOULD be used if you extend your code to classify a new (single)
        // sentence ...
        public int Classify(Document document)
        {
            // Computing log probabilites for each word given each class 
            int numberOfClasses = priorProbabilitiesList.Count;
            int inferredClass = -1;

            List<double> classLogProbabilityList = new List<double>();
            for (int i = 0; i < numberOfClasses; i++)
            {
                List<double> logConditionalProbabilityList = new List<double>();
                foreach (string token in document.TokenList)
                {
                    ConditionalWordProbability cwp = conditionalWordProbabilityList.Find(n => n.Word == token);
                    if (cwp != null)
                    {
                        double logConditionalProbability = Math.Log(cwp.ConditionalProbabilityList[i]);
                        logConditionalProbabilityList.Add(logConditionalProbability);
                    }
                }
                double classLogProbability = Math.Log(priorProbabilitiesList[i]) + logConditionalProbabilityList.Sum();
                classLogProbabilityList.Add(classLogProbability);
            }
            document.ClassLogProbabilityList = classLogProbabilityList;
            int classArgMax = classLogProbabilityList.IndexOf(classLogProbabilityList.Max());
            inferredClass = classArgMax;
            document.InferredLabel = inferredClass;
            return inferredClass;
        }

        public void ComputeConditionalProbabilities(List<Document> documentList)
        {
            // Making a list with all the distinct words of the training set
            List<string> wordList = new List<string>();
            foreach (Document document in documentList) 
            { 
                foreach (string token in document.TokenList)
                {
                    wordList.Add(token);
                }
            }

            // Gathering data for P(w_i)
            int noWords = wordList.Count;
            Console.WriteLine("no. words: " + wordList.Count);
            var groups = wordList.GroupBy(n => n);
            foreach (var group in groups)
            {
                if (string.Equals(group.Key, "friendly"))
                {
                    Console.WriteLine("friendly count: " + group.Count() + "P(w) = ".PadLeft(10) + (double)group.Count()/noWords);
                }
                else if (string.Equals(group.Key, "perfectly"))
                {
                    Console.WriteLine("perfectly count: " + group.Count() + "P(w) = ".PadLeft(10) + (double)group.Count()/noWords);
                }
                else if (string.Equals(group.Key, "horrible"))
                {
                    Console.WriteLine("horrible count: " + group.Count() + "P(w) = ".PadLeft(10) + (double)group.Count()/noWords);
                }
                else if (string.Equals(group.Key, "poor"))
                {
                    Console.WriteLine("poor count: " + group.Count() + "P(w) = ".PadLeft(10) + (double)group.Count()/noWords);
                }
            }

            List<string> distinctWordList = wordList.Distinct().ToList();
            distinctWordList.Sort();

            // Defining conditional probabilities (just the words for now)
            conditionalWordProbabilityList = new List<ConditionalWordProbability>();
            foreach (string word in distinctWordList)
            {
                ConditionalWordProbability cwp = new ConditionalWordProbability();
                cwp.Word = word;
                conditionalWordProbabilityList.Add(cwp);
            }

            // Generating two mergerd documents, one for each class (label)
            int numberOfClasses = priorProbabilitiesList.Count;
            List<Document> mergedClassDocumentList = new List<Document>();

            var groupedDocuments = documentList.GroupBy(n => n.Label);
            foreach (var group in groupedDocuments)
            {
                Document mergedClassDocument = Document.Merge(group.ToList());
                mergedClassDocumentList.Add(mergedClassDocument);

            }

            // Computing conditional word probabilities using Laplace smoothing
            int totalDistinctWordCount = conditionalWordProbabilityList.Count;
            for (int i = 0; i < totalDistinctWordCount; i++)
            {
                string word = conditionalWordProbabilityList[i].Word;
                List<double> conditionalProbabilityList = new List<double>();

                foreach (Document mergedClassDocument in mergedClassDocumentList)
                {
                    int totalWordCountInDocument = mergedClassDocument.TokenList.Count;
                    int occurrencesOfWord = mergedClassDocument.TokenList.Count(n => n == word);
                    double conditionalProbability = (double)(occurrencesOfWord + 1) / (totalWordCountInDocument + totalDistinctWordCount);
                    conditionalProbabilityList.Add(conditionalProbability);
                }
                conditionalWordProbabilityList[i].ConditionalProbabilityList = conditionalProbabilityList;
            }
        }

        public List<double> PriorProbabilitiesList
        {
            get { return priorProbabilitiesList; }
            set { priorProbabilitiesList = value; }
        }

        public List<ConditionalWordProbability> ConditionalWordProbabilityList
        {
            get { return conditionalWordProbabilityList; }
            set { conditionalWordProbabilityList = value; }
        }
    }
}
