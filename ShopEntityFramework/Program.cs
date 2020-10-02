using ShopEntityFramework.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;

namespace ShopEntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ShopContext())
            {
                //Найти самый часто покупаемый товар
                var topProduct = db.Orders
                    .AsNoTracking()
                    .Select(o => o.Product)
                    .GroupBy(p => p.Name)
                    .Select(group => new { Name = group.Key, Count = group.Count() })
                    .OrderByDescending(p => p.Count)
                    .Select(p => p.Name)
                    .FirstOrDefault();

                Console.WriteLine($"Хит продаж: {topProduct}");

                Console.WriteLine();

                //Найти сколько каждый клиент потратил денег за все время
                var spending = db.Orders
                    .AsNoTracking()
                    .Select(o => new { LastName = o.Customer.LastName, Amount = o.Product.Price })
                    .GroupBy(ln => ln.LastName, a => a.Amount)
                    .Select(group => new { LastName = group.Key, Amount = group.Sum(a => a.Value) })
                    .OrderByDescending(c => c.Amount)
                    .ThenBy(c => c.LastName)
                    .ToArray();

                foreach (var s in spending)
                {
                    Console.WriteLine($"Сумма покупок: {s.Amount.ToString().PadLeft(7, '0')}\t{s.LastName}");
                }

                Console.WriteLine();

                //Вывести сколько товаров каждой категории купили
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