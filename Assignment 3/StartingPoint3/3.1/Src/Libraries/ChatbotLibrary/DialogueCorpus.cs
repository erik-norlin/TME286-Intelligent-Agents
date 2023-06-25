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

        public void Process(string rawData)
        {
            string cleanData = TextPreprocessing.Clean(rawData);
            List<string> tokenList = TextPreprocessing.Tokenize(cleanData);
            Console.WriteLine("Preprocessing DONE");

            GenerateVocabulary(tokenList);
            Console.WriteLine("Vocabulary DONE \t No. of words: " + vocabulary.ItemList.Count);

            BuildSentencePair();
            ProcessSentencePair();
            Console.WriteLine("Sentence pairs DONE");

            Stopwatch stopWatch2 = new Stopwatch();
            stopWatch2.Start();

            ComputeIDFs();

            stopWatch2.Stop();
            TimeSpan time2 = stopWatch2.Elapsed;
            string time22 = "IDF values DONE " + time2.ToString(@"m\:ss\.fff");
            Console.WriteLine(time22);

            ComputeTFIDFVectors();
            Console.WriteLine("TF-IDF vectors DONE ");
        }

        public void BuildSentencePair()
        {
            const int MAX_NO_WORDS = 20;

            // Storing all movie lines 
            MovieLines movieLines = new MovieLines();
            foreach (string line in File.ReadLines(@"movieData/movie_lines.tsv"))
            {
                string[] lineData = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                MovieLineItem movieItem = new MovieLineItem();
                movieItem.MovieLabel = lineData[2];
                movieItem.MovieLine = lineData[0].TrimStart('\"');
                movieItem.MovieLineString = lineData[lineData.Count() - 1].Replace('\t',' ').TrimEnd('\"');
                movieLines.ItemList.Add(movieItem);
            }

            // Storing all consecutive move line ID's between two characters
            MovieLines movieConversations = new MovieLines();
            foreach (string line in File.ReadLines(@"movieData/movie_conversations.tsv"))
            {
                string[] lineData = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                MovieLineItem movieItem = new MovieLineItem();
                movieItem.MovieLabel = lineData[2];
                string movieLinesString = lineData[lineData.Count() - 1].Trim('[').Trim(']');
                List<string> movieLinesList = movieLinesString.Split(new char[] { ' ', '\'' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                movieItem.MovieLinesList = movieLinesList;
                movieConversations.ItemList.Add(movieItem);
            }

            // Grouping by movie label
            var groupedMovieLines = movieLines.ItemList.GroupBy(n => n.MovieLabel).ToList();
            var groupedMovieConversations = movieConversations.ItemList.GroupBy(n => n.MovieLabel).ToList();
            MoveLineComparer comparer = new MoveLineComparer();

            // Iterating through every movie label [i]
            for (int i = 0; i < groupedMovieConversations.Count; i++)
            {
                List<MovieLineItem> movieLineItems = groupedMovieLines[i].ToList();
                List<MovieLineItem> movieConversationItems = groupedMovieConversations[i].ToList();

                movieLineItems.Sort(comparer);

                // Iterating through every dialouge between two characters in movie [i]
                foreach (MovieLineItem movieItem in movieConversationItems)
                {
                    List<string> sentences = new List<string>();

                    // Searching for every consecutive movie line based on their ID's
                    foreach (string movieLine in movieItem.MovieLinesList)
                    {
                        MovieLineItem movieLineItem = new MovieLineItem();
                        movieLineItem.MovieLine = movieLine;
                        int movieLineIndex = movieLineItems.BinarySearch(movieLineItem, comparer);
                        string sentence = movieLineItems[movieLineIndex].MovieLineString;
                        sentences.Add(sentence);
                    }

                    // Paring queries and responses
                    for (int j = 0; j < sentences.Count - 1; j++)
                    {
                        string s1 = sentences[j].Trim();
                        string s2 = sentences[j+1].Trim();
                        string[] s1List = s1.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] s2List = s2.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (s1List.Count() < MAX_NO_WORDS && s2List.Count() < MAX_NO_WORDS)
                        {
                            DialogueCorpusItem corpusItem = new DialogueCorpusItem(s1, s2);
                            itemList.Add(corpusItem);
                        }
                    }
                }
                movieLines = null;
                movieConversations = null;
            }
        }

        public void ProcessSentencePair()
        {
            WordDataComparer comparer = new WordDataComparer();

            // Cleaning, tokenizing, and creating token index lists for all queries.
            // This allows for indexing to each respective word in the vocabulary.
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
                 
                if (i % 1000 == 0) { Console.WriteLine("Computing IDFs:      Iteration: " + i + "  /  " + vocabulary.ItemList.Count); }
                vocabulary.ItemList[i].IDF = idf;  
            }
        }

        public void ComputeTFIDFVectors()
        {
            foreach (DialogueCorpusItem item in itemList)
            {
                item.ComputeTFIDFVector(vocabulary);
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
