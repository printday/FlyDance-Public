using Common.Base.Model;
using Common.IOC;
using Fly.System.Application.IServices.Systems;
using Fly.System.Domain.Models.Requests.Permsissions;
using Fly.System.Domain.Models.Responses.Systems;
using Microsoft.AspNetCore.Mvc;

namespace Fly.System.Web.Controllers.PermControllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenuController : FlyControllerBase
    {
        [Autowired]
        public IPermService PermService { get; set; }

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseResponse<List<ContentMenuResp>> GetContentMenu([FromBody] ContentMenuReq req)
        {
            return PermService.GetContentMenu(req.UserId);
        }
    }
}
