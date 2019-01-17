﻿using System;
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
            currentBalanceVendingMachineLabel.Text = currentBalanse;
        }

        private void lockReceiveButtons()
        {
            if (!vendingMachine.publicIsSelected)
            {
                for (int i = 0; i < vendingMachine.myMoney.Count; i++)
                {
                    flowLayoutPanel2.Controls["moneyButton" + i].Enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < vendingMachine.myMoney.Count; i++)
                {
                    flowLayoutPanel2.Controls["moneyButton" + i].Enabled = false;
                }
            }
        }

        private void lockDrinkButtons()
        {
            if (!vendingMachine.publicIsSelected)
            {          
                for (int i = 0; i < vendingMachine.myDrinks.Count; i++)
                {
                    flowLayoutPanel1.Controls["drinkButton" + i].Enabled = false;
                }
            }
            else
            {
                for (int i = 0; i < vendingMachine.myDrinks.Count; i++)
                {
                    flowLayoutPanel1.Controls["drinkButton" + i].Enabled = true;
                }
            }
        }

        public void yourChange()
        {
            int remainingChange = vendingMachine.moneyInvested - vendingMachine.selectedDrinkPrice;
            List<Money> moneyInVendingMachine = vendingMachine.myMoney;
            List<Money> moneyForChange = new List<Money>();

            for (int n = 0; n < vendingMachine.myMoney.Count; n++)
            {
                moneyForChange.Add(new Money(vendingMachine.myMoney[n].Rating, 0));
            }

            for (int i = vendingMachine.myMoney.Count - 1; i >= 0; i--)
            {
                if (remainingChange < moneyInVendingMachine[i].Rating)
                {
                    continue;
                }
                else if (remainingChange == moneyInVendingMachine[i].Rating)
                {
                    remainingChange = 0;
                    moneyForChange[i].Quantity++;
                }
                else
                {
                    while (remainingChange >= moneyInVendingMachine[i].Rating)
                    {
                        moneyForChange[i].Quantity++;
                        remainingChange -= moneyInVendingMachine[i].Rating;
                    }
                }
            }

            string change = "Монеты для садчи\n";

            for (int m = 0; m < moneyForChange.Count; m++)
            {
                change += $"{moneyForChange[m].Quantity.ToString()} штук по {moneyForChange[m].Rating.ToString()} рублей\n";
            }

            yourСhangelabel.Text = change;

            countNumberCoinsInVendingMashine(moneyForChange, moneyInVendingMachine);
        }

        private void countNumberCoinsInVendingMashine(List<Money> moneyForChange, List<Money> moneyInVendingMachine)
        {
            for (int i = 0; i < moneyForChange.Count; i++)
            {
                if (moneyForChange[i].Quantity != 0)
                {
                    moneyInVendingMachine[i].Quantity -= moneyForChange[i].Quantity;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            vendingMachine = new VendingMachine();
            vendingMachine.myMoney.Sort();
            vendingMachine.myDrinks.Sort();

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

                vendingMachine.moneyInvested += insertedCoin.Rating;
                insertedCoin.Quantity++;

                int selectedDrinkPrice = vendingMachine.selectedDrinkPrice;
                int moneyInvested = vendingMachine.moneyInvested;

                if (selectedDrinkPrice > moneyInvested)
                {
                    paymentLabel.Text = $"Вы внесли {Convert.ToString(moneyInvested)} руб.\n" +
                        $"Осталось {selectedDrinkPrice - moneyInvested} руб.";
                }
                else if (selectedDrinkPrice == moneyInvested)
                {
                    buyButton.Enabled = true;
                    paymentLabel.Text = $"Вы внесли {Convert.ToString(moneyInvested)} руб.\n" +
                        $"Ваша сдача 0 руб.";
                    vendingMachine.publicIsSelected = true;
                    lockReceiveButtons();
                }
                else
                {
                    buyButton.Enabled = true;
                    paymentLabel.Text = $"Вы внесли {Convert.ToString(moneyInvested)} руб.\n" +
                        $"Ваша сдача {moneyInvested - selectedDrinkPrice} руб.";
                    vendingMachine.publicIsSelected = true;
                    yourChange();
                    lockReceiveButtons();
                }

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

                vendingMachine.selectedDrinkPrice = selectedDrink.Price;
                selectDrinkButton.Text = $"Вы выбрали\n{selectedDrink.Name} цена {selectedDrink.Price} руб.";
                vendingMachine.publicIsSelected = false;

                lockReceiveButtons();
                lockDrinkButtons();
            }
        }

        private void buyButton_Click(object sender, EventArgs e)
        {
            yourСhangelabel.Text = "";
            vendingMachine.moneyInvested = 0;
            vendingMachine.publicIsSelected = true;
            lockDrinkButtons();
            paymentLabel.Text = "Вы внесли 0 руб.";
            selectDrinkButton.Text = "Выберите напиток";
            MessageBox.Show("Спасибо за покупку!");
        }
    }
}
