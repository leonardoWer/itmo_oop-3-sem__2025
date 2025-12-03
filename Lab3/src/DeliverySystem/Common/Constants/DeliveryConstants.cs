namespace Lab3.DeliverySystem.Common;

public static class DeliveryConstants
{
    // Тарифы доставки
    public const decimal STANDARD_DELIVERY_COST = 100;
    public const decimal EXPRESS_DELIVERY_COST = 250;
    public const decimal SCHEDULED_DELIVERY_COST = 150;
        
    // Минимальная сумма заказа
    public const decimal MINIMUM_ORDER_AMOUNT = 300;
        
    // Бесплатная доставка от суммы
    public const decimal FREE_DELIVERY_THRESHOLD = 1000;
        
    // Время доставки (в минутах)
    public const int STANDARD_DELIVERY_TIME = 60;
    public const int EXPRESS_DELIVERY_TIME = 30;
        
    // Радиус доставки (в км)
    public const int MAX_DELIVERY_DISTANCE = 15;
}