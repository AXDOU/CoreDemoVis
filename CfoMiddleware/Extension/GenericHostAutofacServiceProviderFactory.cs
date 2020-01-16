using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CfoMiddleware.Extension
{
    internal class GenericHostAutofacServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        private readonly AutofacServiceProviderFactory _default;

        public GenericHostAutofacServiceProviderFactory(Action<ContainerBuilder> configure = null)
        {
            _default = new AutofacServiceProviderFactory(configure);
        }

        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            var host = services.Last(sd => sd.ServiceType == typeof(IHost) && sd.ImplementationType != null);
            services.Remove(host);
            var newSd = new ServiceDescriptor(host.ImplementationType, host.ImplementationType, host.Lifetime);
            services.Add(newSd);

            var builder = _default.CreateBuilder(services);
            builder.Register(ctx => new HostDecorator((IHost)ctx.Resolve(host.ImplementationType),
                    ctx.Resolve<ILifetimeScope>(), ctx.Resolve<ILogger<Container>>()))
                .As<IHost>().ExternallyOwned();
            return builder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            return _default.CreateServiceProvider(containerBuilder);
        }

        private class HostDecorator : IHost
        {
            private readonly IHost _host;
            private ILifetimeScope _scope;
            private readonly ILogger<Container> _logger;

            public HostDecorator(IHost host, ILifetimeScope scope, ILogger<Container> logger)
            {
                _host = host;
                _scope = scope;
                _logger = logger;
            }

            public void Dispose()
            {
                var scope = Interlocked.CompareExchange(ref _scope, null, null);
                if (scope != null)
                {
                    scope = Interlocked.CompareExchange(ref _scope, null, scope);
                    if (scope != null)
                    {
                        scope.Dispose();
                        _logger.LogInformation("Autofac container disposed");
                    }
                }

            }

            public Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
                => _host.StartAsync(cancellationToken);

            public Task StopAsync(CancellationToken cancellationToken = new CancellationToken())
                => _host.StopAsync(cancellationToken);

            public IServiceProvider Services => _host.Services;
        }
    }
}
