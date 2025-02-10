using Common.Models.Configs;

namespace Common.Const
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceConfig
    {
        /// <summary>
        /// 网关地址
        /// </summary>
        public static string GatewayUrl = "";

        /// <summary>
        /// IP白名单
        /// </summary>
        public static List<string> AllowClientIP = new();

        /// <summary>
        /// Redis配置类
        /// </summary>
        public static RedisConfigModel Redis = new();
    }
}
