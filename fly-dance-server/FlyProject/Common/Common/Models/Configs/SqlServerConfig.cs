namespace Common.Models.Configs
{
    /// <summary>
    /// SqlServer配置
    /// </summary>
    public class SqlServerConfig : IDisposable
    {
        /// <summary>
        /// 系统数据库连接字符串
        /// </summary>
        public string SystemConnectionString { get; set; }

        public void Dispose()
        {

        }
    }
}
