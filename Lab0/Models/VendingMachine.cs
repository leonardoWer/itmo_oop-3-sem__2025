namespace Lab0.Models;

using System;
using System.Collections.Generic;
using Interfaces;
using Utils;

public class VendingMachine : IVendingMachine
{
    private AdminPanel adminPanel;
    private List<Product> Products { get; set; }
    private decimal currentBalance = 0;
    private decimal earnedMoney = 0;
    
    public VendingMachine(Admin admin)
    {
        adminPanel = new AdminPanel(this, admin);
        Products = new List<Product>();
    }
    public VendingMachine(Admin admin, List<Product> products)
    {
        adminPanel = new AdminPanel(this, admin);
        Products = products;
    }

    public void SetProducts(List<Product> products)
    {
        Products = products;
    }

    public void TurnOn()
    {
        while (true)
        {
            Console.Clear();
            DisplayVendingMachine();
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
                
                case "admin":
                    Console.Write("Login: ");
                    string login = Console.ReadLine();
                    Console.Write("Password: ");
                    string password = Console.ReadLine();
                    
                    LoginViaAdmin(login, password);
                    break;

                default:
                    Console.WriteLine("Такой команды нет, попробуйте ещё раз");
                    break;
            }
            
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    public List<Product> GetProducts()
    {
        return Products;
    }

    public void DisplayProducts()
    {
        if (Products.Count > 0)
        {
            Console.WriteLine("Доступные товары:");
            for (int i = 0; i < Products.Count; i++)
            {
                var product = Products[i];
                Console.Write($"{i + 1} - {product.Name} - {product.Price} руб. | ");
                
                // Разные цвета для разного количества
                Console.ForegroundColor = ConsoleColor.Green;
                if (product.Count < 5)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (product.Count == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write($"{product.Count} шт.");
                
                Console.ResetColor();
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Автомат пуст.");
        }
    }

    public void DisplayVendingMachine()
     {
         const int columnWidth = 30;
         const string separator = "|-------------------------------|";

         var vmColor = ConsoleColor.Black;
         Console.ForegroundColor = vmColor;
         
         // Автомат
         Console.WriteLine(separator);
         ConsoleUtils.WriteLine("|      ВЕНДИНГОВЫЙ АВТОМАТ      |");
         ConsoleUtils.WriteLine(separator, vmColor);
     
         for (int i = 0; i < Products.Count; i++)
         {
             var product = Products[i];
         
             // Первая строка товара
             ConsoleUtils.Write($"| {product.Icon} ");
             ConsoleUtils.Write(product.Name.PadRight(columnWidth - 4));
             ConsoleUtils.WriteLine(" |");
         
             // Вторая строка товара
             string priceInfo = $"{i + 1} - {product.Price} руб.";
             Console.Write($"| {priceInfo.PadRight(columnWidth)}");
             ConsoleUtils.WriteLine("|", vmColor);
         
             ConsoleUtils.WriteLine(separator, vmColor);
         }
     
         // Строка с балансом
         Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine($"| Баланс: {currentBalance} руб.{new string(' ', columnWidth - 14 - currentBalance.ToString().Length)} |");
         Console.ResetColor();
         ConsoleUtils.WriteLine(separator, vmColor);
     }

    public void InsertCoin(decimal value)
    {
        currentBalance += value;
        Console.WriteLine($"Внесено: {value} руб. Текущий баланс: {currentBalance} руб.");
    }

    public void SelectProduct(int productIndex)
    {
        // Ошибки
        if (Products.Count == 0)
        {
            Console.WriteLine("Автомат пуст.");
            return;
        }
        
        if (productIndex < 0 || productIndex >= Products.Count)
        {
            Console.WriteLine("Неверный номер продукта!");
            return;
        }

        var product = Products[productIndex];

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