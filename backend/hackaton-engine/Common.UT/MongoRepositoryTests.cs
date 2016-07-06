using System.Linq;
using Common.Mongo.Repositories;
using hackaton_engine.Models;
using Ninject;
using NUnit.Framework;

namespace Common.UT
{
    [TestFixture]
    public class MongoRepositoryTests
    {
        [Test]
        public void WritesAndReads()
        {
            var repo = IocConfig.GetKernel().Get<MongoRepository<TestModel>>();
            repo.Remove((x) => true);

            repo.Add(new TestModel() {Id = 0, Name = "test"});
            var getAll = repo.GetAll();
            Assert.AreEqual(1,getAll.Count());
        }
    }
}
