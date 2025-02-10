using Common.Base.Model;
using Fly.System.Domain.Models.Interfaces;
using Fly.System.Domain.Models.Responses.Services;

namespace Fly.System.Application.IServices.Services
{
    /// <summary>
    /// 应用服务
    /// </summary>
    public interface IServiceService : ISystemService
    {
        /// <summary>
        /// 获取首页服务
        /// </summary>
        /// <returns></returns>
        BaseResponse<List<ServiceResp>> GetHomeService();
    }
}
