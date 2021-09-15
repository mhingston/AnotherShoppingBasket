namespace AnotherShoppingBasketLibrary.Models
{
    public interface IShoppingBasketItem
    {
        public IProduct Product { get; set; }
        public int Quantity { get; set; }
    }
}