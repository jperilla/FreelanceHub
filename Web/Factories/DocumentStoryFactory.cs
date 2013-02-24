using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Document;
using Raven.Database.Server;

namespace Web.Factories
{    
    public static class DocumentStoreFactory
    {
        public static IDocumentStore CreateDocumentStoreLocal()
        {
            var documentStore = new EmbeddableDocumentStore
            {
                RunInMemory = true,
                ConnectionStringName = "RavenDB",
                UseEmbeddedHttpServer = true,
                Configuration = { Port = 8080 }

            };
            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);
            documentStore.Initialize();

            return documentStore;
        }

        public static IDocumentStore CreateDocumentStoreServer()
        {
            var documentStore = new DocumentStore { ConnectionStringName = "RavenDB" };
            documentStore.Initialize();

            return documentStore;
        }
    }
}