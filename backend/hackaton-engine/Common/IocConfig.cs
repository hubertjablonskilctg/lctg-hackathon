using System.Reflection;
using Common.Mongo;
using Ninject;

namespace Common
{
    public static class IocConfig
    {
        private static IKernel _kernel;

        static IocConfig()
        {
            _kernel = new StandardKernel();
            _kernel.Load(new MongoNinjectModule());
        }

        public static IKernel GetKernel()
        {
            return _kernel;
        }
    }
}