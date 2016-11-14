namespace SalesTaxCSharp
{
    public interface IGoodsFactory
    {
        IGoods Create(string category, int quantity, string name, double price);
    }
}