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
                var topProduct = db.Orders
                    .GroupBy(o => o.Product.Name)
                    .Select(g => new { Name = g.Key, Count = g.Sum(s => s.Count) })
                    .OrderByDescending(p => p.Count)
                    .Select(p => p.Name)
                    .FirstOrDefault();

                Console.WriteLine($"Хит продаж: {topProduct}");

                Console.WriteLine();

                // Найти сколько каждый клиент потратил денег за все время
                var spending = db.Customers
                    .GroupBy(c => c.LastName, c => c.Orders.Sum(a => a.Count * a.Product.Price ?? 0))
                    .Select(g => new { LastName = g.Key, Amount = g.Sum(a => a)})
                    .OrderByDescending(c => c.Amount)
                    .ThenBy(c => c.LastName)
                    .ToArray();

                foreach (var s in spending)
                {
                    Console.WriteLine($"Сумма покупок: {s.Amount.ToString().PadLeft(7, '0')} р. {s.LastName}");
                }

                Console.WriteLine();

                // Вывести сколько товаров каждой категории купили
                var salesByCategory = db.Orders
                    .GroupBy(n => n.Product.Name, c => c.Count)
                    .Select(g => new { ProductName = g.Key, Count = g.Sum(c => c) })
                    .OrderByDescending(c => c.Count)
                    .ThenBy(c => c.ProductName)
                    .ToArray();

                foreach (var s in salesByCategory)
                {
                    Console.WriteLine($"Кол-во продаж: {s.Count.ToString().PadLeft(7, '0')}\t{s.ProductName}");
                }
            }

            Console.ReadKey();
        }
    }
}