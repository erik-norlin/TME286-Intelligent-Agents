namespace NaiveBayesApplication
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
            this.loadTrainingDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTestDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadStopWordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.dataTabPage = new System.Windows.Forms.TabPage();
            this.dataSplitContainer = new System.Windows.Forms.SplitContainer();
            this.trainingDocumentListBox = new System.Windows.Forms.ListBox();
            this.testDocumentListBox = new System.Windows.Forms.ListBox();
            this.classificationTabPage = new System.Windows.Forms.TabPage();
            this.classificationProgressListBox = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cleanButton = new System.Windows.Forms.ToolStripButton();
            this.tokenizeButton = new System.Windows.Forms.ToolStripButton();
            this.removeStopWordsButton = new System.Windows.Forms.ToolStripButton();
            this.findPriorProbabilitiesButton = new System.Windows.Forms.ToolStripButton();
            this.findConditionalProbabilitiesButton = new System.Windows.Forms.ToolStripButton();
            this.classifyAllButton = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.dataTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSplitContainer)).BeginInit();
            this.dataSplitContainer.Panel1.SuspendLayout();
            this.dataSplitContainer.Panel2.SuspendLayout();
            this.dataSplitContainer.SuspendLayout();
            this.classificationTabPage.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1916, 35);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTrainingDocumentsToolStripMenuItem,
            this.loadTestDocumentsToolStripMenuItem,
            this.loadStopWordsToolStripMenuItem,
            this.saveAnalysisToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadTrainingDocumentsToolStripMenuItem
            // 
            this.loadTrainingDocumentsToolStripMenuItem.Name = "loadTrainingDocumentsToolStripMenuItem";
            this.loadTrainingDocumentsToolStripMenuItem.Size = new System.Drawing.Size(312, 34);
            this.loadTrainingDocumentsToolStripMenuItem.Text = "Load training documents";
            this.loadTrainingDocumentsToolStripMenuItem.Click += new System.EventHandler(this.loadTrainingDocumentsToolStripMenuItem_Click);
            // 
            // loadTestDocumentsToolStripMenuItem
            // 
            this.loadTestDocumentsToolStripMenuItem.Name = "loadTestDocumentsToolStripMenuItem";
            this.loadTestDocumentsToolStripMenuItem.Size = new System.Drawing.Size(312, 34);
            this.loadTestDocumentsToolStripMenuItem.Text = "Load test documents";
            this.loadTestDocumentsToolStripMenuItem.Click += new System.EventHandler(this.loadTestDocumentsToolStripMenuItem_Click);
            // 
            // loadStopWordsToolStripMenuItem
            // 
            this.loadStopWordsToolStripMenuItem.Name = "loadStopWordsToolStripMenuItem";
            this.loadStopWordsToolStripMenuItem.Size = new System.Drawing.Size(312, 34);
            this.loadStopWordsToolStripMenuItem.Text = "Load stop words";
            this.loadStopWordsToolStripMenuItem.Click += new System.EventHandler(this.loadStopWordsToolStripMenuItem_Click);
            // 
            // saveAnalysisToolStripMenuItem
            // 
            this.saveAnalysisToolStripMenuItem.Enabled = false;
            this.saveAnalysisToolStripMenuItem.Name = "saveAnalysisToolStripMenuItem";
            this.saveAnalysisToolStripMenuItem.Size = new System.Drawing.Size(312, 34);
            this.saveAnalysisToolStripMenuItem.Text = "Save analysis";
            this.saveAnalysisToolStripMenuItem.Click += new System.EventHandler(this.saveAnalysisToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(312, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.dataTabPage);
            this.mainTabControl.Controls.Add(this.classificationTabPage);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 35);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1916, 1015);
            this.mainTabControl.TabIndex = 1;
            // 
            // dataTabPage
            // 
            this.dataTabPage.Controls.Add(this.dataSplitContainer);
            this.dataTabPage.Location = new System.Drawing.Point(4, 29);
            this.dataTabPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataTabPage.Name = "dataTabPage";
            this.dataTabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataTabPage.Size = new System.Drawing.Size(1908, 982);
            this.dataTabPage.TabIndex = 3;
            this.dataTabPage.Text = "Data";
            this.dataTabPage.UseVisualStyleBackColor = true;
            // 
            // dataSplitContainer
            // 
            this.dataSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataSplitContainer.Location = new System.Drawing.Point(4, 5);
            this.dataSplitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataSplitContainer.Name = "dataSplitContainer";
            this.dataSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // dataSplitContainer.Panel1
            // 
            this.dataSplitContainer.Panel1.Controls.Add(this.trainingDocumentListBox);
            // 
            // dataSplitContainer.Panel2
            // 
            this.dataSplitContainer.Panel2.Controls.Add(this.testDocumentListBox);
            this.dataSplitContainer.Size = new System.Drawing.Size(1900, 972);
            this.dataSplitContainer.SplitterDistance = 462;
            this.dataSplitContainer.SplitterWidth = 6;
            this.dataSplitContainer.TabIndex = 0;
            // 
            // trainingDocumentListBox
            // 
            this.trainingDocumentListBox.BackColor = System.Drawing.Color.Black;
            this.trainingDocumentListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trainingDocumentListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trainingDocumentListBox.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trainingDocumentListBox.ForeColor = System.Drawing.Color.Lime;
            this.trainingDocumentListBox.FormattingEnabled = true;
            this.trainingDocumentListBox.IntegralHeight = false;
            this.trainingDocumentListBox.ItemHeight = 18;
            this.trainingDocumentListBox.Location = new System.Drawing.Point(0, 0);
            this.trainingDocumentListBox.Name = "trainingDocumentListBox";
            this.trainingDocumentListBox.Size = new System.Drawing.Size(1900, 462);
            this.trainingDocumentListBox.TabIndex = 2;
            // 
            // testDocumentListBox
            // 
            this.testDocumentListBox.BackColor = System.Drawing.Color.Black;
            this.testDocumentListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.testDocumentListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.testDocumentListBox.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testDocumentListBox.ForeColor = System.Drawing.Color.Lime;
            this.testDocumentListBox.FormattingEnabled = true;
            this.testDocumentListBox.IntegralHeight = false;
            this.testDocumentListBox.ItemHeight = 18;
            this.testDocumentListBox.Location = new System.Drawing.Point(0, 0);
            this.testDocumentListBox.Name = "testDocumentListBox";
            this.testDocumentListBox.Size = new System.Drawing.Size(1900, 504);
            this.testDocumentListBox.TabIndex = 3;
            // 
            // classificationTabPage
            // 
            this.classificationTabPage.Controls.Add(this.classificationProgressListBox);
            this.classificationTabPage.Controls.Add(this.toolStrip1);
            this.classificationTabPage.Location = new System.Drawing.Point(4, 29);
            this.classificationTabPage.Name = "classificationTabPage";
            this.classificationTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.classificationTabPage.Size = new System.Drawing.Size(1908, 982);
            this.classificationTabPage.TabIndex = 1;
            this.classificationTabPage.Text = "Classification";
            this.classificationTabPage.UseVisualStyleBackColor = true;
            // 
            // classificationProgressListBox
            // 
            this.classificationProgressListBox.BackColor = System.Drawing.Color.Black;
            this.classificationProgressListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.classificationProgressListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.classificationProgressListBox.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.classificationProgressListBox.ForeColor = System.Drawing.Color.Lime;
            this.classificationProgressListBox.FormattingEnabled = true;
            this.classificationProgressListBox.IntegralHeight = false;
            this.classificationProgressListBox.ItemHeight = 18;
            this.classificationProgressListBox.Location = new System.Drawing.Point(3, 37);
            this.classificationProgressListBox.Name = "classificationProgressListBox";
            this.classificationProgressListBox.Size = new System.Drawing.Size(1902, 942);
            this.classificationProgressListBox.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cleanButton,
            this.tokenizeButton,
            this.removeStopWordsButton,
            this.findPriorProbabilitiesButton,
            this.findConditionalProbabilitiesButton,
            this.classifyAllButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1902, 34);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cleanButton
            // 
            this.cleanButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cleanButton.Enabled = false;
            this.cleanButton.Image = ((System.Drawing.Image)(resources.GetObject("cleanButton.Image")));
            this.cleanButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.Size = new System.Drawing.Size(59, 29);
            this.cleanButton.Text = "Clean";
            this.cleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // tokenizeButton
            // 
            this.tokenizeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tokenizeButton.Enabled = false;
            this.tokenizeButton.Image = ((System.Drawing.Image)(resources.GetObject("tokenizeButton.Image")));
            this.tokenizeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tokenizeButton.Name = "tokenizeButton";
            this.tokenizeButton.Size = new System.Drawing.Size(83, 29);
            this.tokenizeButton.Text = "Tokenize";
            this.tokenizeButton.Click += new System.EventHandler(this.tokenizeButton_Click);
            // 
            // removeStopWordsButton
            // 
            this.removeStopWordsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.removeStopWordsButton.Enabled = false;
            this.removeStopWordsButton.Image = ((System.Drawing.Image)(resources.GetObject("removeStopWordsButton.Image")));
            this.removeStopWordsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.removeStopWordsButton.Name = "removeStopWordsButton";
            this.removeStopWordsButton.Size = new System.Drawing.Size(175, 29);
            this.removeStopWordsButton.Text = "Remove stop words";
            this.removeStopWordsButton.Click += new System.EventHandler(this.removeStopWordsButton_Click);
            // 
            // findPriorProbabilitiesButton
            // 
            this.findPriorProbabilitiesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.findPriorProbabilitiesButton.Enabled = false;
            this.findPriorProbabilitiesButton.Image = ((System.Drawing.Image)(resources.GetObject("findPriorProbabilitiesButton.Image")));
            this.findPriorProbabilitiesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findPriorProbabilitiesButton.Name = "findPriorProbabilitiesButton";
            this.findPriorProbabilitiesButton.Size = new System.Drawing.Size(196, 29);
            this.findPriorProbabilitiesButton.Text = "Find prior probabilities";
            this.findPriorProbabilitiesButton.Click += new System.EventHandler(this.findPriorProbabilitiesButton_Click);
            // 
            // findConditionalProbabilitiesButton
            // 
            this.findConditionalProbabilitiesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.findConditionalProbabilitiesButton.Enabled = false;
            this.findConditionalProbabilitiesButton.Image = ((System.Drawing.Image)(resources.GetObject("findConditionalProbabilitiesButton.Image")));
            this.findConditionalProbabilitiesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findConditionalProbabilitiesButton.Name = "findConditionalProbabilitiesButton";
            this.findConditionalProbabilitiesButton.Size = new System.Drawing.Size(246, 29);
            this.findConditionalProbabilitiesButton.Text = "Find conditional probabilities";
            this.findConditionalProbabilitiesButton.Click += new System.EventHandler(this.findConditionalProbabilitiesButton_Click);
            // 
            // classifyAllButton
            // 
            this.classifyAllButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.classifyAllButton.Enabled = false;
            this.classifyAllButton.Image = ((System.Drawing.Image)(resources.GetObject("classifyAllButton.Image")));
            this.classifyAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.classifyAllButton.Name = "classifyAllButton";
            this.classifyAllButton.Size = new System.Drawing.Size(97, 29);
            this.classifyAllButton.Text = "Classify all";
            this.classifyAllButton.Click += new System.EventHandler(this.classifyAllButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1916, 1050);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Naive Bayes classification";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.dataTabPage.ResumeLayout(false);
            this.dataSplitContainer.Panel1.ResumeLayout(false);
            this.dataSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSplitContainer)).EndInit();
            this.dataSplitContainer.ResumeLayout(false);
            this.classificationTabPage.ResumeLayout(false);
            this.classificationTabPage.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage classificationTabPage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton findPriorProbabilitiesButton;
        private System.Windows.Forms.ToolStripButton findConditionalProbabilitiesButton;
        private System.Windows.Forms.ToolStripButton classifyAllButton;
        private System.Windows.Forms.ToolStripMenuItem loadTrainingDocumentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTestDocumentsToolStripMenuItem;
        private System.Windows.Forms.ListBox classificationProgressListBox;
        private System.Windows.Forms.ToolStripMenuItem saveAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadStopWordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton removeStopWordsButton;
        private System.Windows.Forms.ToolStripButton cleanButton;
        private System.Windows.Forms.ToolStripButton tokenizeButton;
        private System.Windows.Forms.TabPage dataTabPage;
        private System.Windows.Forms.SplitContainer dataSplitContainer;
        private System.Windows.Forms.ListBox trainingDocumentListBox;
        private System.Windows.Forms.ListBox testDocumentListBox;
    }
}

