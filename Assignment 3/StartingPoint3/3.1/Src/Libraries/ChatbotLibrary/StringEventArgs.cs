using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    public class StringEventArgs: EventArgs
    {
        private string information;

        public StringEventArgs(string information)
        {
            this.information = information;
        }

        public string Information
        {
            get { return information; }
        }
    }
}
