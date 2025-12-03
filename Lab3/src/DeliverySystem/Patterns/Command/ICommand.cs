namespace Lab3.DeliverySystem.Patterns.Command;

/*
 * Использую чтобы
 * - удобно управлять операциями с заказами
 * - иметь историю дейтсвий
 */
public interface ICommand
{
    void Execute();
    void Undo();
    string GetDescription();
}