using System;

namespace Ilkyar.WebAPI.Helpers
{
    public static class IocHelper
    {
        public static TResult ExecuteCall<T, TResult>(Func<T, TResult> function)
        {
            var proxy = WindsorHelper.Container.Resolve<T>();
            return function.Invoke(proxy);
        }
    }
}