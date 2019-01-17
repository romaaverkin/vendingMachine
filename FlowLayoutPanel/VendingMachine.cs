using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowLayoutPanel
{
    class VendingMachine
    {

        public int moneyInvested { get; set; }
        public int selectedDrinkPrice { get; set; }
        public bool publicIsSelected { get; set; } = false;

        public List<Drink> myDrinks = new List<Drink>
        {
            new Drink("Черный кофе", 16),
            new Drink("Кипяток", 8),
            new Drink("Капучино", 35),
            new Drink("Кофе с молоком", 22),
            new Drink("Латте", 39)
        };

        public List<Money> myMoney = new List<Money>
        {
            new Money(2, 10),
            new Money(10, 0),
            new Money(5, 10),
            new Money(25, 2),
            new Money(1, 0)
        };
    }
}