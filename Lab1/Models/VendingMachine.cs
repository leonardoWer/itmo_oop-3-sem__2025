namespace Lab1.Models;

using System;
using System.Collections.Generic;
using Interfaces;

public class VendingMachine : IVendingMachine
{
    private AdminPanel adminPanel;
    private List<Product> products = new List<Product>();
    private decimal currentBalance = 0;
    private decimal earnedMoney = 0;

    public VendingMachine(Admin admin)
    {
        adminPanel = new AdminPanel(this, admin);
    }

    public void TurnOn()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Вендинговый автомат");
            Console.WriteLine("1. Посмотреть товары");
            Console.WriteLine("2. Внести монету");
            Console.WriteLine("3. Выбрать товар");
            Console.WriteLine("4. Завершить покупку");
            Console.WriteLine("5. Отменить операцию");
            Console.WriteLine("0. Выход");
            Console.Write("Для продолжения введите нужную команду: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayProducts();
                    break;

                case "2":
                    Console.WriteLine("Выберите номинал монеты:");
                    Console.WriteLine("1 - 1 рубль");
                    Console.WriteLine("2 - 2 рубля");
                    Console.WriteLine("5 - 5 рублей");
                    Console.WriteLine("10 - 10 рублей");

                    if (int.TryParse(Console.ReadLine(), out int coinValue) &&
                        (coinValue == 1 || coinValue == 2 || coinValue == 5 || coinValue == 10))
                    {
                        InsertCoin(coinValue);
                    }
                    else
                    {
                        Console.WriteLine($"Заберите нераспознанную монету {coinValue}");
                    }

                    break;

                case "3":
                    DisplayProducts();
                    Console.Write("Введите номер товара: ");
                    if (int.TryParse(Console.ReadLine(), out int productIndex))
                    {
                        SelectProduct(productIndex - 1);
                    }
                    else
                    {
                        Console.WriteLine("Неверный номер товара!");
                    }

                    break;

                case "4":
                    CompleteTransaction();
                    break;

                case "5":
                    CancelTransaction();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Такой команды нет, попробуйте ещё раз");
                    break;
            }
        }
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }
    
    public List<Product> GetProducts()
    {
        return products;
    }

    public void DisplayProducts()
    {
        if (products.Count > 0)
        {
            Console.WriteLine("Доступные продукты");
            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                Console.WriteLine($"{i + 1} | {product.Name} | {product.Price} руб.");
            }
        }
        else
        {
            Console.WriteLine("Автомат пуст.");
        }
    }

    public void InsertCoin(decimal value)
    {
        currentBalance += value;
        Console.WriteLine($"Внесено: {value} руб. Текущий баланс: {currentBalance} руб.");
    }

    public void SelectProduct(int productIndex)
    {
        // Ошибки
        if (productIndex < 0 || productIndex >= products.Count)
        {
            Console.WriteLine("Неверный номер продукта!");
            return;
        }
        
        var product = products[productIndex];
        
        // Проверяем сколько осталось
        if (product.Count <= 0)
        {
            Console.WriteLine("Этот товар закончился.");
            return;
        }
        
        // Покупка
        if (currentBalance >= product.Price)
        {
            currentBalance -= product.Price;
            earnedMoney += product.Price;
            product.Count--;
            Console.WriteLine($"Выдано: {product.Icon} {product.Name}, 1шт.");
            CompleteTransaction();
        }
        else
        {
            Console.WriteLine($"Недостаточно средств! Нужно ещё {product.Price - currentBalance} руб.");
        }
    }

    public void CompleteTransaction()
    {
        if (currentBalance > 0)
        {
            Console.WriteLine($"Сдача {currentBalance} руб.");
            currentBalance = 0;
        }
        Console.WriteLine("Спасибо за покупку!");
    }

    public void CancelTransaction()
    {
        Console.WriteLine($"Операция отменена. Возвращено: {currentBalance} руб.");
        currentBalance = 0;
    }

    public decimal GetEarnedMoney()
    {
        return earnedMoney;
    }
    public void CollectEarnedMoney()
    {
        // Как бы забираем прибыль
        earnedMoney = 0;
    }

    public void LoginViaAdmin(string login, string password)
    {
        adminPanel.Login(login, password);
    }
}