using ChocShop2.DAL.Entities;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace ChocShop2.DAL;

public class ProductRepository
{
    private IDocumentStore _store;
    public ProductRepository()
    {
        _store = DocumentStoreHolder.Store;
    }

    public List<Product> GetProducts()
    {
        using (IDocumentSession session = _store.OpenSession())
        {
            var products = session.Query<Product>()
                .OrderBy(x => x.ProductName)
                .ToList();
            return products;
        }
    }

    public Product GetProduct(string id)
    {
        using (IDocumentSession session = _store.OpenSession())
        {
            var product = session.Query<Product>()
                .Where(x => x.Id == id)
                .FirstOrDefault();
            return product;
        }
    }

}

