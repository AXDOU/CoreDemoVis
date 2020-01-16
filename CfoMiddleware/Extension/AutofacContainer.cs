using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CfoMiddleware.Extension
{
    /// <summary>
    /// Autofac容器
    /// </summary>
    public class AutofacContainer
    {
        public static IContainer Container { get; private set; }

        public static void Set(IContainer c)
        {
            Container = c;
        }

        public static T Resolve<T>()
        {
            if (Container == null) throw new ArgumentNullException(nameof(Container));
            return Container.Resolve<T>();
        }

        public static object Resolve(Type serviceType)
        {
            if (Container == null) throw new ArgumentNullException(nameof(Container));
            return Container.Resolve(serviceType);
        }
    }
}
