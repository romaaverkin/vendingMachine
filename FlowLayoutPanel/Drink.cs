using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowLayoutPanel
{
    class Drink
    {
        public Drink(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public int Price { get; set; }
    }
}
