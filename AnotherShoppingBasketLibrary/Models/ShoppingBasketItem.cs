namespace AnotherShoppingBasketLibrary.Models
{
    public class ShoppingBasketItem : IShoppingBasketItem
    {
        public IProduct Product { get; set; }
        public int Quantity { get; set; }

        public ShoppingBasketItem(IProduct product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}