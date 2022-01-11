using ChocShop2.DAL;
using ChocShop2.DAL.Entities;

namespace ChocShop2
{
    public class DbInitializer
    {
        public static void InitDb()
        {
            var products = new List<Product>();
            products.Add(new Product { ProductName="Kitkat", Price = 1.11m });
            products.Add(new Product { ProductName = "Mars", Price = 2.45m });
            products.Add(new Product { ProductName = "Picnic", Price = 3.5m });
            products.Add(new Product { ProductName = "Turkish Delight", Price = 4.99m });
            products.Add(new Product { ProductName = "Cadbury Dairy Milk", Price = 10.00m });

            InitRepository initRepository = new InitRepository();
            initRepository.InitDbDataIfNotExists(products);
        }
    }
}
