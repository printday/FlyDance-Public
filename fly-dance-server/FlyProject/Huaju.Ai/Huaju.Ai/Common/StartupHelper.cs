using Common.Base.Routes;
using Common.Controllers;
using Common.Filters;
using Microsoft.OpenApi.Models;

namespace Huaju.Ai.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class StartupHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IConfiguration Configuration;

        /// <summary>
        /// 
        /// </summary>
        public StartupHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #region swagger
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetApiVersions()
        {
            return new[] { "v1", "v2" };
        }

        /// <summary>
        /// 初始化Swagger
        /// </summary>
        public void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HUAJU-AI", Version = "v1" });
            });
        }

        /// <summary>
        /// Use Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceSettings"></param>
        public void UseSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var apiVersions = GetApiVersions();
                foreach (var version in apiVersions)
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"HUAJU-AI  {version}");
                }
                c.DefaultModelsExpandDepth(0);
            });
        }
        #endregion

        #region IP限制
        /// <summary>
        /// 配置IP限制（传统）
        /// </summary>
        /// <param name="services"></param>
        public void UseRateLimit(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllOrgin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                    });
            });
        }


        /// <summary>
        /// 根据配置启用IP限制
        /// </summary>
        public void UseIpLimiting(IApplicationBuilder app)
        {
            app.UseCors("AllOrgin");
        }
        #endregion

        #region Controller
        /// <summary>
        /// 
        /// </summary>
        public void AddController(IServiceCollection services)
        {
            services.AddControllers(opt =>
            {
                opt.SuppressAsyncSuffixInActionNames = false;
                opt.Filters.Add<FlyApiExceptionFilter>();
            });
        }
        #endregion

        /// <summary>
        /// register controller
        /// </summary>
        /// <param name="services"></param>
        public void RegisterComController(IServiceCollection services)
        {
            services.AddTransient<HealthController>();
        }

        #region StaticHttpContext
        /// <summary>
        /// UseStaticHttpContext
        /// </summary>
        /// <param name="app"></param>
        public void UseStaticHttpContext(IApplicationBuilder app)
        {
            app.UseStaticHttpContext();
        }
        #endregion
    }
}
