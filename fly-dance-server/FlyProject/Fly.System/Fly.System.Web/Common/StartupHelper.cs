using AspNetCoreRateLimit;
using Common.Base.Routes;
using Common.Const;
using Common.Controllers;
using Common.Filters;
using Common.Models.Configs;
using Common.Models.Configs.ConsulConfig;
using Fly.System.Domain.Models;
using Fly.System.Web.Common.Cap;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using SkyApm.Utilities.DependencyInjection;

namespace Common.Helpers
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
            this.Configuration = configuration;
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
        public void AddSwagger(IServiceCollection services, ServiceSettings serviceSettings)
        {
            if (serviceSettings.IsUseSwagger)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fly-System", Version = "v1" });
                });
            }
        }

        /// <summary>
        /// Use Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceSettings"></param>
        public void UseSwagger(IApplicationBuilder app, ServiceSettings serviceSettings)
        {
            if (serviceSettings.IsUseSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    var apiVersions = GetApiVersions();
                    foreach (var version in apiVersions)
                    {
                        c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Fly-System  {version}");
                    }
                    c.RoutePrefix = serviceSettings.RoutePrefixName;
                    c.DefaultModelsExpandDepth(0);
                });
            }
        }
        #endregion

        #region IP限制

        /// <summary>
        /// 配置IP限制
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <param name="serviceSettings"></param>
        public void AddRateLimit(IServiceCollection services, IConfiguration Configuration, ServiceSettings serviceSettings)
        {
            if (serviceSettings.IsUseIpLimiting)
            {
                services.AddOptions();
                services.AddMemoryCache(); // 必须，用于缓存限流规则
                services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
                services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
                services.AddInMemoryRateLimiting();   // 使用内存存储限流规则
                services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            }
        }

        /// <summary>
        /// 配置IP限制（传统）
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <param name="serviceSettings"></param>
        public void UseRateLimit(IServiceCollection services, IConfiguration Configuration, ServiceSettings serviceSettings)
        {
            if (serviceSettings.IsUseIpLimiting)
            {
                // 待修改的配置
                services.AddCors(options =>
                {
                    options.AddPolicy("FlyOrgin",
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader();
                        });
                });
            }
            else
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
        }

        /// <summary>
        /// 根据配置启用IP限制
        /// </summary>
        public void AddIpLimiting(IApplicationBuilder app, ServiceSettings serviceSettings)
        {
            if (serviceSettings.IsUseIpLimiting) { app.UseIpRateLimiting(); }
        }

        /// <summary>
        /// 根据配置启用IP限制
        /// </summary>
        public void UseIpLimiting(IApplicationBuilder app, ServiceSettings serviceSettings)
        {
            if (serviceSettings.IsUseIpLimiting) { app.UseCors("FlyOrgin"); }
            else { app.UseCors("AllOrgin"); }
        }
        #endregion

        #region Controller
        /// <summary>
        /// 
        /// </summary>
        public void AddController(IServiceCollection services, ServiceSettings serviceSettings)
        {
            services.AddControllers(opt =>
            {
                opt.SuppressAsyncSuffixInActionNames = false;
                opt.UseCentralRoutePrefix(new RouteAttribute(serviceSettings.RoutePrefixName));
                // opt.Filters.Add<AppTokenAuthorization>();    // 暂时注释Token权限验证
                opt.Filters.Add<FlyApiExceptionFilter>();
            });
        }
        #endregion

        #region DotnetCap
        /// <summary>
        /// Register before NetCap
        /// </summary>
        /// <param name="services"></param>
        public void RegBeforeCap(IServiceCollection services)
        {
            services.AddTransient<ICapSubscriberService, CapSubscriberService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        #endregion

        #region Skywalking
        /// <summary>
        /// 
        /// </summary>
        public void AddSkywalking(IServiceCollection services, ServiceSettings serviceSettings)
        {
            if (serviceSettings.IsUseSkywalking)
            {
                services.AddSkyApmExtensions();
            }
        }
        #endregion

        #region Register Other controller/service
        /// <summary>
        /// register controller
        /// </summary>
        /// <param name="services"></param>
        public void RegisterComController(IServiceCollection services)
        {
            services.AddTransient<HealthController>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceSettings"></param>
        public void InjectComService(IServiceCollection services, ServiceSettings serviceSettings)
        {
            if (!string.IsNullOrWhiteSpace(serviceSettings.UploadPath) && Directory.Exists(serviceSettings.UploadPath))
            {
                services.AddSingleton<IFileProvider>(new PhysicalFileProvider(serviceSettings.UploadPath));
            }
        }

        /// <summary>
        /// 获取SqlServer的配置
        /// </summary>
        public void AddSqlServerConfig(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<SqlServerConfig>(sp =>
            {
                var configSection = Configuration.GetSection("SqlServerConfig");
                var sqlServerConfig = new SqlServerConfig();
                configSection.Bind(sqlServerConfig);
                return sqlServerConfig;
            });
        }

        /// <summary>
        /// 获取MySql的配置
        /// </summary>
        public void AddMySqlConfig(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<MySqlConfig>(sp =>
            {
                var configSection = Configuration.GetSection("MySqlConfig");
                var mysqlConfig = new MySqlConfig();
                configSection.Bind(mysqlConfig);
                return mysqlConfig;
            });
        }

        #endregion

        #region Consul
        /// <summary>
        /// Use consul
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="serviceSettings"></param>
        public void UseConsul(Microsoft.Extensions.Configuration.IConfiguration configuration, ServiceSettings serviceSettings)
        {
            if (!serviceSettings.IsUseConsul) return;

            var consulCfgInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<ConsulConfigInfo>(System.IO.File.ReadAllText("consul.json"));

            configuration.UseConsul(new ConsulConfigInfo()
            {
                Address = consulCfgInfo.Address,
                Datacenter = consulCfgInfo.Datacenter,
                RegIP = consulCfgInfo.RegIP,
                RegPort = consulCfgInfo.RegPort,
                Tags = consulCfgInfo.Tags,
                AgentServiceRegInfo = new ConsulCfgAgentSvcReg()
                {
                    CheckAddress = consulCfgInfo.AgentServiceRegInfo.CheckAddress,
                    CheckTimeout = consulCfgInfo.AgentServiceRegInfo.CheckTimeout,
                    DeregisterSecond = consulCfgInfo.AgentServiceRegInfo.DeregisterSecond,
                    IntervalSec = consulCfgInfo.AgentServiceRegInfo.IntervalSec,
                    Name = consulCfgInfo.AgentServiceRegInfo.Name
                }
            });
        }
        #endregion

        #region 邮箱配置
        /// <summary>
        /// 根据配置读邮箱
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="Exception"></exception>
        public void AddEmail(IServiceCollection services)
        {
            EmailConfig emailConfig = Configuration.GetSection("EmailConfig").Get<EmailConfig>() ?? throw new Exception("未能读取到邮箱的配置信息");
            EmailHelper.EmailConfig = emailConfig;
        }
        #endregion

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

        #region Redis
        /// <summary>
        /// 配置Redis
        /// </summary>
        public void AddRedis()
        {
            ServiceConfig.Redis = Configuration.GetSection("Redis").Get<RedisConfigModel>() ?? throw new Exception("未能读取到Redis的配置信息");
        }
        #endregion
    }
}
