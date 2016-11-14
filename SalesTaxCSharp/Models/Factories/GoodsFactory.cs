using System;
using System.Collections.Generic;

namespace SalesTaxCSharp
{
    public class GoodsFactory : IGoodsFactory {

        private List<string> Exemptions;
       
        public GoodsFactory()
        {
            Exemptions = new List<string>() { "books", "food", "medical" };
        }
    
        public IGoods Create(string category, int quantity, string name, double price)
        {
            var good = new Goods();
            if(Exemptions.Contains(category))
            {
                 good = new TaxExemptGood();
            } else
            {
                 good = new Goods();
            }

            good.Quantity = quantity;
            good.Name = name;
            good.Price = price;
            if (good.Name.Length >= 8 && good.Name.Contains("imported"))
            {
                good.Imported = true;
            } else
            {
                good.Imported = false;
            }
            
            return good;
        }
    }
}