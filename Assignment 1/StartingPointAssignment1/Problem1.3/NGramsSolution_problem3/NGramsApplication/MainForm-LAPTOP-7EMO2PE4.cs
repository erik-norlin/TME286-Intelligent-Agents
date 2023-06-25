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

        // this method should display items in the analysis text box (it should be
        // called by ThreadSafeShowAnalysis().
        private void ShowAnalysis(List<string> analysisList)
        {

            // Add code here
            analysisTextBox.Text = string.Join("\n", analysisList);

        }

        // This method should, in turn, call ShowAnalysis() in a thread-safe manner
        private void ThreadSafeShowAnalysis(List<string> analysisList)
        {

            // Add code here
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


        // ========================================================
        // Here, write methods for
        // ========================================================
        //
        // * Finding the 300 most common 1-grams, 2-grams, 3-grams in each set
        //     For the unigrams (1-grams), use the dictionary that you generated above.
        //     You must generate the sets of bigrams (2-grams) and trigrams (3-grams), using
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

            // Add your analysis as a list of strings (that can then be shown on screen and saved to file).
            analysisList = new List<string>();

            // Step (1)
            // Find the 300 most common unigrams (from the dictionaries, after sorting
            //     the dictionary items based on the number of instances:

            writtenUniGramSet = new NGramSet();

             // Add code here


            spokenUniGramSet = new NGramSet();

             // Add code here

            // Step (2) 
            // Find the 300 most common bigrams, after generating the bigram sets,
            //     one for the written data and one for the spoken using the NGramSet class 
            //    (see the NGramSet class).
            writtenBiGramSet = new NGramSet();

            // Add code here for generating and sorting the written bigram set.
            // Before sorting, run through the list and remove rare bigrams (speeds up
            // the sorting - we are only interested in the most frequent bigrams anyway;
            // see also the assignment text.

            // Not using NGramComparer?????
            /*
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //int itWasCount = 0;
            //int itIsCount = 0;
            //int isACount = 0;
            //int heWasCount = 0;
            //int inTheCount = 0;
            // Creating all bigrams and counting them
            foreach (Sentence sentence in writtenDataSet.SentenceList)
            {
                //Console.WriteLine(sentence.Text + "     " + sentence.TokenList.Count);

                int noOfBiGrams = sentence.TokenList.Count - 1;
                for (int i = 0; i <= noOfBiGrams - 1; i++)
                {
                    //Console.WriteLine(sentence.TokenList[i]);
                    string token1 = sentence.TokenList[i];
                    string token2 = sentence.TokenList[i+1];
                    string nGramString = token1 + ' ' + token2;
                    bool addBiGram = true;

                    //Checking if ngram counter works 
                    //if (string.Equals(nGramString, "it was"))
                    //{
                    //    itWasCount++;
                    //}
                    //if (string.Equals(nGramString, "it is"))
                    //{
                    //    itIsCount++;
                    //}
                    //if (string.Equals(nGramString, "is a"))
                    //{
                    //    isACount++;
                    //}
                    //if (string.Equals(nGramString, "he was"))
                    //{
                    //    heWasCount++;
                    //}
                    //if (string.Equals(nGramString, "in the"))
                    //{
                    //    inTheCount++;
                    //}

                    foreach (NGram nGram in writtenBiGramSet.ItemList)
                    {
                        if (string.Equals(nGram.TokenString, nGramString))
                        {
                            nGram.NumberOfInstances++;
                            addBiGram = false;
                            break;
                        }
                        
                    }
                    if (addBiGram)
                    {
                        List<string> tokenList = new List<string>
                        {
                            token1,
                            token2
                        };
                        writtenBiGramSet.Append(tokenList);
                    }
                }
            }

            //Console.WriteLine("it was   " + itWasCount);
            //Console.WriteLine("it is    " + itIsCount);
            //Console.WriteLine("is a     " + isACount);
            //Console.WriteLine("he was   " + heWasCount);
            //Console.WriteLine("in the   " + inTheCount);


            stopWatch.Stop();
            TimeSpan timeTaken = stopWatch.Elapsed;
            string time = "Creating bigrams: " + timeTaken.ToString(@"m\:ss\.fff");
            Console.WriteLine(time);

            // Removing bigrams with less than three occurences

            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();

            //foreach (NGram nGram in tempWrittenBiGramSet.ItemList)
            //{
            //    if (nGram.NumberOfInstances >= 3)
            //    {
            //        writtenBiGramSet.Append(nGram.TokenList); // only adds tokenlist!!!
            //        //add NumberOfInstances, new ngram doesn't have this

            //        Console.WriteLine(nGram.TokenString + "     " + nGram.NumberOfInstances);
            //        //Console.WriteLine(biGram.TokenString + "    " + biGram.NumberOfInstances);
            //    }
            //}

            int iGram = 0;
            bool valid = true;
            while (valid)
            {
                NGram biGram = writtenBiGramSet.ItemList[iGram];
                if (biGram.NumberOfInstances < 3)
                {
                    writtenBiGramSet.ItemList.Remove(biGram);
                }
                else
                {
                    iGram++;
                }
                if (iGram == writtenBiGramSet.ItemList.Count)
                {
                    valid = false;
                }
                //if (iGram > 0)
                //{
                //    Console.WriteLine(writtenBiGramSet.ItemList[iGram-1].TokenString + "    " + writtenBiGramSet.ItemList[iGram-1].NumberOfInstances);
                //}
            }

            stopWatch.Stop();
            TimeSpan timeTaken2 = stopWatch.Elapsed;
            string time2 = "Removing rare bigrams: " + timeTaken2.ToString(@"m\:ss\.fff");
            Console.WriteLine(time2);

            writtenBiGramSet.SortOnFrequency();


            //foreach (NGram nGram in writtenBiGramSet.ItemList)
            //{
            //    Console.WriteLine(nGram.TokenString + "     " + nGram.NumberOfInstances);
            //}
            */

            analysisList.Add("=========================================");
            analysisList.Add("Written 2-grams: ");
            analysisList.Add("=========================================");
            //for (int ii = 0; ii < NUMBER_OF_N_GRAMS_SHOWN; ii++)
            //{
            //    analysisList.Add(writtenBiGramSet.ItemList[ii].AsString());
            //}


            spokenBiGramSet = new NGramSet();

             // Add code here for generating and sorting the spoken bigram set.
             // Before sorting, run through the list and remove rare bigrams (speeds up
             // the sorting - we are only interested in the most frequent bigrams anyway;
             // see also the assignment text.

            analysisList.Add("=========================================");
            analysisList.Add("Spoken 2-grams: ");
            analysisList.Add("=========================================");
            //for (int ii = 0; ii < NUMBER_OF_N_GRAMS_SHOWN; ii++)
            //{
            //    analysisList.Add(spokenBiGramSet.ItemList[ii].AsString());
            //}

            // Step (3) 
            //     Find the 300 most common trigrams (after generating the trigram set.
            //     using the NGramSet class (where you have to write code for appending
            //     n-grams, making sure to keep them sorted in alphabetical order based
            //     on the full token string (see the NGramSet class)

            // Add code here for generating and sorting the written trigrams set.
            // Before sorting, run through the list and remove rare trigrams (speeds up
            // the sorting - we are only interested in the most frequent trigram anyway;
            // see also the assignment text.

            analysisList.Add("=========================================");
            analysisList.Add("Written 3-grams: ");
            analysisList.Add("=========================================");
            //for (int ii = 0; ii < NUMBER_OF_N_GRAMS_SHOWN; ii++)
            //{
            //    analysisList.Add(writtenTriGramSet.ItemList[ii].AsString());
            //}

            spokenTriGramSet = new NGramSet();

            // Add code here for generating and sorting the spoken trigram set.
            // Before sorting, run through the list and remove rare trigrams (speeds up
            // the sorting - we are only interested in the most frequent trigrams anyway;
            // see also the assignment text.

            analysisList.Add("=========================================");
            analysisList.Add("Spoken 3-grams: ");
            analysisList.Add("=========================================");
            //for (int ii = 0; ii < NUMBER_OF_N_GRAMS_SHOWN; ii++)
            //{
            //    analysisList.Add(spokenTriGramSet.ItemList[ii].AsString());
            //}

            // Step (4) Using the dictionaries (one for the written set and one for the spoken),
            // Find the 50 tokens with the largest values of r and the 50 tokens with the
            // smallest values of r. Consider only tokens that are present (with at least
            // one instance) in both the written and the spoken data sets.
            //
            // Here, you can run through one of the dictionaries (e.g. the spoken one, which
            // is probably smaller) token by token, then check if the corresponding token
            // can be found in the other dictionary. If yes, then compute the ratio r,
            // and store the information. To that end, you can define new classes, 
            // similar to the NGramSet and NGram classes, but with items
            // consisting of (i) the token and (ii) the value of r. Then sort on r,
            // and get the top 50 elements as well as the 50 bottom elements ...
            // You'll need a custom comparer too which uses the 
            // value of r as the basis for comparison.
            //
            // There are other ways - this is just a suggestion... You can, for example,
            // use the "Tuple" concept in C#, defining a tuple (string, double) with
            // tokens and r-values, then make a list of such tuples and sort it (based on r, i.e. "Item2";
            // see the definition of the Tuple concept e.g. on MSDN.
            // In that case, use: List<Tuple<string, double>> tokenRTupleList = new List<Tuple<string, double>>();

            //
            // Add code here:

            RRatioComparer ratioComparer = new RRatioComparer();
            RRatioSet ratioSet = new RRatioSet();

            int test = 0;
            foreach (DictionaryItem spokenItem in spokenDataSet.Dictionary.ItemList)
            {
                DictionaryItem writtenItem = writtenDataSet.Dictionary.ItemList.Find(item => item.Token == spokenItem.Token); // correct? .Equals?
                if (writtenItem != null)
                {
                    double ratio = (double)writtenItem.Count / spokenItem.Count;
                    if (test < 6)
                    {
                        Console.WriteLine(writtenItem.Token + "     Ratio: " + ratio + "   Written count: " + writtenItem.Count + "   Spoken count: " + spokenItem.Count);
                        test++;
                    }

                    RRatio ratioItem = new RRatio(writtenItem.Token, ratio);
                    ratioSet.Append(ratioItem);
                }
                //else
                //{
                //    Console.WriteLine("Unique spoken item: " + spokenItem.Token);
                //}
                //foreach (DictionaryItem item in spokenDataSet.Dictionary.ItemList)
                //{
                //    string.Equals(item.Token,
                //}
            }

            ratioSet.ItemList.Sort(ratioComparer);
            foreach (RRatio ratioItem in ratioSet.ItemList)
            {
                Console.WriteLine(ratioItem.Token.PadRight(15) + ratioItem.Ratio);
            }



            // Then store information about the number of tokens in each set, as well as the number of shared tokens:
            analysisList.Add("=========================================");
            analysisList.Add("Number of tokens:");
            analysisList.Add("=========================================");
            analysisList.Add("Spoken set:  " + spokenDataSet.Dictionary.ItemList.Count.ToString());
            analysisList.Add("Written set: " + writtenDataSet.Dictionary.ItemList.Count.ToString());
            analysisList.Add("Shared:      " + ratioSet.ItemList.Count());

            // Then store information about the 50 tokens with the highest r-values:
            analysisList.Add("=========================================");
            analysisList.Add("High-r tokens: ");
            analysisList.Add("=========================================");

            // Add code here
            for (int i = 0; i < 50; i++)
            {
                RRatio ratioItem = ratioSet.ItemList[i];
                analysisList.Add("Token: " + ratioItem + "Ratio: ".PadRight(26) + ratioItem.Ratio);
            }

            // Then store information about the 50 tokens with the lowest r-values:
            analysisList.Add("=========================================");
            analysisList.Add("Small-r tokens: ");
            analysisList.Add("=========================================");

            // Add code here
            int iterations = ratioSet.ItemList.Count;
            for (int i = iterations - 50; i < iterations; i++)
            {
                RRatio ratioItem = ratioSet.ItemList[i];
                analysisList.Add("Token: " + ratioItem + "Ratio: ".PadRight(26) + ratioItem.Ratio);
            }

            // =================================================================
            // (5) Write a thread-safe method here for
            // display the analysis in the analysisTextBox
            // see also ThreadSafeToggleMenuItemEnabled(), to see how it's done. 
            // Hint: The ThreadSafeMethod (that uses "Invoke..." can itself
            // call a standard (non-thread safe) method, in case one needs to
            // carry out more operations than just a single assignment...)
            // See also Appendix A.4 in the compendium.
            // ==================================================================

            ThreadSafeShowAnalysis(analysisList);

            // =============================
            // Don't change below this line:
            // =============================
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
