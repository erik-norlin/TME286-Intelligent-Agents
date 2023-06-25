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

        private void generate3gramsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generate3gramsToolStripMenuItem.Enabled = false;
            spokenDataSet.Tokenize();
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

            // Making text box enabled in a thread safe manner
            ThreadSafeShowNGramProcess();
        }
        private void ThreadSafeShowNGramProcess()
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => ShowNGramProcess())); }
            else { ShowNGramProcess(); }
        }

        private void ShowNGramProcess()
        {
            string message = "Processing n-grams done. Type something in the grey box.";
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
            string noInput = "";
            bool clear = string.Equals(userTextBox.Text, noInput);

            if (endWithSpace || clear)
            {
                ThreadSafeShowNGramList(clear);
            }
        }

        private void ThreadSafeShowNGramList(bool clear)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => ShowNGramList(clear))); }
            else { ShowNGramList(clear); }
        }

        private void ShowNGramList(bool clear)
        {
            const int NO_TRIGRAMS = 0;
            const int NO_BIGRAMS = 0;
            const int TRIGRAM_LENGTH_VALID = 2;
            const int TRIGRAM_LENGTH_INVALID = 1;

            listBox.Items.Clear();

            if (!clear)
            {
                string[] inputTokenArray = userTextBox.Text.ToLower().Trim(' ').Split(' ');
                List<string> inputTokenList = new List<string>(inputTokenArray);
                inputTokenList.RemoveAll(emptyString => emptyString == "");
                int inputTokenListLength = inputTokenList.Count;
                
                // Finding tri-grams for auto completion
                if (inputTokenListLength >= TRIGRAM_LENGTH_VALID)
                {
                    List<string> triGramTokenList = new List<string> { inputTokenList[inputTokenListLength - 2],
                                                                       inputTokenList[inputTokenListLength - 1] };
                    NGram autoCompleteTriGram = new NGram(triGramTokenList);
                    List<NGram> completedTriGrams = spokenTriGramSet.ItemList.Where(n => n.TokenString.Contains(autoCompleteTriGram.TokenString + " ")).ToList();

                    for (int i = completedTriGrams.Count - 1; i >= 0; i--)
                    {
                        if (!string.Equals(completedTriGrams[i].TokenList[0], inputTokenList[inputTokenListLength - 2]))
                        {
                            completedTriGrams.RemoveAt(i);
                        }
                    }

                    completedTriGrams = completedTriGrams.OrderByDescending(n => n.NumberOfInstances).ToList();
                    foreach (NGram nGram in completedTriGrams)
                    {
                        string nGramAsString = nGram.AsString();
                        listBox.Items.Add(nGramAsString);
                    }

                    // If no tri-grams are found, suggest potential bi-grams
                    if (completedTriGrams.Count == NO_TRIGRAMS)
                    {
                        List<string> biGramTokenList = new List<string> { inputTokenList[inputTokenListLength - 1] };
                        NGram autoCompleteBiGram = new NGram(biGramTokenList);
                        List<NGram> completedBiGrams = spokenBiGramSet.ItemList.Where(n => n.TokenString.Contains(autoCompleteBiGram.TokenString + " ")).ToList();

                        if (completedBiGrams.Count > NO_BIGRAMS)
                        {
                            for (int i = completedBiGrams.Count - 1; i >= 0; i--)
                            {
                                if (!string.Equals(completedBiGrams[i].TokenList[0], inputTokenList[inputTokenListLength - 1]))
                                {
                                    completedBiGrams.RemoveAt(i);
                                }
                            }
                            completedBiGrams = completedBiGrams.OrderByDescending(n => n.NumberOfInstances).ToList();
                            foreach (NGram nGram in completedBiGrams)
                            {
                                string nGramAsString = nGram.AsString();
                                listBox.Items.Add(nGramAsString);
                            }
                        }
                    }  
                }
                
                // Finding bi-grams for auto completion (for when only one word is typed)
                else if (inputTokenListLength == TRIGRAM_LENGTH_INVALID)
                {    
                    List<string> biGramTokenList = new List<string> { inputTokenList[inputTokenListLength - 1] };
                    NGram autoCompleteBiGram = new NGram(biGramTokenList);
                    List<NGram> completedBiGrams = spokenBiGramSet.ItemList.Where(n => n.TokenString.Contains(autoCompleteBiGram.TokenString + " ")).ToList();
                    
                    if (completedBiGrams.Count > NO_BIGRAMS)
                    {
                        for (int i = completedBiGrams.Count - 1; i >= 0; i--)
                        {
                            if (!string.Equals(completedBiGrams[i].TokenList[0], inputTokenList[inputTokenListLength - 1]))
                            {
                                completedBiGrams.RemoveAt(i);
                            }
                        }
                        completedBiGrams = completedBiGrams.OrderByDescending(n => n.NumberOfInstances).ToList();
                        foreach (NGram nGram in completedBiGrams)
                        {
                            string nGramAsString = nGram.AsString();
                            listBox.Items.Add(nGramAsString);
                        }
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
