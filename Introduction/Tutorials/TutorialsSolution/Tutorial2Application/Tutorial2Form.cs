using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeographyLibrary;

namespace Tutorial2Application
{
    public partial class Tutorial2Form : Form
    {
        private List<Country> countryList;
        public Tutorial2Form()
        {
            InitializeComponent();
        }

        private void initializeButton_Click(object sender, EventArgs e)
        {
            countryList = new List<Country>();
            Country country = new Country("Sweden", 10215250);
            countryList.Add(country);
            country = new Country("Finland", 5521533);
            countryList.Add(country);
            country = new Country("Norway", 5295519);
            countryList.Add(country);
            sortButton.Enabled = true;
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            countryList.Sort((a, b) => a.Population.CompareTo(b.Population));
            informationListBox.Items.Clear();
            foreach(Country country in countryList)
            {
                string countryInformation = country.Name + ": population = " + country.Population;
                informationListBox.Items.Add(countryInformation);
            }
        }
    }
}
