namespace AnotherShoppingBasketLibrary.Models
{
    public abstract class Product : IProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        protected Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }
}