namespace Lab3.DeliverySystem.Common;

public static class OrderConstants
{
    // Лимиты заказов
    public const int MAX_ITEMS_PER_ORDER = 20;
    public const decimal MAX_ORDER_AMOUNT = 10000; // (10 кг)
        
    // Время приготовления (минимальное в минутах)
    public const int MIN_PREPARATION_TIME = 10;
        
    // Доплата для разных типов заказа
    public const decimal EXPRESS_SURCHARGE = 1.2m;
    public const decimal WEEKEND_SURCHARGE = 1.15m;
    public const decimal NIGHT_SURCHARGE = 1.25m;
        
    // Временные промежутки (часы)
    public const int NIGHT_START_HOUR = 23;
    public const int NIGHT_END_HOUR = 6;
}