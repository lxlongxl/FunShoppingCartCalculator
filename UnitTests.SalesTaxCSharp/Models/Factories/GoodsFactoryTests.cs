using NUnit.Framework;
using SalesTaxCSharp;
using SalesTaxCSharp.Models;
using SalesTaxCSharp.Models.Factories;

namespace UnitTests.SalesTaxCSharp.Models.Factories
{
    [TestFixture]
    public class GoodsFactoryTests
    {
        [Test]
        public void Create_Returns_TaxExemptGood_Given_Valid_TaxExemptGoodInfo()
        {

            //Arrange
            string category = "books";
            int quantity = 1;
            string name = "cook book";
            double price = 12.15;

            IGoodsFactory target = new GoodsFactory();

            //Act
            IGoods actual = target.Create(category, quantity, name, price);

            //Assert
            Assert.AreEqual(actual.Name, name);
            Assert.AreEqual(actual.Price, price);
            Assert.AreEqual(actual.Quantity, quantity);
            Assert.IsInstanceOf<TaxExemptGood>(actual, typeof(TaxExemptGood).ToString());
        }

        [Test]
        public void Create_Returns_ImportedTaxExemptGood_Given_Valid_ImportedTaxExemptGoodInfo()
        {

            //Arrange
            string category = "food";
            int quantity = 1;
            string name = "imported chocolate";
            double price = 10.50;

            IGoodsFactory target = new GoodsFactory();

            //Act
            IGoods actual = target.Create(category, quantity, name, price);

            //Assert
            Assert.AreEqual(actual.Name, name);
            Assert.AreEqual(actual.Price, price);
            Assert.AreEqual(actual.Quantity, quantity);
            Assert.AreEqual(actual.Imported, true);
            Assert.IsInstanceOf<TaxExemptGood>(actual, typeof(TaxExemptGood).ToString());
        }

        [Test]
        public void Create_Returns_NonImportedGood_Given_Valid_GoodsInfo()
        {

            //Arrange
            string category = "general";
            int quantity = 1;
            string name = "music CD";
            double price = 15.50;

            IGoodsFactory target = new GoodsFactory();

            //Act
            IGoods actual = target.Create(category, quantity, name, price);

            //Assert
            Assert.AreEqual(actual.Name, name);
            Assert.AreEqual(actual.Price, price);
            Assert.AreEqual(actual.Quantity, quantity);
            Assert.IsInstanceOf<Goods>(actual, typeof(Goods).ToString());
        }

        [Test]
        public void Create_Returns_ImportedGood_Given_Valid_ImportedGoodInfo()
        {

            //Arrange
            string category = "general";
            int quantity = 1;
            string name = "imported perfume";
            double price = 15.50;

            IGoodsFactory target = new GoodsFactory();

            //Act
            IGoods actual = target.Create(category, quantity, name, price);

            //Assert
            Assert.AreEqual(actual.Name, name);
            Assert.AreEqual(actual.Price, price);
            Assert.AreEqual(actual.Quantity, quantity);
            Assert.AreEqual(actual.Imported, true);
            Assert.IsInstanceOf<Goods>(actual, typeof(Goods).ToString());
        }
    }
}
