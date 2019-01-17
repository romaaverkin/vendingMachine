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

                drinkButton.Click += DrinkButtonOnClick;
                flowLayoutPanel1.Controls.Add(drinkButton);
            };

            for (int i = 0; i < vendingMachine.moneyInVendingMashine.Count; i++)
            {
                Button moneyButton = new Button()
                {
                    Name = "moneyButton" + i,
                    Width = 150,
                    Tag = i,
                    Text = $"Внести {vendingMachine.moneyInVendingMashine[i].Rating} руб.",
                    Enabled = false
                };

                moneyButton.Click += MoneyButtonOnClick;
                flowLayoutPanel2.Controls.Add(moneyButton);
            };

            //Какие монеты есть в автомате
            HowMuchMoneyInTheMachine();
        }

        //Кликаем по кнопке выбора кофе
        private void DrinkButtonOnClick(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (button != null)
            {
                int buttonTag = Convert.ToInt32(button.Tag);
                Drink selectedDrink = vendingMachine.myDrinks[buttonTag];

                vendingMachine.SelectedDrinkPrice = selectedDrink.Price;
                selectDrinkButton.Text = $"Вы выбрали\n{selectedDrink.Name} цена {selectedDrink.Price} руб.";
                vendingMachine.AmountPaidInFull = false;

                //Если кофе не выбран то кнопки внести деньги не активны и наоборот
                LockMoneyButtons();
                //Если сумма за кофе внесена не полностью, то кнопуи выбора кофе не активны
                LockDrinkButtons();
            }
        }

        //Нажатие кнопки внесения денег
        private void MoneyButtonOnClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            int buttonTag = Convert.ToInt32(button.Tag);
            vendingMachine.CustomerDepositsMoney(buttonTag);

            int selectedDrinkPrice = vendingMachine.SelectedDrinkPrice;
            int moneyInvested = vendingMachine.AmountPaid;

            string amountPaidAndChange = vendingMachine.FindOutHowMuchMoneyIsLeftToPay();

            if (selectedDrinkPrice > moneyInvested)
            {
                paymentLabel.Text = amountPaidAndChange;
            }
            else if (selectedDrinkPrice == moneyInvested)
            {
                buyButton.Enabled = true;
                paymentLabel.Text = amountPaidAndChange;
                //Если кофе не выбран то кнопки внести деньги не активны и наоборот
                LockMoneyButtons();
            }
            else
            {
                buyButton.Enabled = true;
                paymentLabel.Text = amountPaidAndChange;
                //Если кофе не выбран то кнопки внести деньги не активны и наоборот
                LockMoneyButtons();
                //yourChange();
            }

            //Какие монеты есть в автомате
            HowMuchMoneyInTheMachine();

        }

        //Если кофе не выбран то кнопки внести деньги не активны и наоборот
        private void LockMoneyButtons()
        {
            if (!vendingMachine.AmountPaidInFull)
            {
                for (int i = 0; i < vendingMachine.moneyInVendingMashine.Count; i++)
                {
                    flowLayoutPanel2.Controls["moneyButton" + i].Enabled = true;
                }
            }
            else
            {
                for (int i = 0; i < vendingMachine.moneyInVendingMashine.Count; i++)
                {
                    flowLayoutPanel2.Controls["moneyButton" + i].Enabled = false;
                }
            }
        }

        //Если сумма за кофе внесена не полностью, то кнопуи выбора кофе не активны
        private void LockDrinkButtons()
        {
            if (!vendingMachine.AmountPaidInFull)
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

        //Какие монеты есть в автомате
        private void HowMuchMoneyInTheMachine()
        {
            string CoinsInTheMachine = vendingMachine.FindOutWhatCoinsInTheMachine();

            currentBalanceVendingMachineLabel.Text = CoinsInTheMachine;
        }
                
        //Нажатие кнопки купить
        private void BuyButton_Click(object sender, EventArgs e)
        {
            buyButton.Enabled = false;
            yourСhangelabel.Text = "";
            vendingMachine.AmountPaid = 0;
            vendingMachine.AmountPaidInFull = true;
            LockDrinkButtons();
            paymentLabel.Text = "Вы внесли 0 руб.";
            selectDrinkButton.Text = "Выберите напиток";
            MessageBox.Show("Спасибо за покупку!");
            YourChange();
        }

        //Сдача
        public void YourChange()
        {
            //сумма сдачи
            int amountOfChange = vendingMachine.AmountPaid - vendingMachine.SelectedDrinkPrice;
            int totalSurrender = amountOfChange;
            List<Money> moneyInVendingMashine = vendingMachine.moneyInVendingMashine;
            List<Money> moneyForChange = new List<Money>();

            for (int n = 0; n < vendingMachine.moneyInVendingMashine.Count; n++)
            {
                moneyForChange.Add(new Money(vendingMachine.moneyInVendingMashine[n].Rating, 0));
            }

            for (int i = vendingMachine.moneyInVendingMashine.Count - 1; i >= 0; i--)
            {
                if (amountOfChange < moneyInVendingMashine[i].Rating)
                {
                    continue;
                }
                else if (amountOfChange == moneyInVendingMashine[i].Rating)
                {
                    if (moneyInVendingMashine[i].Quantity != 0)
                    {
                        amountOfChange = 0;
                        moneyForChange[i].Quantity++;
                        moneyInVendingMashine[i].Quantity--;
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (moneyInVendingMashine[i].Quantity != 0)
                {
                    while (moneyInVendingMashine[i].Quantity != 0 && amountOfChange >= moneyInVendingMashine[i].Rating)
                    {
                        moneyForChange[i].Quantity++;
                        moneyInVendingMashine[i].Quantity--;
                        amountOfChange -= moneyInVendingMashine[i].Rating;
                    }
                }
                else
                {
                    continue;
                }
            }

            int changeinList = 0;

            for (int j = 0; j < moneyForChange.Count; j++)
            {
                changeinList += moneyForChange[j].Quantity * moneyForChange[j].Rating;
            }

            if (changeinList > totalSurrender)
            {
                string change = "Монеты для садчи\n";

                for (int m = 0; m < moneyForChange.Count; m++)
                {
                    change += $"{moneyForChange[m].Quantity.ToString()} штук по {moneyForChange[m].Rating.ToString()} рублей\n";
                }

                yourСhangelabel.Text = change;
            }
            else
            {
                yourСhangelabel.Text = "Извините не хватает\nсдачи для выдачи";
            }
        }
    }
}
