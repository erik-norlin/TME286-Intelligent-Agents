namespace NGramsApplication
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTrainingDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importNegativeTrainingReviewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPositiveTrainingReviewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tokenizeTrainingDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeDictionaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processTrainingDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTokenListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAnalysisForReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTestDataToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.importNegativeTestReviewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPositiveTestReviewsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tokenizeTestDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.classifyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analysisTextBox = new System.Windows.Forms.TextBox();
            this.classificationListBox = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.importTrainingDataToolStripMenuItem1,
            this.importTestDataToolStripMenuItem1,
            this.classifyAllToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1536, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAnalysisToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAnalysisToolStripMenuItem
            // 
            this.saveAnalysisToolStripMenuItem.Enabled = false;
            this.saveAnalysisToolStripMenuItem.Name = "saveAnalysisToolStripMenuItem";
            this.saveAnalysisToolStripMenuItem.Size = new System.Drawing.Size(217, 34);
            this.saveAnalysisToolStripMenuItem.Text = "Save analysis";
            this.saveAnalysisToolStripMenuItem.Click += new System.EventHandler(this.saveAnalysisToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(217, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // importTrainingDataToolStripMenuItem1
            // 
            this.importTrainingDataToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importNegativeTrainingReviewsToolStripMenuItem,
            this.importPositiveTrainingReviewsToolStripMenuItem,
            this.tokenizeTrainingDataToolStripMenuItem,
            this.makeDictionaryToolStripMenuItem,
            this.processTrainingDataToolStripMenuItem,
            this.showTokenListToolStripMenuItem,
            this.showAnalysisForReportToolStripMenuItem});
            this.importTrainingDataToolStripMenuItem1.Name = "importTrainingDataToolStripMenuItem1";
            this.importTrainingDataToolStripMenuItem1.Size = new System.Drawing.Size(129, 29);
            this.importTrainingDataToolStripMenuItem1.Text = "Training data";
            // 
            // importNegativeTrainingReviewsToolStripMenuItem
            // 
            this.importNegativeTrainingReviewsToolStripMenuItem.Name = "importNegativeTrainingReviewsToolStripMenuItem";
            this.importNegativeTrainingReviewsToolStripMenuItem.Size = new System.Drawing.Size(369, 34);
            this.importNegativeTrainingReviewsToolStripMenuItem.Text = "Import negative training reviews";
            this.importNegativeTrainingReviewsToolStripMenuItem.Click += new System.EventHandler(this.importNegativeTrainingReviewsToolStripMenuItem_Click);
            // 
            // importPositiveTrainingReviewsToolStripMenuItem
            // 
            this.importPositiveTrainingReviewsToolStripMenuItem.Enabled = false;
            this.importPositiveTrainingReviewsToolStripMenuItem.Name = "importPositiveTrainingReviewsToolStripMenuItem";
            this.importPositiveTrainingReviewsToolStripMenuItem.Size = new System.Drawing.Size(369, 34);
            this.importPositiveTrainingReviewsToolStripMenuItem.Text = "Import positive training reviews";
            this.importPositiveTrainingReviewsToolStripMenuItem.Click += new System.EventHandler(this.importPositiveTrainingReviewsToolStripMenuItem_Click);
            // 
            // tokenizeTrainingDataToolStripMenuItem
            // 
            this.tokenizeTrainingDataToolStripMenuItem.Enabled = false;
            this.tokenizeTrainingDataToolStripMenuItem.Name = "tokenizeTrainingDataToolStripMenuItem";
            this.tokenizeTrainingDataToolStripMenuItem.Size = new System.Drawing.Size(369, 34);
            this.tokenizeTrainingDataToolStripMenuItem.Text = "Tokenize training data";
            this.tokenizeTrainingDataToolStripMenuItem.Click += new System.EventHandler(this.tokenizeTrainingDataToolStripMenuItem_Click);
            // 
            // makeDictionaryToolStripMenuItem
            // 
            this.makeDictionaryToolStripMenuItem.Enabled = false;
            this.makeDictionaryToolStripMenuItem.Name = "makeDictionaryToolStripMenuItem";
            this.makeDictionaryToolStripMenuItem.Size = new System.Drawing.Size(369, 34);
            this.makeDictionaryToolStripMenuItem.Text = "Make dictionary of training data";
            this.makeDictionaryToolStripMenuItem.Click += new System.EventHandler(this.makeDictionaryToolStripMenuItem_Click);
            // 
            // processTrainingDataToolStripMenuItem
            // 
            this.processTrainingDataToolStripMenuItem.Enabled = false;
            this.processTrainingDataToolStripMenuItem.Name = "processTrainingDataToolStripMenuItem";
            this.processTrainingDataToolStripMenuItem.Size = new System.Drawing.Size(369, 34);
            this.processTrainingDataToolStripMenuItem.Text = "Process training data";
            this.processTrainingDataToolStripMenuItem.Click += new System.EventHandler(this.processTrainingDataToolStripMenuItem_Click);
            // 
            // showTokenListToolStripMenuItem
            // 
            this.showTokenListToolStripMenuItem.Enabled = false;
            this.showTokenListToolStripMenuItem.Name = "showTokenListToolStripMenuItem";
            this.showTokenListToolStripMenuItem.Size = new System.Drawing.Size(369, 34);
            this.showTokenListToolStripMenuItem.Text = "Show token list";
            this.showTokenListToolStripMenuItem.Click += new System.EventHandler(this.showTokenListToolStripMenuItem_Click);
            // 
            // showAnalysisForReportToolStripMenuItem
            // 
            this.showAnalysisForReportToolStripMenuItem.Enabled = false;
            this.showAnalysisForReportToolStripMenuItem.Name = "showAnalysisForReportToolStripMenuItem";
            this.showAnalysisForReportToolStripMenuItem.Size = new System.Drawing.Size(369, 34);
            this.showAnalysisForReportToolStripMenuItem.Text = "Show analysis for report";
            this.showAnalysisForReportToolStripMenuItem.Click += new System.EventHandler(this.showAnalysisForReportToolStripMenuItem_Click);
            // 
            // importTestDataToolStripMenuItem1
            // 
            this.importTestDataToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importNegativeTestReviewsToolStripMenuItem,
            this.importPositiveTestReviewsToolStripMenuItem,
            this.tokenizeTestDataToolStripMenuItem});
            this.importTestDataToolStripMenuItem1.Name = "importTestDataToolStripMenuItem1";
            this.importTestDataToolStripMenuItem1.Size = new System.Drawing.Size(157, 29);
            this.importTestDataToolStripMenuItem1.Text = "Import test data";
            // 
            // importNegativeTestReviewsToolStripMenuItem
            // 
            this.importNegativeTestReviewsToolStripMenuItem.Enabled = false;
            this.importNegativeTestReviewsToolStripMenuItem.Name = "importNegativeTestReviewsToolStripMenuItem";
            this.importNegativeTestReviewsToolStripMenuItem.Size = new System.Drawing.Size(337, 34);
            this.importNegativeTestReviewsToolStripMenuItem.Text = "import negative test reviews";
            this.importNegativeTestReviewsToolStripMenuItem.Click += new System.EventHandler(this.importNegativeTestReviewsToolStripMenuItem_Click);
            // 
            // importPositiveTestReviewsToolStripMenuItem
            // 
            this.importPositiveTestReviewsToolStripMenuItem.Enabled = false;
            this.importPositiveTestReviewsToolStripMenuItem.Name = "importPositiveTestReviewsToolStripMenuItem";
            this.importPositiveTestReviewsToolStripMenuItem.Size = new System.Drawing.Size(337, 34);
            this.importPositiveTestReviewsToolStripMenuItem.Text = "Import positive test reviews";
            this.importPositiveTestReviewsToolStripMenuItem.Click += new System.EventHandler(this.importPositiveTestReviewsToolStripMenuItem_Click);
            // 
            // tokenizeTestDataToolStripMenuItem
            // 
            this.tokenizeTestDataToolStripMenuItem.Enabled = false;
            this.tokenizeTestDataToolStripMenuItem.Name = "tokenizeTestDataToolStripMenuItem";
            this.tokenizeTestDataToolStripMenuItem.Size = new System.Drawing.Size(337, 34);
            this.tokenizeTestDataToolStripMenuItem.Text = "Tokenize test data";
            this.tokenizeTestDataToolStripMenuItem.Click += new System.EventHandler(this.tokenizeTestDataToolStripMenuItem_Click);
            // 
            // classifyAllToolStripMenuItem
            // 
            this.classifyAllToolStripMenuItem.Enabled = false;
            this.classifyAllToolStripMenuItem.Name = "classifyAllToolStripMenuItem";
            this.classifyAllToolStripMenuItem.Size = new System.Drawing.Size(109, 29);
            this.classifyAllToolStripMenuItem.Text = "Classify all";
            this.classifyAllToolStripMenuItem.Click += new System.EventHandler(this.classifyAllToolStripMenuItem_Click);
            // 
            // analysisTextBox
            // 
            this.analysisTextBox.BackColor = System.Drawing.Color.Black;
            this.analysisTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.analysisTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analysisTextBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analysisTextBox.ForeColor = System.Drawing.Color.Lime;
            this.analysisTextBox.Location = new System.Drawing.Point(0, 33);
            this.analysisTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.analysisTextBox.Multiline = true;
            this.analysisTextBox.Name = "analysisTextBox";
            this.analysisTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.analysisTextBox.Size = new System.Drawing.Size(1536, 809);
            this.analysisTextBox.TabIndex = 2;
            // 
            // classificationListBox
            // 
            this.classificationListBox.BackColor = System.Drawing.Color.Black;
            this.classificationListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.classificationListBox.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.classificationListBox.ForeColor = System.Drawing.Color.Lime;
            this.classificationListBox.FormattingEnabled = true;
            this.classificationListBox.ItemHeight = 18;
            this.classificationListBox.Location = new System.Drawing.Point(443, 36);
            this.classificationListBox.Name = "classificationListBox";
            this.classificationListBox.Size = new System.Drawing.Size(556, 108);
            this.classificationListBox.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1536, 842);
            this.Controls.Add(this.classificationListBox);
            this.Controls.Add(this.analysisTextBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "n-grams application";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox analysisTextBox;
        private System.Windows.Forms.ToolStripMenuItem saveAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTrainingDataToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem importPositiveTrainingReviewsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importNegativeTrainingReviewsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTestDataToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem importPositiveTestReviewsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importNegativeTestReviewsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem classifyAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processTrainingDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTokenListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAnalysisForReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tokenizeTrainingDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tokenizeTestDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeDictionaryToolStripMenuItem;
        private System.Windows.Forms.ListBox classificationListBox;
    }
}

