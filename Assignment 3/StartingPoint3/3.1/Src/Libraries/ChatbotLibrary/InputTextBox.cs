using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatbotLibrary
{
    public partial class InputTextBox : TextBox
    {
        public event EventHandler<StringEventArgs> InputReceived = null;

        public InputTextBox()
        {
            InitializeComponent();
        }

        private void OnInputReceived(string message)
        {
            if (InputReceived != null)
            {
                EventHandler<StringEventArgs> handler = InputReceived;
                StringEventArgs e = new StringEventArgs(message);
                handler(this, e);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter)
            {
                string message = this.Text;
                this.Text = "";
                e.Handled = true;
                e.SuppressKeyPress = true;
                OnInputReceived(message);
            }
        }
    }
}
