using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

namespace ChocShop2.DAL;

public class DocumentStoreHolder
{
    //private static string _connectionString = "http://localhost:8080";
    private static string _connectionString = "http://ravendb:8080";
    private static string _defaultDatabaseName = "chocshop";
    // You can also use http://localhost:8080 to access the RavenDB management UI

    // Use Lazy<IDocumentStore> to initialize the document store lazily. 
    // This ensures that it is created only once - when first accessing the public `Store` property.
    private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

    public static IDocumentStore Store => store.Value;

    private static IDocumentStore CreateStore()
    {
        IDocumentStore store = new DocumentStore()
        {
            // Define the cluster node URLs (required)
            Urls = new[] { _connectionString, 
                           /*some additional nodes of this cluster*/ },

            // Set conventions as necessary (optional)
            Conventions =
            {
                MaxNumberOfRequestsPerSession = 10,
                UseOptimisticConcurrency = true,
            },

            // Define a default database (optional)
            Database = _defaultDatabaseName,

            // Define a client certificate (optional)
            //Certificate = new X509Certificate2("C:\\path_to_your_pfx_file\\cert.pfx"),

            // Initialize the Document Store
        }.Initialize();

        var result = store.Maintenance.Server.Send(new GetDatabaseRecordOperation(_defaultDatabaseName));
        if (result == null)
        {
            store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(_defaultDatabaseName)));
        }
        

        return store;
    }
}