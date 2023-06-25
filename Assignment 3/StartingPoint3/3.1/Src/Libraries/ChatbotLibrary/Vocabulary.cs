using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ChatbotLibrary
{
    [DataContract]
    public class Vocabulary
    {
        List<WordData> itemList;

        public Vocabulary()
        {
            itemList = new List<WordData>();
        }

        [DataMember]
        public List<WordData> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
    }
}
