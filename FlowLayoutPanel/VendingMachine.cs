using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowLayoutPanel
{
    class VendingMachine
    {

        public bool Transaction { get; set; } = false; //Прошла ли трнзакция
        public int AmountPaid { get; set; } = 0; //Внесенная клиентом сумма
        public int SelectedDrinkPrice { get; set; } //Цена выбранного клиентом напитка
        public bool AmountPaidInFull { get; set; } = false; //Полностью внесенная сумма
        public List<Money> moneyForChange = new List<Money>(); //монеты для сдачи

        int amountOfChange;

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
            new Money(2, 10),
            new Money(10, 0),
            new Money(5, 10),
            new Money(25, 2),
            new Money(1, 0)

            //new Money(2, 10),
            //new Money(10, 10),
            //new Money(5, 10),
            //new Money(25, 2),
            //new Money(1, 15)
        };

        //Конструктор
        public VendingMachine()
        {
            myDrinks.Sort();
            moneyInVendingMashine.Sort();

            //заполняем коллекцию для сдачи
            for (int i = 0; i < moneyInVendingMashine.Count; i++)
            {
                moneyForChange.Add(new Money(moneyInVendingMashine[i].Rating, 0));
            }
        }            

        //Клиент вносит деньги
        public void CustomerDepositsMoney(int tag)
        {
            moneyInVendingMashine[tag].Quantity++;
            AmountPaid += moneyInVendingMashine[tag].Rating;
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
            string CoinsInTheMachine = "Сейчас есть\n";

            for (int i = 0; i < moneyInVendingMashine.Count; i++)
            {
                CoinsInTheMachine += $"{moneyInVendingMashine[i].Rating} руб. в количестве {moneyInVendingMashine[i].Quantity} штук\n";
            }

            return CoinsInTheMachine;
        }

        //Сдача
        public string YourChange()
        {

            //сумма сдачи
            if (Transaction)
            {
                amountOfChange = AmountPaid - SelectedDrinkPrice;
            }
            else
            {
                amountOfChange = AmountPaid;
            }

            for (int i = moneyInVendingMashine.Count - 1; i >= 0; i--)
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
                        moneyInVendingMashine[i].Quantity--;
                        moneyForChange[i].Quantity++;
                        Transaction = true;
                        break;
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
                        moneyInVendingMashine[i].Quantity--;
                        moneyForChange[i].Quantity++;
                        amountOfChange -= moneyInVendingMashine[i].Rating;
                    }
                }
                else
                {
                    continue;
                }

            }
            //вспомогательная функция
            return MyChange();

        }

        //Выдать сдачу
        public string MyChange()
        {
            string change = "Монеты для сдачи\n";

            int sumMoneyForChange = 0;

            for (int i = 0; i < moneyForChange.Count; i++)
            {
                sumMoneyForChange += moneyForChange[i].Quantity * moneyForChange[i].Rating;
            }

            if (sumMoneyForChange < AmountPaid - SelectedDrinkPrice)
            {
                Transaction = false;
                amountOfChange = AmountPaid;
                YourChange();
            }
            else
            {
                Transaction = true;
            }

            //формируем строку для сдачи
            for (int j = 0; j < moneyForChange.Count; j++)
            {
                change += $"{moneyForChange[j].Quantity.ToString()} штук по {moneyForChange[j].Rating.ToString()}\n";
            }

            AmountPaid = 0;
            Transaction = false;
            //очищаем коллекцию для сдачи
            for (int i = 0; i < moneyInVendingMashine.Count; i++)
            {
                moneyForChange[i].Quantity = 0;
            }

            return change;
        }
    }
}