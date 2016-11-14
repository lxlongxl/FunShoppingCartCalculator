using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCSharp
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
