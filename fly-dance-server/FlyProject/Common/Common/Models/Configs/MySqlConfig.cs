namespace Common.Models.Configs
{
    public class MySqlConfig : IDisposable
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
