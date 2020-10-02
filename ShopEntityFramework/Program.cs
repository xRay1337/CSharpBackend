using ShopEntityFramework.Models;
using System;
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
                var processors = db.Categories.Add(new Category("Процессоры"));
                var videoCards = db.Categories.Add(new Category("Видеокарты"));
                var randomAccessMemory = db.Categories.Add(new Category("Оперативная память"));

                db.SaveChanges();

                var processor1 = db.Products.Add(new Product(processors.Id, "Intel Core i5 Coffee Lake", 11_999));
                var processor2 = db.Products.Add(new Product(processors.Id, "Intel Xeon Platinum Skylake", 199_628));
                var processor3 = db.Products.Add(new Product(processors.Id, "Intel Core i3-9100F", 6_290));
                var processor4 = db.Products.Add(new Product(processors.Id, "AMD Ryzen Threadripper Colfax", 34_360));
                var processor5 = db.Products.Add(new Product(processors.Id, "AMD Ryzen 5 2600", 11_290));

                var videoCard1 = db.Products.Add(new Product(videoCards.Id, "GIGABYTE GeForce GTX 1660", 21_990));
                var videoCard2 = db.Products.Add(new Product(videoCards.Id, "MSI GeForce RTX 2060 SUPER", 40_340));
                var videoCard3 = db.Products.Add(new Product(videoCards.Id, "GIGABYTE GeForce GTX 1650", 15_870));
                var videoCard4 = db.Products.Add(new Product(videoCards.Id, "Palit GeForce GTX 1650", 14_190));
                var videoCard5 = db.Products.Add(new Product(videoCards.Id, "GeForce GTX 1050 Ti", 11_610));

                var randomAccessMemory1 = db.Products.Add(new Product(randomAccessMemory.Id, "Samsung M378A1K43CB2-CTD", 2_650));
                var randomAccessMemory2 = db.Products.Add(new Product(randomAccessMemory.Id, "HyperX Fury HX426C16FB3K2/16", 5_990));
                var randomAccessMemory3 = db.Products.Add(new Product(randomAccessMemory.Id, "AMD Radeon R7 Performance R744G2400U1S-UO", 1_290));

                var customer1 = db.Customers.Add(new Customer("Злобин", "Виталий", "Андреевич", "89137373455", "zlovian@yandex.ru"));
                var customer2 = db.Customers.Add(new Customer("Григорьев", "Игорь", "Дмитриевич", "89529373625", "grigid@yandex.ru"));
                var customer3 = db.Customers.Add(new Customer("Зебницкий", "Илья", "Валерьевич", "89065473638", "zebniciv@yandex.ru"));

                db.SaveChanges();

                db.Orders.Add(new Order(customer1.Id, processor1.Id, DateTime.Now));
                db.Orders.Add(new Order(customer1.Id, videoCard1.Id, DateTime.Now));
                db.Orders.Add(new Order(customer1.Id, randomAccessMemory1.Id, DateTime.Now));

                Thread.Sleep(500);

                db.Orders.Add(new Order(customer2.Id, processor2.Id, DateTime.Now));
                db.Orders.Add(new Order(customer2.Id, videoCard2.Id, DateTime.Now));
                db.Orders.Add(new Order(customer2.Id, randomAccessMemory2.Id, DateTime.Now));
                db.Orders.Add(new Order(customer2.Id, randomAccessMemory2.Id, DateTime.Now));
                db.Orders.Add(new Order(customer2.Id, randomAccessMemory2.Id, DateTime.Now));

                Thread.Sleep(500);

                db.Orders.Add(new Order(customer3.Id, processor3.Id, DateTime.Now));
                db.Orders.Add(new Order(customer3.Id, videoCard3.Id, DateTime.Now));
                db.Orders.Add(new Order(customer3.Id, randomAccessMemory3.Id, DateTime.Now));

                db.SaveChanges();

                //TODO
                db.Database.ExecuteSqlCommand("INSERT INTO [ProductCategories] SELECT [Id], [CategoryId] FROM [Products]");

                //Найти самый часто покупаемый товар
                var topProduct = db.Orders.Select(o => o.Product)
                    .GroupBy(p => p.Name)
                    .Select(group => new
                    {
                        Name = group.Key,
                        Count = group.Count()
                    })
                    .OrderByDescending(p => p.Count)
                    .Select(p => p.Name)
                    .FirstOrDefault();

                Console.WriteLine($"Хит продаж: {topProduct}");

                Console.WriteLine();
                //Найти сколько каждый клиент потратил денег за все время
                var spending = db.Orders.Select(o => new
                    {
                        LastName = o.Customer.LastName,
                        Amount = o.Product.Price
                    })
                    .GroupBy(ln => ln.LastName, a => a.Amount)
                    .Select(group => new
                    {
                        LastName = group.Key,
                        Amount = group.Sum(a => a.Value)
                    })
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
                    .SelectMany(o => o.Product.Categories)
                    .GroupBy(c => c.Name)
                    .Select(group => new
                    {
                        Name = group.Key,
                        Count = group.Count()
                    })
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