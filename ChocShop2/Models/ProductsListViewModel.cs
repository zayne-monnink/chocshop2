namespace ChocShop2.Models
{
    public class ProductsListViewModel
    {
        public List<ListProductModel> Products { get; set; }

        public class ListProductModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
        }

    }
}