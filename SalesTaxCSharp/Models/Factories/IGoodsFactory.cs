namespace SalesTaxCSharp.Models.Factories
{
    public interface IGoodsFactory
    {
        IGoods Create(string category, int quantity, string name, double price);
    }
}