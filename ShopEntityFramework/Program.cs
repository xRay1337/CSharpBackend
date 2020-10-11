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
                    .AsNoTracking()
                    .GroupBy(o => o.Product.Name)
                    .OrderByDescending(g => g.Count())
                    .Select(n => n.Key)
                    .FirstOrDefault();

                Console.WriteLine($"Хит продаж: {topProduct}");

                Console.WriteLine();

                // Найти сколько каждый клиент потратил денег за все время
                var spending = db.Orders
                    .AsNoTracking()
                    .GroupBy(Id => Id.Customer.Id, a => a.Product.Price)
                    .Select(group => new { CustomerId = group.Key, Amount = group.Sum(a => a.Value) })
                    .Join(db.Customers,
                        o => o.CustomerId,
                        c => c.Id,
                        (o, c) => new { c.LastName, o.Amount })
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
                    .AsNoTracking()
                    .SelectMany(o => o.Product.Categories)
                    .GroupBy(c => c.Name)
                    .Select(group => new { Name = group.Key, Count = group.Count() })
                    .OrderByDescending(c => c.Count)
                    .ThenBy(c => c.Name)
                    .ToArray();

                foreach (var s in salesByCategory)
                {
                    Console.WriteLine($"Кол-во продаж: {s.Count.ToString().PadLeft(7, '0')}\t{s.Name}");
                }
            }

            Console.ReadKey();
        }
    }
}