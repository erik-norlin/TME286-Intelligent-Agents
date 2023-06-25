using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    // This class stores information of properties of every movie line before they are paired
    public class MovieLineItem
    {
        private string movieLabel;
        private string movieLine;
        private string movieLineString;
        private List<string> movieLinesList;

        public MovieLineItem()
        {
            movieLinesList = new List<string>();
        }

        public string MovieLabel
        {
            get { return movieLabel; }
            set { movieLabel = value; }
        }

        public string MovieLine
        {
            get { return movieLine; }
            set { movieLine = value; }
        }

        public string MovieLineString
        {
            get { return movieLineString; }
            set { movieLineString = value; }
        }

        public List<string> MovieLinesList
        {
            get { return movieLinesList; }
            set { movieLinesList = value; }
        }
    }
}
