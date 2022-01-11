using ChocShop2.DAL.Entities;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace ChocShop2.DAL;

public class OrderRepository
{
    private IDocumentStore _store;
    public OrderRepository()
    {
        _store = DocumentStoreHolder.Store;
    }

    public void CreateOrder(Order order)
    {
        using (IDocumentSession session = _store.OpenSession())
        {
            session.Store(order);

            session.SaveChanges();
        }
    }

}

