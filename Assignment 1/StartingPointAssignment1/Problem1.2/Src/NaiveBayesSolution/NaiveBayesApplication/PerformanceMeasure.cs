using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaiveBayesApplication
{
    public class PerformanceMeasure
    {
        public double Accuracy { get; set; }
        public double Precision { get; set; }
        public double Recall { get; set; }
        public double F1 { get; set; }

        public void Compute(List<Document> documentList)
        {
            int truePositiveCount = 0;
            int falsePositiveCount = 0;
            int trueNegativeCount = 0;
            int falseNegativeCount = 0;

            // Counting TP, FP, TN, and FN and then computing accuracy, precision, recall, and F1.
            foreach (Document document in documentList)
            {
                if (document.Label == 1 && document.InferredLabel == 1) { truePositiveCount++; }
                else if (document.Label == 0 && document.InferredLabel == 1) { falsePositiveCount++; }
                else if (document.Label == 0 && document.InferredLabel == 0) { trueNegativeCount++; }
                else if (document.Label == 1 && document.InferredLabel == 0) { falseNegativeCount++; }
            }
            Accuracy = (double)(truePositiveCount + trueNegativeCount) / (truePositiveCount + trueNegativeCount + falsePositiveCount + falseNegativeCount);
            Precision = (double)truePositiveCount / (truePositiveCount + falsePositiveCount);
            Recall = (double)truePositiveCount / (truePositiveCount + falseNegativeCount);
            F1 = (double)(2*Precision*Recall) / (Recall + Precision);
        }
    }
}
