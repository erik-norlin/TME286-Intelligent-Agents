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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTextDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tokenizeButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.makeDictionaryAndIndexButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.processButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.analysisTextBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1536, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importTextDataToolStripMenuItem,
            this.saveAnalysisToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importTextDataToolStripMenuItem
            // 
            this.importTextDataToolStripMenuItem.Name = "importTextDataToolStripMenuItem";
            this.importTextDataToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.importTextDataToolStripMenuItem.Text = "Import text data";
            this.importTextDataToolStripMenuItem.Click += new System.EventHandler(this.importTextDataToolStripMenuItem_Click);
            // 
            // saveAnalysisToolStripMenuItem
            // 
            this.saveAnalysisToolStripMenuItem.Enabled = false;
            this.saveAnalysisToolStripMenuItem.Name = "saveAnalysisToolStripMenuItem";
            this.saveAnalysisToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.saveAnalysisToolStripMenuItem.Text = "Save analysis";
            this.saveAnalysisToolStripMenuItem.Click += new System.EventHandler(this.saveAnalysisToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tokenizeButton,
            this.toolStripSeparator1,
            this.makeDictionaryAndIndexButton,
            this.toolStripSeparator2,
            this.processButton,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 33);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1536, 34);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // makeDictionaryAndIndexButton
            // 
            this.makeDictionaryAndIndexButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.makeDictionaryAndIndexButton.Enabled = false;
            this.makeDictionaryAndIndexButton.Image = ((System.Drawing.Image)(resources.GetObject("makeDictionaryAndIndexButton.Image")));
            this.makeDictionaryAndIndexButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.makeDictionaryAndIndexButton.Name = "makeDictionaryAndIndexButton";
            this.makeDictionaryAndIndexButton.Size = new System.Drawing.Size(142, 29);
            this.makeDictionaryAndIndexButton.Text = "Make dictionary";
            this.makeDictionaryAndIndexButton.Click += new System.EventHandler(this.makeDictionaryAndIndexButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // processButton
            // 
            this.processButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.processButton.Enabled = false;
            this.processButton.Image = ((System.Drawing.Image)(resources.GetObject("processButton.Image")));
            this.processButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(76, 29);
            this.processButton.Text = "Process";
            this.processButton.Click += new System.EventHandler(this.processButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 34);
            // 
            // analysisTextBox
            // 
            this.analysisTextBox.BackColor = System.Drawing.Color.Black;
            this.analysisTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.analysisTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analysisTextBox.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.analysisTextBox.ForeColor = System.Drawing.Color.Lime;
            this.analysisTextBox.Location = new System.Drawing.Point(0, 67);
            this.analysisTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.analysisTextBox.Multiline = true;
            this.analysisTextBox.Name = "analysisTextBox";
            this.analysisTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.analysisTextBox.Size = new System.Drawing.Size(1536, 775);
            this.analysisTextBox.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1536, 842);
            this.Controls.Add(this.analysisTextBox);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "n-grams application";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTextDataToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tokenizeButton;
        private System.Windows.Forms.TextBox analysisTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton processButton;
        private System.Windows.Forms.ToolStripMenuItem saveAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton makeDictionaryAndIndexButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

