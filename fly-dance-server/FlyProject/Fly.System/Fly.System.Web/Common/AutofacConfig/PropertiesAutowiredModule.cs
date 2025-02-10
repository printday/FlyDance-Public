using Autofac;
using Common.IOC;
using Fly.System.Domain.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fly.System.Web.Common.AutofacConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class PropertiesAutowiredModule : Autofac.Module
    {
        /// <summary>
        /// 始化容器的时候，自动注入属性
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired(new AutowiredPropertySelector());

            // 自动注册所有实现了特定接口的服务，并启用属性注入
            builder.RegisterAssemblyTypes(typeof(ISystemService).Assembly)
                   .Where(t => t.Name.EndsWith("Service") && !t.IsAbstract)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope()
                   .PropertiesAutowired(new AutowiredPropertySelector());

            // 自动注册所有实现了特定接口的服务，并启用属性注入
            builder.RegisterAssemblyTypes(typeof(ISystemReport).Assembly)
                   .Where(t => t.Name.EndsWith("Report") && !t.IsAbstract)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope()
                   .PropertiesAutowired(new AutowiredPropertySelector());
        }
    }
}
