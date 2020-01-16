using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace CfoMiddleware.Extension
{
    public static class AutofacHostBuilderExtensions
    {
        public static IHostBuilder UseAutofac(this IHostBuilder builder, Action<ContainerBuilder> configure = null)
        {
            builder.UseServiceProviderFactory(new GenericHostAutofacServiceProviderFactory(configure));

            builder.ConfigureContainer<ContainerBuilder>((_, cb) => cb.RegisterBuildCallback(AutofacContainer.Set));
            return builder;
        }

        public static IHostBuilder ConfigureAutofac(this IHostBuilder builder, Action<HostBuilderContext, ContainerBuilder> configure) => builder.ConfigureContainer(configure);

        public static IHostBuilder ConfigureAutofac(this IHostBuilder builder, Action<ContainerBuilder> configure) => builder.ConfigureAutofac((_, cb) => configure(cb));

        public static IHostBuilder AddAutofacModule<T>(this IHostBuilder builder) where T : Module => builder.ConfigureAutofac((ctx, cb) =>
        {
            var constructors = typeof(T).GetConstructors();
            var knownTypes = new Dictionary<Type, object>
            {
                [typeof(IConfiguration)] = ctx.Configuration,
                [typeof(IHostingEnvironment)] = ctx.HostingEnvironment,
                [typeof(HostBuilderContext)] = ctx
            };
            var cnt = -1;
            ConstructorInfo constructor = null;
            ParameterInfo[] constrParams = null;
            foreach (var item in constructors)
            {
                var parameters = item.GetParameters();
                if (parameters.Length <= cnt) continue;
                if (!parameters.All(info => knownTypes.ContainsKey(info.ParameterType))) continue;

                cnt = parameters.Length;
                constructor = item;
                constrParams = parameters;
            }

            if (constructor == null)
                throw new DependencyResolutionException(
                    $"Cannot find compatible constructor for module {typeof(T)}, can have parametrized constructor with {nameof(IConfiguration)}, {nameof(IHostingEnvironment)} or/and {nameof(HostBuilderContext)} parameters");

            var args = constrParams.Select(p => knownTypes[p.ParameterType]).ToArray();

            var module = (IModel)constructor.Invoke(args);
            cb.RegisterModule(module);
        });

        public static IHostBuilder AddAutofacModule(this IHostBuilder builder, IModule module) => builder.ConfigureAutofac(cb => cb.RegisterModule(module));

        public static IHostBuilder AddAutofacModule(this IHostBuilder builder, Func<HostBuilderContext, IModule> factory) => builder.ConfigureAutofac((ctx, cb) => cb.RegisterModule(factory(ctx)));

    }
}
