namespace Lab3.Tests;

using DeliverySystem.Core.Enums;
using DeliverySystem.Core.Models;
using DeliverySystem.Patterns.Decorator;
using DeliverySystem.Patterns.Factory;
using DeliverySystem.Patterns.State;
using DeliverySystem.Patterns.Strategy;
using DeliverySystem.Services;
using Xunit;


public class DeliverySystemTests
{
    // Проверка создания разных типов заказов через фабрику
    [Fact]
    public void Factory_CreatesCorrectOrderType()
    {
        // Arrange
        var customer = new Customer(1, "Test", "123", "Address");
        var menuItem = new MenuItem(1, "Бургер", "Вкусный", 250, "Фастфуд");
        var items = new List<OrderItem> { new OrderItem(menuItem, 1) };
        
        // Act
        var standardFactory = new StandardOrderFactory();
        var expressFactory = new ExpressOrderFactory();
        
        var standardOrder = standardFactory.CreateOrder(1, items,customer,  DeliveryType.Standard);
        var expressOrder = expressFactory.CreateOrder(2, items, customer, DeliveryType.Express);
        
        // Assert
        Assert.IsType<StandardOrder>(standardOrder);
        Assert.IsType<ExpressOrder>(expressOrder);
    }

    // Проверка расчета стоимости через разные стратегии
    [Fact]
    public void Strategy_CalculatesDifferentPrices()
    {
        // Arrange
        var customer = new Customer(1, "Test", "123", "Address");
        var menuItem = new MenuItem(1, "Пицца", "Большая", 600, "Пицца");
        var order = new StandardOrder(1, new List<OrderItem> { new OrderItem(menuItem, 1) }, customer,
            DeliveryType.Standard);
        
        // Act
        var standardStrategy = new StandardPricingStrategy();
        var discountStrategy = new DiscountPricingStrategy(0.2m); // 20% скидка
        
        var standardPrice = standardStrategy.CalculatePrice(order);
        var discountPrice = discountStrategy.CalculatePrice(order);
        
        // Assert
        Assert.True(discountPrice < standardPrice, "Цена со скидкой должна быть меньше");
        Assert.Equal("Стандартный расчет", standardStrategy.GetStrategyName());
    }

    // Проверка перехода состояний заказа
    [Fact]
    public void State_TransitionsCorrectly()
    {
        // Arrange
        var order = CreateTestOrder();
        
        // Act & Assert цепочки переходов
        Assert.IsType<NewOrderState>(order.State); // Начальное состояние
        
        order.Process(); // New -> Confirmed
        Assert.IsType<ConfirmedOrderState>(order.State);
        
        order.Process(); // Confirmed -> Preparing
        Assert.IsType<PreparingOrderState>(order.State);
        
        order.Cancel(); // Preparing -> Cancelled
        Assert.IsType<CancelledOrderState>(order.State);
    }

    // Проверка добавления опций через декоратор
    [Fact]
    public void Decorator_AddsFeaturesAndCost()
    {
        // Arrange
        var order = CreateTestOrder();
        IOrderComponent component = new OrderBase(order);
        var baseCost = component.GetCost();
        
        // Act
        component = new GiftDecorator(component);
        
        var finalCost = component.GetCost();
        var features = component.GetFeatures();
        
        // Assert
        Assert.True(finalCost > baseCost);
        Assert.Contains("Подарочная упаковка", features);
    }

    // Проверка основных операций сервиса заказов
    [Fact]
    public void OrderService_ManagesOrdersCorrectly()
    {
        // Arrange
        var service = new OrderService();
        var customer = new Customer(1, "Test", "123", "Address");
        var menuItem = new MenuItem(1, "Салат", "Свежий", 300, "Салаты");
        var items = new List<OrderItem> { new OrderItem(menuItem, 2) };
        
        // Act
        var order = service.CreateOrder(items, customer, DeliveryType.Standard);
        var retrievedOrder = service.GetOrder(order.Id);
        var customerOrders = service.GetCustomerOrders(customer.Id);
        
        // Assert
        Assert.NotNull(retrievedOrder);
        Assert.Equal(order.Id, retrievedOrder.Id);
        Assert.Single(customerOrders);
    }
    
    private Order CreateTestOrder()
    {
        var customer = new Customer(1, "Test", "123", "Address");
        var menuItem = new MenuItem(1, "Тестовый товар", "Описание", 100, "Категория");
        var items = new List<OrderItem> { new OrderItem(menuItem, 1) };
        
        return new StandardOrder(1, items, customer, DeliveryType.Standard);
    }
}