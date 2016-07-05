using System.Linq;
using Common.Models;
using Common.Mongo.Repositories;
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

            repo.Add(new TestModel() {Id = 0, Name = "test"});

            var getAll = repo.GetAll();

            Assert.GreaterOrEqual(1,getAll.Count());
        }
    }
}
