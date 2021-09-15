using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AnotherShoppingBasketLibrary.Models;
using AnotherShoppingBasketLibrary.Models.ProductTypes;
using Xunit;

namespace AnotherShoppingBasketTest
{
    public class UnitTest1
    {
        /// <summary>
        /// Create a basket that can hold products
        /// Create a product which can be added to your basket
        /// </summary>
        [Fact]
        public void BasketThatCanHoldProducts()
        {
            var basket = new ShoppingBasket();
            var items = new List<IShoppingBasketItem>
            {
                new ShoppingBasketItem(new Sofa("1 seat sofa", 1, 200), 1),
                new ShoppingBasketItem(new Sofa("2 seat sofa", 2, 500), 1),
                new ShoppingBasketItem(new Sofa("3 seat sofa", 3, 1000), 1)
            };
            basket.Items = items;
            Assert.True(basket.Items.Any()); // basket contains products
        }
        
        /// <summary>
        /// Add functionality that will calculate the total value of all items in the basket
        /// </summary>
        [Fact]
        public void BasketCalculateTotal()
        {
            var basket = new ShoppingBasket();
            var items = new List<IShoppingBasketItem>
            {
                new ShoppingBasketItem(new Sofa("1 seat sofa", 1, 200), 1),
                new ShoppingBasketItem(new Sofa("2 seat sofa", 2, 500), 1),
                new ShoppingBasketItem(new Sofa("3 seat sofa", 3, 1000), 1)
            };
            basket.Items = items;
            Assert.Equal(1700, basket.Subtotal);
        }
        
        /// <summary>
        /// Add functionality that will return the discount value of a 10% staff discount applied to the basket
        /// </summary>
        [Fact]
        public void BasketCalculateStaffDiscount()
        {
            var basket = new ShoppingBasket();
            var items = new List<IShoppingBasketItem>
            {
                new ShoppingBasketItem(new Sofa("1 seat sofa", 1, 200), 1),
                new ShoppingBasketItem(new Sofa("2 seat sofa", 2, 500), 1),
                new ShoppingBasketItem(new Sofa("3 seat sofa", 3, 1000), 1)
            };
            basket.Items = items;
            basket.StaffDiscountApplied = true;
            Assert.Equal(170, basket.Discount);
        }
        
        /// <summary>
        /// Return all items in the basket with the staff discount applied.
        /// Staff discount applies on the basket, therefore all items?
        /// </summary>
        [Fact]
        public void BasketReturnItemsWithStaffDiscount()
        {
            var basket = new ShoppingBasket();
            var items = new List<IShoppingBasketItem>
            {
                new ShoppingBasketItem(new Sofa("1 seat sofa", 1, 200), 1),
                new ShoppingBasketItem(new Sofa("2 seat sofa", 2, 500), 1),
                new ShoppingBasketItem(new Sofa("3 seat sofa", 3, 1000), 1)
            };
            basket.Items = items;
            basket.StaffDiscountApplied = true;
            Assert.Equal(3, basket.Items.Count);
        }
        
        /// <summary>
        /// Sort and return the basket, highest price first
        /// </summary>
        [Fact]
        public void BasketSortByHighestPrice()
        {
            var basket = new ShoppingBasket();
            var items = new List<IShoppingBasketItem>
            {
                new ShoppingBasketItem(new Sofa("1 seat sofa", 1, 200), 1),
                new ShoppingBasketItem(new Sofa("2 seat sofa", 2, 500), 1),
                new ShoppingBasketItem(new Sofa("3 seat sofa", 3, 1000), 1)
            };
            basket.Items = items;
            Assert.Equal(items.OrderByDescending(c => c.Product.Price), basket.Items);
        }
        
        /// <summary>
        /// Create a product of type table
        /// </summary>
        [Fact]
        public void TableProduct()
        {
            var table = new Table("table", 150);
            Assert.True(table.GetType().BaseType == typeof(Product));
        }
        
        /// <summary>
        /// Create a product of type sofa. Sofas should have a number of seats property
        /// </summary>
        [Fact]
        public void SofaProduct()
        {
            var sofa = new Sofa("3 seat sofa", 3, 1000);
            Assert.True(sofa.GetType().BaseType == typeof(Product) && sofa.GetType().GetProperties().FirstOrDefault(c => c.Name == "Seats" && c.MemberType == MemberTypes.Property && c.PropertyType == typeof(int)) != null);
        }

        /// <summary>
        /// Tables will only have 5% staff discount
        /// Contradicts "Add functionality that will return the discount value of a 10% staff discount applied to the basket."
        /// Staff discount applies to the basket therefore any table staff discounts will be ignored
        /// Only one type of discount can be applied, the highest discount should be used.
        /// </summary>
        [Fact]
        public void TableStaffDiscount()
        {
            var basket = new ShoppingBasket();
            var items = new List<IShoppingBasketItem>
            {
                new ShoppingBasketItem(new Table("table", 150), 2),
            };
            basket.Items = items;
            basket.StaffDiscountApplied = true;
            Assert.Equal(30, basket.Discount); // 10% staff discount has precedence over the table's 5% staff discount 
        }
        
        /// <summary>
        /// If you have a 2 seat sofa, a 1 seat sofa and  a table you get a bundle discount of 15%
        /// </summary>
        [Fact]
        public void BundleDiscount()
        {
            var basket = new ShoppingBasket();
            var items = new List<IShoppingBasketItem>
            {
                new ShoppingBasketItem(new Sofa("1 seat sofa", 1, 200), 1),
                new ShoppingBasketItem(new Sofa("2 seat sofa", 2, 500), 1),
                new ShoppingBasketItem(new Table("table", 150), 2)
            };
            basket.Items = items;
            Assert.Equal(150, basket.Discount);
        }
        
        /// <summary>
        /// Only one type of discount can be applied, the highest discount should be used
        /// </summary>
        [Fact]
        public void OnlyHighestDiscountApplies()
        {
            var basket = new ShoppingBasket();
            var items = new List<IShoppingBasketItem>
            {
                new ShoppingBasketItem(new Sofa("1 seat sofa", 1, 200), 1),
                new ShoppingBasketItem(new Sofa("2 seat sofa", 2, 500), 1),
                new ShoppingBasketItem(new Table("table", 150), 2)
            };
            basket.Items = items;
            basket.StaffDiscountApplied = true;
            Assert.Equal(150, basket.Discount);
        }
    }
}