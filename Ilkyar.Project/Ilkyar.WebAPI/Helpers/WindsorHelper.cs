using Castle.Windsor;
using Castle.Windsor.Installer;
using Ilkyar.WebAPI.Windsor;

namespace Ilkyar.WebAPI.Helpers
{
    public static class WindsorHelper
    {
        public static WindsorContainer Container { get; private set; }
        private static WindsorHttpDependencyResolver _resolver;
        private static bool _initialized;

        static WindsorHelper()
        {
            Container = new WindsorContainer();
            _initialized = false;
        }

        public static WindsorHttpDependencyResolver GetDependencyResolver()
        {
            if (_initialized)
                return _resolver;

            _initialized = true;
            Container.Install(FromAssembly.This());
            _resolver = new WindsorHttpDependencyResolver(Container);

            return _resolver;
        }
    }
}