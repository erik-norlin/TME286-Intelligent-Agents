
namespace IRChatbotApplication
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRawDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDialogueCorpusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.dataTabPage = new System.Windows.Forms.TabPage();
            this.dialogueCorpusListBox = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.generateDialogueCorpusButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.generateChatBotButton = new System.Windows.Forms.ToolStripButton();
            this.chatTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dialogueListBox = new System.Windows.Forms.ListBox();
            this.inputTextBox = new ChatbotLibrary.InputTextBox();
            this.menuStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.dataTabPage.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.chatTabPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1822, 36);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadRawDataToolStripMenuItem,
            this.saveDialogueCorpusToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 30);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadRawDataToolStripMenuItem
            // 
            this.loadRawDataToolStripMenuItem.Name = "loadRawDataToolStripMenuItem";
            this.loadRawDataToolStripMenuItem.Size = new System.Drawing.Size(284, 34);
            this.loadRawDataToolStripMenuItem.Text = "Load raw data";
            this.loadRawDataToolStripMenuItem.Click += new System.EventHandler(this.loadRawDataToolStripMenuItem_Click);
            // 
            // saveDialogueCorpusToolStripMenuItem
            // 
            this.saveDialogueCorpusToolStripMenuItem.Enabled = false;
            this.saveDialogueCorpusToolStripMenuItem.Name = "saveDialogueCorpusToolStripMenuItem";
            this.saveDialogueCorpusToolStripMenuItem.Size = new System.Drawing.Size(284, 34);
            this.saveDialogueCorpusToolStripMenuItem.Text = "Save dialogue corpus";
            this.saveDialogueCorpusToolStripMenuItem.Click += new System.EventHandler(this.saveDialogueCorpusToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(284, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.dataTabPage);
            this.mainTabControl.Controls.Add(this.chatTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 36);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1822, 1006);
            this.mainTabControl.TabIndex = 1;
            // 
            // dataTabPage
            // 
            this.dataTabPage.Controls.Add(this.dialogueCorpusListBox);
            this.dataTabPage.Controls.Add(this.toolStrip1);
            this.dataTabPage.Location = new System.Drawing.Point(4, 29);
            this.dataTabPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataTabPage.Name = "dataTabPage";
            this.dataTabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataTabPage.Size = new System.Drawing.Size(1814, 973);
            this.dataTabPage.TabIndex = 0;
            this.dataTabPage.Text = "Data";
            this.dataTabPage.UseVisualStyleBackColor = true;
            // 
            // dialogueCorpusListBox
            // 
            this.dialogueCorpusListBox.BackColor = System.Drawing.Color.Black;
            this.dialogueCorpusListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dialogueCorpusListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dialogueCorpusListBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dialogueCorpusListBox.ForeColor = System.Drawing.Color.Lime;
            this.dialogueCorpusListBox.FormattingEnabled = true;
            this.dialogueCorpusListBox.ItemHeight = 17;
            this.dialogueCorpusListBox.Location = new System.Drawing.Point(4, 39);
            this.dialogueCorpusListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dialogueCorpusListBox.Name = "dialogueCorpusListBox";
            this.dialogueCorpusListBox.Size = new System.Drawing.Size(1806, 929);
            this.dialogueCorpusListBox.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateDialogueCorpusButton,
            this.toolStripSeparator1,
            this.generateChatBotButton});
            this.toolStrip1.Location = new System.Drawing.Point(4, 5);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1806, 34);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // generateDialogueCorpusButton
            // 
            this.generateDialogueCorpusButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.generateDialogueCorpusButton.Enabled = false;
            this.generateDialogueCorpusButton.Image = ((System.Drawing.Image)(resources.GetObject("generateDialogueCorpusButton.Image")));
            this.generateDialogueCorpusButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.generateDialogueCorpusButton.Name = "generateDialogueCorpusButton";
            this.generateDialogueCorpusButton.Size = new System.Drawing.Size(219, 29);
            this.generateDialogueCorpusButton.Text = "Generate dialogue corpus";
            this.generateDialogueCorpusButton.Click += new System.EventHandler(this.generateDialogueCorpusButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // generateChatBotButton
            // 
            this.generateChatBotButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.generateChatBotButton.Enabled = false;
            this.generateChatBotButton.Image = ((System.Drawing.Image)(resources.GetObject("generateChatBotButton.Image")));
            this.generateChatBotButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.generateChatBotButton.Name = "generateChatBotButton";
            this.generateChatBotButton.Size = new System.Drawing.Size(152, 29);
            this.generateChatBotButton.Text = "Generate chatbot";
            this.generateChatBotButton.Click += new System.EventHandler(this.generateChatBotButton_Click);
            // 
            // chatTabPage
            // 
            this.chatTabPage.Controls.Add(this.tableLayoutPanel1);
            this.chatTabPage.Location = new System.Drawing.Point(4, 29);
            this.chatTabPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chatTabPage.Name = "chatTabPage";
            this.chatTabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chatTabPage.Size = new System.Drawing.Size(1814, 973);
            this.chatTabPage.TabIndex = 1;
            this.chatTabPage.Text = "Chat";
            this.chatTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.inputTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dialogueListBox, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 5);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.441224F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.55878F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1806, 963);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dialogueListBox
            // 
            this.dialogueListBox.BackColor = System.Drawing.Color.Black;
            this.dialogueListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dialogueListBox.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dialogueListBox.ForeColor = System.Drawing.Color.Lime;
            this.dialogueListBox.FormattingEnabled = true;
            this.dialogueListBox.ItemHeight = 23;
            this.dialogueListBox.Location = new System.Drawing.Point(4, 67);
            this.dialogueListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dialogueListBox.Name = "dialogueListBox";
            this.dialogueListBox.Size = new System.Drawing.Size(1798, 891);
            this.dialogueListBox.TabIndex = 1;
            // 
            // inputTextBox
            // 
            this.inputTextBox.BackColor = System.Drawing.Color.Black;
            this.inputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTextBox.Enabled = false;
            this.inputTextBox.Font = new System.Drawing.Font("Lucida Console", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputTextBox.ForeColor = System.Drawing.Color.Lime;
            this.inputTextBox.Location = new System.Drawing.Point(4, 5);
            this.inputTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(1798, 52);
            this.inputTextBox.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1822, 1042);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Information-retrieval chatbot";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.dataTabPage.ResumeLayout(false);
            this.dataTabPage.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.chatTabPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRawDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage dataTabPage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton generateDialogueCorpusButton;
        private System.Windows.Forms.TabPage chatTabPage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton generateChatBotButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ChatbotLibrary.InputTextBox inputTextBox;
        private System.Windows.Forms.ListBox dialogueListBox;
        private System.Windows.Forms.ListBox dialogueCorpusListBox;
        private System.Windows.Forms.ToolStripMenuItem saveDialogueCorpusToolStripMenuItem;
    }
}