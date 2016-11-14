using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxCSharp
{
    public class TaxExemptGood : Goods
    {
        public TaxExemptGood()
        {
            TaxExempt = true;
        }

    }
}
