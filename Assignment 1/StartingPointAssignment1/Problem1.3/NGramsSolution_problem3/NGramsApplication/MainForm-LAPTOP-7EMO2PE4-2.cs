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
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml.Linq;
using NaturalLanguageProcessing.Dictionaries;
using NaturalLanguageProcessing.NGrams;
using NaturalLanguageProcessing.TextData;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace NGramsApplication
{
    public partial class MainForm : Form
    {
        private const string TEXT_FILTER = "Text files (*.txt)|*.txt";

        private TextDataSet spokenDataSet;
        private NGramSet spokenBiGramSet;
        private NGramSet spokenTriGramSet;

        private Thread tokenizationThread;
        private Thread indexingThread;
        private Thread processingThread;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ImportTextData(string fileName)
        {
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

                    // Only using spoken language, i.e. class 0.
                    if (lineSplit[0] == "0") // Spoken sentence (Class 0)
                    {
                        spokenDataSet.SentenceList.Add(sentence);
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
                    generate3gramsToolStripMenuItem.Enabled = true;
                }
            }
        }

        //private void ThreadSafeToggleButtonEnabled(ToolStripButton button, Boolean enabled)
        //{
        //    if (InvokeRequired) { this.Invoke(new MethodInvoker(() => button.Enabled = enabled)); }
        //    else { button.Enabled = enabled; }
        //}

        private void generate3gramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generate3gramsToolStripMenuItem.Enabled = false;
            spokenDataSet.Tokenize();
            //spokenDataSet.MakeDictionaryAndIndex();       // Delete dictionary?
            ThreadSafeProcessing();
        }

        private void ThreadSafeProcessing()
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => ProcessingLoop())); }
            else { ProcessingLoop(); }
        }

        private void ProcessingLoop()
        {
            const int BIGRAM_INDEX_CORRECTING = 1;
            const int TRIGRAM_INDEX_CORRECTING = 2;

            // Creating spoken bi-grams
            Stopwatch stopWatchSpokenBi = new Stopwatch();
            stopWatchSpokenBi.Start();

            spokenBiGramSet = new NGramSet();

            //foreach (Sentence sentence in spokenDataSet.SentenceList)
            //{
            //    int noOfNGrams = sentence.TokenList.Count - BIGRAM_INDEX_CORRECTING;
            //    for (int i = 0; i <= noOfNGrams - 1; i++)
            //    {
            //        int tokenIndex1 = sentence.TokenIndexList[i];
            //        int tokenIndex2 = sentence.TokenIndexList[i + 1];
            //        List<string> nGramTokenList = new List<string> { spokenDictionaryItemList[tokenIndex1].Token, 
            //                                                         spokenDictionaryItemList[tokenIndex2].Token };
            //        tempSpokenBiGramSet.Append(nGramTokenList);
            //    }
            //}

            foreach (Sentence sentence in spokenDataSet.SentenceList)
            {
                int noOfNGrams = sentence.TokenList.Count - BIGRAM_INDEX_CORRECTING;
                for (int i = 0; i <= noOfNGrams - 1; i++)
                {
                    string token1 = sentence.TokenList[i];
                    string token2 = sentence.TokenList[i + 1];
                    List<string> nGramTokenList = new List<string> { token1,
                                                                     token2 };
                    spokenBiGramSet.Append(nGramTokenList);
                }
            }

            Console.WriteLine("Pass spokenBiGramSet");

            stopWatchSpokenBi.Stop();
            TimeSpan timeTakenSpokenBi = stopWatchSpokenBi.Elapsed;
            string timeSpokenBi = "Creating spoken bigrams: " + timeTakenSpokenBi.ToString(@"m\:ss\.fff");
            Console.WriteLine(timeSpokenBi);

            //spokenBiGramSet.SortOnFrequency();          // Keep?

            //foreach (NGram nGram in spokenBiGramSet.ItemList)
            //{
            //    analysisList.Add(nGram.AsString());
            //}

           
            // Creating spoken tri-grams
            Stopwatch stopWatchSpokenTriGram = new Stopwatch();
            stopWatchSpokenTriGram.Start();

            spokenTriGramSet = new NGramSet();

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
                    spokenTriGramSet.Append(nGramTokenList);
                }
            }

            Console.WriteLine("Pass tempSpokenTriGramSet");

            stopWatchSpokenTriGram.Stop();
            TimeSpan timeTakenSpokenTriGram = stopWatchSpokenTriGram.Elapsed;
            string timeSpokenTriGram = "Creating spoken trigrams: " + timeTakenSpokenTriGram.ToString(@"m\:ss\.fff");
            Console.WriteLine(timeSpokenTriGram);

            //spokenTriGramSet.SortOnFrequency();

            //foreach(NGram nGram in spokenTriGramSet.ItemList)
            //{
            //    analysisList.Add(nGram.AsString());
            //}

            // Showing the analysis in a thread-safe manner


            // Make textbox enabled
            ThreadSafeShowNGramProcess();
        }
        private void ThreadSafeShowNGramProcess()
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => ShowNGramProcess())); }
            else { ShowNGramProcess(); }
        }

        private void ShowNGramProcess()
        {
            string message = "Processing n-grams done. Type something in the upper box.";
            string title = "Finished";
            MessageBox.Show(message, title);
            ThreadSafeTextBoxEnabled(userTextBox, true);
        }

        private void ThreadSafeTextBoxEnabled(TextBox textBox, Boolean enabled)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => textBox.Enabled = enabled)); }
            else { textBox.Enabled = enabled; }
        }



        private void userTextBox_TextChanged(object sender, EventArgs e)
        {
            string endWithSpacePattern = @".+\s$";
            bool endWithSpace = Regex.IsMatch(userTextBox.Text, endWithSpacePattern);
            if (endWithSpace)
            {
                Console.WriteLine("Regex success");
                ThreadSafeShowNGramList();
            }
        }

        private void ThreadSafeShowNGramList()
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => ShowNGramList())); }
            else { ShowNGramList(); }
        }

        private void ShowNGramList()
        {
            Console.WriteLine("Regex success, new thread");
            listBox.Items.Clear();
            listBox.Items.Add("Gött mos");

            string[] inputTokenList = userTextBox.Text.Trim().Split(' ');
            int inputTokenListLength = inputTokenList.Length;
            NGramComparer nGramComparer = new NGramComparer();

            if (inputTokenListLength >= 2)
            {
                // Finding tri-grams for auto completion
                List<string> tokenList = new List<string> { inputTokenList[inputTokenListLength - 1], 
                                                            inputTokenList[inputTokenListLength - 2] };
                NGram triGram = new NGram(tokenList);
                spokenTriGramSet.ItemList.BinarySearch(triGram, nGramComparer);

                // if not found, find bigram
            }
            else if (inputTokenListLength == 1)
            {
                // Finding bi-grams for auto completion
                List<string> tokenList = new List<string> { inputTokenList[inputTokenListLength - 1] };
                NGram biGram = new NGram(tokenList);
                spokenBiGramSet.ItemList.BinarySearch(biGram, nGramComparer);
                // if not found, dont show anything
            }


            //foreach (NGram nGram in nGramList)
            //{
            //    string nGramAsString = nGram.AsString();
            //    listBox.Items.Add(nGramAsString);
            //}

            // if no trigrams, go for bigrams
        }




        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
