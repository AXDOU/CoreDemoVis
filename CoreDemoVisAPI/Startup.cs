using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cfo.DTO.Base;
using CfoBusiness;
using CfoMiddleware;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreDemoVisAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            }); ;
            return AutofacConfigure.Register(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.ApplicationServices.GetService(typeof(ILoggerFactory));
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseHsts();
            }
            //更新响应头
            app.Use(async (context, next) =>
            {
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers["server"] = "Kestrel Server 2098";
                    context.Response.Headers.Add("x-powered-by", "Asp.NET Core 33");
                    return Task.CompletedTask;
                });
                await next();
            });


            app.UseStaticFiles();//启用静态文件
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    /// <summary>
    /// 注入
    /// </summary>
    public class AutofacConfigure
    {
        public static ILoggerRepository log4Repository { get; set; }

        public static AutofacServiceProvider Register(IServiceCollection services)
        {
            ContainerBuilder builder = new ContainerBuilder();

            //注入log4
            Log4Register();

            //services.addscoped<iuserservice, userservice>();
            builder.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces();
            var assembly = new Assembly[]
            {
                Assembly.Load("CfoBusiness")
                //Assembly.Load("CfoMiddleware")
            };

            //直接注册
            //builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope().PropertiesAutowired();
            var types = Assembly.Load("CfoBusiness").GetTypes().Where(x => x.GetCustomAttribute<AutofacAutoRegisterAttribute>() != null).ToArray();
            //.Where(x => x.Name.Contains("Service")).ToArray();
            builder.RegisterTypes(types).AsImplementedInterfaces().InstancePerLifetimeScope().PropertiesAutowired();
            builder.Populate(services);

            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        /// <summary>
        /// log4日志配置项
        /// </summary>
        private static void Log4Register()
        {
            //log4net
            log4Repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(log4Repository, new FileInfo("log4net.config"));
        }
    }

}
