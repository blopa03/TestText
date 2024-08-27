using ConsoleTest.Model;

namespace ConsoleTest.Services
{
    static class Discount
    {
        public static void ApplyDiscount(Product shoes)
        {
            shoes = new Product { Name = "disounted shows ", Price = shoes.Price / 2 };
        }
    }
}
