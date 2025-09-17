namespace Lab1.Models;

using System;
public class AdminPanel
{
    private VendingMachine _vendingMachine;
    private Admin _admin;
    
    public AdminPanel(VendingMachine vm, Admin admin)
    {
        _vendingMachine = vm;
        _admin = admin;
    }

    public void Login(string login, string password)
    {
        if (login == _admin.Password && password == _admin.Password)
        {
            ShowMenu();
        }
    }
    
    private void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("АДМИН ПАНЕЛЬ");
            Console.WriteLine($"> {_admin.Login}");
            
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Пополнить количество товара");
            Console.WriteLine("3. Посмотреть статистику");
            Console.WriteLine("4. Собрать деньги");
            Console.WriteLine("5. Показать все товары");
            Console.WriteLine("0. Выйти");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    RestockProduct();
                    break;
                case "3":
                    ShowStatistics();
                    break;
                case "4":
                    CollectMoney();
                    break;
                case "5":
                    ShowAllProducts();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    private void AddProduct()
    {
        Console.Write("Название товара: ");
        var name = Console.ReadLine();
        
        Console.Write("Иконка (эмодзи): ");
        var icon = Console.ReadLine();
        
        Console.Write("Цена: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            Console.WriteLine("Неверная цена!");
            return;
        }
        
        Console.Write("Количество: ");
        if (!int.TryParse(Console.ReadLine(), out int cnt))
        {
            Console.WriteLine("Неверное количество!");
            return;
        }

        _vendingMachine.AddProduct(new Product
            { 
                Name = name, 
                Icon = icon, 
                Price = price, 
                Count = cnt 
            });
        
        Console.WriteLine("Товар успешно добавлен!");
    }

    private void RestockProduct()
    {
        var products = _vendingMachine.GetProducts();
        
        ShowAllProducts();
        Console.Write("Выберите номер товара для пополнения: ");
        
        if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > products.Count)
        {
            Console.WriteLine("Неверный номер товара!");
            return;
        }

        Console.Write("Сколько добавить: ");
        if (!int.TryParse(Console.ReadLine(), out int cnt))
        {
            Console.WriteLine("Неверное количество!");
            return;
        }

        products[index - 1].Count += cnt;
        Console.WriteLine($"Товар пополнен! Теперь: {products[index - 1].Count} шт.");
    }

    private void ShowStatistics()
    {
        var earnedMoney = _vendingMachine.GetEarnedMoney();
        var products = _vendingMachine.GetProducts();
        
        Console.WriteLine($"Собранные деньги: {earnedMoney} руб.");
        Console.WriteLine($"Количество товаров: {products.Count}");
        
        var totalItems = 0;
        foreach (var product in products)
        {
            totalItems += product.Count;
        }
        Console.WriteLine($"Общее количество товаров: {totalItems} шт.");
    }

    private void CollectMoney()
    {
        var earnedMoney = _vendingMachine.GetEarnedMoney();
        
        Console.WriteLine($"Собрано денег: {earnedMoney} руб.");
        _vendingMachine.CollectEarnedMoney();
        Console.WriteLine("Деньги успешно собраны");
    }

    private void ShowAllProducts()
    {
        _vendingMachine.DisplayProducts();
    }
}