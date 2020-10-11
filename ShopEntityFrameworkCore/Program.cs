using Microsoft.EntityFrameworkCore;
using ShopEntityFrameworkCore.Models;
using System;
using System.Linq;
using System.Threading;

namespace ShopEntityFrameworkCore
{
    internal class Program
    {
        internal static void Main()
        {
            using (var context = new ShopContext())
            {
                #region Database initialization

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var processors = context.Categories.Add(new Category("Процессоры"));
                var videoCards = context.Categories.Add(new Category("Видеокарты"));
                var randomAccessMemory = context.Categories.Add(new Category("Оперативная память"));

                context.SaveChanges();

                var processor1 = context.Products.Add(new Product("Intel Core i5 Coffee Lake", 11_999));
                var processor2 = context.Products.Add(new Product("Intel Xeon Platinum Skylake", 199_628));
                var processor3 = context.Products.Add(new Product("Intel Core i3-9100F", 6_290));
                var processor4 = context.Products.Add(new Product("AMD Ryzen Threadripper Colfax", 34_360));
                var processor5 = context.Products.Add(new Product("AMD Ryzen 5 2600", 11_290));

                var videoCard1 = context.Products.Add(new Product("GIGABYTE GeForce GTX 1660", 21_990));
                var videoCard2 = context.Products.Add(new Product("MSI GeForce RTX 2060 SUPER", 40_340));
                var videoCard3 = context.Products.Add(new Product("GIGABYTE GeForce GTX 1650", 15_870));
                var videoCard4 = context.Products.Add(new Product("Palit GeForce GTX 1650", 14_190));
                var videoCard5 = context.Products.Add(new Product("GeForce GTX 1050 Ti", 11_610));

                var randomAccessMemory1 = context.Products.Add(new Product("Samsung M378A1K43CB2-CTD", 2_650));
                var randomAccessMemory2 = context.Products.Add(new Product("HyperX Fury HX426C16FB3K2/16", 5_990));
                var randomAccessMemory3 = context.Products.Add(new Product("AMD Radeon R7 Performance R744G2400U1S-UO", 1_290));

                var customer1 = context.Customers.Add(new Customer("Злобин", "Виталий", "Андреевич", "89137373455", "zlovian@yandex.ru"));
                var customer2 = context.Customers.Add(new Customer("Григорьев", "Игорь", "Дмитриевич", "89529373625", "grigid@yandex.ru"));
                var customer3 = context.Customers.Add(new Customer("Зебницкий", "Илья", "Валерьевич", "89065473638", "zebniciv@yandex.ru"));

                context.ProductCategories.Add(new ProductCategory(processor1, processors));
                context.ProductCategories.Add(new ProductCategory(processor2, processors));
                context.ProductCategories.Add(new ProductCategory(processor3, processors));
                context.ProductCategories.Add(new ProductCategory(processor4, processors));
                context.ProductCategories.Add(new ProductCategory(processor5, processors));

                context.ProductCategories.Add(new ProductCategory(videoCard1, videoCards));
                context.ProductCategories.Add(new ProductCategory(videoCard2, videoCards));
                context.ProductCategories.Add(new ProductCategory(videoCard3, videoCards));
                context.ProductCategories.Add(new ProductCategory(videoCard4, videoCards));
                context.ProductCategories.Add(new ProductCategory(videoCard5, videoCards));

                context.ProductCategories.Add(new ProductCategory(randomAccessMemory1, randomAccessMemory));
                context.ProductCategories.Add(new ProductCategory(randomAccessMemory2, randomAccessMemory));
                context.ProductCategories.Add(new ProductCategory(randomAccessMemory3, randomAccessMemory));

                context.SaveChanges();

                context.Orders.Add(new Order(customer1.Entity.Id, processor1.Entity.Id, DateTime.Now));
                context.Orders.Add(new Order(customer1.Entity.Id, videoCard1.Entity.Id, DateTime.Now));
                context.Orders.Add(new Order(customer1.Entity.Id, randomAccessMemory1.Entity.Id, DateTime.Now));

                Thread.Sleep(500);

                context.Orders.Add(new Order(customer2.Entity.Id, processor2.Entity.Id, DateTime.Now));
                context.Orders.Add(new Order(customer2.Entity.Id, videoCard2.Entity.Id, DateTime.Now));
                context.Orders.Add(new Order(customer2.Entity.Id, randomAccessMemory2.Entity.Id, DateTime.Now));
                context.Orders.Add(new Order(customer2.Entity.Id, randomAccessMemory2.Entity.Id, DateTime.Now));
                context.Orders.Add(new Order(customer2.Entity.Id, randomAccessMemory2.Entity.Id, DateTime.Now));

                Thread.Sleep(500);

                context.Orders.Add(new Order(customer3.Entity.Id, processor3.Entity.Id, DateTime.Now));
                context.Orders.Add(new Order(customer3.Entity.Id, videoCard3.Entity.Id, DateTime.Now));
                context.Orders.Add(new Order(customer3.Entity.Id, randomAccessMemory3.Entity.Id, DateTime.Now));

                context.SaveChanges();

                #endregion

                // Найти самый часто покупаемый товар
                var topProduct = context.Orders
                    .AsNoTracking()
                    .GroupBy(o => o.Product.Name)
                    .OrderByDescending(g => g.Count())
                    .Select(n => n.Key)
                    .FirstOrDefault();

                Console.WriteLine($"Хит продаж: {topProduct}");

                Console.WriteLine();

                // Найти сколько каждый клиент потратил денег за все время
                var spending = context.Orders
                    .AsNoTracking()
                    .GroupBy(Id => Id.Customer.Id, a => a.Product.Price)
                    .Select(group => new { CustomerId = group.Key, Amount = group.Sum(a => a.Value) })
                    .Join(context.Customers,
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
                var salesByCategory = context.Orders
                    .AsNoTracking()
                    .Join(context.ProductCategories,
                        o => o.ProductId,
                        pc => pc.ProductId,
                        (o, pc) => pc.Category.Name)
                    .GroupBy(n => n)
                    .Select(group => new { Name = group.Key, Count = group.Count() })
                    .OrderByDescending(c => c.Count)
                    .ThenBy(c => c.Name)
                    .ToArray();

                foreach (var s in salesByCategory)
                {
                    Console.WriteLine($"Кол-во продаж: {s.Count.ToString().PadLeft(7, '0')}\t{s.Name}");
                }
            }
        }
    }
}