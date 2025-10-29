using System.Collections.Generic;
using Lab0.Models;

namespace Lab0
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // 1. Создаём машину, указываем админа
            var admin = new Admin("admin", "admin1");
            VendingMachine vm = new VendingMachine(admin);
            
            vm.DisplayProducts();
            
            // 2. Заходим как админ, чтобы пополнить товар \ Задаём заранее список торваров
            // vm.LoginViaAdmin("admin", "admin1");
            
            List<Product> products = new List<Product>
            {
                new Product { Name = "Вода", Icon = "💧", Price = 30, Count = 10 },
                new Product { Name = "Добрый кола", Icon = "🥤", Price = 50, Count = 8 },
                new Product { Name = "Чипсы Lays", Icon = "🍟", Price = 45, Count = 5 }
            };
            
            vm.SetProducts(products);
            
            // Включаем машину и взаимодействуем через консоль
            vm.TurnOn();
        }
    }
}