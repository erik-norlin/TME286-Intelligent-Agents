namespace Tutorial1Application
{
    partial class Tutorial1Form
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setBlueButton = new System.Windows.Forms.Button();
            this.setGreenButton = new System.Windows.Forms.Button();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(423, 35);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // setBlueButton
            // 
            this.setBlueButton.Location = new System.Drawing.Point(12, 38);
            this.setBlueButton.Name = "setBlueButton";
            this.setBlueButton.Size = new System.Drawing.Size(156, 30);
            this.setBlueButton.TabIndex = 1;
            this.setBlueButton.Text = " Set blue";
            this.setBlueButton.UseVisualStyleBackColor = true;
            this.setBlueButton.Click += new System.EventHandler(this.setBlueButton_Click);
            // 
            // setGreenButton
            // 
            this.setGreenButton.Location = new System.Drawing.Point(76, 96);
            this.setGreenButton.Name = "setGreenButton";
            this.setGreenButton.Size = new System.Drawing.Size(215, 43);
            this.setGreenButton.TabIndex = 2;
            this.setGreenButton.Text = "Greeeeeeeen :D";
            this.setGreenButton.UseVisualStyleBackColor = true;
            this.setGreenButton.Click += new System.EventHandler(this.HandleSetGreenButtonClick);
            // 
            // Tutorial1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 309);
            this.Controls.Add(this.setGreenButton);
            this.Controls.Add(this.setBlueButton);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Tutorial1Form";
            this.Text = "Tutorial 1";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button setBlueButton;
        private System.Windows.Forms.Button setGreenButton;
    }
}

