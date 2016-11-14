namespace SalesTaxCSharp.Models
{
    public class Goods : IGoods
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool Imported { get; set; }
        public bool TaxExempt { get; set; }
    }
}
