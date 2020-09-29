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
            var productsCount = (int)DataBase.Execute(getProductsCount, connectionString, SqlOperation.ExecuteScalar);
            Console.WriteLine($"Кол-во товаров: {productsCount}");

            var getCategoriesCount = "SELECT COUNT(*) FROM [SHOP].[CATEGORIES]";
            var categoriesCount = (int)DataBase.Execute(getCategoriesCount, connectionString, SqlOperation.ExecuteScalar);

            //Вставка новой категории
            var newCategoryId = categoriesCount + 1;
            var addNewCategory = $"INSERT INTO [SHOP].[CATEGORIES] ([NAME]) VALUES ('Category {newCategoryId}')";
            DataBase.Execute(addNewCategory, connectionString, SqlOperation.ExecuteNonQuery);
            Console.WriteLine($"Добавлена категория: Category {categoriesCount}");

            //Вставка нового продукта
            var newProductId = productsCount + 1;
            var addNewProduct = $"INSERT INTO [SHOP].[PRODUCTS] ([CATEGORY_ID], [NAME]) VALUES ({newCategoryId}, 'Product {newProductId}')";
            DataBase.Execute(addNewProduct, connectionString, SqlOperation.ExecuteNonQuery);
            Console.WriteLine($"Добавлен продукт: Product {newProductId}");

            //Обновление нового продукта
            var updateNewProduct = $"UPDATE [SHOP].[PRODUCTS] SET [NAME] = 'To remove' WHERE [NAME] = 'Product {newProductId}'";
            DataBase.Execute(updateNewProduct, connectionString, SqlOperation.ExecuteNonQuery);
            Console.WriteLine($"Обновлён продукт: Product {newProductId}");

            //Удаление нового продукта
            var dateteNewProduct = $"DELETE FROM [SHOP].[PRODUCTS] WHERE [NAME] = 'To remove'";
            DataBase.Execute(dateteNewProduct, connectionString, SqlOperation.ExecuteNonQuery);
            Console.WriteLine($"Удалён продукт: Product {newProductId}");

            var selectProducts = @" SELECT TOP(10)
                                            [P].[ID] AS [PRODUCT_ID],
                                            [P].[NAME] AS [PRODUCT_NAME],
		                                    [C].[NAME] AS [CATEGORY_NAME]
                                    FROM [SHOP].[PRODUCTS] AS[P]
                                    INNER JOIN [SHOP].[CATEGORIES] AS [C]
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
            var productsTable = (DataTable)DataBase.Execute(selectProducts, connectionString, SqlOperation.ExecuteDataTable);

            Console.WriteLine($"[{productsTable.Columns[0].ColumnName}] \t [{productsTable.Columns[1].ColumnName}] \t\t [{productsTable.Columns[2].ColumnName}]");

            foreach (DataRow productsRow in productsTable.Rows)
            {
                Console.WriteLine($"ID: {productsRow[0]}   \t Product: {productsRow[1]}     \t Category: {productsRow[2]}");
            }

            Console.ReadKey();
        }
    }
}