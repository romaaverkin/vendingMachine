using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlowLayoutPanel
{
    public partial class Form1 : Form
    {
        VendingMachine vendingMachine;

        private void howMuchMoneyInTheMachine()
        {
            string currentBalanse = "Сейчас есть\n";

            foreach (Money money in vendingMachine.myMoney)
            {
                currentBalanse += $"{money.Rating} руб. в количестве {money.Quantity} штук\n";
            }
            currentBalanceLabel.Text = currentBalanse;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            vendingMachine = new VendingMachine();

            for (int i = 0; i < vendingMachine.myDrinks.Count; i++)
            {
                Button drinkButton = new Button()
                {
                    Width = 150,
                    Name = "drinkButton" + i,
                    Tag = i,
                    Text = $"{vendingMachine.myDrinks[i].Name} - {vendingMachine.myDrinks[i].Price} руб.",
                };

                drinkButton.Click += drinkButtonOnClick;
                flowLayoutPanel1.Controls.Add(drinkButton);
            };

            for (int i = 0; i < vendingMachine.myMoney.Count; i++)
            {
                Button moneyButton = new Button()
                {
                    Name = "moneyButton" + i,
                    Width = 150,
                    Tag = i,
                    Text = $"Внести {vendingMachine.myMoney[i].Rating} руб.",
                    Enabled = false
                };

                moneyButton.Click += moneyButtonOnClick;
                flowLayoutPanel2.Controls.Add(moneyButton);
            };

            howMuchMoneyInTheMachine();
        }

        private void moneyButtonOnClick(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (button != null)
            {
                int buttonTag = Convert.ToInt32(button.Tag);
                Money insertedCoin = vendingMachine.myMoney[buttonTag];

                vendingMachine.madeMoney += insertedCoin.Rating;
                insertedCoin.Quantity++;

                madeLabel.Text = $"Вы внесли {Convert.ToString(vendingMachine.madeMoney)} руб.";
                howMuchMoneyInTheMachine();
            }
        }

        private void drinkButtonOnClick(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (button != null)
            {
                int buttonTag = Convert.ToInt32(button.Tag);
                Drink selectedDrink = vendingMachine.myDrinks[buttonTag];

                vendingMachine.selectedDrink = selectedDrink.Price;
                selectDrinkButton.Text = $"Вы выбрали\n{selectedDrink.Name} цена {selectedDrink.Price} руб.";

                if (!vendingMachine.publicIsSelected)
                {
                    for (int i = 0; i < vendingMachine.myMoney.Count; i++)
                    {
                        flowLayoutPanel2.Controls["moneyButton" + i].Enabled = true;
                    }
                }
            }
        }
    }
}
