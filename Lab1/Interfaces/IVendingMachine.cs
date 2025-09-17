using Lab1.Models;

using System.Collections.Generic;

namespace Lab1.Interfaces;

public interface IVendingMachine
{
    void TurnOn();
    void AddProduct(Product product);
    List<Product> GetProducts();
    void DisplayProducts();
    void InsertCoin(decimal value);
    void SelectProduct(int productIndex);
    void CompleteTransaction();
    void CancelTransaction();
    void LoginViaAdmin(string login, string password);
}