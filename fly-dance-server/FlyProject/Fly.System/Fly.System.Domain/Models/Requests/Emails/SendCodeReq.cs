namespace Fly.System.Domain.Models.Requests.Emails
{
    /// <summary>
    /// 发送验证码请求
    /// </summary>
    public class SendCodeReq
    {
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }
}
