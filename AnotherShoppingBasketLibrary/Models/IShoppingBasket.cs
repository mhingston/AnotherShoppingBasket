using System.Collections.Generic;
using System.Linq;
using AnotherShoppingBasketLibrary.Models.ProductTypes;

namespace AnotherShoppingBasketLibrary.Models
{
    public interface IShoppingBasket
    {
        public ICollection<IShoppingBasketItem> Items { get; set; }
        public bool StaffDiscountApplied { get; set; }
        public decimal Subtotal { get; }
        public decimal Discount { get; }
        public decimal Total { get; }
    }
}