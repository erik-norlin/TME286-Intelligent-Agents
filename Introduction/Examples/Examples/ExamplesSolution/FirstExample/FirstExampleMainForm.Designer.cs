namespace FirstExample
{
    partial class FirstExampleMainForm
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
            this.exitButton = new System.Windows.Forms.Button();
            this.helloButton = new System.Windows.Forms.Button();
            this.responseTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(140, 58);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(112, 35);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // helloButton
            // 
            this.helloButton.Location = new System.Drawing.Point(18, 58);
            this.helloButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.helloButton.Name = "helloButton";
            this.helloButton.Size = new System.Drawing.Size(112, 35);
            this.helloButton.TabIndex = 4;
            this.helloButton.Text = "Hello";
            this.helloButton.UseVisualStyleBackColor = true;
            this.helloButton.Click += new System.EventHandler(this.helloButton_Click);
            // 
            // responseTextBox
            // 
            this.responseTextBox.Location = new System.Drawing.Point(18, 18);
            this.responseTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.responseTextBox.Name = "responseTextBox";
            this.responseTextBox.Size = new System.Drawing.Size(388, 26);
            this.responseTextBox.TabIndex = 3;
            // 
            // FirstExampleMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 112);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.helloButton);
            this.Controls.Add(this.responseTextBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FirstExampleMainForm";
            this.Text = "First example";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button helloButton;
        private System.Windows.Forms.TextBox responseTextBox;
    }
}

