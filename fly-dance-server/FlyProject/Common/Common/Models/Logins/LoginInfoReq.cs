namespace Common.Models.Logins
{
    /// <summary>
    /// 登录信息请求类
    /// </summary>
    public class LoginInfoReq
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// 登录类型（1：账号密码登录， 2：邮箱登录）
        /// </summary>
        public int LoginType { get; set; }
    }
}
