using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeographyLibrary
{
    public class Country
    {
        private string name;
        private int population;

        public Country(string name, int population)
        {
            this.name = name;
            this.population = population;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Population
        {
            get { return population; }
            set { population = value; }
        }
    }
}
