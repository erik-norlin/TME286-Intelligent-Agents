using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    public class DialogueCorpus
    {
        private List<DialogueCorpusItem> itemList;
        private Vocabulary vocabulary;

        public DialogueCorpus()
        {
            itemList = new List<DialogueCorpusItem>();
        }

        // To do: Write the Process() method below.

        // The method will be quite complex, so you may (should, probably) implement it
        // in parts, adding more methods as needed, or perhaps more classes.
        // Note, however, that some methods (which you should use) have been added already (see below).
       
        // You may, for example, want to add a class for preprocessing (cleaning) the text, and
        // another class for identifying the text that should be kept (following the
        // three conditions above). For any class that you add, make sure to put it
        // in a *separate* file, named accordingly. Do *not* place multiple classes in
        // the same file.

        // (i)   Preprocess the data, i.e. remove (some) special characters, turn the text to lower-case, and so on.
        // (ii)  Tokenize and generate the vocabulary with the (distinct) tokens from the raw data set(s).
        // (iii) Build sentence pairs (S_1,S_2), making sure that S_2 really follows S_1 in the dialogue.
        //       You can do that by, for example, considering exchanges such as
        //       SpeakerA: ... 
        //       SpeakerB: ...
        //       SpeakerA: ...
        //       In that case, it is very likely that the first two sentences form a pair. If, instead, a third
        //       speaker (SpeakerC) were to give the third utterance (intead of SpeakerA), it is far from certain
        //       that the first two sentences form a valid pair, and so on. In other words: To identify valid
        //       pairs, you must consider exchanges involving *two* speakers, and omitting the final sentence.
        //       Note that you should also discard exchanges involving *long* sentences - see the assignment PDF.
        //
        //       NOTE: An easier way is to use the movie_conversations.txt file that is released along with
        //       the list of sentences (movie_lines.txt) for the Cornell Movie Dialog Corpus.
        //       This file does precisely what is described above! (You just have to figure out how ...)
        // 
        // (iv)  Compute and store (e.g. in the vocabulary) the IDF for each word in the vocabulary, treating 
        //       sentences as documents.
        // (v)   Compute and store normalized TF-IDF vectors for each sentence S_1 of every sentence pair. 

        // If you like, you may also add code for showing the dialogue corpus on-screen, in
        // the dialogueCorpusListBox. However, since the dialogue corpus will be very large,
        // you should only display (say) the first 1000 sentence pairs or so.
        // Here, you can use the AsString() method in the DialogueCorpusItem.

        //  The end result of calling the Process() method should be a list of DialogueCorpusItems added
        //  to the itemList. 
        //
        public void Process(string rawData)
        {
            string cleanData = TextPreprocessing.Clean(rawData);
            List<string> tokenList = TextPreprocessing.Tokenize(cleanData);
            Console.WriteLine("Preprocessing data done");

            GenerateVocabulary(tokenList);
            Console.WriteLine("Generating vocabulary done \t No. of words: " + vocabulary.ItemList.Count);

            BuildSentencePair();
            Console.WriteLine("Building sentences done");

            Console.WriteLine("Processing sentences...");
            Stopwatch stopWatchMovieConversations = new Stopwatch();
            stopWatchMovieConversations.Start();

            ProcessSentencePair();

            stopWatchMovieConversations.Stop();
            TimeSpan timeMovieConversations = stopWatchMovieConversations.Elapsed;
            string timeMovieConv = "Processing sentences done " + timeMovieConversations.ToString(@"m\:ss\.fff");
            Console.WriteLine(timeMovieConv);

            Console.WriteLine("Computing IDF...");
            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();

            // Remove unused words from vocabulary? IF PERFORMANCE IS AN ISSUE, TRY THIS
            ComputeIDFs();

            stopWatch2.Stop();
            TimeSpan time2 = stopWatch2.Elapsed;
            string time22 = "Computing IDF done " + time2.ToString(@"m\:ss\.fff");
            Console.WriteLine(time22);

            Console.WriteLine("Computing TF-IDF...");
            Stopwatch stopWatch3 = new Stopwatch();
            stopWatch3.Start();

            ComputeTFIDFVectors();

            stopWatch3.Stop();
            TimeSpan time3 = stopWatch3.Elapsed;
            string time33 = "Computing TF-IDF done " + time3.ToString(@"m\:ss\.fff");
            Console.WriteLine(time33);

            // show s1 s2, make sure thta \t are removed from s1 s2


            //foreach (DialogueCorpusItem corpusItem in itemList)
            //{
            //    string line = "";
            //    foreach (int tokenIndex in corpusItem.QueryTokenIndexList)
            //    {
            //        line += vocabulary.ItemList[tokenIndex].Word + " ";
            //    }
            //    foreach (int tokenIndex in corpusItem.ResponseTokenIndexList)
            //    {
            //        line += vocabulary.ItemList[tokenIndex].Word + " ";
            //    }
            //    line = line.Trim();
            //    Console.WriteLine(line);
            //    Console.WriteLine(corpusItem.Query + " " + corpusItem.Response);
            //}
        }

        public void BuildSentencePair()
        {
            int MAX_NO_WORDS = 20;

            MovieLines movieLines = new MovieLines();
            foreach (string line in File.ReadLines(@"movieData/movie_lines.tsv")) // CHANGE THIS
            {
                string[] lineData = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                MovieLineItem movieItem = new MovieLineItem();
                movieItem.MovieLabel = lineData[2];
                movieItem.MovieLine = lineData[0].TrimStart('\"');
                movieItem.MovieLineString = lineData[lineData.Count() - 1].Replace('\t',' ').TrimEnd('\"');
                movieLines.ItemList.Add(movieItem);
            }

            MovieLines movieConversations = new MovieLines();
            foreach (string line in File.ReadLines(@"movieData/movie_conversations.tsv")) // CHANGE THIS
            {
                string[] lineData = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                MovieLineItem movieItem = new MovieLineItem();
                movieItem.MovieLabel = lineData[2];
                string movieLinesString = lineData[lineData.Count() - 1].Trim('[').Trim(']');
                List<string> movieLinesList = movieLinesString.Split(new char[] { ' ', '\'' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                movieItem.MovieLinesList = movieLinesList;
                movieConversations.ItemList.Add(movieItem);
            }

            var groupedMovieLines = movieLines.ItemList.GroupBy(n => n.MovieLabel).ToList();
            var groupedMovieConversations = movieConversations.ItemList.GroupBy(n => n.MovieLabel).ToList();
            MoveLineComparer comparer = new MoveLineComparer();

            for (int i = 0; i < groupedMovieConversations.Count; i++)
            {
                List<MovieLineItem> movieLineItems = groupedMovieLines[i].ToList();
                List<MovieLineItem> movieConversationItems = groupedMovieConversations[i].ToList();

                movieLineItems.Sort(comparer);
                foreach (MovieLineItem movieItem in movieConversationItems)
                {
                    List<string> sentences = new List<string>();
                    foreach (string movieLine in movieItem.MovieLinesList)
                    {
                        MovieLineItem movieLineItem = new MovieLineItem();
                        movieLineItem.MovieLine = movieLine;
                        int movieLineIndex = movieLineItems.BinarySearch(movieLineItem, comparer);
                        string sentence = movieLineItems[movieLineIndex].MovieLineString;
                        sentences.Add(sentence);
                    }

                    for (int j = 0; j < sentences.Count - 1; j++)
                    {
                        string s1 = sentences[j];
                        string s2 = sentences[j+1];
                        string[] s1List = s1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] s2List = s2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (s1List.Count() < MAX_NO_WORDS && s2List.Count() < MAX_NO_WORDS)
                        {
                            DialogueCorpusItem corpusItem = new DialogueCorpusItem(s1, s2);
                            itemList.Add(corpusItem);
                        }
                    }
                }
            }
        }

        public void ProcessSentencePair()
        {
            // Removing words from the vocabulary that only appear in the responses
            //for (int i = vocabulary.ItemList.Count - 1; i >= 0; i--)
            //{
            //    bool tokenExist = false;
            //    foreach (DialogueCorpusItem corpusItem in ItemList)
            //    {
            //        string cleanQuery = TextPreprocessing.Clean(corpusItem.Query);
            //        List<string> queryTokenList = TextPreprocessing.Tokenize(cleanQuery);

            //        if (queryTokenList.Contains(vocabulary.ItemList[i].Word))
            //        {
            //            tokenExist = true;
            //            break;
            //        }
            //    }
            //    if (!tokenExist)
            //    {
            //        vocabulary.ItemList.RemoveAt(i);
            //    }
            //    if (i % 1000 == 0) { Console.WriteLine("Iteration: " + i + "     Out of: " + vocabulary.ItemList.Count); }
            //}

            WordDataComparer comparer = new WordDataComparer();
            foreach (DialogueCorpusItem corpusItem in itemList)
            {
                string cleanQuery = TextPreprocessing.Clean(corpusItem.Query);
                List<string> queryTokenList = TextPreprocessing.Tokenize(cleanQuery);
                List<int> queryTokenIndexList = new List<int>();

                foreach (string token in queryTokenList)
                {
                    WordData wordData = new WordData();
                    wordData.Word = token; 
                    int tokenIndex = vocabulary.ItemList.BinarySearch(wordData, comparer);
                    queryTokenIndexList.Add(tokenIndex);
                }
                corpusItem.QueryTokenIndexList = queryTokenIndexList;

                // necessary?
                //string cleanResponse = TextPreprocessing.Clean(corpusItem.Response);
                //List<string> responseTokenList = TextPreprocessing.Tokenize(cleanResponse);
                //List<int> responseTokenIndexList = new List<int>();

                //foreach (string token in responseTokenList)
                //{
                //    WordData wordData = new WordData();
                //    wordData.Word = token;
                //    int tokenIndex = vocabulary.ItemList.BinarySearch(wordData, comparer);
                //    responseTokenIndexList.Add(tokenIndex);
                //}
                //corpusItem.ResponseTokenIndexList = responseTokenIndexList;
            }
        }

        public void GenerateVocabulary(List<string> tokenList)
        {
            vocabulary = new Vocabulary();
            var groupedTokenList = tokenList.GroupBy(i => i);

            foreach (var group in groupedTokenList)
            {
                WordData wordData = new WordData();
                wordData.Word = group.Key;
                vocabulary.ItemList.Add(wordData);
            }
            WordDataComparer comparer = new WordDataComparer();
            vocabulary.ItemList.Sort(comparer);
        }

        // This method should compute the IDF for each word in the vocabulary
        // Note that, here, each sentence (S_1) of a pair counts as a *document*.
        // Once you have preprocessed all the data files, you will have a set of
        // sentence pairs (each stored as a DialogueCorpusItem in the dialogue corpus)
        // where the sentence S_1 (query) forms a document, for the purpose of the IDF
        // calculation here.
        public void ComputeIDFs()
        {
            for (int i = 0; i < vocabulary.ItemList.Count; i++)
            {
                int sentenceCount = 0;
                foreach (DialogueCorpusItem corpusItem in ItemList)
                {
                    if (corpusItem.QueryTokenIndexList.Contains(i))
                    {
                        sentenceCount++;
                    }
                }

                double idf = 0; // IDF-values of some words in the vocabulary will be 0 because
                                // too long sentences are neglected, thus some words are neglected
                if (sentenceCount != 0) 
                {
                    idf = -Math.Log10((double)sentenceCount/itemList.Count);
                    vocabulary.ItemList[i].IDF = idf;
                }

                if (i % 1000 == 0) { Console.WriteLine("Iteration: " + i + "     Out of: " + vocabulary.ItemList.Count); }
                vocabulary.ItemList[i].IDF = idf; 
            }
            //for (int i = 0; i < vocabulary.ItemList.Count - 1; i++) { Console.WriteLine(i + "    " + vocabulary.ItemList[i].Word + "    " + vocabulary.ItemList[i].IDF); }
        }

        public void ComputeTFIDFVectors()
        {
            int counter = 0;
            foreach (DialogueCorpusItem item in itemList)
            {
                item.ComputeTFIDFVector(vocabulary);
                counter++;
                if (counter % 1000 == 0) { Console.WriteLine("Iteration: " + counter + "\t Out of: " + itemList.Count);  }
            }
        }

        public List<DialogueCorpusItem> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }

        public Vocabulary Vocabulary
        {
            get { return vocabulary; }
            set { vocabulary = value; }
        }
    }
}
