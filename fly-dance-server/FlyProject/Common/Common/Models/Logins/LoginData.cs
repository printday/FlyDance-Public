namespace Common.Models.Logins
{
    /// <summary>
    /// 登录人信息
    /// </summary>
    public class LoginData
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// token   有效时间
        /// </summary>
        public string strExpireTime { get; set; }

        /// <summary>
        /// 修改密码时间
        /// </summary>
        public DateTime? ChangePwdTime { get; set; }
    }
}
