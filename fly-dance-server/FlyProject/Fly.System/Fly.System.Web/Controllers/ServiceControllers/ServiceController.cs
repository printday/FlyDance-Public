using Common.Base.Model;
using Common.IOC;
using Fly.System.Application.IServices.Services;
using Fly.System.Domain.Models.Responses.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fly.System.Web.Controllers.ServiceControllers
{
    /// <summary>
    /// 服务控制器
    /// </summary>
    public class ServiceController : FlyControllerBase
    {
        [Autowired]
        public IServiceService ServiceService {  get; set; }

        /// <summary>
        /// 获取首页服务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BaseResponse<List<ServiceResp>> GetHomeService()
        {
            return ServiceService.GetHomeService();
        }
    }
}
