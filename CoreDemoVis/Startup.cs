using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.DependencyModel;
using Microsoft.AspNetCore.Builder.Internal;
using Autofac.Extensions.DependencyInjection;

namespace CoreDemoVis
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return AutofacConfigure.Register(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class AutofacConfigure
    {
        public static AutofacServiceProvider Register(IServiceCollection services)
        {
            ContainerBuilder builder = new Autofac.ContainerBuilder();

            //builder.RegisterType<User>().As<IUser>().PropertiesAutowired();直接对类进行注册

            //Assembly entityAss = Assembly.Load("CfoBusiness"); //对Entity这个类库进行里的类进行集体注册
            //Type[] etypes = entityAss.GetTypes();
            //builder.RegisterTypes(etypes).AsImplementedInterfaces().PropertiesAutowired();
          
            //.Where(x=>x.Name.Contains("UserManage")限制仅包含UserManage)
            builder.RegisterAssemblyTypes(Assembly.Load("CfoBusiness")).AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Populate(services);

            //builder.RegisterType<CfoBusiness.UserManage>().As<CfoBusiness.IUserService>();

            //builder.RegisterAssemblyTypes(typeof(Program).Assembly).AsImplementedInterfaces();
            var container = builder.Build();
            //container.Resolve<CfoBusiness.IUserService>();
            return new AutofacServiceProvider(container);
        }
    }
}
