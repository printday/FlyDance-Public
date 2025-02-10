using Common.Base.Model;
using Common.IOC;
using Fly.System.Application.IServices.Systems;
using Fly.System.Domain.Models.Entities.Systems;
using Fly.System.Domain.Models.Requests.Users;
using Microsoft.AspNetCore.Mvc;

namespace Fly.System.Web.Controllers.UserControllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : FlyControllerBase
    {
        [Autowired]
        public IUserService UserService { get; set; }

        /// <summary>
        /// 根据id获取用户
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost]
        public User GetUserById([FromBody] UserReq req)
        {
            return UserService.GetUserById(req.Id);
        }
    }
}
