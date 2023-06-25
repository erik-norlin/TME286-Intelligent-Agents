using System;
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
using System.Windows.Forms;
using ChatbotLibrary;

namespace IRChatbotApplication
{
    public partial class MainForm : Form
    {
        private const string TEXT_FILTER = "tsv files (*.tsv)|*.tsv";

        private DialogueCorpus corpus = null; // The dialogue corpus, consisting of sentence pairs.
        private Chatbot chatbot;

        private Thread importDataThread;
        private Thread generateCorpusThread;
        private Thread chatbotThread;

        private List<string> rawDataList;
        private string rawDataString;


        public MainForm()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                inputTextBox.InputReceived += new EventHandler<StringEventArgs>(HandleInputReceived);
            }
        }

        private void ImportTextData()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = TEXT_FILTER;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    rawDataList = new List<string>();
                    using (StreamReader dataReader = new StreamReader(openFileDialog.FileName))
                    {
                        while (!dataReader.EndOfStream)
                        {
                            string line = dataReader.ReadLine();
                            List<string> lineSplit = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                            int conversationIndex = lineSplit.Count() - 1;
                            string conversationLine = lineSplit[conversationIndex];
                            rawDataList.Add(conversationLine);
                        }
                        dataReader.Close();
                    }
                    rawDataString = string.Join(" ", rawDataList.ToArray());
                    ThreadSafeToggleButtonEnabled(generateDialogueCorpusButton, true);

                    // Checking so that dialogue_corpus.tsv only contain one tab character
                    //using (StreamReader dataReader = new StreamReader(openFileDialog.FileName))
                    //{
                    //    while (!dataReader.EndOfStream)
                    //    {
                    //        string line = dataReader.ReadLine();
                    //        List<string> lineSplit = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    //        foreach (string lineSplit2 in lineSplit)
                    //        {
                    //            Console.WriteLine(lineSplit2);
                    //        }


                    //    }
                    //    dataReader.Close();
                    //}
                }
            }
        }


        private void loadRawDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadRawDataToolStripMenuItem.Enabled = false;
            importDataThread = new Thread(new ThreadStart(() => ImportTextData()));
            importDataThread.SetApartmentState(ApartmentState.STA); 
            importDataThread.Start();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void generateChatBotButton_Click(object sender, EventArgs e)
        {
            generateChatBotButton.Enabled = false;
            chatbot = new Chatbot();
            chatbot.SetDialogueCorpus(corpus);
            inputTextBox.Enabled = true;
            mainTabControl.SelectedTab = chatTabPage;
        }

        private void GenerateChatbotResponse(string inputSentence)
        {
            chatbot.Initialize();
            string response = chatbot.GenerateResponse(inputSentence);
            ThreadSafeShowChatbotDialogue(inputSentence, response);
        }

        private void HandleInputReceived(object sender, StringEventArgs e)
        {
            string inputSentence = e.Information;
            chatbotThread = new Thread(new ThreadStart(() => GenerateChatbotResponse(inputSentence)));
            chatbotThread.Start();
        }

        private void saveDialogueCorpusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "tsv files (*.tsv)|*.tsv";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter corpusWriter = new StreamWriter(saveFileDialog.FileName))
                    {
                        for (int i = 0; i < corpus.ItemList.Count; i++)
                        {
                            string information = corpus.ItemList[i].AsString();
                            corpusWriter.WriteLine(information);
                        }
                        corpusWriter.Close();
                    }
                }
            }
        }

        public void ShowCorpusDialouge()
        {
            const int MAX_OUTPUT = 1000;
            for (int i = 0; i < MAX_OUTPUT; i++)
            {
                dialogueCorpusListBox.Items.Add(corpus.ItemList[i].AsString());
            }
        }

        private void GenerateCorpus()
        {
            corpus = new DialogueCorpus();
            corpus.Process(rawDataString);
            ThreadShowCorpusDialouge();
            ThreadSafeToggleButtonEnabled(generateChatBotButton, true);
            ThreadSafeToggleMenuItemEnabled(saveDialogueCorpusToolStripMenuItem, true);
        }

        private void generateDialogueCorpusButton_Click(object sender, EventArgs e)
        {
            loadRawDataToolStripMenuItem.Enabled = false;
            generateDialogueCorpusButton.Enabled = false;
            generateCorpusThread = new Thread(new ThreadStart(() => GenerateCorpus()));
            generateCorpusThread.Start();
        }

        private void ShowChatbotDialogue(string inputSentence, string response)
        {
            const int INSERT_INDEX = 0;
            inputTextBox.Clear();
            dialogueListBox.Items.Insert(INSERT_INDEX, "");
            dialogueListBox.Items.Insert(INSERT_INDEX, "Bot:  " + response);
            dialogueListBox.Items.Insert(INSERT_INDEX, "You:  " + inputSentence);
        }

        private void ThreadSafeShowChatbotDialogue(string inputSentence, string response)
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => ShowChatbotDialogue(inputSentence, response))); }
            else { ShowChatbotDialogue(inputSentence, response); }
        }

        private void ThreadShowCorpusDialouge()
        {
            if (InvokeRequired) { this.Invoke(new MethodInvoker(() => ShowCorpusDialouge())); }
            else { ShowCorpusDialouge(); }
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
    }
}
