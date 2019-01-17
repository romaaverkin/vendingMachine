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
        private List<Money> moneyForChange = new List<Money>(); // Монеты для сдачи

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
            new Money(10, 10),
            new Money(5, 10),
            new Money(25, 2),
            new Money(1, 15)
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

        //Конструктор
        public VendingMachine()
        {
            myDrinks.Sort();
            moneyInVendingMashine.Sort();
            customerMoney.Sort();

            //Заполняем коллекцию для сдачи
            ClearClientCollection();
        }            

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
            string CoinsInTheMachine = "Сейчас есть\n";

            for (int i = 0; i < moneyInVendingMashine.Count; i++)
            {
                CoinsInTheMachine += $"{moneyInVendingMashine[i].Rating} руб. в количестве {moneyInVendingMashine[i].Quantity + customerMoney[i].Quantity} штук\n";
            }

            return CoinsInTheMachine;
        }

        //Сдача
        public string YourChange()
        {
            //сумма сдачи
            int amountOfChange = AmountPaid - SelectedDrinkPrice;
            //Сумма монет определенного номинала в автомате
            int SumTotal = 0;

            for (int i = moneyInVendingMashine.Count - 1; i >= 0; i--)
            {
                SumTotal = moneyInVendingMashine[i].Quantity + customerMoney[i].Quantity;

                if (amountOfChange < moneyInVendingMashine[i].Rating)
                {
                    continue;
                }
                else if (amountOfChange == moneyInVendingMashine[i].Rating)
                {
                    if (SumTotal != 0)
                    {
                        amountOfChange = 0;
                        moneyForChange[i].Quantity++;
                        Transaction = true;
                        break;
                        //moneyInVendingMachine[i].Quantity--;
                        //Выдать сдачу
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (SumTotal != 0)
                {
                    while (SumTotal != 0 && amountOfChange >= moneyInVendingMashine[i].Rating)
                    {
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
            return myChande();

        }

        //Выдать сдачу
        public string myChande()
        {
            string change = "Монеты для садчи\n";

            //перекладываем монеты из коллекции клиента в коллекцию автомата
            for (int i = 0; i < customerMoney.Count; i++)
            {
                moneyInVendingMashine[i].Quantity += customerMoney[i].Quantity;
            }

            //очищаем коллекцию клиента
            ClearClientCollection();

            //убираем из автомата монеты, которые пойдут на сдачу
            for (int j = 0; j < moneyForChange.Count; j++)
            {
                moneyInVendingMashine[j].Quantity -= moneyForChange[j].Quantity;
            }

            //формируем строку для сдачи
            for (int j = 0; j < moneyForChange.Count; j++)
            {
                change += $"{moneyInVendingMashine[j].Quantity.ToString()} штук по {moneyInVendingMashine[j].Rating.ToString()}";
            }

            return change;
        }

        //очистить коллекцию клиента
        public void ClearClientCollection()
        {
            for (int i = 0; i < moneyInVendingMashine.Count; i++)
            {
                moneyForChange.Add(new Money(moneyInVendingMashine[i].Rating, 0));
            }
        }
    }
}