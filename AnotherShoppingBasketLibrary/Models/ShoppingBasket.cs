using System.Collections.Generic;
using System.Linq;
using AnotherShoppingBasketLibrary.Models.ProductTypes;

namespace AnotherShoppingBasketLibrary.Models
{
    public class ShoppingBasket : IShoppingBasket
    {
        private ICollection<IShoppingBasketItem> _items = new List<IShoppingBasketItem>();
        public ICollection<IShoppingBasketItem> Items
        {
            get => _items.OrderByDescending(c => c.Product.Price).ToList();
            set => _items = value;
        }

        public bool StaffDiscountApplied { get; set; }
        public decimal Subtotal => _items.Sum(c => c.Product.Price * c.Quantity);

        public decimal Discount
        {
            get
            {
                var discount = 0.0;
                var oneSeatSofaAdded = _items.Any(c => c.Product.GetType() == typeof(Sofa) && ((Sofa)c.Product).Seats == 1);
                var twoSeatSofaAdded = _items.Any(c => c.Product.GetType() == typeof(Sofa) && ((Sofa)c.Product).Seats == 2);
                var tableAdded = _items.Any(c => c.Product.GetType() == typeof(Table));

                if (oneSeatSofaAdded && twoSeatSofaAdded && tableAdded)
                {
                    discount = 0.15;
                }
                
                else if (StaffDiscountApplied)
                {
                    discount = 0.1;
                }
                
                else if (tableAdded)
                {
                    discount = 0.05;
                }

                return Subtotal * (decimal)discount;
            }
        }

        public decimal Total => Subtotal - Discount;
    }
}