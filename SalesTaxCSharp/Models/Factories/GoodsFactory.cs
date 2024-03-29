﻿using System.Collections.Generic;

namespace SalesTaxCSharp.Models.Factories
{
    public class GoodsFactory : IGoodsFactory {

        private readonly List<string> Exemptions;
       
        public GoodsFactory()
        {
            Exemptions = new List<string>() { "books", "food", "medical" };
        }
    
        public IGoods Create(string category, int quantity, string name, double price)
        {
            IGoods good = Exemptions.Contains(category) ? new TaxExemptGood() : new Goods();
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