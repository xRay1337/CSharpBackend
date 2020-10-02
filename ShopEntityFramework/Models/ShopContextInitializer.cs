using System.Data.Entity;

namespace ShopEntityFramework.Models
{
    class ShopContextInitializer : CreateDatabaseIfNotExists<ShopContext>
    {
    }
}