using ChocShop2.DAL.Entities;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace ChocShop2.DAL;

public class InitRepository
{
    private IDocumentStore _store;
    public InitRepository()
    {
        _store = DocumentStoreHolder.Store;
    }

    public void InitDbDataIfNotExists(List<Product> products)
    {
        using (IDocumentSession session = _store.OpenSession())
        {
            // Check if the products table has been initialised
            var existingCount = session.Query<Product>()
                .Count();
            if (existingCount > 0)
                return;

            foreach (var product in products)
            {
                session.Store(product);
            }

            session.SaveChanges();
        }
    }



}

