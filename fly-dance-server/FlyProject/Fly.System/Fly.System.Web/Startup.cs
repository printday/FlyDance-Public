using Autofac;
using Common.Factorys;
using Common.Helpers;
using Common.IOC;
using Fly.System.Domain.Models;
using Fly.System.Web.Common.AutofacConfig;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fly.System.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置帮助类
        /// </summary>
        private StartupHelper StartupHelper;

        /// <summary>
        /// 
        /// </summary>
        private ServiceSettings ServiceSettings;

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this.StartupHelper = new StartupHelper(Configuration);
            this.ServiceSettings = configuration.GetSection("ServiceInfo").Get<ServiceSettings>() ?? throw new Exception("未能读取到服务的配置信息");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            services.AddMemoryCache();
            StartupHelper.UseRateLimit(services, Configuration, ServiceSettings);
            StartupHelper.RegBeforeCap(services);
            StartupHelper.AddSwagger(services, ServiceSettings);
            StartupHelper.AddSkywalking(services, ServiceSettings);
            StartupHelper.AddController(services, ServiceSettings);
            StartupHelper.RegisterComController(services);
            StartupHelper.AddSqlServerConfig(services, Configuration);
            StartupHelper.AddMySqlConfig(services, Configuration);
            StartupHelper.AddEmail(services);
            StartupHelper.AddRedis();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            StartupHelper.UseIpLimiting(app, ServiceSettings);
            // StartupHelper.UseConsul(Configuration, ServiceSettings);
            StartupHelper.UseSwagger(app, ServiceSettings);
            StartupHelper.UseStaticHttpContext(app);

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(end =>
            {
                end.MapControllers();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<PropertiesAutowiredModule>();
            builder.RegisterType<SqlSugarFactory>();
            builder.RegisterBuildCallback(container =>
            {
                AutofacContainer.Container = (IContainer)container;
            });
        }
    }
}
