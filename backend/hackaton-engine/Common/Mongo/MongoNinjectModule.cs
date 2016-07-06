using Common.Mongo.Repositories;
using MongoDB.Driver;
using Ninject;
using Ninject.Modules;

namespace Common.Mongo
{
    public class MongoNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IMongoClient>()
                .To<MongoClient>().InSingletonScope()
                .WithConstructorArgument(typeof(string), "mongodb://localhost:27017");
            Bind<IMongoDatabase>().ToMethod((context) => context.Kernel.Get<IMongoClient>().GetDatabase("core", null));

            Bind(typeof(IMongoRepository<>)).To(typeof(MongoRepository<>));
        }
    }
}