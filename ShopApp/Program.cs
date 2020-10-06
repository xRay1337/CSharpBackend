using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ShopApp
{
    public class Program
    {
        public static void Main()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ShopConnection"].ToString();

            var getProductsCountQuery = @"  SELECT COUNT(*)
                                            FROM [Shop].[Products]";
            var selectProductsCountCmd = new SqlCommand(getProductsCountQuery);
            var productsCount = (int)Database.ExecuteScalar(selectProductsCountCmd, connectionString);
            Console.WriteLine($"Кол-во товаров: {productsCount}");

            var getCategoriesCountQuery = @"SELECT COUNT(*)
                                            FROM [Shop].[Categories]";
            var selectCategoriesCountCmd = new SqlCommand(getCategoriesCountQuery);
            var categoriesCount = (int)Database.ExecuteScalar(selectCategoriesCountCmd, connectionString);

            //Вставка новой категории
            var newCategoryId = categoriesCount + 1;
            var insertNewCategoryQuery = @" INSERT INTO [Shop].[Categories] ([Name])
                                            VALUES (CONCAT('Category ', @newCategoryId))";
            var insertNewCategoryCmd = new SqlCommand(insertNewCategoryQuery);
            insertNewCategoryCmd.Parameters.Add(new SqlParameter("@newCategoryId", newCategoryId) { SqlDbType = SqlDbType.Int });
            Database.ExecuteNonQuery(insertNewCategoryCmd, connectionString);
            Console.WriteLine($"Добавлена категория: Category {categoriesCount}");

            //Вставка нового продукта
            var newProductId = productsCount + 1;
            var insertNewProductQuery = @"  INSERT INTO [Shop].[Products] ([CategoryId], [Name])
                                            VALUES (@newCategoryId, CONCAT('Product ', @newProductId))";
            var insertNewProductCmd = new SqlCommand(insertNewProductQuery);
            insertNewProductCmd.Parameters.Add(new SqlParameter("@newCategoryId", newCategoryId) { SqlDbType = SqlDbType.Int });
            insertNewProductCmd.Parameters.Add(new SqlParameter("@newProductId", newProductId) { SqlDbType = SqlDbType.Int });
            Database.ExecuteNonQuery(insertNewProductCmd, connectionString);
            Console.WriteLine($"Добавлен продукт: Product {newProductId}");

            //Обновление нового продукта
            var updateNewProductQuery = @"  UPDATE [Shop].[Products]
                                            SET [Name] = @newValue
                                            WHERE [Name] = CONCAT('Product ', @newProductId)";
            var updateNewProductCmd = new SqlCommand(updateNewProductQuery);
            updateNewProductCmd.Parameters.Add(new SqlParameter("@newProductId", newProductId) { SqlDbType = SqlDbType.Int });
            updateNewProductCmd.Parameters.Add(new SqlParameter("@newValue", "To remove") { SqlDbType = SqlDbType.NVarChar });
            Database.ExecuteNonQuery(updateNewProductCmd, connectionString);
            Console.WriteLine($"Обновлён продукт: Product {newProductId}");

            //Удаление нового продукта
            var dateteNewProductQuery = @"  DELETE FROM [Shop].[Products]
                                            WHERE [Name] = @value";
            var dateteNewProductCmd = new SqlCommand(dateteNewProductQuery);
            dateteNewProductCmd.Parameters.Add(new SqlParameter("@value", "To remove") { SqlDbType = SqlDbType.NVarChar });
            Database.ExecuteNonQuery(dateteNewProductCmd, connectionString);
            Console.WriteLine($"Удалён продукт: Product {newProductId}");

            var selectProducts = @" SELECT TOP(10)
                                            [P].[Id] AS [ProductId],
                                            [P].[Name] AS [ProductName],
		                                    [C].[Name] AS [CategoryName]
                                    FROM [Shop].[Products] AS [P]
                                    INNER JOIN [Shop].[Categories] AS [C]
                                    ON [P].[CategoryId] = [C].[Id]
                                    ORDER BY [ProductId]";

            Console.WriteLine("Выгрузить весь список товаров вместе с именами категорий через reader, и распечатайте все данные в цикле:");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(selectProducts, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("[ProductId] \t [ProductName] \t\t [CategoryName]");

                        while (reader.Read())
                        {
                            Console.WriteLine("ID: {0}   \t Product: {1}     \t Category: {2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        }
                    }
                }
            }

            Console.WriteLine("Выгрузить весь список товаров вместе с именами категорий в DataSet через SqlDataAdapter, и распечатайте все данные в цикле:");
            var productsTable = Database.ExecuteDataTable(selectProducts, connectionString);

            Console.WriteLine($"[{productsTable.Columns[0].ColumnName}] \t [{productsTable.Columns[1].ColumnName}] \t\t [{productsTable.Columns[2].ColumnName}]");

            foreach (DataRow productsRow in productsTable.Rows)
            {
                Console.WriteLine($"ID: {productsRow[0]}   \t Product: {productsRow[1]}     \t Category: {productsRow[2]}");
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var insertNewCategoryAndProduct = connection.BeginTransaction();
                try
                {
                    var insertCategory = @" INSERT INTO [Shop].[Categories] ([Name])
                                            VALUES (CONCAT('Category ', @categoryId))"; ;
                    var insertNewCategory = new SqlCommand(insertCategory, connection);
                    insertNewCategory.Parameters.Add(new SqlParameter("@categoryId", newCategoryId + 1) { SqlDbType = SqlDbType.Int });
                    insertNewCategory.Transaction = insertNewCategoryAndProduct;
                    insertNewCategory.ExecuteNonQuery();

                    //throw new Exception();

                    var insertProduct = @"  INSERT INTO [Shop].[Products] ([CategoryId], [Name])
                                            VALUES (@categoryId, CONCAT('Product ', @productId))";
                    var insertNewProduct = new SqlCommand(insertProduct, connection);
                    insertNewProduct.Parameters.Add(new SqlParameter("@categoryId", newCategoryId) { SqlDbType = SqlDbType.Int });
                    insertNewProduct.Parameters.Add(new SqlParameter("@productId", newProductId) { SqlDbType = SqlDbType.Int });
                    insertNewProduct.Transaction = insertNewCategoryAndProduct;
                    insertNewProduct.ExecuteNonQuery();

                    insertNewCategoryAndProduct.Commit();
                    Console.WriteLine("Транзакция успешно завершена");
                }
                catch (Exception ex)
                {
                    insertNewCategoryAndProduct.Rollback();
                    Console.WriteLine($"Откат транзакции: {ex.Message}");
                }
            }
            Console.ReadKey();
        }
    }
}