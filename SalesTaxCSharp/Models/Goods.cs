using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCSharp
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
