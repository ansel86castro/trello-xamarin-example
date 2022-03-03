using System;
using System.Collections.Generic;
using System.Text;

namespace TestXamarin.Models
{
    public class ListModel
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public int CardsCount { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
