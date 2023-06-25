using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NaiveBayesApplication
{
    public partial class MainForm : Form
    {
        private List<Document> trainingDocumentList = null;
        private List<Document> testDocumentList = null;
        private List<string> stopWordList = null;
        private BayesianDocumentClassifier documentClassifier;
        private int numberOfClasses = 2; 

        public MainForm()
        {
            InitializeComponent();
        }

        private void ShowClassification(List<Document> documentList, ListBox listBox)
        {
            for (int ii = 0; ii < documentList.Count; ii++)
            {
                Document document = documentList[ii];
                string information = "Document " + ii.ToString().PadRight(4)
                    + " Actual class: " + document.Label.ToString().PadRight(2);
                if (document.InferredLabel >= 0)
                {
                    information += " Inferred class: " + document.InferredLabel.ToString();
                }
                if (document.ClassLogProbabilityList != null)
                {
                    for (int jj = 0; jj < document.ClassLogProbabilityList.Count; jj++)
                    {
                        information += " P(c=" + jj.ToString() + ") = "
                            + document.ClassLogProbabilityList[jj].ToString("0.0000").PadLeft(10);
                    }
                }
                listBox.Items.Add(information);
            }
        }

        private void findPriorProbabilitiesButton_Click(object sender, EventArgs e)
        {
            findPriorProbabilitiesButton.Enabled = false;

            const int CLASS_0 = 0;
            const int CLASS_1 = 1;


            documentClassifier = new BayesianDocumentClassifier();

            // Computing the (two) entries of the documentClassifier.PriorProbabilitiesList
            int noOfDocs = trainingDocumentList.Count;
            var groupedTrainingDocuments = trainingDocumentList.GroupBy(n => n.Label);
            double priorNegativeProbability = 0.0;
            double priorPositiveProbability = 0.0;

            foreach (var group in groupedTrainingDocuments)
            {
                if (group.Key == CLASS_0)
                {
                    priorNegativeProbability = (double)group.Count() / noOfDocs;
                }
                else if (group.Key == CLASS_1)
                {
                    priorPositiveProbability = (double)group.Count() / noOfDocs;
                }
            }
            List<double> priorProbabilitiesList = new List<double>{ priorNegativeProbability, priorPositiveProbability };
            documentClassifier.PriorProbabilitiesList = priorProbabilitiesList;

            // Display on-screen: Already completed, no need to change below this line, for this method
            List<string> informationList = new List<string>();
            string information = "Prior probabilities";
            informationList.Add(information);
            for (int ii = 0; ii < documentClassifier.PriorProbabilitiesList.Count; ii++)
            {
                information = "P(" + ii.ToString() + ") = " +
                    documentClassifier.PriorProbabilitiesList[ii].ToString("0.0000");
                informationList.Add(information);
            }
            foreach (string informationString in informationList)
            {
                classificationProgressListBox.Items.Add(informationString); //  Insert(0, informationString);
            }
            findConditionalProbabilitiesButton.Enabled = true;
        }

        private void findConditionalProbabilitiesButton_Click(object sender, EventArgs e)
        {
            findConditionalProbabilitiesButton.Enabled = false;
            documentClassifier.ComputeConditionalProbabilities(trainingDocumentList);
            List<string> informationList = new List<string>();
            string information = "Conditional probabilities";
            informationList.Add(information);
            for (int ii = 0; ii < documentClassifier.ConditionalWordProbabilityList.Count; ii++)
            {
                information = documentClassifier.ConditionalWordProbabilityList[ii].Word;
                information = information.PadRight(18) + " ";
                for (int jj = 0; jj<numberOfClasses; jj++)
                {
                    information += "P(w|c=" + jj.ToString() + ") = " +
                        documentClassifier.ConditionalWordProbabilityList[ii].
                                           ConditionalProbabilityList[jj].ToString("0.000000").PadLeft(10) + " ";

                }
                informationList.Add(information);
            }
            for (int ii = 0; ii < informationList.Count; ii++)
            {
                string informationString = informationList[ii];
                classificationProgressListBox.Items.Add(informationString);
            }
            classifyAllButton.Enabled = true;
        }

        private void ShowDocumentList(List<Document> documentList, ListBox listBox)
        {
            listBox.Items.Clear();
            foreach (Document document in documentList)
            {
                string documentAsString = document.AsString();
                listBox.Items.Add(documentAsString);
            }
        }

        private void loadTrainingDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    trainingDocumentList = new List<Document>();
                    StreamReader dataReader = new StreamReader(openFileDialog.FileName);
                    {
                        while (!dataReader.EndOfStream)
                        {
                            string information = dataReader.ReadLine();
                            List<string> informationSplit = information.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            Document document = new Document();
                            document.RawData = informationSplit[0];
                            document.Label = int.Parse(informationSplit[1]);
                            trainingDocumentList.Add(document);
                        }
                        dataReader.Close();
                        ShowDocumentList(trainingDocumentList, trainingDocumentListBox);

                        if (stopWordList != null)
                        {
                            cleanButton.Enabled = true;
                        }
                    }
                }
            }
        }

        private void loadTestDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    testDocumentList = new List<Document>();
                    StreamReader dataReader = new StreamReader(openFileDialog.FileName);
                    {
                        while (!dataReader.EndOfStream)
                        {
                            string information = dataReader.ReadLine();
                            List<string> informationSplit = information.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            Document document = new Document();
                            document.RawData = informationSplit[0];
                            document.Label = int.Parse(informationSplit[1]);
                            testDocumentList.Add(document);
                        }
                        dataReader.Close();
                        ShowDocumentList(testDocumentList, testDocumentListBox);
                    }
                }
            }
        }  

        private void classifyAllButton_Click(object sender, EventArgs e)
        {
            classifyAllButton.Enabled = false;
            foreach (Document document in trainingDocumentList)
            {
                documentClassifier.Classify(document);
            }
            classificationProgressListBox.Items.Add("Classification of training documents:");
            PerformanceMeasure trainingPerformance = new PerformanceMeasure();
            trainingPerformance.Compute(trainingDocumentList);
            classificationProgressListBox.Items.Add("Training performance: " +
                                                    " P = " + trainingPerformance.Precision.ToString("0.0000") +
                                                    ", R = " + trainingPerformance.Recall.ToString("0.0000") +
                                                    ", A = " + trainingPerformance.Accuracy.ToString("0.0000") +
                                                    ", F1 = " + trainingPerformance.F1.ToString("0.0000"));
            ShowClassification(trainingDocumentList, classificationProgressListBox);
            if (testDocumentList != null)
            {
                foreach (Document document in testDocumentList)
                {
                    documentClassifier.Classify(document);
                }
                classificationProgressListBox.Items.Add("Classification of test documents:");
                PerformanceMeasure testPerformance = new PerformanceMeasure();
                testPerformance.Compute(testDocumentList);
                classificationProgressListBox.Items.Add("Test performance: " +
                                                        " P = " + testPerformance.Precision.ToString("0.0000") +
                                                        ", R = " + testPerformance.Recall.ToString("0.0000") +
                                                        ", A = " + testPerformance.Accuracy.ToString("0.0000") +
                                                        ", F1 = " + testPerformance.F1.ToString("0.0000"));
                ShowClassification(testDocumentList, classificationProgressListBox);
            }
            saveAnalysisToolStripMenuItem.Enabled = true;
        }

        private void saveAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "text files (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter analysisWriter = new StreamWriter(saveFileDialog.FileName))
                    {
                        for (int ii = 0; ii < classificationProgressListBox.Items.Count; ii++)
                        {
                            string information = classificationProgressListBox.Items[ii].ToString();
                            analysisWriter.WriteLine(information);
                        }
                        analysisWriter.Close();
                    }
                }
            }
        }

        private void loadStopWordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "text files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader stopWordReader = new StreamReader(openFileDialog.FileName))
                    {
                        stopWordList = new List<string>();
                        while (!stopWordReader.EndOfStream)
                        {
                            string information = stopWordReader.ReadLine();
                            string stopWord = information.Trim().ToLower();
                            stopWordList.Add(stopWord);
                        }
                        stopWordReader.Close();
                        if (trainingDocumentList != null)
                        {
                            cleanButton.Enabled = true;
                        }
                    }
                }
            }
        }

        private void removeStopWordsButton_Click(object sender, EventArgs e)
        {
            removeStopWordsButton.Enabled = false;
            foreach (Document trainingDocument in trainingDocumentList)
            {
                trainingDocument.RemoveStopWords(stopWordList);
            }
            if (testDocumentList != null)
            {
                foreach (Document testDocument in testDocumentList)
                {
                    testDocument.RemoveStopWords(stopWordList);
                }
            }

            findPriorProbabilitiesButton.Enabled = true;
        }

        private void Clean(List<Document> documentList)
        {
            foreach (Document document in documentList)
            {
                document.Clean();
            }
        }

        private void cleanButton_Click(object sender, EventArgs e)
        {
            cleanButton.Enabled = false;
            Clean(trainingDocumentList);
            ShowDocumentList(trainingDocumentList, trainingDocumentListBox);
            if (testDocumentList != null)
            {
                Clean(testDocumentList);
                ShowDocumentList(testDocumentList, testDocumentListBox);
            }
            tokenizeButton.Enabled = true;
        }

        private void Tokenize(List<Document> documentList)
        {
            foreach (Document document in documentList)
            {
                document.Tokenize();
            }
        }

        private void tokenizeButton_Click(object sender, EventArgs e)
        {
            tokenizeButton.Enabled = false;
            Tokenize(trainingDocumentList);
            ShowDocumentList(trainingDocumentList, trainingDocumentListBox);
            if (testDocumentList != null)
            {
                Tokenize(testDocumentList);
                ShowDocumentList(testDocumentList, testDocumentListBox);
            }
            removeStopWordsButton.Enabled = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
