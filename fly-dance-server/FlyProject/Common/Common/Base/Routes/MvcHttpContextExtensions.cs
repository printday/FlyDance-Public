using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Base.Routes
{
    /// <summary>
    /// 
    /// </summary>
    public static class MvcHttpContextExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpContextInfo.Configure(httpContextAccessor);
            return app;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class HttpContextInfo
    {
        private static IHttpContextAccessor _accessor;

        /// <summary>
        /// 
        /// </summary>
        public static Microsoft.AspNetCore.Http.HttpContext Current => _accessor == null ? null : _accessor.HttpContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessor"></param>
        internal static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
    }
}
