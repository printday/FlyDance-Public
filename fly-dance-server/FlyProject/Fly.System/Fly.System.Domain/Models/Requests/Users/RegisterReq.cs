namespace Fly.System.Domain.Models.Requests.Users
{
    /// <summary>
    /// 注册请求类
    /// </summary>
    public class RegisterReq
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 注册邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string PasswordConfirm { get; set; }

        /// <summary>
        /// 验证码键
        /// </summary>
        public string Key { get; set;  }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }
}
