namespace Tutorial2Application
{
    partial class Tutorial2Form
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
            this.initializeButton = new System.Windows.Forms.Button();
            this.informationListBox = new System.Windows.Forms.ListBox();
            this.sortButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // initializeButton
            // 
            this.initializeButton.Location = new System.Drawing.Point(12, 12);
            this.initializeButton.Name = "initializeButton";
            this.initializeButton.Size = new System.Drawing.Size(111, 41);
            this.initializeButton.TabIndex = 0;
            this.initializeButton.Text = "Initialize";
            this.initializeButton.UseVisualStyleBackColor = true;
            this.initializeButton.Click += new System.EventHandler(this.initializeButton_Click);
            // 
            // informationListBox
            // 
            this.informationListBox.FormattingEnabled = true;
            this.informationListBox.ItemHeight = 20;
            this.informationListBox.Location = new System.Drawing.Point(12, 76);
            this.informationListBox.Name = "informationListBox";
            this.informationListBox.Size = new System.Drawing.Size(776, 344);
            this.informationListBox.TabIndex = 1;
            // 
            // sortButton
            // 
            this.sortButton.Enabled = false;
            this.sortButton.Location = new System.Drawing.Point(140, 12);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(172, 41);
            this.sortButton.TabIndex = 2;
            this.sortButton.Text = "Sort on population";
            this.sortButton.UseVisualStyleBackColor = true;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // Tutorial2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sortButton);
            this.Controls.Add(this.informationListBox);
            this.Controls.Add(this.initializeButton);
            this.Name = "Tutorial2Form";
            this.Text = "Tutorial2Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button initializeButton;
        private System.Windows.Forms.ListBox informationListBox;
        private System.Windows.Forms.Button sortButton;
    }
}

