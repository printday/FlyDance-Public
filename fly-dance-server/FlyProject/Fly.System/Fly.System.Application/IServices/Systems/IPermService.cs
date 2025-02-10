using Common.Base.Model;
using Fly.System.Domain.Models.Interfaces;
using Fly.System.Domain.Models.Responses.Systems;

namespace Fly.System.Application.IServices.Systems
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public interface IPermService: ISystemService
    {
        /// <summary>
        /// 根据用户Id获取权限菜单(默认只拿启用权限)
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        BaseResponse<List<ContentMenuResp>> GetContentMenu(string userId, bool isEnabled = true);
    }
}
