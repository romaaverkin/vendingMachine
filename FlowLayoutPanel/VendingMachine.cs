using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowLayoutPanel
{
    class VendingMachine
    {

        bool Transaction { get; set; } = false; //Прошла ли трнзакция
        public int AmountPaid { get; set; } = 0; //Внесенная клиентом сумма
        public int SelectedDrinkPrice { get; set; } //Цена выбранного клиентом напитка
        public bool AmountPaidInFull { get; set; } = false; //Полностью внесенная сумма

        public VendingMachine()
        {
            myDrinks.Sort();
            moneyInVendingMashine.Sort();
            customerMoney.Sort();
        }
                
        //Коллекция видов кофе
        public List<Drink> myDrinks = new List<Drink>
        {
            new Drink("Черный кофе", 16),
            new Drink("Кипяток", 8),
            new Drink("Капучино", 35),
            new Drink("Кофе с молоком", 22),
            new Drink("Латте", 39)
        };

        //Коллекция видов монет
        public List<Money> moneyInVendingMashine = new List<Money>
        {
            //new Money(2, 10),
            //new Money(10, 0),
            //new Money(5, 10),
            //new Money(25, 2),
            //new Money(1, 0)

            new Money(2, 10),
            new Money(10, 15),
            new Money(5, 10),
            new Money(25, 2),
            new Money(1, 10)
        };

        //Внесенные клиентом монеты до транзакции
        public List<Money> customerMoney = new List<Money>
        {
            new Money(2, 0),
            new Money(10, 0),
            new Money(5, 0),
            new Money(25, 0),
            new Money(1, 0)
        };

        //Клиент вносит деньги
        public void CustomerDepositsMoney(int tag)
        {
            customerMoney[tag].Quantity++;
            AmountPaid += customerMoney[tag].Rating;
        }

        //Узнать сколько денег осталось заплатить
        public string FindOutHowMuchMoneyIsLeftToPay()
        {
            if (SelectedDrinkPrice > AmountPaid)
            {
                return $"Вы внесли {AmountPaid} руб.\n" +
                        $"Осталось {SelectedDrinkPrice - AmountPaid} руб.";
            }
            else if (SelectedDrinkPrice == AmountPaid)
            {
                AmountPaidInFull = true;
                return $"Вы внесли {AmountPaid} руб.\n" +
                        $"Ваша сдача 0 руб.";
            }
            else
            {
                AmountPaidInFull = true;
                return $"Вы внесли {AmountPaid} руб.\n" +
                    $"Ваша сдача {AmountPaid - SelectedDrinkPrice} руб.";
            }
        }

        //Узнать сколько монет есть в машине
        public string FindOutWhatCoinsInTheMachine()
        {
            List<Money> customerMoney123 = customerMoney;
            string CoinsInTheMachine = "Сейчас есть\n";

            for (int i = 0; i < moneyInVendingMashine.Count; i++)
            {
                CoinsInTheMachine += $"{moneyInVendingMashine[i].Rating} руб. в количестве {moneyInVendingMashine[i].Quantity + customerMoney[i].Quantity} штук\n";
            }

            return CoinsInTheMachine;
        }
    }
}