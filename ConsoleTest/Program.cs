


using ConsoleTest.Model;
using ConsoleTest.Services;

var shoes = new Product() { Name = "my shoes", Price = 100 };
Discount.ApplyDiscount(shoes);
Console.WriteLine(shoes);
Console.WriteLine("the output is my shoes 100");
//the output is my shoes 100