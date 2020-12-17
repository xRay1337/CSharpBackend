using System;
using System.Linq;

namespace ShopEntityFramework
{
    internal class Program
    {
        internal static void Main()
        {
            using (var db = new ShopContext())
            {
                // Найти самый часто покупаемый товар
                var topProduct = db.Products
                    .Select(p => new { p.Name, Count = p.Orders.Sum(o => o.Count) })
                    .OrderByDescending(p => p.Count)
                    .FirstOrDefault();

                Console.WriteLine($"Хит продаж: {topProduct.Name}");

                Console.WriteLine();

                // Найти сколько каждый клиент потратил денег за все время
                var spending = db.Customers
                    .Select(c => new { c.LastName, Amount = c.Orders.Sum(o => o.Count * o.Product.Price ?? 0) })
                    .OrderByDescending(c => c.Amount)
                    .ThenBy(c => c.LastName)
                    .ToArray();

                foreach (var s in spending)
                {
                    Console.WriteLine($"Сумма покупок: {s.Amount:00000000} р. {s.LastName}");
                }

                Console.WriteLine();

                // Вывести сколько товаров каждой категории купили
                var salesByCategory = db.Categories
                    .Select(c => new { c.Name, Count = c.CategoryProducts.Sum(p => p.Product.Orders.Sum(o => o.Count)) })
                    .OrderByDescending(c => c.Count)
                    .ThenBy(c => c.Name)
                    .ToArray();

                foreach (var s in salesByCategory)
                {
                    Console.WriteLine($"Кол-во продаж: {s.Count:00000000}\t{s.Name}");
                }
            }

            Console.ReadKey();
        }
    }
}