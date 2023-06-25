using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NaturalLanguageProcessing.TextData
{
    public class PerformanceMeasure
    {
        public double Accuracy { get; set; }
        public double Precision { get; set; }
        public double Recall { get; set; }
        public double F1 { get; set; }

        public void Compute(List<TextDataSet> dataSetList)
        {
            int truePositiveCount = 0;
            int falsePositiveCount = 0;
            int trueNegativeCount = 0;
            int falseNegativeCount = 0;

            // Counting TP, FP, TN, and FN and then computing accuracy, precision, recall, and F1.
            foreach (TextDataSet dataSet in dataSetList)
            {
                foreach (Sentence review in dataSet.SentenceList)
                {
                    if (review.Label == 1 && review.InferredLabel == 1) { truePositiveCount++; }
                    else if (review.Label == 0 && review.InferredLabel == 1) { falsePositiveCount++; }
                    else if (review.Label == 0 && review.InferredLabel == 0) { trueNegativeCount++; }
                    else if (review.Label == 1 && review.InferredLabel == 0) { falseNegativeCount++; }
                }
                // Console.WriteLine("truePositiveCount: " + truePositiveCount + "     falsePositiveCount: " + falsePositiveCount + "     trueNegativeCount: " + trueNegativeCount + "     falseNegativeCount: " + falseNegativeCount);
                // Console.WriteLine("truePositiveCount: " + truePositiveCount + "     falsePositiveCount: " + falsePositiveCount);

            }
            Accuracy = (double)(truePositiveCount + trueNegativeCount) / (truePositiveCount + trueNegativeCount + falsePositiveCount + falseNegativeCount);
            Precision = (double)truePositiveCount / (truePositiveCount + falsePositiveCount);
            Recall = (double)truePositiveCount / (truePositiveCount + falseNegativeCount);
            F1 = (double)(2*Precision*Recall) / (Recall + Precision);
        }
    }
}
