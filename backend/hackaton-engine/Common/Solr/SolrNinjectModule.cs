using Common.Mongo.Repositories;
using Microsoft.Practices.ServiceLocation;
using MongoDB.Driver;
using Ninject;
using Ninject.Modules;
using SolrNet;

namespace Common.Solr
{
    public class SolrNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(ISolrOperations<>))
                .To(typeof(SolrConnector<>)).InSingletonScope()
                .WithConstructorArgument(typeof(string), "http://localhost:8989/solr/test_core");
        }
    }
}