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
            var credential = MongoCredential.CreateMongoCRCredential("admin", "root", "bitnami");
            var mongoSettings = new MongoClientSettings
            {
                Credentials = new[] { credential },
                
            };


            Bind<IMongoClient>()
                .To<MongoClient>().InSingletonScope()
                //.WithConstructorArgument(typeof(string), "mongodb://localhost:27017");
                .WithConstructorArgument(typeof(string), "mongodb://root:bitnami@13.79.175.46/admin");
                //.WithConstructorArgument(typeof(MongoClientSettings), mongoSettings);
            Bind<IMongoDatabase>().ToMethod((context) => context.Kernel.Get<IMongoClient>().GetDatabase("core", null));

            Bind(typeof(IMongoRepository<>)).To(typeof(MongoRepository<>));
        }
    }
}