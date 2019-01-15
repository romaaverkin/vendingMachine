using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowLayoutPanel
{
    class Money
    {
        public Money(int rating, int quantity)
        {
            Rating = rating;
            Quantity = quantity;
        }

        public int Rating { get; set; }
        public int Quantity { get; set; }
    }
}
