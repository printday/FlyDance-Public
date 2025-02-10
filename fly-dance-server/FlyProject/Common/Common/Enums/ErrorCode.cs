using System.ComponentModel;

namespace Common.Enums
{
    /// <summary>
    /// 错误类型枚举
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        Success = 200,
        /// <summary>
        /// 资源被创建
        /// </summary>
        [Description("资源被创建")]
        Created = 201,
        /// <summary>
        /// 服务器端通用错误响应
        /// </summary>
        [Description("服务器响应错误")]
        Error = 500,

        /// <summary>
        /// 认证失败
        /// </summary>
        [Description("认证失败")]
        AuthenticationFailed = 401,
        /// <summary>
        /// 无权限
        /// </summary>
        [Description("无权限")]
        NoPermission = 403,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail = 601,
        /// <summary>
        /// 资源不存在
        /// </summary>
        [Description("资源不存在")]
        NotFound = 404,
        /// <summary>
        /// 客户端一般性错误
        /// </summary>
        [Description("客户端一般性错误")]
        BadRequest = 400,

        /// <summary>
        /// 必须登录
        /// </summary>
        [Description("必须登录")]
        MustLogin = 405,
    }
}
