using Common.Base.Model;
using Common.Models.Logins;
using Fly.System.Domain.Models.Entities.Systems;
using Fly.System.Domain.Models.Requests.Users;
using Fly.System.Domain.Models.Responses.Systems;

namespace Fly.System.Application.IServices.Systems
{
    /// <summary>
    ///  用户服务
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 根据id获取用户信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        User GetUserById(int id);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        BaseResponse<LoginUserResp> Login(LoginInfoReq req);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        BaseResponse<bool> Register(RegisterReq req);
    }
}
