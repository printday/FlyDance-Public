using Common.Models.Logins;
using Fly.System.Domain.Models.Entities.Systems;
using Fly.System.Domain.Models.Requests.Users;

namespace Fly.System.Domain.IReportsitorys.Systems
{
    public interface IUserReport
    {
        /// <summary>
        /// 根据Id获取用户信息
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        User GetUserById(int id);

        /// <summary>
        /// 根据账号密码获取用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        User GetByUPwd(LoginInfoReq req);

        /// <summary>
        /// 根据邮箱密码获取用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        User GetByEmailPwd(LoginInfoReq req);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        bool Register(RegisterReq req);

        /// <summary>
        /// 根据用户名取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        User GetByUName(string userName);

        /// <summary>
        /// 根据邮箱获取用户信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User GetByEmail(string email);
    }
}
