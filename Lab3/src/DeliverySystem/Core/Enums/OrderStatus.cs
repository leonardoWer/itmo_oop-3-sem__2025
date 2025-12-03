namespace Lab3.DeliverySystem.Core.Enums;

public enum OrderStatus
{
    New,            // Новый заказ
    Confirmed,      // Подтвержден
    Preparing,      // Готовится
    ReadyForDelivery, // Готов к доставке
    OnDelivery,     // В доставке
    Delivered,      // Доставлен
    Cancelled       // Отменеён
}