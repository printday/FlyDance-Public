using StackExchange.Redis;

namespace Common.Models.Configs
{
    /// <summary>
    /// redis配置
    /// </summary>
    public class RedisConfigModel
    {
        /// <summary>
		/// 主机
		/// </summary>
		public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
