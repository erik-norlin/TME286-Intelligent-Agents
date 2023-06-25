using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml.Linq;
using NaturalLanguageProcessing.Dictionaries;
using NaturalLanguageProcessing.RRatios;
using NaturalLanguageProcessing.TextData;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace NGramsApplication
{
    public partial class MainForm : Form
    {
        private const string TEXT_FILTER = "Text files (*.txt)|*.txt";
        const int FOUND_INDEX = 0;

        private TextDataSet positiveTrainingDataSet = new TextDataSet();
        private TextDataSet negativeTrainingDataSet = new TextDataSet();

        private TextDataSet positiveTestDataSet = new TextDataSet();
        private TextDataSet negativeTestDataSet = new TextDataSet();

        private Thread tokenizationThread;
        private Thread indexingThread;
        private Thread processingThread;
        private Thread classifyThread;

        private List<string> analysisRatioList;
        private List<string> analysisTokenList;

        private RRatioSet ratioSet;

        public MainForm()
        {
            InitializeComponent();
        }
        private void importTrainingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void importTestDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
  
        }


        private void importNegativeTrainingReviewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importNegativeTrainingReviewsToolStripMenuItem.Enabled = false;

            Console.WriteLine("Loading...");
            Stopwatch stopWatchNegTrainingLoad = new Stopwatch();
            stopWatchNegTrainingLoad.Start();

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string[] txtFiles = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                    foreach (string currentFile in txtFiles)
                    {
                        string review = File.ReadAllText(currentFile);
                        Sentence sentence = new Sentence();
                        sentence.Text = review.Replace(" , ", " ");
                        sentence.Label = 0; // Negative review (class 0)
                        negativeTrainingDataSet.SentenceList.Add(sentence);
                    }
                    importPositiveTrainingReviewsToolStripMenuItem.Enabled = true;
                }
            }
            stopWatchNegTrainingLoad.Stop();
            TimeSpan timeTakenNegTrainingLoad = stopWatchNegTrainingLoad.Elapsed;
            string time = "Loading negative training reviews: " + timeTakenNegTrainingLoad.ToString(@"m\:ss\.fff");
            Console.WriteLine(time);
        }

        private void importPositiveTrainingReviewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importPositiveTrainingReviewsToolStripMenuItem.Enabled = false;

            Console.WriteLine("Loading...");
            Stopwatch stopWatchPosTrainingLoad = new Stopwatch();
            stopWatchPosTrainingLoad.Start();

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string[] txtFiles = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                    foreach (string currentFile in txtFiles)
                    {
                        string review = File.ReadAllText(currentFile);
                        Sentence sentence = new Sentence();
                        sentence.Text = review.Replace(" , ", " ");
                        sentence.Label = 1; // Positive review (class 1)
                        positiveTrainingDataSet.SentenceList.Add(sentence);
                    }
                    tokenizeTrainingDataToolStripMenuItem.Enabled = true;
                }
            }
            stopWatchPosTrainingLoad.Stop();
            TimeSpan timeTakenPosTrainingLoad = stopWatchPosTrainingLoad.Elapsed;
            string time = "Loading positive training reviews: " + timeTakenPosTrainingLoad.ToString(@"m\:ss\.fff");
            Console.WriteLine(time);
        }
        

        private void ThreadSafeToggleButtonEnabled(ToolStripButton button, Boolean enabled)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => button.Enabled = enabled)); }
            else { button.Enabled = enabled; }
        }

        private void ThreadSafeToggleMenuItemEnabled(ToolStripMenuItem menuItem, Boolean enabled)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => menuItem.Enabled = enabled)); }
            else { menuItem.Enabled = enabled; }
        }
        private void tokenizeTrainingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tokenizeTrainingDataToolStripMenuItem.Enabled = false;
            tokenizationThread = new Thread(new ThreadStart(() => TokenizationTrainingLoop()));
            tokenizationThread.Start();
        }

        private void TokenizationTrainingLoop()
        {
            positiveTrainingDataSet.Tokenize();
            negativeTrainingDataSet.Tokenize();
            ThreadSafeToggleMenuItemEnabled(makeDictionaryToolStripMenuItem, true);
        }

        private void makeDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeDictionaryToolStripMenuItem.Enabled = false;
            indexingThread = new Thread(new ThreadStart(() => IndexingLoop()));
            indexingThread.Start();
        }

        private void IndexingLoop()
        {
            positiveTrainingDataSet.MakeDictionaryAndIndex();
            negativeTrainingDataSet.MakeDictionaryAndIndex();
            ThreadSafeToggleMenuItemEnabled(processTrainingDataToolStripMenuItem, true);
        }

        private void processTrainingDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processTrainingDataToolStripMenuItem.Enabled = false;
            processingThread = new Thread(new ThreadStart(() => ProcessingLoop()));
            processingThread.Start();
        }

        private void ProcessingLoop()
        {
            List<DictionaryItem> negativeDictionaryItemList = negativeTrainingDataSet.Dictionary.ItemList;
            List<DictionaryItem> positiveDictionaryItemList = positiveTrainingDataSet.Dictionary.ItemList;

            // Computing r-ratios of shared tokens between the positive and negative reviews
            DictionaryItemComparer dictionaryComparer = new DictionaryItemComparer();
            ratioSet = new RRatioSet();

            foreach (DictionaryItem negativeItem in negativeTrainingDataSet.Dictionary.ItemList)
            {
                int indexOfPositiveItem = positiveTrainingDataSet.Dictionary.ItemList.BinarySearch(negativeItem, dictionaryComparer);

                if (indexOfPositiveItem >= FOUND_INDEX)
                {
                    DictionaryItem positiveItem = positiveTrainingDataSet.Dictionary.ItemList[indexOfPositiveItem];
                    RRatio ratioItem = new RRatio(positiveItem.Token);
                    ratioItem.ComputeAndSetLogRatio(positiveItem.Count, negativeItem.Count);
                    ratioSet.Append(ratioItem);
                }
            }
            ThreadSafeToggleMenuItemEnabled(showTokenListToolStripMenuItem, true);
        }

        private void showTokenListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showTokenListToolStripMenuItem.Enabled = false;
            processingThread = new Thread(new ThreadStart(() => ShowTokenListLoop()));
            processingThread.Start();
        }

        private void ShowTokenListLoop()
        {
            analysisTokenList = new List<string>();

            analysisTokenList.Add("=========================================");
            analysisTokenList.Add("Tokens with their respective log-ratios: ");
            analysisTokenList.Add("=========================================");

            int noOfItems = ratioSet.ItemList.Count();
            for (int i = 0; i < noOfItems; i++)
            {
                analysisTokenList.Add(ratioSet.ItemList[i].AsString());
            }

            // Showing the analysis in a thread-safe manner
            ThreadSafeShowAnalysis(analysisTokenList);
            ThreadSafeToggleMenuItemEnabled(saveAnalysisToolStripMenuItem, true);
            ThreadSafeToggleMenuItemEnabled(showAnalysisForReportToolStripMenuItem, true);
        }

        private void showAnalysisForReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showAnalysisForReportToolStripMenuItem.Enabled = false;
            processingThread = new Thread(new ThreadStart(() => ShowAnalysisForReportLoop()));
            processingThread.Start();
        }

        private void ShowAnalysisForReportLoop()
        {
            const int TOP_30_R_VALUES = 30;

            analysisRatioList = new List<string>();

            analysisRatioList.Add("=========================================");
            analysisRatioList.Add("Number of tokens:");
            analysisRatioList.Add("=========================================");
            analysisRatioList.Add("Negative set:  " + negativeTrainingDataSet.Dictionary.ItemList.Count.ToString());
            analysisRatioList.Add("Positive set: " + positiveTrainingDataSet.Dictionary.ItemList.Count.ToString());
            analysisRatioList.Add("Shared:      " + ratioSet.ItemList.Count());

            analysisRatioList.Add("=========================================");
            analysisRatioList.Add("High-r tokens: ");
            analysisRatioList.Add("=========================================");

            ratioSet.SortOnFrequencyDescending();

            for (int i = 0; i < TOP_30_R_VALUES; i++)
            {
                analysisRatioList.Add(ratioSet.ItemList[i].AsString());
            }

            analysisRatioList.Add("=========================================");
            analysisRatioList.Add("Small-r tokens: ");
            analysisRatioList.Add("=========================================");

            ratioSet.SortOnFrequencyAscending();
            for (int i = 0; i < TOP_30_R_VALUES; i++)
            {
                analysisRatioList.Add(ratioSet.ItemList[i].AsString());
            }

            // Showing the analysis in a thread-safe manner
            ThreadSafeShowAnalysis(analysisRatioList);
            ThreadSafeToggleMenuItemEnabled(saveAnalysisToolStripMenuItem, true);
            ThreadSafeToggleMenuItemEnabled(importNegativeTestReviewsToolStripMenuItem, true);
        }

        private void ThreadSafeShowAnalysis(List<string> analysisList)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => ShowAnalysis(analysisList))); }
            else { ShowAnalysis(analysisList); }
        }

        private void ShowAnalysis(List<string> analysisList)
        {
            analysisTextBox.Text = "";
            analysisTextBox.Text = string.Join("\r\n", analysisList);
        }

        private void saveAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = TEXT_FILTER;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter dataWriter = new StreamWriter(saveFileDialog.FileName))
                    {
                        for (int ii = 0; ii < analysisTextBox.Lines.Count(); ii++)
                        {
                            dataWriter.WriteLine(analysisTextBox.Lines[ii]);
                        }
                        dataWriter.Close();
                    }
                }
            }
        }
        
        private void importNegativeTestReviewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importNegativeTestReviewsToolStripMenuItem.Enabled = false;

            Console.WriteLine("Loading...");
            Stopwatch stopWatchNegTestLoad = new Stopwatch();
            stopWatchNegTestLoad.Start();

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string[] txtFiles = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                    foreach (string currentFile in txtFiles)
                    {
                        string review = File.ReadAllText(currentFile);
                        Sentence sentence = new Sentence();
                        sentence.Text = review.Replace(" , ", " ");
                        sentence.Label = 0; // Negative review (class 0)
                        negativeTestDataSet.SentenceList.Add(sentence);
                        int index = negativeTestDataSet.SentenceList.Count - 1;
                    }
                    importPositiveTestReviewsToolStripMenuItem.Enabled = true;
                }
            }
            stopWatchNegTestLoad.Stop();
            TimeSpan timeTakenNegTestLoad = stopWatchNegTestLoad.Elapsed;
            string time = "Loading negative test reviews: " + timeTakenNegTestLoad.ToString(@"m\:ss\.fff");
            Console.WriteLine(time);
        }

        private void importPositiveTestReviewsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importPositiveTestReviewsToolStripMenuItem.Enabled = false;

            Console.WriteLine("Loading...");
            Stopwatch stopWatchPosTestLoad = new Stopwatch();
            stopWatchPosTestLoad.Start();

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    string[] txtFiles = Directory.GetFiles(folderBrowserDialog.SelectedPath);
                    foreach (string currentFile in txtFiles)
                    {
                        string review = File.ReadAllText(currentFile);
                        Sentence sentence = new Sentence();
                        sentence.Text = review.Replace(" , ", " ");
                        sentence.Label = 1; // Positive review (class 1)
                        positiveTestDataSet.SentenceList.Add(sentence);
                        int index = positiveTestDataSet.SentenceList.Count - 1;
                    }
                    tokenizeTestDataToolStripMenuItem.Enabled = true;
                }
            }
            stopWatchPosTestLoad.Stop();
            TimeSpan timeTakenPosTestLoad = stopWatchPosTestLoad.Elapsed;
            string time = "Loading positive test reviews: " + timeTakenPosTestLoad.ToString(@"m\:ss\.fff");
            Console.WriteLine(time);
        }

        private void tokenizeTestDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tokenizeTestDataToolStripMenuItem.Enabled = false;
            tokenizationThread = new Thread(new ThreadStart(() => TokenizationTestLoop()));
            tokenizationThread.Start();
        }

        private void TokenizationTestLoop()
        {
            positiveTestDataSet.Tokenize();
            negativeTestDataSet.Tokenize();
            ThreadSafeToggleMenuItemEnabled(classifyAllToolStripMenuItem, true);
        }

        private void classifyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            classifyAllToolStripMenuItem.Enabled = false;
            classifyThread = new Thread(new ThreadStart(() => Classify()));
            classifyThread.Start();
        }

        private void Classify()
        {
            
            int counter = 0;
            Console.WriteLine(negativeTrainingDataSet.SentenceList.Count);

            classifyAllToolStripMenuItem.Enabled = false;
            foreach (Sentence negativeReview in negativeTrainingDataSet.SentenceList)
            {
                double sum = 0;
                foreach (string token in negativeReview.TokenList)
                {
                    int tokenIndex = ratioSet.ItemList.FindIndex(n => n.Token == token);
                    if (tokenIndex >= FOUND_INDEX)
                    {
                        sum += ratioSet.ItemList[tokenIndex].LogRatio;
                    }
                }
                if (sum > 0) { negativeReview.InferredLabel = 1; }
                else { negativeReview.InferredLabel = 0; }

                counter++;
                Console.WriteLine(counter);
            }

            counter = 0;
            Console.WriteLine(positiveTrainingDataSet.SentenceList.Count);

            foreach (Sentence positiveReview in positiveTrainingDataSet.SentenceList)
            {
                double sum = 0;
                foreach (string token in positiveReview.TokenList)
                {
                    int tokenIndex = ratioSet.ItemList.FindIndex(n => n.Token == token);
                    if (tokenIndex >= FOUND_INDEX)
                    {
                        sum += ratioSet.ItemList[tokenIndex].LogRatio;
                    }
                }
                if (sum > 0) { positiveReview.InferredLabel = 1; }
                else { positiveReview.InferredLabel = 0; }

                counter++;
                Console.WriteLine(counter);
            }
            
            List<TextDataSet> trainingData = new List<TextDataSet> { negativeTrainingDataSet, positiveTrainingDataSet };
            PerformanceMeasure trainingPerformance = new PerformanceMeasure();
            trainingPerformance.Compute(trainingData);
            int trainingSet = 0;
            ThreadSafeShowPerformance(trainingPerformance, trainingSet);
            

            counter = 0;
            Console.WriteLine(negativeTestDataSet.SentenceList.Count);

            foreach (Sentence negativeReview in negativeTestDataSet.SentenceList)
            {
                double sum = 0;
                foreach (string token in negativeReview.TokenList)
                {
                    int tokenIndex = ratioSet.ItemList.FindIndex(n => n.Token == token);
                    if (tokenIndex >= FOUND_INDEX)
                    {
                        sum += ratioSet.ItemList[tokenIndex].LogRatio;
                    }
                }
                if (sum > 0) { negativeReview.InferredLabel = 1; }
                else { negativeReview.InferredLabel = 0; }

                counter++;
                Console.WriteLine(counter);
            }

            counter = 0;
            Console.WriteLine(positiveTestDataSet.SentenceList.Count);

            foreach (Sentence positiveReview in positiveTestDataSet.SentenceList)
            {
                double sum = 0;
                foreach (string token in positiveReview.TokenList)
                {
                    int tokenIndex = ratioSet.ItemList.FindIndex(n => n.Token == token);
                    if (tokenIndex >= FOUND_INDEX)
                    {
                        sum += ratioSet.ItemList[tokenIndex].LogRatio;
                    }
                }
                if (sum > 0) { positiveReview.InferredLabel = 1; }
                else { positiveReview.InferredLabel = 0; }

                counter++;
                Console.WriteLine(counter);
            }

            List<TextDataSet> testData = new List<TextDataSet> { negativeTestDataSet, positiveTestDataSet };
            PerformanceMeasure testPerformance = new PerformanceMeasure();
            testPerformance.Compute(testData);
            int testSet = 1;
            ThreadSafeShowPerformance(testPerformance, testSet);
        }

        private void ThreadSafeShowPerformance(PerformanceMeasure dataSetPerformance, int dataSet)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => ShowPerformance(dataSetPerformance, dataSet))); }
            else { ShowPerformance(dataSetPerformance, dataSet); }
        }

        private void ShowPerformance(PerformanceMeasure dataSetPerformance, int dataSet)
        {
            if (dataSet == 0) // if showing training set
            {
                classificationListBox.Items.Add("Training performance: ");
            }
            else if (dataSet == 1) // if showing test set
            {
                classificationListBox.Items.Add("Test performance: ");
            }
            classificationListBox.Items.Add(" P = " + dataSetPerformance.Precision.ToString("0.0000") +
                                            " R = " + dataSetPerformance.Recall.ToString("0.0000") +
                                            " A = " + dataSetPerformance.Accuracy.ToString("0.0000") +
                                            " F1 = " + dataSetPerformance.F1.ToString("0.0000"));
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
