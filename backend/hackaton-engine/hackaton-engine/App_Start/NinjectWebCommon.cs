using System.Text.RegularExpressions;
using Common.Mongo.Repositories;
using MongoDB.Driver;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(hackaton_engine.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(hackaton_engine.App_Start.NinjectWebCommon), "Stop")]

namespace hackaton_engine.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMongoClient>()
                .To<MongoClient>().InSingletonScope()
                //.WithConstructorArgument(typeof(string), "mongodb://localhost:27017");
                .WithConstructorArgument(typeof(string), "mongodb://root:bitnami@13.79.175.46/admin");
            //.WithConstructorArgument(typeof(MongoClientSettings), mongoSettings);
            kernel.Bind<IMongoDatabase>().ToMethod((context) => context.Kernel.Get<IMongoClient>().GetDatabase("core", null));

            kernel.Bind(typeof(IMongoRepository<>)).To(typeof(MongoRepository<>));
        }        
    }
}
