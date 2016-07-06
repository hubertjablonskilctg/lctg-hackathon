using Common.Mongo.Repositories;
using Common.Solr;
using hackaton_engine.Models;
using Ninject;
using NUnit.Framework;
using SolrNet;

namespace Common.UT
{
    [TestFixture]
    public class SolrConnectorTests
    {
        [Test]
        public void Adds()
        {
            var solrConnector = IocConfig.GetKernel().Get<ISolrOperations<SolrBaseItem>>();
            solrConnector.Delete(new[] {"0"});
            solrConnector.Commit();
            solrConnector.Add(new SolrBaseItem() {Id="0", Title = "title1", Body = "body1"});
            solrConnector.Commit();
        }
    }
}