namespace AnotherShoppingBasketLibrary.Models.ProductTypes
{
    public class Sofa : Product, ISofa
    {
        public int Seats { get; set; }

        public Sofa(string name, int seats, decimal price) : base(name, price)
        {
            Seats = seats;
        }
    }
}