using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tutorial1Application
{
    public partial class Tutorial1Form : Form
    {
        public Tutorial1Form()
        {
            InitializeComponent();
            setGreenButton.Click += new EventHandler(HandleSetGreenButtonClick);

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void setBlueButton_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Blue;
        }

        private void HandleSetGreenButtonClick(object sender, EventArgs e)
        {
            this.BackColor = Color.Green;
        }
    }
}
