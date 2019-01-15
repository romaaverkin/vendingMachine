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
            String currentBalanse = "Сейчас есть\n";

            foreach (Money money in vendingMachine.myMoney)
            {
                currentBalanse += money.Rating + " руб. в количестве " + money.Quantity + " штук\n";
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
                    Text = vendingMachine.myDrinks[i].Name + " - " + vendingMachine.myDrinks[i].Price + " руб."
                };

                drinkButton.Click += ButtonOnClick;
                flowLayoutPanel1.Controls.Add(drinkButton);
            };

            for (int i = 0; i < vendingMachine.myMoney.Count; i++)
            {
                Button moneyButton = new Button()
                {
                    Width = 150,
                    Name = "moneyButton" + i,
                    Tag = vendingMachine.myMoney[i].Rating,
                    Text = "Внести " + vendingMachine.myMoney[i].Rating + " руб."
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
                vendingMachine.madeMoney += Convert.ToInt32(button.Tag);
                
                for (int i = 0; i < vendingMachine.myMoney.Count; i++)
                {
                    if (vendingMachine.myMoney[i].Rating == Convert.ToInt32(button.Tag))
                    {
                        vendingMachine.myMoney[i].Quantity++;
                    }
                }

                madeLabel.Text = "Вы внесли " + Convert.ToString(vendingMachine.madeMoney) + " руб.";
                howMuchMoneyInTheMachine();
            }
        }

        private void ButtonOnClick(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (button != null)
            {
                MessageBox.Show("Сорт: " + button.Name);
            }
        }
    }
}
