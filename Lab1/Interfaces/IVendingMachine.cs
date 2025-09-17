namespace Lab1.Interfaces;

public interface IVendingMachine
{
    void TurnOn();
    void TurnOff();
    void DisplayProducts();
    void InsertCoin(decimal value);
    void SelectProduct(int productIndex);
    void CompleteTransaction();
    void CancelTransaction();
}