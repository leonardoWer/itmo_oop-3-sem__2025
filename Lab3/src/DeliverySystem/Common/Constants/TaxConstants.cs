namespace Lab3.DeliverySystem.Common;

public static class TaxConstants
{
    // Налоги
    public const decimal STANDARD_TAX = 0.1m; // НДС 10 процентов от доставвки
    public const decimal REDUCED_TAX = 0.05m;
    public const decimal ELEVATED_TAX = 0.2m;
    
    // Налоги на конкретные категории
    public static readonly Dictionary<string, decimal> CATEGORY_TAX_RATES = new()
    {
        { "Еда", STANDARD_TAX },
        { "Напитки", STANDARD_TAX },
        { "Алкоголь", ELEVATED_TAX },
        { "Десерты", STANDARD_TAX },
        { "Детское питание", REDUCED_TAX }
    };
}