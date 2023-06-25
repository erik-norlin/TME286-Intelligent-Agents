using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace ChatbotLibrary
{
    public class Chatbot
    {
        private const int FOUND_INDEX = 0;
        protected const int DEFAULT_NUMBER_OF_MATCHES = 5;

        protected DialogueCorpus dialogueCorpus;
        protected Random randomNumberGenerator;
        protected int numberOfMatches = DEFAULT_NUMBER_OF_MATCHES;
        private List<CosineSimilarityItem> itemList;
        List<double> tfIdfVector;
        List<int> tfIdfIndex;

        public virtual void Initialize()
        {
            randomNumberGenerator = new Random();
            itemList = new List<CosineSimilarityItem>();
            tfIdfVector = new List<double>();
            tfIdfIndex = new List<int>();
        }

        public void SetDialogueCorpus(DialogueCorpus dialogueCorpus)
        {
            this.dialogueCorpus = dialogueCorpus;
        }

        public virtual string GenerateResponse(string inputSentence) 
        {
            string cleanInput = TextPreprocessing.Clean(inputSentence);
            List<string> inputTokenList = TextPreprocessing.Tokenize(cleanInput);
            ComputeTFIDFVector(inputTokenList);
            ComputeCosineSimilarities(tfIdfVector, tfIdfIndex);
            string response = PickRespone();

            Console.WriteLine("--------------------------------------------------------------------------------------");
            for (int i = 0; i < 5; i++) { Console.WriteLine(itemList[i].CosineSimilarity + "    " + dialogueCorpus.ItemList[itemList[i].CorpusIndex].Response); }

            return response;
        }

        public string PickRespone()
        {
            CosineSimilarityComparer comparer = new CosineSimilarityComparer();
            itemList.Sort(comparer);

            int responseIndex = randomNumberGenerator.Next(numberOfMatches);
            int corpusIndex = itemList[responseIndex].CorpusIndex;
            string response = dialogueCorpus.ItemList[corpusIndex].Response;

            return response;
        }

        public void ComputeCosineSimilarities(List<double> tfIdfVectorInput, List<int> tfIdfIndexInput)
        {
            for (int i = 0; i < dialogueCorpus.ItemList.Count; i++)
            {
                DialogueCorpusItem corpusItem = dialogueCorpus.ItemList[i];
                double cosineSimilarity = 0;

                for (int j = 0; j < tfIdfIndexInput.Count; j++)
                {
                    int inputIndex = tfIdfIndexInput[j];
                    for (int k = 0; k < corpusItem.TFIDFIndex.Count; k++)
                    {
                        int corpusIndex = corpusItem.TFIDFIndex[k];
                        if (inputIndex == corpusIndex)
                        {
                            cosineSimilarity += tfIdfVectorInput[j] * corpusItem.TFIDFVector[k];
                            break;
                        }
                    }
                }
                CosineSimilarityItem cosineSimilarityItem = new CosineSimilarityItem();
                cosineSimilarityItem.CosineSimilarity = cosineSimilarity;
                cosineSimilarityItem.CorpusIndex = i;
                itemList.Add(cosineSimilarityItem);
            }
        }

        public void ComputeTFIDFVector(List<string> inputTokenList)
        {
            WordDataComparer comparer = new WordDataComparer();
            List<int> inputTokenIndexList = new List<int>();

            foreach (string token in inputTokenList)
            {
                WordData wordData = new WordData();
                wordData.Word = token;
                int tokenIndex = dialogueCorpus.Vocabulary.ItemList.BinarySearch(wordData, comparer);
                if (tokenIndex >= FOUND_INDEX)
                {
                    inputTokenIndexList.Add(tokenIndex);
                }
            }

            var groupedTokenIndices = inputTokenIndexList.GroupBy(i => i);
            foreach (var groupedTokenIndex in groupedTokenIndices)
            {
                int tokenIndex = groupedTokenIndex.Key;
                double tf = (double)groupedTokenIndex.Count();
                double idf = dialogueCorpus.Vocabulary.ItemList[tokenIndex].IDF;
                double tfidf = tf * idf;
                tfIdfVector.Add(tfidf);
                tfIdfIndex.Add(tokenIndex);
            }

            double norm = 0;
            foreach (double tfidf in tfIdfVector)
            {
                norm += Math.Pow(tfidf, 2);
            }

            for (int i = 0; i < tfIdfVector.Count; i++)
            {
                tfIdfVector[i] = tfIdfVector[i] / Math.Sqrt(norm);
            }
        }
    }
}
