namespace SalesTaxCSharp.Models
{
    public interface IGoods
    {
        string Name { get; set; }
        double Price { get; set; }
        int Quantity { get; set; }
        bool Imported { get; set; }
        bool TaxExempt { get; set; } 
    }
}
