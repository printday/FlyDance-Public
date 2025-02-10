using Autofac;
using System.Reflection;

namespace Common.IOC
{
    public static class AutofacExtensions
    {
        // 自动化注册带有RegisterAttribute标记的类型，并启用属性注入
        public static void RegisterMarkedTypesWithPropertyInjection(this ContainerBuilder builder, Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<RegisterAttribute>() != null &&
                            t.IsClass && !t.IsAbstract && !t.IsInterface && !t.IsEnum &&
                            !t.IsGenericTypeDefinition && !t.IsSealed);

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces();
                if (interfaces.Any())
                {
                    builder.RegisterType(type).AsImplementedInterfaces().PropertiesAutowired();
                }
            }
        }
    }
}
