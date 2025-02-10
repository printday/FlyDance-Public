using Common.Const;
using Common.Helpers;
using Huaju.Ai.Common;

namespace Huaju.Ai
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private AiConst _aiConst { get; }

        private StartupHelper _startupHelper { get; }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration _Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {       
            this._Configuration = configuration;
            this._startupHelper = new StartupHelper(_Configuration);
            _aiConst = configuration.GetSection("AiConst").Get<AiConst>() ?? throw new Exception("未读取到Ai的基础配置");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSingleton(_aiConst);
            services.AddScoped<AiHelper>();
            this._startupHelper.UseRateLimit(services);
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
            this._startupHelper.UseIpLimiting(app);
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(e => e.MapControllers());
        }
    }
}