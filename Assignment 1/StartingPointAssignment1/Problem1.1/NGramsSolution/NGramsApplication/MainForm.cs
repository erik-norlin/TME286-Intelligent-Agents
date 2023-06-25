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
using NaturalLanguageProcessing.Dictionaries;
using NaturalLanguageProcessing.NGrams;
using NaturalLanguageProcessing.RRatios;
using NaturalLanguageProcessing.TextData;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace NGramsApplication
{
    public partial class MainForm : Form
    {
        private const string TEXT_FILTER = "Text files (*.txt)|*.txt";

        private TextDataSet writtenDataSet;
        private TextDataSet spokenDataSet;
        private NGramSet writtenUniGramSet; 
        private NGramSet writtenBiGramSet;
        private NGramSet writtenTriGramSet;
        private NGramSet spokenUniGramSet;
        private NGramSet spokenBiGramSet;
        private NGramSet spokenTriGramSet;

        private Thread tokenizationThread;
        private Thread indexingThread;
        private Thread processingThread;

        private List<string> analysisList;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ImportTextData(string fileName)
        {
            writtenDataSet = new TextDataSet();
            spokenDataSet = new TextDataSet();
            using (StreamReader dataReader = new StreamReader(fileName))
            {
                while (!dataReader.EndOfStream)
                {
                    string line = dataReader.ReadLine();
                    List<string> lineSplit = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    Sentence sentence = new Sentence();
                    sentence.Text = lineSplit[1];
                    sentence.Text = sentence.Text.Replace(" , ", " ");

                    if (lineSplit[0] == "0") // Spoken sentence (Class 0)
                    {
                        spokenDataSet.SentenceList.Add(sentence);
                    }
                    else // Written sentence (Class 1)
                    {
                        writtenDataSet.SentenceList.Add(sentence);
                    }
                }
                dataReader.Close();
            }
        }

        private void importTextDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = TEXT_FILTER;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImportTextData(openFileDialog.FileName);
                    tokenizeButton.Enabled = true;  
                }
            }
        }

        private void ShowAnalysis(List<string> analysisList)
        {
            analysisTextBox.Text = string.Join("\r\n", analysisList);
        }

        private void ThreadSafeShowAnalysis(List<string> analysisList)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => ShowAnalysis(analysisList))); }
            else { ShowAnalysis(analysisList); }
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

        private void TokenizationLoop()
        {
            writtenDataSet.Tokenize();
            spokenDataSet.Tokenize();
            ThreadSafeToggleButtonEnabled(makeDictionaryAndIndexButton, true);
        }

        private void tokenizeButton_Click(object sender, EventArgs e)
        {
            tokenizeButton.Enabled = false;
            tokenizationThread = new Thread(new ThreadStart(() => TokenizationLoop()));
            tokenizationThread.Start();
        }

        private void IndexingLoop()
        {
            writtenDataSet.MakeDictionaryAndIndex();
            spokenDataSet.MakeDictionaryAndIndex();
            ThreadSafeToggleButtonEnabled(processButton, true);
        }

        private void makeDictionaryAndIndexButton_Click(object sender, EventArgs e)
        {
            makeDictionaryAndIndexButton.Enabled = false;
            indexingThread = new Thread(new ThreadStart(() => IndexingLoop()));
            indexingThread.Start();
        }

        // ProcessingLoop method:
        // * Finding the 300 most common 1-grams, 2-grams, 3-grams in each set
        //     For the unigrams (1-grams), using the dictionary that was generated above.
        //     Generating the sets of bigrams (2-grams) and trigrams (3-grams), using
        //     the NGramSet and its methods (see NGramSet.Append(...) ...)
        // * Finding the number of distinct tokens (unigrams) in the spoken set (i.e. the
        //   number of items in its dictionary) and the written set, as well as the number of shared (distinct) tokens.
        // * Finding the 50 tokens with the largest values of r (see the problem formulation)
        //     and the 50 tokens with the smallest values of r.
        // 
        private void ProcessingLoop()
        {
            const int COUNT_CUTOFF = 5;
            const int NUMBER_OF_N_GRAMS_SHOWN = 300;
            const int TOP_50_R_VALUES = 50;
            const int BIGRAM_INDEX_CORRECTING = 1;
            const int TRIGRAM_INDEX_CORRECTING = 2;
            const int INDEX_FOUND = 0;

            List<DictionaryItem> spokenDictionaryItemList = spokenDataSet.Dictionary.ItemList;
            List<DictionaryItem> writtenDictionaryItemList = writtenDataSet.Dictionary.ItemList;


            // Adding analysis as a list of strings (that can then be shown on screen and saved to file).
            analysisList = new List<string>();


            // Creating written uni-grams, saving the 300 most common ones.
            writtenUniGramSet = new NGramSet();

            for (int i = 0; i < writtenDictionaryItemList.Count; i++)
            {
                DictionaryItem writtenDictionaryItem = writtenDictionaryItemList[i];
                List<string> tokenList = new List<string> { writtenDictionaryItem.Token };
                writtenUniGramSet.Append(tokenList);
                writtenUniGramSet.ItemList[i].NumberOfInstances = writtenDictionaryItem.Count;
            }

            writtenUniGramSet.SortOnFrequency();

            analysisList.Add("=========================================");
            analysisList.Add("Written 1-grams: ");
            analysisList.Add("=========================================");
            for (int ii = 0; ii < NUMBER_OF_N_GRAMS_SHOWN; ii++)
            {
                analysisList.Add(writtenUniGramSet.ItemList[ii].AsString());
            }



            // Creating spoken uni-grams, saving the 300 most common ones.
            spokenUniGramSet = new NGramSet();

            for (int i = 0; i < spokenDictionaryItemList.Count; i++)
            {
                DictionaryItem spokenDictionaryItem = spokenDictionaryItemList[i];
                List<string> tokenList = new List<string> { spokenDictionaryItem.Token };
                spokenUniGramSet.Append(tokenList);
                spokenUniGramSet.ItemList[i].NumberOfInstances = spokenDictionaryItem.Count;
            }

            spokenUniGramSet.SortOnFrequency();

            analysisList.Add("=========================================");
            analysisList.Add("Spoken 1-grams: ");
            analysisList.Add("=========================================");
            for (int ii = 0; ii < NUMBER_OF_N_GRAMS_SHOWN; ii++)
            {
                analysisList.Add(spokenUniGramSet.ItemList[ii].AsString());
            }

            Console.WriteLine("Pass uniGramSets");



            // Creating written bi-grams, saving the 300 most common ones.
            writtenBiGramSet = new NGramSet();

            Stopwatch stopWatchWrittenBiGram = new Stopwatch();
            stopWatchWrittenBiGram.Start();

            NGramSet tempWrittenBiGramSet = new NGramSet();
            foreach (Sentence sentence in writtenDataSet.SentenceList)
            {
                int noOfNGrams = sentence.TokenList.Count - BIGRAM_INDEX_CORRECTING;
                for (int i = 0; i <= noOfNGrams - 1; i++)
                {
                    string token1 = sentence.TokenList[i];
                    string token2 = sentence.TokenList[i + 1];
                    List<string> nGramTokenList = new List<string> { token1,
                                                                     token2 };
                    tempWrittenBiGramSet.Append(nGramTokenList);
                }
            }

            Console.WriteLine("Pass tempWrittenBiGramSet");

            foreach (NGram nGram in tempWrittenBiGramSet.ItemList)
            {
                if (nGram.NumberOfInstances >= COUNT_CUTOFF)
                {
                    writtenBiGramSet.Append(nGram.TokenList);
                    int lastIndex = writtenBiGramSet.ItemList.Count - 1;
                    writtenBiGramSet.ItemList[lastIndex].NumberOfInstances = nGram.NumberOfInstances;
                }
            }

            stopWatchWrittenBiGram.Stop();
            TimeSpan timeTakenWrittenBiGram = stopWatchWrittenBiGram.Elapsed;
            string timeWrittenBiGram = "Creating written bigrams: " + timeTakenWrittenBiGram.ToString(@"m\:ss\.fff");
            Console.WriteLine(timeWrittenBiGram);

            writtenBiGramSet.SortOnFrequency();

            analysisList.Add("=========================================");
            analysisList.Add("Written 2-grams: ");
            analysisList.Add("=========================================");
            for (int ii = 0; ii < NUMBER_OF_N_GRAMS_SHOWN; ii++)
            {
                analysisList.Add(writtenBiGramSet.ItemList[ii].AsString());
            }



            // Creating spoken bi-grams, saving the 300 most common ones.
            spokenBiGramSet = new NGramSet();

            Stopwatch stopWatchSpokenBi = new Stopwatch();
            stopWatchSpokenBi.Start();

            NGramSet tempSpokenBiGramSet = new NGramSet();
            foreach (Sentence sentence in spokenDataSet.SentenceList)
            {
                int noOfNGrams = sentence.TokenList.Count - BIGRAM_INDEX_CORRECTING;
                for (int i = 0; i <= noOfNGrams - 1; i++)
                {
                    string token1 = sentence.TokenList[i];
                    string token2 = sentence.TokenList[i + 1];
                    List<string> nGramTokenList = new List<string> { token1,
                                                                     token2 };
                    tempSpokenBiGramSet.Append(nGramTokenList);
                }
            }

            Console.WriteLine("Pass tempSpokenBiGramSet");

            foreach (NGram nGram in tempSpokenBiGramSet.ItemList)
            {
                if (nGram.NumberOfInstances >= COUNT_CUTOFF)
                {
                    spokenBiGramSet.Append(nGram.TokenList);
                    int lastIndex = spokenBiGramSet.ItemList.Count - 1;
                    spokenBiGramSet.ItemList[lastIndex].NumberOfInstances = nGram.NumberOfInstances;
                }
            }

            stopWatchSpokenBi.Stop();
            TimeSpan timeTakenSpokenBi = stopWatchSpokenBi.Elapsed;
            string timeSpokenBi = "Creating spoken bigrams: " + timeTakenSpokenBi.ToString(@"m\:ss\.fff");
            Console.WriteLine(timeSpokenBi);

            spokenBiGramSet.SortOnFrequency();

            analysisList.Add("=========================================");
            analysisList.Add("Spoken 2-grams: ");
            analysisList.Add("=========================================");
            for (int ii = 0; ii < NUMBER_OF_N_GRAMS_SHOWN; ii++)
            {
                analysisList.Add(spokenBiGramSet.ItemList[ii].AsString());
            }

            // Step (3) 
            //     Find the 300 most common trigrams (after generating the trigram set.
            //     using the NGramSet class (where you have to write code for appending
            //     n-grams, making sure to keep them sorted in alphabetical order based
            //     on the full token string (see the NGramSet class)

            // Creating written tri-grams, saving the 300 most common ones.
            writtenTriGramSet = new NGramSet();

            Stopwatch stopWatchWrittenTriGram = new Stopwatch();
            stopWatchWrittenTriGram.Start();

            NGramSet tempWrittenTriGramSet = new NGramSet();
            foreach (Sentence sentence in writtenDataSet.SentenceList)
            {
                int noOfNGrams = sentence.TokenList.Count - TRIGRAM_INDEX_CORRECTING;
                for (int i = 0; i <= noOfNGrams - 1; i++)
                {
                    string token1 = sentence.TokenList[i];
                    string token2 = sentence.TokenList[i + 1];
                    string token3 = sentence.TokenList[i + 2];
                    List<string> nGramTokenList = new List<string> { token1,
                                                                     token2,
                                                                     token3 };
                    tempWrittenTriGramSet.Append(nGramTokenList);
                }
            }

            Console.WriteLine("Pass tempWrittenTriGramSet");

            foreach (NGram nGram in tempWrittenTriGramSet.ItemList)
            {
                if (nGram.NumberOfInstances >= COUNT_CUTOFF)
                {
                    writtenTriGramSet.Append(nGram.TokenList);
                    int lastIndex = writtenTriGramSet.ItemList.Count - 1;
                    writtenTriGramSet.ItemList[lastIndex].NumberOfInstances = nGram.NumberOfInstances;
                }
            }

            stopWatchWrittenTriGram.Stop();
            TimeSpan timeTakenWrittenTriGram = stopWatchWrittenTriGram.Elapsed;
            string timeWrittenTriGram = "Creating written trigrams: " + timeTakenWrittenTriGram.ToString(@"m\:ss\.fff");
            Console.WriteLine(timeWrittenTriGram);

            writtenTriGramSet.SortOnFrequency();

            analysisList.Add("=========================================");
            analysisList.Add("Written 3-grams: ");
            analysisList.Add("=========================================");
            for (int ii = 0; ii < NUMBER_OF_N_GRAMS_SHOWN; ii++)
            {
                analysisList.Add(writtenTriGramSet.ItemList[ii].AsString());
            }


            // Creating spoken tri-grams, saving the 300 most common ones.
            spokenTriGramSet = new NGramSet();

            Stopwatch stopWatchSpokenTriGram = new Stopwatch();
            stopWatchSpokenTriGram.Start();

            NGramSet tempSpokenTriGramSet = new NGramSet();
            foreach (Sentence sentence in spokenDataSet.SentenceList)
            {
                int noOfNGrams = sentence.TokenList.Count - TRIGRAM_INDEX_CORRECTING;
                for (int i = 0; i <= noOfNGrams - 1; i++)
                {
                    string token1 = sentence.TokenList[i];
                    string token2 = sentence.TokenList[i + 1];
                    string token3 = sentence.TokenList[i + 2];
                    List<string> nGramTokenList = new List<string> { token1,
                                                                     token2,
                                                                     token3 };
                    tempSpokenTriGramSet.Append(nGramTokenList);
                }
            }

            Console.WriteLine("Pass tempSpokenTriGramSet");

            foreach (NGram nGram in tempSpokenTriGramSet.ItemList)
            {
                if (nGram.NumberOfInstances >= COUNT_CUTOFF)
                {
                    spokenTriGramSet.Append(nGram.TokenList);
                    int lastIndex = spokenTriGramSet.ItemList.Count - 1;
                    spokenTriGramSet.ItemList[lastIndex].NumberOfInstances = nGram.NumberOfInstances;
                }
            }

            stopWatchSpokenTriGram.Stop();
            TimeSpan timeTakenSpokenTriGram = stopWatchSpokenTriGram.Elapsed;
            string timeSpokenTriGram = "Creating spoken trigrams: " + timeTakenSpokenTriGram.ToString(@"m\:ss\.fff");
            Console.WriteLine(timeSpokenTriGram);

            spokenTriGramSet.SortOnFrequency();

            analysisList.Add("=========================================");
            analysisList.Add("Spoken 3-grams: ");
            analysisList.Add("=========================================");
            for (int ii = 0; ii < NUMBER_OF_N_GRAMS_SHOWN; ii++)
            {
                analysisList.Add(spokenTriGramSet.ItemList[ii].AsString());
            }
           
            
            // Computing r-ratios of shared tokens between the written and spoken language
            DictionaryItemComparer dictionaryComparer = new DictionaryItemComparer();
            RRatioSet ratioSet = new RRatioSet();

            foreach (DictionaryItem spokenItem in spokenDataSet.Dictionary.ItemList)
            {
                int indexOfWrittenItem = writtenDataSet.Dictionary.ItemList.BinarySearch(spokenItem, dictionaryComparer);

                if (indexOfWrittenItem >= INDEX_FOUND)
                {
                    DictionaryItem writtenItem = writtenDataSet.Dictionary.ItemList[indexOfWrittenItem];
                    RRatio ratioItem = new RRatio(writtenItem.Token);
                    ratioItem.ComputeAndSetRatio(writtenItem.Count, spokenItem.Count);
                    ratioSet.Append(ratioItem);
                }
            }

            analysisList.Add("=========================================");
            analysisList.Add("Number of tokens:");
            analysisList.Add("=========================================");
            analysisList.Add("Spoken set:  " + spokenDataSet.Dictionary.ItemList.Count.ToString());
            analysisList.Add("Written set: " + writtenDataSet.Dictionary.ItemList.Count.ToString());
            analysisList.Add("Shared:      " + ratioSet.ItemList.Count());

            analysisList.Add("=========================================");
            analysisList.Add("High-r tokens: ");
            analysisList.Add("=========================================");

            ratioSet.SortOnFrequencyDescending();
            for (int i = 0; i < TOP_50_R_VALUES; i++)
            {
                analysisList.Add(ratioSet.ItemList[i].AsString());
            }

            analysisList.Add("=========================================");
            analysisList.Add("Small-r tokens: ");
            analysisList.Add("=========================================");

            ratioSet.SortOnFrequencyAscending();
            for (int i = 0; i < TOP_50_R_VALUES; i++)
            {
                analysisList.Add(ratioSet.ItemList[i].AsString());
            }

            // Showing the analysis in a thread-safe manner
            ThreadSafeShowAnalysis(analysisList);
            ThreadSafeToggleMenuItemEnabled(saveAnalysisToolStripMenuItem, true);
        }

        private void processButton_Click(object sender, EventArgs e)
        {
            processButton.Enabled = false;
            processingThread = new Thread(new ThreadStart(() => ProcessingLoop()));
            processingThread.Start();
        }

        // Here, the results shown in the analysisTextBox are shown. One can also
        // (equivalently) just save the contents of the analysisList (which is indeed
        // what is shown in the textBox...)
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
