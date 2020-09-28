using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ShopApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ShopConnection"].ToString();

            var getProductsCount = "SELECT COUNT(*) FROM [SHOP].[PRODUCTS]";
            var productsCount = (int)DataBase.ExecuteScalar(getProductsCount, connectionString);
            Console.WriteLine($"Кол-во товаров: {productsCount}");

            var getCategoriesCount = "SELECT COUNT(*) FROM [SHOP].[CATEGORIES]";
            var categoriesCount = (int)DataBase.ExecuteScalar(getCategoriesCount, connectionString);

            //Вставка новой категории
            var newCategoryId = categoriesCount + 1;
            var addNewCategory = $"INSERT INTO [SHOP].[CATEGORIES] ([NAME]) VALUES ('Category {newCategoryId}')";
            DataBase.ExecuteNonQuery(addNewCategory, connectionString);
            Console.WriteLine($"Добавлена категория: Category {categoriesCount}");

            //Вставка нового продукта
            var newProductId = productsCount + 1;
            var addNewProduct = $"INSERT INTO [SHOP].[PRODUCTS] ([CATEGORY_ID], [NAME]) VALUES ({newCategoryId}, 'Product {newProductId}')";
            DataBase.ExecuteNonQuery(addNewProduct, connectionString);
            Console.WriteLine($"Добавлен продукт: Product {newProductId}");

            //Обновление нового продукта
            var updateNewProduct = $"UPDATE [SHOP].[PRODUCTS] SET [NAME] = 'To remove' WHERE [NAME] = 'Product {newProductId}'";
            DataBase.ExecuteNonQuery(updateNewProduct, connectionString);
            Console.WriteLine($"Обновлён продукт: Product {newProductId}");

            //Удаление нового продукта
            var dateteNewProduct = $"DELETE FROM [SHOP].[PRODUCTS] WHERE [NAME] = 'To remove'";
            DataBase.ExecuteNonQuery(dateteNewProduct, connectionString);
            Console.WriteLine($"Удалён продукт: Product {newProductId}");

            var selectProducts = @" SELECT TOP(10)
                                            [P].[ID] AS [PRODUCT_ID],
                                            [P].[NAME] AS [PRODUCT_NAME],
		                                    [C].[NAME] AS [CATEGORY_NAME]
                                    FROM [SHOP].[PRODUCTS] AS[P]
                                    JOIN [SHOP].[CATEGORIES] AS [C]
                                    ON [P].[CATEGORY_ID] = [C].[ID]
                                    ORDER BY [PRODUCT_ID]";

            Console.WriteLine("Выгрузить весь список товаров вместе с именами категорий через reader, и распечатайте все данные в цикле:");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(selectProducts, connection))
                {
                    var reader = command.ExecuteReader();

                    Console.WriteLine("[PRODUCT_ID] \t [PRODUCT_NAME] \t\t [CATEGORY_NAME]");

                    while (reader.Read())
                    {
                        Console.WriteLine("ID: {0}   \t Product: {1}     \t Category: {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                    }
                }
            }

            Console.WriteLine("Выгрузить весь список товаров вместе с именами категорий в DataSet через SqlDataAdapter, и распечатайте все данные в цикле:");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(selectProducts, connection))
                {
                    var sqlDataAdapter = new SqlDataAdapter(selectProducts, connection);
                    var products = new DataSet();
                    sqlDataAdapter.Fill(products, "Products");

                    var productsTable = products.Tables["Products"];

                    Console.WriteLine($"[{productsTable.Columns[0].ColumnName}] \t [{productsTable.Columns[1].ColumnName}] \t\t [{productsTable.Columns[2].ColumnName}]");

                    foreach (DataRow productsRow in products.Tables["Products"].Rows)
                    {
                        Console.WriteLine("ID: {0}   \t Product: {1}     \t Category: {2}", productsRow[0], productsRow[1], productsRow[2]);
                    }
                }
            }

            Console.ReadKey();
        }
    }
}