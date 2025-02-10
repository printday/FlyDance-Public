namespace Common.Models.Responses
{
    /// <summary>
    /// 验证Token 返回类
    /// </summary>
    public class CheckTokenRes : Res
    {
        /// <summary>
        /// token 数据
        /// </summary>
        public SSOTokenData Data { get; set; }
    }
}
