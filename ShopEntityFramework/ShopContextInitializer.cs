﻿using ShopEntityFramework.Models;
using System.Data.Entity;
using System.Threading;

namespace ShopEntityFramework
{
    class ShopContextInitializer : CreateDatabaseIfNotExists<ShopContext>//DropCreateDatabaseAlways<ShopContext>
    {
        protected override void Seed(ShopContext context)
        {
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

            context.SaveChanges();

            context.CategoryProducts.Add(new CategoryProduct(processors, processor1));
            context.CategoryProducts.Add(new CategoryProduct(processors, processor1));
            context.CategoryProducts.Add(new CategoryProduct(processors, processor2));
            context.CategoryProducts.Add(new CategoryProduct(processors, processor3));
            context.CategoryProducts.Add(new CategoryProduct(processors, processor4));
            context.CategoryProducts.Add(new CategoryProduct(processors, processor5));
            context.CategoryProducts.Add(new CategoryProduct(videoCards, videoCard1));
            context.CategoryProducts.Add(new CategoryProduct(videoCards, videoCard2));
            context.CategoryProducts.Add(new CategoryProduct(videoCards, videoCard3));
            context.CategoryProducts.Add(new CategoryProduct(videoCards, videoCard4));
            context.CategoryProducts.Add(new CategoryProduct(videoCards, videoCard5));
            context.CategoryProducts.Add(new CategoryProduct(randomAccessMemory, randomAccessMemory1));
            context.CategoryProducts.Add(new CategoryProduct(randomAccessMemory, randomAccessMemory2));
            context.CategoryProducts.Add(new CategoryProduct(randomAccessMemory, randomAccessMemory3));

            context.Orders.Add(new Order(customer1.Id, processor1.Id, 1));
            context.Orders.Add(new Order(customer1.Id, videoCard1.Id, 1));
            context.Orders.Add(new Order(customer1.Id, randomAccessMemory1.Id, 1));

            Thread.Sleep(500);

            context.Orders.Add(new Order(customer2.Id, processor2.Id, 1));
            context.Orders.Add(new Order(customer2.Id, videoCard2.Id, 1));
            context.Orders.Add(new Order(customer2.Id, randomAccessMemory2.Id, 3));

            Thread.Sleep(500);

            context.Orders.Add(new Order(customer3.Id, processor3.Id, 1));
            context.Orders.Add(new Order(customer3.Id, videoCard3.Id, 1));
            context.Orders.Add(new Order(customer3.Id, randomAccessMemory3.Id, 1));

            context.SaveChanges();

            base.Seed(context);
        }
    }
}