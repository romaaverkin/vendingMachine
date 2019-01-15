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

            currentBalanceLabel.Text = "gdfgdg";
        }

        private void moneyButtonOnClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (button != null)
            {
                vendingMachine.madeMoney += Convert.ToInt32(button.Tag);
                madeLabel.Text = "Вы внесли " + Convert.ToString(vendingMachine.madeMoney) + " руб.";
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
