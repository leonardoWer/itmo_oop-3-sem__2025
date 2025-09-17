namespace DefaultNamespace;

public interface IVendingMachine
{
    void DisplayProducts();
    void InsertCoin(decimal value);
    void SelectProduct(int productIndex);
    void CompleteTransaction();
    void CancelTransaction();
    
    decimal CurrentBalance { get; }
    IEnumerable<Product> GetAvailableProducts(); // Отдаём продукты только для чтения
}