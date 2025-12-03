using Lab3.DeliverySystem.Core.Enums;
using Lab3.DeliverySystem.Core.Models;
using Lab3.DeliverySystem.Patterns.Command;
using Lab3.DeliverySystem.Patterns.Decorator;
using Lab3.DeliverySystem.Patterns.Observer;
using Lab3.DeliverySystem.Patterns.Strategy;
using Lab3.DeliverySystem.Services;

namespace Lab3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Покупатели
            var customer1 = new Customer(1, "Вася Пупкин", "Улица Пушкина дом Калатушкина", "+7999999999");
            var loyaltyCustomer = new Customer(2, "Николай Петров", "Кронверский проспект 49", "+78005553535");
            loyaltyCustomer.ActivateLoyalty();

            // Меню
            var menuItems = new List<MenuItem>
            {
                new MenuItem(1, "Пицца Маргарита", "Классическая пицца", 450, "Пицца"),
                new MenuItem(2, "Паста Карбонара", "Спагетти с беконом", 350, "Паста"),
                new MenuItem(3, "Кола", "Напиток 0.5л", 100, "Напитки"),
                new MenuItem(4, "Салат Цезарь", "Куриный салат", 300, "Салаты")
            };

            // Фабрика для создания заказа
            var orderService = new OrderService();
            var orderItems = new List<OrderItem>
            {
                new OrderItem(menuItems[0], 1, "Без лука"),
                new OrderItem(menuItems[2], 2)
            };

            var standardOrder = orderService.CreateOrder(orderItems, customer1, DeliveryType.Standard);
            var expressOrder = orderService.CreateOrder(orderItems, loyaltyCustomer, DeliveryType.Express, "express");

            
            // Стратегия для оплаты
            var pricingContext = new PricingContext(new StandardPricingStrategy());

            Console.WriteLine($"Стандартный расчет: {pricingContext.CalculatePrice(standardOrder)}");

            pricingContext.SetStrategy(new DiscountPricingStrategy(0.2m)); // можно сделать кастомную скидку
            Console.WriteLine($"Со скидкой 20%: {pricingContext.CalculatePrice(standardOrder)}");

            pricingContext.SetStrategy(new LoyaltyPricingStrategy());
            Console.WriteLine(pricingContext.GetStrategyInfo());
            Console.WriteLine($"Для лояльного клиента: {pricingContext.CalculatePrice(expressOrder)}");
            
            // Состояние (State) для отслеживания статуса у заказа
            Console.WriteLine($"Текущий статус заказа #{standardOrder.Id}: {standardOrder.State.GetStatusName()}");
            // Можем переходить к следующему этапу, но не знаем что это за этап
            standardOrder.Process(); // Подтвердить
            standardOrder.Process(); // Начать готовить
            standardOrder.Process(); // Готов к доставке
            standardOrder.Process(); // В доставке
            standardOrder.Complete(); // Доставлен
            
            expressOrder.Cancel(); // Отмена
            
            
            // Наблюдатель для уведомлений пользователя
            var orderSubject = new OrderSubject(standardOrder);
        
            orderSubject.Attach(new CustomerNotifier()); // Наши нотифаеры подписываются на заказ (orderSubject), чтобы отправить уведомление при изменении
            orderSubject.Attach(new KitchenNotifier());
            orderSubject.Attach(new DeliveryNotifier());
        
            orderSubject.Notify("Заказ создан и обрабатывается");
        
            // Декоратор для дополнительных услуг к заказу
            IOrderComponent orderComponent1 = new OrderBase(standardOrder);
            IOrderComponent orderComponent2 = new OrderBase(expressOrder);
        
            Console.WriteLine($"Базовая стоимость: {orderComponent1.GetCost()}");
            Console.WriteLine($"Опции: {string.Join(", ", orderComponent1.GetFeatures())}");
        
            // Добавляем опции
            orderComponent1 = new GiftDecorator(orderComponent1); // Добавляем подарочную обёртку
            orderComponent2 = new UrgentDecorator(orderComponent2); // Добавляем приоритеот
        
            Console.WriteLine($"\nПосле добавления доп опций:");
            Console.WriteLine($"Описание 1: {orderComponent1.GetDescription()}");
            Console.WriteLine($"Описание 2: {orderComponent2.GetDescription()}");
            Console.WriteLine($"Итоговая стоимость 1: {orderComponent1.GetCost()}");
            Console.WriteLine($"Итоговая стоимость 2: {orderComponent2.GetCost()}");
            Console.WriteLine($"Все опции 1:");
            foreach (var feature in orderComponent1.GetFeatures())
            {
                Console.WriteLine($" - {feature}");
            }
            Console.WriteLine($"Все опции 2:");
            foreach (var feature in orderComponent2.GetFeatures())
            {
                Console.WriteLine($" - {feature}");
            }
            Console.WriteLine();
            
            
            // Команда (Command) для 
            var commandInvoker = new CommandInvoker();
        
            // Создаем команды
            var createOrderCommand = new CreateOrderCommand(
                orderService, 
                new List<OrderItem> { new OrderItem(menuItems[3], 2) },
                customer1,
                DeliveryType.Standard
            );
        
            var updateStatusCommand = new UpdateOrderStatusCommand(
                orderService,
                standardOrder.Id,
                OrderStatus.Preparing
            );
        
            var cancelOrderCommand = new CancelOrderCommand(
                orderService,
                expressOrder.Id
            );
            
            commandInvoker.ExecuteCommand(createOrderCommand);
            commandInvoker.ExecuteCommand(updateStatusCommand);
            commandInvoker.ExecuteCommand(cancelOrderCommand);
            commandInvoker.ShowHistory();
            commandInvoker.Undo();
            commandInvoker.Undo();
            commandInvoker.ShowHistory();
        }
    }
}