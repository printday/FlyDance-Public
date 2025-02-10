namespace Common.Models.Logins
{
    /// <summary>
    /// 登录信息返回类
    /// </summary>
    public class LoginInfoRes
    {
        /// <summary>
        /// token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// 修改密码时间
        /// </summary>
        public DateTime? ChangePwdTime { get; set; }
    }
}
