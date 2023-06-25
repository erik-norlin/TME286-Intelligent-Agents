using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalLanguageProcessing.RRatios
{
    // This class contains a shared token between the written and spoken language, as well as
    // the r-ratio (hence RRatio), n_w/n_s, between them.
    public class RRatio
    {
        private const int INSTANCE_FORMAT_WIDTH = 8;

        private string token;
        private double ratio;

        public RRatio(string token)
        {
            this.token = token;
        }

        public void ComputeAndSetRatio(int writtenTokenCount, int spokenTokenCount)
        {
            ratio = (double)writtenTokenCount / spokenTokenCount;
        }

        public string AsString()
        {
            string ratioAsString = Math.Round(ratio, 2).ToString().PadLeft(INSTANCE_FORMAT_WIDTH) + " " + token;
            return ratioAsString;
        }

        public string Token
        {
            get { return token; }
            set { token = value; }
        }

        public double Ratio
        {
            get { return ratio; }
            set { ratio = value; }
        }
    }
}
