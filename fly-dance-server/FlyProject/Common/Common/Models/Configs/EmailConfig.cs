namespace Common.Models.Configs
{
    /// <summary>
    /// 邮箱配置
    /// </summary>
    public class EmailConfig
    {
        /// <summary>
        /// 发送邮件的系统邮箱
        /// </summary>
        public string SystemEmail {  get; set; }

        /// <summary>
        /// 发送邮件的系统邮箱密码
        /// </summary>
        public string SystemEmailPassword {  get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        public string SystemEmailTokenPassword { get; set; }
    }
}
